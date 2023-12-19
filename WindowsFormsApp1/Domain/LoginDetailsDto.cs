using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceInventory
{
    public class LoginDetailsDto
    {
        public string UserName { get;}
        public string Password { get;}
        public string ServerURL { get;}

        public LoginDetailsDto(string UserName, string Password, string ServerURL)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.ServerURL = ServerURL;
        }

    }
}
