using BTRS.Data;
using BTRS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTRS.Controllers
{
    public class PassengerController : Controller
    {
        //DB
        private SystemDbContext _context;
        public PassengerController(SystemDbContext context)
        {

            this._context = context;
        }
        //SignUp Get
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }


        //SignUp Post
        [HttpPost]
        public IActionResult Signup(Passengers passenger)
        {
            
            bool duplicate = checkData(passenger.username, passenger.email);



            if (ModelState.IsValid)
            {
                if (duplicate)
                {
                    _context.passengers.Add(passenger);
                    _context.SaveChanges();

                    TempData["Msg"] = "the data was saved";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["Msg"] = "The user name or the Email is already used";

                    return View();
                }
            }
            else
            {
                TempData["Msg"] = "Please fill all input ";
                return View();
            }



        }

        //Check UserName and Email
        public bool checkData(string username, string email)
        {
            Passengers PEmail = _context.passengers.Where(t => t.email.Equals(email)).FirstOrDefault();
            Passengers passenger = _context.passengers.Where(u => u.username.Equals(username)).FirstOrDefault();
            if (passenger != null && PEmail != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
      



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login userlogin)
        {
            //needs work !


            if (ModelState.IsValid)
            {
                string username = userlogin.username;
                string password = userlogin.password;

                Passengers passenger = _context.passengers.Where(
                     u => u.username.Equals(username) &&
                     u.password.Equals(password)
                     ).FirstOrDefault();

                Admin admin = _context.admins.Where(
                    a => a.username.Equals(username)
                    &&
                    a.password.Equals(password)
                    ).FirstOrDefault();




                if (passenger != null)

                {
                    HttpContext.Session.SetInt32("userID", passenger.ID);

                    return RedirectToAction("BusTripsList");
                }
                else if (admin != null)
                {

                    HttpContext.Session.SetInt32("adminID", admin.ID);

                    return RedirectToAction("Index", "BusTrip");
                }
                else
                {
                    TempData["Msg"] = "The user Not Found";
                }


            }
            else
            {

            }
            return View();
        }
        [HttpGet]
        public IActionResult BusTripsList()
        {
            return View(_context.bus_trips.ToList());
        }
    }

}
