using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adel.Areas.Adel.Data
{
    public class ResponceToTime:IResponceToTime
    {
        private string _responce;
        public string Responce { get { return _responce; } }
        public ResponceToTime()
        {
            DateTime DT = DateTime.Now;
            int NowHours = DT.Hour;
            if (NowHours >= 6 && NowHours < 11)
                _responce= "Доброе утро, Адель!";
            if (NowHours >= 11 && NowHours < 15)
                _responce = "Приятного обеда, Адель!";
            if (NowHours >= 15 && NowHours < 18)
                _responce = "Добрый день, Адель!";
            if (NowHours >= 18 && NowHours < 23)
                _responce = "Хорошего вечера, Адель!";
            if (NowHours >= 23)
                _responce = "Сладких снов, Адель!";
        }



    }
}