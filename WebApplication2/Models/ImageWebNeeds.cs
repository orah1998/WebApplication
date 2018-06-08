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
    
    public class ImageWebNeeds
    {
        static NetworkStream stream;
        public int donePictures { get; set; }
        public string ServerStatus { get; set; }
        public ImageWebNeeds()
        {
            try { 
            TcpClient client = new TcpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            client = new TcpClient();

            client.Connect(ep);

            stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);

            JObject obj = new JObject();
            obj["inst"] = "1";
            obj["etc"] = "1";

            writer.Write(JsonConvert.SerializeObject(obj));
            string cmd = reader.ReadString();
            JObject obj2 = JsonConvert.DeserializeObject<JObject>(cmd);

            string outputdir=(obj2["OutputDir"].ToString());
                DirectoryInfo di = new DirectoryInfo(outputdir);
                rec(di);
                client.Close();
                this.ServerStatus = "ON";
            }
            catch {
                this.ServerStatus = "OFF";


            }




            


        }







        public void rec(DirectoryInfo di)
        {
            DirectoryInfo[] subFolders = di.GetDirectories();
            foreach (DirectoryInfo item in subFolders)
            {
                foreach (FileInfo pic in item.GetFiles())
                {
                    if (pic.DirectoryName.ToString().Contains("Thumbnails"))
                    {

                        this.donePictures++;
                    }
                }
                rec(item);
            }
        }
    }
}