using System.Text;
using System.Text.Json;
namespace ThronConfigConverter
{


    public class VMessRootobject
    {
           
        public int v { get; set; }
        public string ps { get; set; }
        public string add { get; set; }
        public int port { get; set; }
        public string id { get; set; }
        public int? aid { get; set; } = 0;
        public string scy { get; set; }
        public string net { get; set; }
        public string type { get; set; }

        public string host { get; set; }
        public string path { get; set; }        
        public string tls { get; set; }

        public string sni { get; set; }
        public string fp { get; set; }

        






        public override string ToString()
        {
            
            string jsonstr = JsonSerializer.Serialize(this);
            var byteArray = Encoding.UTF8.GetBytes(jsonstr);            
            return "vmess://" + Convert.ToBase64String(byteArray);
        }

    }


}
