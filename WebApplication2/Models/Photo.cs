﻿using Newtonsoft.Json;
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
    public class Photo
    {

        public string OutputDir { get; set; }
        static NetworkStream stream;
        public List<string> list { get; set; }
        public List<string> listOfDates { get; set; }

        public Photo()
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
            this.OutputDir = obj2["OutputDir"].ToString();

            DirectoryInfo dir =new DirectoryInfo(OutputDir);
            this.list = new List<string>();
            this.listOfDates = new List<string>();
            rec(dir);

            client.Close();
            }catch(Exception e)
            {
                this.OutputDir = @"C:\Users\Operu\Desktop\dest";
                DirectoryInfo dir = new DirectoryInfo(OutputDir);
                this.list = new List<string>();
                this.listOfDates = new List<string>();
                rec(dir);
            }
        }


        


        public void rec(DirectoryInfo di)
        {
            DirectoryInfo[] subFolders = di.GetDirectories();
            foreach (DirectoryInfo item in subFolders)
            {
                foreach (FileInfo pic in item.GetFiles())
                {
                    if (pic.DirectoryName.ToString().Contains("Thumbnails")) {
                        string path = pic.DirectoryName.ToString() + "\\" + pic.ToString();
                        list.Add(GetRelaPath(path));
                        DateTime dt = File.GetCreationTime(path);
                        listOfDates.Add(dt.Month.ToString()+"/"+dt.Year.ToString());
                    }
                }
                rec(item);
            }
        }



        public string GetRelaPath(string str)
        {
            string res = "";
            int flag = 0;
            string prevItem = "";
            foreach (string item in str.Split('\\'))
            {
                if (item.Contains("Thumbnails"))
                {
                    flag = 1;
                    res +="\\"+ prevItem+"\\";
                }
                if (flag == 1)
                {
                    res += item + "\\";
                }
                prevItem = item;
            }

            res = res.Remove(res.Length - 1);
            res = "~" + res;

            return res;
        }






    }
}






