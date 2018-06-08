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
        
        public DeletePhotoNeeds(string del)
        {
            this.deleting = del;
        }

        public void deletePhoto()
        {
            File.Delete(this.deleting);
        }


    }
}