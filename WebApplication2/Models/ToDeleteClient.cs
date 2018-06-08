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
    public class ToDeleteClient
    {
        static NetworkStream stream;

        public string ID { get; set; }

        public ToDeleteClient(string id)
        {
            ID = id;
        }

        public void DeleteHandler()
        {
            TcpClient client = new TcpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            client = new TcpClient();

            client.Connect(ep);

            stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);


            JObject obj = new JObject();
            obj["inst"] = "3";
            obj["etc"] = ID;

            writer.Write(JsonConvert.SerializeObject(obj));


            //waiting for verification
            string cmd=reader.ReadString();
            JObject res = JsonConvert.DeserializeObject<JObject>(cmd);
            if (res["res"].ToString() == "1")
            {
                client.Close();
            }
            
        }




    }
}