using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ThronConfigConverter
{
    //public class ThroneConfigItem
    //{
    //    public SubInstance bean { get; set; }

    //    [JsonPropertyName("type")]
    //    public string DocType { get; set; }
    //    public string ul { get; set; }
    //    public int yc { get; set; }

    //}

    //public class SubInstance
    //{
    //    public string addr { get; set; }
    //    public int brutal_speed { get; set; }

    //    public string method { get; set; }

    //}




    public class Rootobject
    {
        public Bean bean { get; set; }
        public string dl { get; set; }
        public int gid { get; set; }
        public int id { get; set; }
        public string report { get; set; }
        public Traffic traffic { get; set; }
        public string type { get; set; }
        public string ul { get; set; }
        public int yc { get; set; }

    }

    public class Bean
    {
        public int _v { get; set; }
        public string addr { get; set; }
        public int brutal_speed { get; set; }
        public string c_cfg { get; set; }
        public string c_out { get; set; }
        public bool enable_brutal { get; set; }
        public string id { get; set; }
        public int? aid { get; set; }
       
        public bool? allowInsecure { get; set; }
        public string sec { get; set; }
        public string method { get; set; }
        public int mux { get; set; }
        public string name { get; set; }

        public string pre_shared_key { get; set; }
        public string private_key { get; set; }
        public string public_key { get; set; }
        public int? persistent_keepalive {  get; set; }
        public int[]? reserved { get; set; }
        public string[] local_address { get; set; }
        public string sni { get; set; }
        public string path { get; set; }

        public bool? use_system_proxy { get; set; }

        public int? worker_count { get; set; }
        public string obfs { get; set; }
        public string obfsPassword { get; set; }
        public string pass { get; set; }
        public string plugin { get; set; }
        public int port { get; set; }
        public Stream stream { get; set; }
        public int uot { get; set; }
    }

    public class Stream
    {
        public string alpn { get; set; }
        public string cert { get; set; }
        public int ed_len { get; set; }
        public string ed_name { get; set; }
        public string h_type { get; set; }
        public string headers { get; set; }
        public string host { get; set; }
        public bool insecure { get; set; }
        public string method { get; set; }
        public string net { get; set; }
        public string pac_enc { get; set; }
        public string path { get; set; }
        public string pbk { get; set; }
        public string sec { get; set; }
        public string sid { get; set; }
        public string sni { get; set; }
        public bool tls_frag { get; set; }
        public string tls_frag_fall_delay { get; set; }
        public bool tls_record_frag { get; set; }
        public string utls { get; set; }
    }

    public class Traffic
    {
        public Int64 dl { get; set; }
        public Int64 ul { get; set; }
    }


}
