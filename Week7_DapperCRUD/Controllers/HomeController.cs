using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Week7_DapperCRUD.Models;

namespace Week7_DapperCRUD.Controllers
{
    public class HomeController : Controller
    {
        public MovieDAL MovieDB = new MovieDAL();

        public IActionResult Index()
        {
            List<Movie> movies = MovieDB.GetMovies();
            return View(movies);
        }

        public IActionResult Details(int id)
        {
            Movie m = MovieDB.GetMovies(id);
            return View(m);
        }

        //This first action creates the View
        public IActionResult Delete(int id)
        {
            Movie m = MovieDB.GetMovies(id);
            return View(m);
        }

        //This second action processes input/results from the view.
        //Usually you'll be processing form input.
        [HttpPost]
        public IActionResult DeleteFromDb(int id)
        {
            MovieDB.DeleteMovie(id);
            return RedirectToAction("index", "home");
        }

        public IActionResult Edit(int Id)
        {
            Movie m = MovieDB.GetMovies(Id);
            return View(m);
        }

        [HttpPost]
        public IActionResult Edit(Movie m)
        {
            MovieDB.UpdateMovie(m);
            return RedirectToAction("Index", "Home");
        }

        //We're making a new model so we just need to display the view
        public IActionResult Create()
        {
            return View();
        }

        //Now we perform our 'form' processing
        [HttpPost]
        public IActionResult Create(Movie m)
        {
            //ModelState is Valid checks the model against its data annotations
            //it returns true if all rules are met
            //it returns false if any rule is violated
            
            if (ModelState.IsValid)
            {
                MovieDB.CreateMovie(m);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //If the model is bad we will return to the same page
                return View(m);
            }
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ReturnSearch(string title)
        {
            List<Movie> movies = MovieDB.SearchByTitle(title);
            return View(movies);
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