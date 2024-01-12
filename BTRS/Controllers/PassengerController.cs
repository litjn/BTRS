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

                    return RedirectToAction("BusTripList");
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
        public IActionResult BusTripList()
        {

            int userID = (int)HttpContext.Session.GetInt32("userID");
            List<int> lst_user = _context.passenger_BusTrips.Where(
              t => t.passenger.ID == userID).Select(s => s.bus_trip.ID).ToList();

            List<BusTrip> lst_bus = _context.bus_trips.Where(
                t => lst_user.Contains(t.ID)
                ).ToList();
            List<BusTrip> bus1 = _context.bus_trips.ToList();
            List<BusTrip> bus2 = bus1.Except(lst_bus).ToList();

            return View(bus2);
        }
        
        public IActionResult AddBooking(int ID)
        {
            int UserID = (int)HttpContext.Session.GetInt32("userID");

            PassengerBusTrip passengertrip = new PassengerBusTrip();
            passengertrip.passenger = _context.passengers.Find(UserID);
            passengertrip.bus_trip = _context.bus_trips.Find(ID);

            _context.passenger_BusTrips.Add(passengertrip);
            _context.SaveChanges();
            return RedirectToAction("Booked");
        }
   
        public IActionResult Booked()
        {

            int userID = (int)HttpContext.Session.GetInt32("userID");


            List<int> lst_user = _context.passenger_BusTrips.Where(
                t => t.passenger.ID == userID).Select(s => s.bus_trip.ID).ToList();

            List<BusTrip> lst_bus = _context.bus_trips.Where(
                t => lst_user.Contains(t.ID)
                ).ToList();


            return View(lst_bus);
        }
        [HttpGet]
        public IActionResult cancel(int id)
        {
            int userID = (int)HttpContext.Session.GetInt32("userID");

            PassengerBusTrip cancelled = _context.passenger_BusTrips.Where(u => u.passenger.ID == userID).FirstOrDefault(r => r.bus_trip.ID == id);

            _context.passenger_BusTrips.Remove(cancelled);
            _context.SaveChanges();

            return RedirectToAction("Booked");
        }
    }

}
