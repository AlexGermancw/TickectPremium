using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using TickectPremium.Models;
using TickectPremium.Resources;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;

namespace TickectPremium.Services
{
    public class MatchService
    {
        private string connectionString;

        public MatchService()
        {
            this.connectionString = Constant.stringDeConexion;
        }

        public ObservableCollection<Match> MatchList()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT P.CODIGO AS ID, P.EQUIPO_LOCAL AS LocalTeam, P.EQUIPO_VISITA AS VisitingTeam, " +
                           "CAST(P.FECHA AS DATE) AS DateMatch, CAST(P.FECHA AS TIME) AS Hour, LUGAR AS Place, " +
                           "(SELECT SUM(DISPONIBILIDAD) FROM localidad_partido WHERE P.CODIGO = PARTIDO_CODIGO) AS TotalTickets " +
                           "FROM PARTIDO_FUTBOL P " +
                           "WHERE CAST(P.FECHA AS DATE) >= CURDATE() " + 
                           "ORDER BY DateMatch, Hour;";

                MySqlCommand command = new MySqlCommand(query, connection);
                ObservableCollection<Match> matches = new ObservableCollection<Match>();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Match match = new Match
                        {
                            Id = reader.GetInt32("ID"),
                            LocalTeam = reader.GetString("LocalTeam"),
                            VisitingTeam = reader.GetString("VisitingTeam"),
                            DateMatch = reader.GetDateTime("DateMatch"),
                            Hour = reader.GetTimeSpan("Hour"),
                            Place = reader.GetString("Place"),
                            TotalTickets = reader.GetInt32("TotalTickets")
                        };

                        matches.Add(match);
                    }
                }
                return matches;
            }
        }

        public ObservableCollection<MatchSeatingArea> MatchSeatingAreaList(int _idMatch)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT LP.CODIGO_LOCALIDAD AS Id, " +
                               "LP.DISPONIBILIDAD AS TotalTickets, " +
                               "LP.PRECIO AS Price, " +
                               "LP.PARTIDO_CODIGO AS IdMatch, " +
                               "TL.CODIGO AS IdArea, " +
                               "TL.NOMBRE AS Name " +
                               "FROM localidad_partido LP " +
                               "LEFT JOIN tipo_localidad TL ON LP.CODIGO_TIPO_LOCALIDAD = TL.CODIGO " +
                               "WHERE LP.PARTIDO_CODIGO = @idMatch;";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@idMatch", _idMatch);

                ObservableCollection<MatchSeatingArea> matchSeatingAreas = new ObservableCollection<MatchSeatingArea>();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MatchSeatingArea matchSeatingArea = new MatchSeatingArea
                        {
                            Id = reader.GetInt32("Id"),
                            TotalTickets = reader.GetInt32("TotalTickets"),
                            Price = reader.GetDouble("Price"),
                            IdMatch = reader.GetInt32("IdMatch"),
                            SeatingArea = new SeatingArea { Id = reader.GetInt32("IdArea"), Name = reader.GetString("Name") }
                        };

                        matchSeatingAreas.Add(matchSeatingArea);
                    }
                }
                return matchSeatingAreas;
            }
        }

        public int InsertBill(Bill bill)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO factura (ID_USUARIO) VALUES (@idUsuario); SELECT LAST_INSERT_ID();";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@idUsuario", bill.IdUser);

                int codigoFactura = Convert.ToInt32(command.ExecuteScalar());
                return codigoFactura;
            }
        }

        public bool InsertBuyTicket(BuyTicket buyTicket)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO compra_boletos (PARTIDO_CODIGO, LOCALIDAD_CODIGO, CANTIDAD, TOTAL, ID_USUARIO, CODIGO_FACTURA) " +
                                   "VALUES (@partidoCodigo, @localidadCodigo, @cantidad, @total, @idUsuario, @codigoFactura); SELECT LAST_INSERT_ID();";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@partidoCodigo", buyTicket.IdMatch);
                    command.Parameters.AddWithValue("@localidadCodigo", buyTicket.seatingArea.Id);
                    command.Parameters.AddWithValue("@cantidad", buyTicket.Quantity);
                    command.Parameters.AddWithValue("@total", buyTicket.Total);
                    command.Parameters.AddWithValue("@idUsuario", buyTicket.IdUser);
                    command.Parameters.AddWithValue("@codigoFactura", buyTicket.IdBill);
                    command.ExecuteScalar();

                    // Actualizar la cantidad de disponibilidad en localidad_partido
                    string updateQuery = "UPDATE localidad_partido SET DISPONIBILIDAD = DISPONIBILIDAD - @cantidad " +
                                         "WHERE CODIGO_LOCALIDAD = @localidadCodigo AND PARTIDO_CODIGO = @partidoCodigo;";
                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@cantidad", buyTicket.Quantity);
                    updateCommand.Parameters.AddWithValue("@localidadCodigo", buyTicket.seatingArea.Id);
                    updateCommand.Parameters.AddWithValue("@partidoCodigo", buyTicket.IdMatch);
                    updateCommand.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<ShoppingSummaryDTO> GetDistinctBillCodes(int idUser)
        {
            List<ShoppingSummaryDTO> shoppingSummaries = new List<ShoppingSummaryDTO>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT DISTINCT CB.PARTIDO_CODIGO AS PARTIDO_CODIGO, CB.CODIGO_FACTURA AS CODIGO_FACTURA, PT.EQUIPO_LOCAL AS EQUIPO_LOCAL, PT.EQUIPO_VISITA AS EQUIPO_VISITA
                FROM compra_boletos CB
                INNER JOIN partido_futbol PT ON PT.CODIGO = CB.PARTIDO_CODIGO
                WHERE CB.ID_USUARIO = @idUsuario
                ORDER BY CB.CODIGO_FACTURA DESC";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@idUsuario", idUser);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ShoppingSummaryDTO summary = new ShoppingSummaryDTO
                        {
                            IdMatch = reader.IsDBNull(reader.GetOrdinal("PARTIDO_CODIGO")) ? 0 : reader.GetInt32("PARTIDO_CODIGO"),
                            IdBill = reader.IsDBNull(reader.GetOrdinal("CODIGO_FACTURA")) ? 0 : reader.GetInt32("CODIGO_FACTURA"),
                            LocalTeam = reader.IsDBNull(reader.GetOrdinal("EQUIPO_LOCAL")) ? string.Empty : reader.GetString("EQUIPO_LOCAL"),
                            VisitingTeam = reader.IsDBNull(reader.GetOrdinal("EQUIPO_VISITA")) ? string.Empty : reader.GetString("EQUIPO_VISITA")
                        };

                        shoppingSummaries.Add(summary);
                    }
                }
            }

            return shoppingSummaries;
        }

        public Match GetMatchByCode(int code)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT CODIGO AS ID, EQUIPO_LOCAL AS LocalTeam, EQUIPO_VISITA AS VisitingTeam, 
                                CAST(FECHA AS DATE) AS DateMatch, CAST(FECHA AS TIME) AS Hour, 
                                LUGAR AS Place, (SELECT SUM(DISPONIBILIDAD) FROM localidad_partido 
                                               WHERE PARTIDO_CODIGO = P.CODIGO) AS TotalTickets 
                        FROM PARTIDO_FUTBOL P 
                        WHERE CODIGO = @code";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@code", code);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Match match = new Match
                        {
                            Id = reader.GetInt32("ID"),
                            LocalTeam = reader.GetString("LocalTeam"),
                            VisitingTeam = reader.GetString("VisitingTeam"),
                            DateMatch = reader.GetDateTime("DateMatch"),
                            Hour = reader.GetTimeSpan("Hour"),
                            Place = reader.GetString("Place"),
                            TotalTickets = reader.GetInt32("TotalTickets")
                        };

                        return match;
                    }
                }
            }

            return null;
        }


        public List<BuyTicket> GetBuyTicketsByInvoiceCode(int invoiceCode)
        {
            List<BuyTicket> buyTickets = new List<BuyTicket>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT CB.*, TL.NOMBRE AS TIPO_LOCALIDAD_NOMBRE
                        FROM compra_boletos CB
                        LEFT JOIN TIPO_LOCALIDAD TL ON CB.LOCALIDAD_CODIGO = TL.CODIGO
                        WHERE CB.CODIGO_FACTURA = @invoiceCode";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@invoiceCode", invoiceCode);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SeatingArea seatingArea = new SeatingArea
                        {
                            Id = reader.IsDBNull(reader.GetOrdinal("LOCALIDAD_CODIGO")) ? 0 : reader.GetInt32("LOCALIDAD_CODIGO"),
                            Name = reader.IsDBNull(reader.GetOrdinal("TIPO_LOCALIDAD_NOMBRE")) ? string.Empty : reader.GetString("TIPO_LOCALIDAD_NOMBRE")
                        };

                        BuyTicket buyTicket = new BuyTicket
                        {
                            Id = reader.GetInt32("CODIGO_COMPRA"),
                            IdMatch = reader.GetInt32("PARTIDO_CODIGO"),
                            seatingArea = seatingArea,
                            Quantity = reader.GetInt32("CANTIDAD"),
                            Total = reader.GetDouble("TOTAL"),
                            IdUser = reader.GetInt32("ID_USUARIO"),
                            IdBill = reader.GetInt32("CODIGO_FACTURA")
                        };

                        buyTickets.Add(buyTicket);
                    }
                }
            }

            return buyTickets;
        }


    }
}
