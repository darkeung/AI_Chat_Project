using Java.IO;
using Java.Net;
using System;
using System.Text;
namespace SimsimiChat.Helper
{
    public class HttpDataHandler
    {
        static string stream = null;
        public HttpDataHandler() { }
        public string GetHTTPData(string urlString)
        {
            try
            {
                URL url = new URL(urlString);
                HttpURLConnection urLConnection = (HttpURLConnection)url.OpenConnection();
                if (urLConnection.ResponseCode == HttpStatus.Ok)
                {
                    BufferedReader r = new BufferedReader(new InputStreamReader(urLConnection.InputStream));
                    StringBuilder sb = new StringBuilder();
                    String line;
                    while ((line = r.ReadLine()) != null) sb.Append(line);
                    stream = sb.ToString();
                    urLConnection.Disconnect();
                }
            }
            catch (Exception ex) { }
            return stream;
        }
    }
}