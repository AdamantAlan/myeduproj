using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdelMobileBackEnd.models
{
    public interface IBook
    {
        public string Title { get; set; }
        public int Comments { get; set; }
        public int Likes { get; set; }
    }
    public class Book :IBook
    {
        public string Title { get; set; }
        public int Comments { get; set; }
        public int Likes { get; set; }
    }
}
