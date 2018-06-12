using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Web;

namespace WebApplication2.Models
{
    
    public class DeletePhotoNeeds
    {
        public string deleting { get; set; }
        public string date { get; set; }

        public DeletePhotoNeeds()
        {
        }



            public DeletePhotoNeeds(string del)
        {
            SingletonClient.Instance.Connect();
            NetworkStream stream = SingletonClient.Instance.getClient().GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);

            JObject obj = new JObject();
            obj["inst"] = "1";
            obj["etc"] = "1";

            writer.Write(JsonConvert.SerializeObject(obj));


            //waiting for verification
            string cmd = reader.ReadString();
            JObject res = JsonConvert.DeserializeObject<JObject>(cmd);
            string outputdir=res["OutputDir"].ToString() ;
            this.deleting=getFullPath(outputdir,del);

            PhotoDates();
        }

        public void deletePhoto()
        {

            if (this.deleting.Contains("Thumbnails")) { 

            string res = "";
            foreach (string item in this.deleting.Split('\\'))
            {
                if (!item.Contains("Thumbnails"))
                {
                    res += item + "\\";
                }
            }
             
            res = res.Remove(res.Length - 1);
            File.Delete(res);
            File.Delete(this.deleting);

            }


            else
            {
                string res = "";
                string[] arr=this.deleting.Split('\\');
                res += arr[arr.Length - 3] +"\\"+ arr[arr.Length - 2] + "\\" + arr[arr.Length - 1];
                string res2 = "Thumbnails\\" + res;
                string getpath="";
                for (int i = 0; i < arr.Length-3; i++)
                {
                    getpath += arr[i]+"\\";
                }
                res2 = getpath + res2;


                File.Delete(res2);
                File.Delete(this.deleting);
            }




        }

        public void PhotoDates()
        {
            string ret = "";
            DateTime file = File.GetCreationTime(deleting);
            ret +=file.Day.ToString()+" / "+file.Month.ToString() + " / " + file.Year.ToString();
            this.date= ret;
        }

        public string getFullPath(string destPath,string picPath)
        {
            string temp="";
            string[] arr = picPath.Split('\\');
            for (int i = 2; i < arr.Length; i++)
            {
                temp += '\\' + arr[i];
            }
            temp = destPath + temp;

            return temp;


        }

    }
}