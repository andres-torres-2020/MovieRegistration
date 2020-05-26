using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRegistration.Models;

namespace MovieRegistration.Controllers
{
    public class MovieRegistrationController : Controller
    {
        public static List<Movie> Movies = new List<Movie>
            {
                // this list is static because this object is re-created by .Net
                new Movie(1002, "movie-1", 110, "Drama", 1992, "a1,a2", "d1"),
                new Movie(1004, "movie-2", 80, "Comedy", 1993, "a1,a2", "d1"),
                new Movie(1006, "movie-3", 120, "Drama", 2015, "a1,a2", "d1"),
                new Movie(1008, "movie-4", 90, "Comedy", 2019, "a1,a2", "d1"),
                new Movie(1010, "movie-5", 55, "Documentary", 2008, "a1,a2", "d1"),
            };
        public static bool CanShowCart { get; set; } = false; //set when cart has at least one item
        public IActionResult Index()
        {
            //show the view to display the list of Movies
            return View(Movies);
        }
        public IActionResult AddMovieToCart()
        {
            string movieStringId = Request.Query["ID"];
            int movieId = int.Parse(movieStringId);
            string sessionName = "SessionName";
            List<int> sessionCartIdList = GetCartFromSession(sessionName);
            bool alreadyAdded = false;
            foreach (int id in sessionCartIdList)
            {
                if (id == movieId)
                {
                    alreadyAdded = true;
                    break;
                }
            }
            if (alreadyAdded)
            {
                TempData["MovieAddCartMessage"] = "The movie is already in your cart.";
                ViewData["MovieAddCartMessage"] = "The movie is already in your cart.";
            }
            else
            {
                CanShowCart = true;
                sessionCartIdList.Add(movieId);
                SaveCartToSession(sessionName, sessionCartIdList);
                TempData["MovieAddCartMessage"] = $"Movie {movieId} added to your cart.";
                ViewData["MovieAddCartMessage"] = $"Movie {movieId} added to your cart.";
            }
            return View("Index", Movies);
        }
        public IActionResult MovieRegistration()
        {
            // show the Movie Registration form;
            // any Movie processing messages are also displayed
            return View();
        }
        public IActionResult Register(Movie movie)
        {
            if (ModelState.IsValid)
            {
                bool alreadyInDb = false;
                foreach(Movie m in Movies)
                {
                    if (m.ID == movie.ID)
                    {
                        ViewBag.MovieResultMessage = "The movie ID is already in the database.";
                        TempData["MovieResultMessage"] = "The movie ID is already in the database.";
                        ViewData["MovieResultMessage"] = "The movie ID is already in the database.";
                        alreadyInDb = true;
                        break;
                    }
                    else if (m.Title == movie.Title)
                    {
                        ViewBag.MovieResultMessage = "The movie Title is already in the database.";
                        TempData["MovieResultMessage"] = "The movie Title is already in the database.";
                        ViewData["MovieResultMessage"] = "The movie Title is already in the database.";
                        alreadyInDb = true;
                        break;
                    }
                }

                if (!alreadyInDb)
                {
                    Movies.Add(movie);
                    ViewBag.MovieResultMessage = "Movie added ({movie.Title})";
                    TempData["MovieResultMessage"] = $"Movie added ({movie.Title})";
                    ViewData["MovieResultMessage"] = $"Movie added ({movie.Title})";
                }
            }
            return View("MovieRegistration");
        }
        #region InternalMethods
        private List<int> GetCartFromSession(string sessionName)
        {
            string movieListJSON = HttpContext.Session.GetString(sessionName) ?? "AltString";
            List<int> sessionFilledList = null;
            if (movieListJSON != "AltString")
            {
                sessionFilledList = JsonSerializer.Deserialize<List<int>>(movieListJSON);
            }
            else
            {
                sessionFilledList = new List<int>();
            }
            return sessionFilledList;
        }
        private void SaveCartToSession(string sessionName, List<int> sessionFilledList)
        {
            string movieListJSON = JsonSerializer.Serialize(sessionFilledList);
            HttpContext.Session.SetString(sessionName, movieListJSON);
        }
        #endregion
    }
}