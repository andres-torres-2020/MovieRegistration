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
    public class MovieCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MovieCart()
        {
            List<Movie> movieCart = GetSelectedMovies();
            return View(movieCart);
        }
        public IActionResult Receipt()
        {
            List<Movie> movieCart = GetSelectedMovies();
            MovieRegistration.Controllers.MovieRegistrationController.CanShowCart = false;
            HttpContext.Session.Clear(); // clear the movie cart
            return View(movieCart);
        }
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
        private List<Movie> GetSelectedMovies()
        {
            string sessionName = "SessionName";
            List<int> sessionCartIdList = GetCartFromSession(sessionName);
            List<Movie> movieCart = new List<Movie>();
            foreach (int id in sessionCartIdList)
            {
                foreach (Movie movie in MovieRegistrationController.Movies)
                {
                    if (id == movie.ID)
                    {
                        movieCart.Add(movie);
                        break;
                    }
                }
            }
            return movieCart;
        }
        private void SaveCartToSession(string sessionName, List<int> sessionFilledList, int newMovieToAdd)
        {
            string movieListJSON = JsonSerializer.Serialize(sessionFilledList);
            HttpContext.Session.SetString(sessionName, movieListJSON);
        }
    }
}