using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    
    public class DeletePhotoNeeds
    {
        public string deleting { get; set; }
        public string date { get; set; }



        public DeletePhotoNeeds(string del)
        {
            this.deleting = del;
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



    }
}