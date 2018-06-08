using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;

namespace WebApplication2.Models
{
    public class ConfigClient
    {
        public List<string> Handler{get; set;}
        public string OutputDir { get; set; }
        public string SourceName { get; set; }
        public string LogName { get; set; }
        public int ThumbnailSize { get; set; }
        NetworkStream stream;
        TcpClient client;
        BinaryReader reader ;
        BinaryWriter writer ;



        public ConfigClient() {
            SingletonClient.Instance.Connect();
            stream = SingletonClient.Instance.getClient().GetStream();


            using (NetworkStream stream = SingletonClient.Instance.getClient().GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {


                JObject obj = new JObject();
                obj["inst"] = "1";
                obj["etc"] = "1";

                writer.Write(JsonConvert.SerializeObject(obj));
                string cmd = reader.ReadString();
                JObject obj2 = JsonConvert.DeserializeObject<JObject>(cmd);
                this.LogName = obj2["LogName"].ToString();
                this.OutputDir = obj2["OutputDir"].ToString();
                this.SourceName = obj2["SourceName"].ToString();
                this.ThumbnailSize = int.Parse(obj2["ThumbnailSize"].ToString());
                string toBreak = obj2["Handler"].ToString();


                List<string> temp = new List<string>();
                foreach (string item in toBreak.Split(';'))
                {
                    if (item != "NONE") { 
                    temp.Add(item);
                    }
                }
                this.Handler = temp;




            }
            SingletonClient.Instance.Closing();
        }


        
        public void TellClient(string path)
        {
            TcpClient client = new TcpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            client = new TcpClient();

            client.Connect(ep);

            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);


            JObject obj = new JObject();
            obj["inst"] = "3";
            obj["etc"] = path;

            writer.Write(JsonConvert.SerializeObject(obj));

            List<string> temp = new List<string>();
            foreach(string item in this.Handler)
            {
                if (item.ToString() !=path) {
                    temp.Add(item);
                }
            }
            Handler =temp;


            
            client.Close();
        }







    }
}