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
            File.Delete(this.deleting);
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