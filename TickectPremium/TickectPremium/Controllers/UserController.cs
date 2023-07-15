using System;
using System.Collections.Generic;
using System.Text;
using TickectPremium.Models;
using TickectPremium.Services;

namespace TickectPremium.Controllers
{
    public class UserController
    {
        private UserService userService;

        public UserController()
        {
            this.userService = new UserService();
        }

        public bool Authenticate(LoginModel loginModel)
        {
            if (userService.Login(loginModel))
            {
                User loggedInUser = userService.GetUserByUsernameAndPassword(loginModel.Username, loginModel.Password);

                AppData.LoggedInUser = loggedInUser;
                return true;
            }
            return false;
        }

        public bool UserRegistrer(UserDTO user)
        {
            if (!user.IsNullOrEmpty())
            {
                return userService.SigninUser(user);
            }
            else
            { 
                return false;
            }
        }

    }
}
