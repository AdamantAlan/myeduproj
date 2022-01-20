using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adel.Areas.Adel.Data
{
    public class AuthentificationAdel:IAuthentificationAdel
    {
        public string Authentification(string password)
        {
            return (password == "123") ? "Access" : "404Error";
        }

    }
}