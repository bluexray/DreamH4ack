using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using  Newtonsoft.Json;

namespace DH.Authorization.Server.Controllers
{
    [RoutePrefix("api/v1/order")]
    public class OrderController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        /// <summary>
        /// 获得订单信息
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetOrderAsync(int? id)
        {
            string username = User.Identity.Name;
            return Ok(new { IsError = true, Msg = username, Data = string.Empty });
        }


        /// <summary>
        /// 获得资讯
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("news")]
        [HttpPost]
        public async Task<IHttpActionResult> GetNewsAsync()
        {
            var authentication = HttpContext.Current.GetOwinContext().Authentication;
            var ticket = authentication.AuthenticateAsync("Bearer").Result;

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var data = claimsIdentity.Claims.Where(c => c.Type == "urn:oauth:scope").ToList();
            var claims = ((ClaimsIdentity)Thread.CurrentPrincipal.Identity).Claims;
            return Ok(new { IsError = true, Msg = string.Empty, Data = Thread.CurrentPrincipal.Identity.Name + " It's about news !!! token expires: "  });
        }
    }
}