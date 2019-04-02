using Business;
using Common;
using Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using HttpDeleteAttribute = System.Web.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using HttpPutAttribute = System.Web.Mvc.HttpPutAttribute;

namespace NetFramework.Controllers
{
    public class ProductController : ApiController
    {
        private static string cs = System.Configuration.ConfigurationManager.ConnectionStrings["TestGeorge"].ConnectionString;

        [HttpGet]
        public IHttpActionResult Get()
        {
            var itemResult = CategoryBl.GetAll(x => x.tblSubCategories);
            List<dynamic> dlist = new List<dynamic>();
            foreach (var item in itemResult)
            {
                dynamic d = new ExpandoObject();
                d.Id = item.Id;
                d.Name = item.Name;
                d.Description = item.Description;
                dlist.Add(d);
            }
            return Json(dlist);
        }

        [HttpGet]
        public IHttpActionResult GetSP()
        {
            string spname = "sp_Categories";
            var dt = SqlConnector.ReadFromSP(cs, spname);
            return Json(dt.Convert());
        }


        [HttpGet]
        public IHttpActionResult GetSP(int id)
        {
            string spname = "sp_Categories_Search";
            var p = new SqlParameter("@id", id);
            var dt = SqlConnector.ReadFromSP(cs, spname, p);
            return Json(dt.Convert());
        }

        // GET api/values/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var itemResult = CategoryBl.SingleOrDefault(x => x.Id == id, x => x.tblSubCategories);
            dynamic d = new ExpandoObject();
            d.Id = itemResult.Id;
            d.Name = itemResult.Name;
            d.Description = itemResult.Description;

            return Json(d);
        }

        // POST http://localhost:53147/api/product postman body raw json   { "name":"name 1","description":"desc 1" }
        [HttpPost]
        public IHttpActionResult Post([FromBody] tblCategory category)
        {
            CategoryBl.Add(category);
            return Json("Success");
        }

        // PUT api/values/5
        [HttpPut]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            CategoryBl.Delete(x => x.Id == id);
            return Json("Success");

        }
    }
}