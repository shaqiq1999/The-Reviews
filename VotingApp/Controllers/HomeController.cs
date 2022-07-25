using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Areas.Identity.Data;
using VotingApp.Data;
using VotingApp.Models;

namespace VotingApp.Controllers
{
    public class HomeController : Controller

    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=VotingApp;Trusted_Connection=True;MultipleActiveResultSets=true");
        private readonly ILogger<HomeController> _logger;
        
        private readonly UserManager<VoteAppUser> _userManager;
        public HomeController(ILogger<HomeController> logger, UserManager<VoteAppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
            con.ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=VotingApp;Trusted_Connection=True;MultipleActiveResultSets=true";
            
        }
        static string n="";
        private void FetchData()
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = " Select ShowName from [dbo].[TvShow] where id = 1;";
                dr = com.ExecuteReader();
                
                    n = com.ExecuteScalar().ToString();
                Console.WriteLine(n);
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            VoteAppUser user = _userManager.FindByIdAsync(userId).Result;

            if (user is not null)
            {
                ViewBag.userName = user.Name;
            }
            else
                ViewBag.userName = n;
            //name= _context.AspNetUsers.FromSql("select Name from AspNetUsers where UserName=userName");
            
            return View();
        }
       

        public IActionResult Privacy()
        {
            return View();
        }
        





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
