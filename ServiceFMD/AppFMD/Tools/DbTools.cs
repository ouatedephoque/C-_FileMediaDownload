using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppFMD
{
    class DbTools
    {
        public static async Task<MemoryStream> WCFRestServiceCall(Settings settings, String methodRequestType, String methodName, String bodyParam = "")
        {
            if (!settings.IpComputer.Equals(":51589"))
            {
                string ServiceURI = "http://" + settings.IpComputer + "/FilmRESTService.svc/" + methodName + "";

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.IfModifiedSince = DateTimeOffset.Now;

                    HttpRequestMessage request = new HttpRequestMessage(methodRequestType == "GET" ? HttpMethod.Get : HttpMethod.Post, ServiceURI);

                    if (!string.IsNullOrEmpty(bodyParam))
                    {
                        request.Content = new StringContent(bodyParam, Encoding.UTF8, "application/json");
                    }

                    HttpResponseMessage response = await httpClient.SendAsync(request);

                    string returnString = await response.Content.ReadAsStringAsync();
                    byte[] data = Encoding.UTF8.GetBytes(returnString);
                    MemoryStream stream = new MemoryStream(data);

                    return stream;
                }
            }
            return null;
        }
    }
}
