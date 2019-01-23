using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FurnutureShop.Models
{//the main class for furnutures and all the proprties, the class have
    public class Furnuture
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public bool DriveToHome { get; set; }
        public int Cost { get; set; }
        public int Count { get; set; }
        public int InitialCount { get; set; }
        public string AllFurn { get; set; }
        public int BuyCount { get; set; }
        public int Points { get; set; }
        public int tempPointsInitialCount { get; set; }
        public int tempPointsBuyCount { get; set; }


        //List Furnuture

        public static List<Furnuture> CreateData()
        {
            List<Furnuture> FurnutureList = new List<Furnuture>();

            FurnutureList.Add(new Sofa { Id = 1, AllFurn= "En soffa som ger en skön och musig upplevelse när du kollar på tv.", Type = "Soffa", Name = "LandsKrona", Color = "Grå", Seats = 6, Sofabed = true, Width = 356, Height = 78, Cost = 12999, DriveToHome = true, Count = 5, InitialCount = 5 });
            FurnutureList.Add(new Sofa { Id = 2, AllFurn=" En liten och smidig soffa som passar i alla rum",Type = "Soffa", Name = "Grönlid", Color = "Beige", Seats = 3, Sofabed = false, Width = 247, Height = 70, Cost = 5345, DriveToHome = true, Count = 5, InitialCount = 5 });
            FurnutureList.Add(new Sofa { Id = 3, AllFurn= "Om du vill ha en färgkklick i ditt liv, så är denna blåa soffa perfekt till dig ",Type = "Soffa", Name = " Stocksund", Color = "Blue", Seats = 2, Sofabed = true, Width = 204, Height = 204, Cost = 2000, DriveToHome = true, Count = 5, InitialCount = 5 });
            FurnutureList.Add(new Beds { Id = 4, AllFurn= " Nordli är en stabil och skön säng för dig", Type = "Säng", Name = " Nordli", Color = "Vit", Madrass = "Mjuk", BedFrame = false, BedSize = 90, Cost = 2765, DriveToHome = true, Count = 3, InitialCount = 3 });
            FurnutureList.Add(new Beds { Id = 5, AllFurn= " En hård säng för en trasig rygg. ", Type = "Säng", Name = "Songesand", Color = "Svart", Madrass = "Hård", BedFrame = true, BedSize = 160, Cost = 1569, DriveToHome = true, Count = 3, InitialCount = 3 });
            FurnutureList.Add(new Beds { Id = 6, AllFurn= " En Barnsäng för ditt barn så hen inte skall få mardrömmar", Type = "Barnsäng", Name = " Kura", Color = "Grön", Madrass = "Mjuk", BedFrame = true, BedSize = 100, Cost = 1400, DriveToHome = true, Count = 3, InitialCount = 3 });
            FurnutureList.Add(new ArmChair { Id = 7, AllFurn= "En fotölj som har fotpall och lätt att stiga upp från" , Type = "Fotölj", Name = "Ekenäset", Color = "Beige", Footstool = true, Cost = 600, DriveToHome = false, Count = 3, InitialCount = 3 });
            FurnutureList.Add(new ArmChair { Id = 8, AllFurn="En perfekt och varm fotölj att somna i när det är som kallas under vintern ", Type = "Fotölj", Name = "Vebo", Color = "Grå", Footstool = false, Cost = 400, DriveToHome = false, Count = 3, InitialCount = 3 });



            return FurnutureList;
        }
        //save data to json file
        public static string filepath = HttpContext.Current.Server.MapPath("~/App_Data/Storage/library.json");



        public static bool SaveData(List<Furnuture> furnuturelist)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(furnuturelist.ToArray(), settings);
            System.IO.File.WriteAllText(filepath, json);

            return true;
        }




        //get data search the filepath
        public static List<Furnuture> GetData()
        {
            List<Furnuture> data;
            if (System.IO.File.Exists(filepath))
            {
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };
                var json = System.IO.File.ReadAllText(filepath);
                data = JsonConvert.DeserializeObject<List<Furnuture>>(json, settings);
            }
            else
            {
                data = CreateData();
            }
            // Algoritm using points from var intialcount and buycount, you get more points when you add from the list

            data = data.OrderBy(x => x.InitialCount).ToList();
            int points = 0;
            foreach (var d1 in data)
            {
                points = points + 5;
                d1.tempPointsInitialCount = points;
                d1.Points = points;
            }


            data = data.OrderBy(x => x.BuyCount).ToList();
            points = 0;
            foreach (var d2 in data)
            {
                points = points + 3;
                d2.tempPointsBuyCount = points;
                d2.Points += points;
            }

            data = data.OrderByDescending(x => x.Points).ToList();
            SaveData(data);
            return data;
        }
    }
    //Class furnutures and Arv, propreties
    public class Sofa : Furnuture
    {
        public bool Sofabed { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Seats { get; set; }
    }
    public class Beds : Furnuture
    {
        public string Madrass { get; set; }
        public bool BedFrame { get; set; }
        public int BedSize { get; set; }
    }
    public class ArmChair : Furnuture
    {
        public bool Footstool { get; set; }

    }

}
