using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.IO;
using HtmlAgilityPack;
using AdelMobileBackEnd.models.absFactoryOfBook;
using AdelMobileBackEnd.models.absFactoryOfBook.factories;
using AdelMobileBackEnd.models.absFactoryOfBook.products;

namespace AdelMobileBackEnd.models
{
    public interface IParser
    {
        public Task<T> GetBookAsync<T>() where T : class;
        public Task<Dictionary<string, IBook>> GetAllBooksAsync();
    }

    public class Parser:IParser 
    {
        private FactoryOfBook _absFactory;
        public async Task<T> GetBookAsync<T>() where T:class
        {
            try
            {
                _absFactory = GetFactory<T>();
                return await _absFactory.GetBook() as T;
            }
            catch (AggregateException exs)
            {
                foreach (var e in exs.InnerExceptions)
                    await Log.LoggingAsync(e, "GetBookAsync");
                return null;
            }
        }
        public async  Task<Dictionary<string, IBook>> GetAllBooksAsync()
        {
            return new Dictionary<string, IBook>
            {
              //  [nameof(Rubin)] = await new Parser().GetBookAsync<Rubin>(),
                [nameof(Wool)] = await new Parser().GetBookAsync<Wool>(),
                [nameof(Prayer)] = await new Parser().GetBookAsync<Prayer>(),
                [nameof(Portrait)] = await new Parser().GetBookAsync<Portrait>(),
                [nameof(Curls)] = await new Parser().GetBookAsync<Curls>(),
                [nameof(Glut)] = await new Parser().GetBookAsync<Glut>()
            };
   
        }
        private FactoryOfBook GetFactory<T>()
        {
           // if (typeof(T).Name == "Rubin")
           //     return new RubinFactory();
            if (typeof(T).Name is "Portrait")
                return new PortraitFactory();
            if (typeof(T).Name is "Prayer")
                return new PrayerFactory();
            if (typeof(T).Name is "Wool")
                return new WoolFactory();
            if (typeof(T).Name is "Curls")
                return new CurlsFactory();
            if (typeof(T).Name is "Glut")
                return new GlutFactory();
            throw new Exception("factory not found!");
        }
    
    }
}
