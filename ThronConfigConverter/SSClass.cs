using System.Text;
using System.Text.Json.Serialization;
using System.Web;

namespace ThronConfigConverter
{
    // https://shadowsocks.org/doc/sip002.html
    public class SSObject
    {
        public string server { get; set; }
        public int server_port { get; set; }
        public int local_port { get; set; }
        public string password { get; set; }
        public string method { get; set; }

        public string plugin { get; set; }

        [JsonIgnore]
        public string name { get; set; }

        public override string ToString()
        {            

            string ret_string = method + ":" + password;           
            var byteArray = Encoding.UTF8.GetBytes(ret_string);
            string base64String = Convert.ToBase64String(byteArray);       
                       

            return "ss://"+ base64String + "@" + server + ":" +
                server_port+(string.IsNullOrEmpty(plugin) ? "" : "/?plugin=" +HttpUtility.UrlEncode(plugin))+
                "#" + name;
        }

    }

}
