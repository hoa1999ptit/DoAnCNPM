using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using doan_cnpm.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using doan_cnpm.Models;
using doan_cnpm.Data;
using doan_cnpm.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using doan_cnpm.Utility;

namespace doan_cnpm.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<int> lsNotification = HttpContext.Session.Get<List<int>>("Notification");
            List<Appoinments> appoinmentslist = new List<Appoinments>();

            if (lsNotification != null)
            {
                foreach(int id in lsNotification)
                {
                    Appoinments appointment = _db.Appoinments.Where(m=>m.isConfirmed==false).FirstOrDefault(m => m.Id == id);
                    appoinmentslist.Add(appointment);
                }
            }
            return View(appoinmentslist);
        }
    }
}