using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurnutureShop.Models;
namespace FurnutureShop.Controllers
{
    //Saves userdata to json
    public class HomeController : Controller
    {

        public List<Furnuture> furnutureList = Models.Furnuture.GetData();
        public UserData userdata;

        public ActionResult Index()
        {
            if (Session["UserId"] is int)

            {
                userdata = UserData.GetUserData((int)Session["UserId"]);

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            ViewModel VM = ViewModel.viewmodel(furnutureList, userdata);

            return View(VM);
        }


        //Add/Buy to/from Cart gets and saves userdata

        public ActionResult Buy(int id)
        {
            foreach (Furnuture furnuture in furnutureList)
            {
                if (furnuture.Id == id)
                {
                    furnuture.Count--;
                    furnuture.BuyCount++;

                    Furnuture.SaveData(furnutureList);
                    userdata = UserData.GetUserData((int)Session["UserId"]);
                    if (userdata.CartList == null)
                    {
                        userdata.CartList = new List<UserData.Cart>();
                    }
                    userdata.CartList.Add(new UserData.Cart { Id = furnuture.Id });
                    UserData.SaveUserData(userdata);
                }

            }
            userdata = UserData.GetUserData((int)Session["UserId"]);
            
            ViewModel VM = ViewModel.viewmodel(furnutureList, userdata);
            
            return View("Index", VM);
          
        }
    
        //delete from Cart and saves and gets 
        public ActionResult Return(int id)
        {
            foreach (Furnuture furnuture in furnutureList)
            {
                if (furnuture.Id == id)
                {
                    furnuture.Count++;
                    Furnuture.SaveData(furnutureList);
                    userdata = UserData.GetUserData((int)Session["UserId"]);
                    var itemToRemove = userdata.CartList.FirstOrDefault(r => r.Id == id);
                    if (itemToRemove != null)
                    {
                        userdata.CartList.Remove(itemToRemove);
                        UserData.SaveUserData(userdata);
                    }
                }

            }

            ViewModel VM = ViewModel.viewmodel(furnutureList, userdata);
            return View("Index", VM);
        }


        //Pay method
        public ActionResult Pay()
        {
            return View("Pay");
            
        }
        //complete page after paying, and clear the cart from itmes furnuture
        public ActionResult Complete()
        {
            userdata = UserData.GetUserData((int)Session["UserId"]);

            ViewModel VM = ViewModel.viewmodel(furnutureList, userdata);
            userdata.CartList.Clear();
            UserData.SaveUserData(userdata);
           
            return View("Complete");

           
        }
    






        public ActionResult furnutures(int Id)
        {

            Furnuture product = furnutureList.Where(x => x.Id == Id).FirstOrDefault();
            return View(product);
        }

        // public ActionResult BasketAdd(int id)
        //{
        //  foreach (Models.Furnuture furnuture in FurnutureList)
        //{
        //  furnuture.Id++;
        //Models.Furnuture.SaveData(FurnutureList);
        //break;
        //}
        //return View(FurnutureList);
    }

}
    




