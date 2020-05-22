using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieRegistration.Models;

namespace MovieRegistration.Controllers
{
    public class MovieRegistrationController : Controller
    {
        public IActionResult MovieRegistration()
        {
            return View();
        }
        public IActionResult Register(Movie movie)
        //public IActionResult Register(int id, string title, string genre, int year, string actors, string directors)
        {
            if (ModelState.IsValid)
            {

                //Movie movie = new Movie();
                //movie.ID = id;
                //movie.Title = title;
                //movie.Genre = genre;
                //movie.Year = year;
                //movie.Actors = actors;
                //movie.Directors = directors;
                return View("MovieRegistrationSummary", movie);
            }
            else
            {
                return View("Index");
            }
        }
    }
}