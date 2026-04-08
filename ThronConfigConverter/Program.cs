using System.Text;
using System.Text.Json;
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


                } else if (convItem.type == "hysteria2") {
                    string hs2 = $"hysteria2://{convItem.bean.pass}@{convItem.bean.addr}:{convItem.bean.port}";
                    if (convItem.bean.allowInsecure != null)
                    {
                        hs2 += $"?insecure={(convItem.bean.allowInsecure.Value ? "1" : "0")}";
                    }
                    if (!string.IsNullOrEmpty(convItem.bean.obfs))
                    {
                        hs2 += $"&obfs={convItem.bean.obfs}";
                    }

                    if (!string.IsNullOrEmpty(convItem.bean.obfsPassword))
                    {
                        hs2 += $"&obfs-password={convItem.bean.obfsPassword}";
                    }
                    if (!string.IsNullOrEmpty(convItem.bean.sni))
                    {
                        hs2 += $"&sni={convItem.bean.sni}";
                    }
                    hysteria2Out.AppendLine(hs2 + "#" + convItem.bean.name);

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
                }
                else if (convItem.type == "wireguard")
                {
                    Console.WriteLine($"wireguard not supported");
                }
                else
                {
                    Console.WriteLine($"STRANGE: {convItem.type}");
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

File.WriteAllText("shadowsocks.txt", shadowSockOut.ToString());
File.WriteAllText("vmess.txt", vmessOut.ToString());
File.WriteAllText("trojan.txt", trojanOut.ToString());
File.WriteAllText("hysteria2.txt", hysteria2Out.ToString());
File.WriteAllText("vless.txt", vlessOut.ToString());
Console.WriteLine("Complete, all files was created...");






