using AdelMobileBackEnd.models;
using AdelMobileBackEnd.models.absFactoryOfBook.products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdelMobileBackEnd.Controllers
{
    [Route("api/v1/ficbook")]
    [ApiController]
    public class FicbookController : ControllerBase
    {
        private readonly IParser _parser;
        public FicbookController(IParser parser)
        {
            _parser = parser;
        }
        // GET:api/v1/ficbook/rubin
        [HttpGet("rubin")]
        public async Task<ActionResult> GetRubinFromFicbook()
        {

            _ = await JsonAsync.SerializeForFileAsync<Rubin>(await _parser.GetBookAsync<Rubin>());
            return Ok();
        }
        // GET:api/v1/ficbook/rubin/get
        [HttpGet("rubin/get")]
        public async Task<IBook> GetRubinForAdel()
        {
            await GetRubinFromFicbook();
            return await JsonAsync.DeserializeOfFileAsync<Rubin>(); 
        }

        // GET:api/v1/ficbook/wool
        [HttpGet("wool")]
        public async Task<ActionResult> GetWoolFromFicbook()
        {
            _ = await JsonAsync.SerializeForFileAsync<Wool>(await _parser.GetBookAsync<Wool>());
            return Ok();
        }
        // GET:api/v1/ficbook/wool/get
        [HttpGet("wool/get")]
        public async Task<IBook> GetWoolForAdel()
        {
            await GetWoolFromFicbook();
            return await JsonAsync.DeserializeOfFileAsync<Wool>();
        }

        // GET:api/v1/ficbook/prayer
        [HttpGet("prayer")]
        public async Task<ActionResult> GetPrayerFromFicbook()
        {
            _ = await JsonAsync.SerializeForFileAsync<Prayer>(await _parser.GetBookAsync<Prayer>());
            return Ok();
        }
        // GET:api/v1/ficbook/prayer/get
        [HttpGet("prayer/get")]
        public async Task<IBook> GetPrayerForAdel()
        {
            await GetPrayerFromFicbook();
            return await JsonAsync.DeserializeOfFileAsync<Prayer>();
        }

        // GET:api/v1/ficbook/portrait
        [HttpGet("portrait")]
        public async Task<ActionResult> GetPortraitFromFicbook()
        {
            _ = await JsonAsync.SerializeForFileAsync<Portrait>(await _parser.GetBookAsync<Portrait>());
            return Ok();
        }
        // GET:api/v1/ficbook/portrait/get
        [HttpGet("portrait/get")]
        public async Task<IBook> GetPortraitForAdel()
        {
            await GetPortraitFromFicbook();
            return await JsonAsync.DeserializeOfFileAsync<Portrait>();
        }

        // GET:api/v1/ficbook/curls
        [HttpGet("curls")]
        public async Task<ActionResult> GetCurlsFromFicbook()
        {
            _ = await JsonAsync.SerializeForFileAsync<Curls>(await _parser.GetBookAsync<Curls>());
            return Ok();
        }

        // GET:api/v1/ficbook/curls/get
        [HttpGet("curls/get")]
        public async Task<IBook> GetCurlsForAdel()
        {
            await GetCurlsFromFicbook();
            return await JsonAsync.DeserializeOfFileAsync<Curls>();
        }

        // GET:api/v1/ficbook/glut
        [HttpGet("glut")]
        public async Task<ActionResult> GetGlutFromFicbook()
        {
            _ = await JsonAsync.SerializeForFileAsync<Glut>(await _parser.GetBookAsync<Glut>());
            return Ok();
        }

        // GET:api/v1/ficbook/glut/get
        [HttpGet("glut/get")]
        public async Task<IBook> GetGlutForAdel()
        {
            await GetCurlsFromFicbook();
            return await JsonAsync.DeserializeOfFileAsync<Glut>();
        }

        // GET:api/v1/ficbook/all
        [HttpGet("all")]
        public async Task<ActionResult> GetAllFicbook()
        {
            Dictionary<string, IBook> books = await _parser.GetAllBooksAsync();
            foreach (var book in books) {
                if(book.Key == "Rubin" )
                _ = await JsonAsync.SerializeForFileAsync<Rubin>(book.Value);
                    if (book.Key == "Wool")
                        _ = await JsonAsync.SerializeForFileAsync<Wool>(book.Value);
                    if (book.Key == "Prayer")
                        _ = await JsonAsync.SerializeForFileAsync<Prayer>(book.Value);
                    if (book.Key == "Portrait")
                        _ = await JsonAsync.SerializeForFileAsync<Portrait>(book.Value);
                    if (book.Key == "Curls")
                        _ = await JsonAsync.SerializeForFileAsync<Curls>(book.Value);
                    if (book.Key == "Glut")
                    _ = await JsonAsync.SerializeForFileAsync<Glut>(book.Value);
            }
         
            return Ok();
        }
        // GET:api/v1/ficbook/all/get
        [HttpGet("all/get")]
        public async Task<Dictionary<string, IBook>> GetAllForAdel()
        {
            Dictionary<string, IBook> books = await _parser.GetAllBooksAsync();
            foreach (var book in books)
            {
                //     if (book.Key == "Rubin")
                //         _ = await JsonAsync.SerializeForFileAsync<Rubin>(book.Value);
                if (book.Key == "Wool")
                    _ = await JsonAsync.SerializeForFileAsync<Wool>(book.Value);
                if (book.Key == "Prayer")
                    _ = await JsonAsync.SerializeForFileAsync<Prayer>(book.Value);
                if (book.Key == "Portrait")
                    _ = await JsonAsync.SerializeForFileAsync<Portrait>(book.Value);
                if (book.Key == "Curls")
                    _ = await JsonAsync.SerializeForFileAsync<Curls>(book.Value);
                if (book.Key == "Glut")
                    _ = await JsonAsync.SerializeForFileAsync<Glut>(book.Value);
            }
            return new Dictionary<string, IBook>
            {
            //    ["Rubin"] = await JsonAsync.DeserializeOfFileAsync<Rubin>(),
                ["Wool"] = await JsonAsync.DeserializeOfFileAsync<Wool>(),
                ["Prayer"] = await JsonAsync.DeserializeOfFileAsync<Prayer>(),
                ["Portrait"] = await JsonAsync.DeserializeOfFileAsync<Portrait>(),
                ["Curls"] = await JsonAsync.DeserializeOfFileAsync<Curls>(),
                ["Glut"] = await JsonAsync.DeserializeOfFileAsync<Glut>()
            };
        }

    }
}
