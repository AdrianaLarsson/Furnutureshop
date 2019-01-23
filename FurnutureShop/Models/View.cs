using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnutureShop.Models
{
    public class ViewModel
    {
        public List<Furnuture> FurnutureList { get; set; }
        public  UserData UserData{ get; set; }

        public static ViewModel viewmodel(List<Furnuture> furnuturelist, UserData userdata)
        {
            ViewModel VM = new ViewModel();
            VM.FurnutureList = furnuturelist;
            VM.UserData = userdata;

            return VM;
        }

    }
}