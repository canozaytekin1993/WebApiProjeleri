using System.Collections.Generic;
using System.Web.Http;

namespace WebApiProjesi.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        private static List<string> degerler = new List<string>()
        {
            "value0","value1","value2"
        };

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Degerler()
        {
            return degerler;
        }

        // GET api/values/5
        [HttpGet]
        public string DegerGetir(int id)
        {
            return degerler[id];
        }

        // POST api/values
        [HttpPost]
        public void DegerEkle([FromBody]string value)
        {
            degerler.Add(value);
        }

        // PUT api/values/5
        [HttpPut]
        public void DegerGuncelle(int id, [FromBody]string value)
        {
            degerler[id] = value;
        }

        // DELETE api/values/5
        [HttpDelete]
        public void DegerSil(int id)
        {
            degerler.RemoveAt(id);
        }
    }
}