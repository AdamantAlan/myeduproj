using IShop.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IShop.Domain.Models;
namespace WebnAPI.Controllers
{
    public class CategoryController : ApiController
    {
        ICategoryService _categoryServices = new CategoryService();

        public IHttpActionResult GetAll()
        {
            return Ok(_categoryServices.GetAll());
        }
        public IHttpActionResult Get(int id)
        {
            return Ok(_categoryServices.Get(id));
        }

        [HttpPost]
        public IHttpActionResult Add([FromBody] Category category)
        {
            _categoryServices.Add(category);
            return Ok();
        }
        [HttpPut]
        public IHttpActionResult Update([FromBody] Category category)
        {
            _categoryServices.Update(category);
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _categoryServices.Delete(id);
            return Ok();
        }
    }
}
