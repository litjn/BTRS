using BTRS.Data;
using BTRS.Models;
using Microsoft.AspNetCore.Mvc;


namespace BTRS.Controllers
{
    public class BusTripController : Controller
    {
        private SystemDbContext _context;
        public BusTripController(SystemDbContext context)
        {
            _context = context;
        }

        // GET: BusTripController
        public ActionResult Index()
        {
            return View(_context.bus_trips.ToList());
        }

        // GET: BusTripController/Details/5
        public ActionResult Details(int id)
        {
            BusTrip busTrip = _context.bus_trips.Find(id);

            return View(busTrip);
        }

        // GET: BusTripController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusTripController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BusTrip busTrip)
        {

            try
            {

                int adminid = (int)HttpContext.Session.GetInt32("adminID");

                Admin admin = _context.admins.Where(
                  a => a.ID == adminid
                  ).FirstOrDefault();

                busTrip.admin = admin;

                _context.bus_trips.Add(busTrip);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        // GET: BusTripController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            BusTrip busTrip= _context.bus_trips.Find(id);

            return View(busTrip);
        }

        // POST: BusTripController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BusTrip bustrip)
        {
            try
            {

                _context.bus_trips.Update(bustrip);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        // GET: BusTripController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            BusTrip busTrip = _context.bus_trips.Find(id);

            _context.bus_trips.Remove(busTrip);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));



        }

        // POST: BusTripController/Delete/5

    }
}
