using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TickectPremium.Resources
{
    public class CollectionHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Implementa la lógica para calcular la altura según el valor proporcionado
            // En este caso, puedes retornar el valor multiplicado por la altura estimada de cada elemento en la CollectionView
            // Por ejemplo:
            if (value is int itemCount)
            {
                const double estimatedItemHeight = 100; // Altura estimada de cada elemento en la CollectionView
                return itemCount * estimatedItemHeight;
            }

            return 0; // Valor predeterminado si no se cumple la condición anterior
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
