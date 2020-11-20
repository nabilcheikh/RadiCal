using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Appel_Leaflet_et_Conversion.Models;
using Appel_Leaflet_et_Conversion.Services;

namespace Appel_Leaflet_et_Conversion.Controllers
{
    public class FireFrontController : ApiController
    {
        private FireFrontRepository fireFrontRepository;

        public FireFrontController()
        {
            this.fireFrontRepository = new FireFrontRepository();
        }

        // GET: api/FireFront
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FireFront/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/FireFront
        public HttpResponseMessage Post(FireFront fireFront)
        {
            this.fireFrontRepository.SaveFireFront(fireFront);
            var response = Request.CreateResponse<FireFront>(System.Net.HttpStatusCode.Created, fireFront);
            return response;
        }

        // PUT: api/FireFront/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FireFront/5
        public void Delete(int id)
        {
        }
    }
}
