using System.Text;
using System.Text.Json;
using System.Web;
using ThronConfigConverter;



// protocols info
// https://github.com/throneproj/Throne/issues/1270
// https://deepwiki.com/aiboboxx/v2rayfree/4.3-vmess-protocol


Console.WriteLine("Converter form old Throne profile configuration json files to text format files\r\nfor import to new version of Throne");

string base_directory = args[0];// "E:\\Throne\\config\\profiles\\";
if (!Directory.Exists(base_directory))
{
    Console.WriteLine("Input directory not found!\r\n");
    return;
}

var files=Directory.GetFiles(base_directory, "*.json");

StringBuilder shadowSockOut=new StringBuilder();
StringBuilder vmessOut = new StringBuilder();
StringBuilder vlessOut = new StringBuilder();
StringBuilder trojanOut = new StringBuilder();
StringBuilder hysteria2Out= new StringBuilder();
StringBuilder wgOut = new StringBuilder();

int shadowSockRecs=0, vmessRecs=0, vlessRecs=0, trojanRecs=0, hysteria2Recs=0, wgRecs=0 ;

foreach (string fl in files)
{
    using (var file = File.OpenRead(fl))
    {
        try
        {
            using JsonDocument jsDoc = JsonDocument.Parse(file);
            var convItem = jsDoc.Deserialize<Rootobject>();
            if (convItem != null)
            {
                Console.WriteLine($"file: {fl}");
                if (convItem.type == "shadowsocks")
                {                                 


                    var ssObj = new SSObject()
                    {
                        method = convItem.bean.method,
                        local_port = convItem.bean.port,
                        password = convItem.bean.pass,
                        server = convItem.bean.addr,
                        server_port = convItem.bean.port,
                        name = convItem.bean.name,
                        plugin = convItem.bean.plugin,
                    };


                    shadowSockOut.AppendLine(ssObj.ToString());
                    shadowSockRecs++;



                } else if (convItem.type == "vmess")
                {
                    
                    VMessRootobject vmessObj = new VMessRootobject()
                    {
                        ps = convItem.bean.name,
                        port = convItem.bean.port,
                        id = convItem.bean.id,
                        v = convItem.bean._v,
                        add = convItem.bean.addr,
                        aid = convItem.bean.aid,
                        scy = convItem.bean.sec,
                        tls = convItem.bean.stream.sec,
                        sni = convItem.bean.stream.sni,
                        type = convItem.bean.stream.h_type,
                        host = convItem.bean.stream.host,
                        path = convItem.bean.stream.path,
                        net = convItem.bean.stream.net,
                        fp= convItem.bean.stream.utls,

                    };
                    vmessOut.AppendLine(vmessObj.ToString());
                    vmessRecs++;

                } else if (convItem.type == "trojan")
                {

                    string tro_str = $"trojan://{convItem.bean.pass}@{convItem.bean.addr}:{convItem.bean.port}";

                    tro_str += $"?allowInsecur={(convItem.bean.stream.insecure ? "1" : "0")}";

                    if (!string.IsNullOrEmpty(convItem.bean.stream.sni))
                    {
                        tro_str += $"&sni={convItem.bean.stream.sni}";
                    }
                    if (!string.IsNullOrEmpty(convItem.bean.stream.h_type))
                    {
                        tro_str += $"&type={convItem.bean.stream.h_type}";
                    }
                    if (!string.IsNullOrEmpty(convItem.bean.stream.path))
                    {
                        tro_str += $"&path={convItem.bean.stream.path}";
                    }
                    if (!string.IsNullOrEmpty(convItem.bean.stream.host))
                    {
                        tro_str += $"&host={convItem.bean.stream.host}";
                    }
                    if (!string.IsNullOrEmpty(convItem.bean.sec))
                    {
                        tro_str += $"&security={convItem.bean.sec}";
                    }

                    trojanOut.AppendLine(tro_str + "#" + convItem.bean.name);
                    trojanRecs++;

                } else if (convItem.type == "hysteria2") {
                    string hs2 = $"hysteria2://{convItem.bean.pass}@{convItem.bean.addr}:{convItem.bean.port}";
                    if (convItem.bean.allowInsecure != null)
                    {
                        hs2 += $"?insecure={(convItem.bean.allowInsecure.Value ? "1" : "0")}";
                    }
                    if (!string.IsNullOrEmpty(convItem.bean.obfs))
                    {
                        hs2 += $"&obfs={HttpUtility.UrlEncode(convItem.bean.obfs)}";
                    }

                    if (!string.IsNullOrEmpty(convItem.bean.obfsPassword))
                    {
                        hs2 += $"&obfs-password={HttpUtility.UrlEncode(convItem.bean.obfsPassword)}";
                    }
                    if (!string.IsNullOrEmpty(convItem.bean.sni))
                    {
                        hs2 += $"&sni={HttpUtility.UrlEncode(convItem.bean.sni)}";
                    }
                    hysteria2Out.AppendLine(hs2 + "#" + convItem.bean.name);
                    hysteria2Recs++;
                }
                else if (convItem.type == "vless")
                {
                    string vl = $"vless://{convItem.bean.pass}@{convItem.bean.addr}:{convItem.bean.port}";
                    vl += $"?allowInsecure={(convItem.bean.stream.insecure ? "1" : "0")}";
                    if (!string.IsNullOrEmpty(convItem.bean.stream.sec))
                    {
                        vl += $"&security={convItem.bean.stream.sec}";
                    }

                    if (!string.IsNullOrEmpty(convItem.bean.stream.pac_enc))
                    {
                        vl += $"&flow={convItem.bean.stream.pac_enc}";
                    }

                    if (!string.IsNullOrEmpty(convItem.bean.stream.sni))
                    {
                        vl += $"&sni={convItem.bean.stream.sni}";
                    }

                    if (!string.IsNullOrEmpty(convItem.bean.stream.utls)) 
                    {
                        vl += $"&fp={convItem.bean.stream.utls}";
                    }

                    if (!string.IsNullOrEmpty(convItem.bean.stream.net)) 
                    {
                        vl += $"&type={convItem.bean.stream.net}";
                    }
                    if (!string.IsNullOrEmpty(convItem.bean.stream.net)) 
                    {
                        vl += $"&type={convItem.bean.stream.net}";
                    }
                    if (!string.IsNullOrEmpty(convItem.bean.stream.headers)) 
                    {
                        vl += $"&host={convItem.bean.stream.headers}";
                    }
                    if (!string.IsNullOrEmpty(convItem.bean.stream.path)) 
                    {
                        vl += $"&path={convItem.bean.stream.path}";
                    }
                    if (!string.IsNullOrEmpty(convItem.bean.stream.alpn))
                    {
                        vl += $"&alpn={convItem.bean.stream.alpn}";
                    }
                    vlessOut.AppendLine(vl + "#" + convItem.bean.name);
                    vlessRecs++;
                }
                else if (convItem.type == "wireguard")
                {
                    string vg = $"wg://{convItem.bean.addr}:{convItem.bean.port}?";
                    vg += $"private_key={convItem.bean.private_key}";
                    vg += $"&public_key={convItem.bean.public_key}";
                    if (convItem.bean.persistent_keepalive != null && convItem.bean.persistent_keepalive > 0)
                    {
                        vg += $"&persistent_keepalive_interval={convItem.bean.persistent_keepalive}";
                    }

                    if (!string.IsNullOrEmpty(convItem.bean.pre_shared_key))
                    {
                        vg += $"&pre_shared_key={convItem.bean.pre_shared_key}";
                    }
                    if (convItem.bean.local_address != null)
                    {                        
                        vg += $"&local_address={string.Join("-", convItem.bean.local_address)}";
                    }
                    if (convItem.bean.reserved != null)
                    {
                        vg += $"&reserved={string.Join("-", convItem.bean.reserved.Select(m=>m.ToString()))}";
                    }
                    if (convItem.bean.worker_count!=null && convItem.bean.worker_count > 0)
                    {
                        vg += $"&workers={convItem.bean.worker_count}";
                    }
                    if (convItem.bean.use_system_proxy != null && convItem.bean.use_system_proxy==true)
                    {
                        vg += $"&use_system_interface=true";
                    }
                    wgOut.AppendLine(vg + "#" + convItem.bean.name);
                    wgRecs++;
                }
                else
                {
                    Console.WriteLine($"Warning! Usupported type: {convItem.type}");
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error In file {fl} {ex.ToString()}");            
            string dp = "WrongFiles";
            if (!Directory.Exists(dp))
            {
                Directory.CreateDirectory(dp);
            }
            string out_pth=Path.Combine(dp,Path.GetFileName(fl));
            File.Copy(fl, out_pth);

        }
    }

}
Console.WriteLine();
if (shadowSockOut.Length > 0)
{
    Console.WriteLine($"shadowsocks.txt - {shadowSockRecs} records.");
    File.WriteAllText("shadowsocks.txt", shadowSockOut.ToString());
}
if (vmessOut.Length > 0)
{
    Console.WriteLine($"vmess.txt - {vmessRecs} records.");
    File.WriteAllText("vmess.txt", vmessOut.ToString());
}
if (trojanOut.Length > 0)
{
    Console.WriteLine($"trojan.txt - {trojanRecs} records.");
    File.WriteAllText("trojan.txt", trojanOut.ToString());
}
if (hysteria2Out.Length > 0)
{
    Console.WriteLine($"hysteria2.txt - {hysteria2Recs} records.");
    File.WriteAllText("hysteria2.txt", hysteria2Out.ToString());
}
if (vlessOut.Length > 0)
{
    Console.WriteLine($"vless.txt - {vlessRecs} records.");
    File.WriteAllText("vless.txt", vlessOut.ToString());
}
if (wgOut.Length > 0)
{
    Console.WriteLine($"wireguard.txt - {wgRecs} records.");
    File.WriteAllText("wireguard.txt", wgOut.ToString());
}
Console.WriteLine("Complete, all files was created...");






