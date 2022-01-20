using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adel.Areas.Adel.Data
{
    public class StubAuthentificationAdel:IAuthentificationAdel
    {

        public string Authentification(string password)
        {
            password = "123";
            return (password == "123") ? "200OK" : "404Error";
        }
    }
}