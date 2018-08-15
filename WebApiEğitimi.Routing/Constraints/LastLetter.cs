using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace WebApiEğitimi.Routing.Constraints
{
    public class LastLetter : IHttpRouteConstraint
    {
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {
            string paramVal = values[parameterName].ToString();

            if (paramVal.EndsWith("a") || paramVal.EndsWith("b") || paramVal.EndsWith("c"))
                return true;
            return false;
        }
    }
}