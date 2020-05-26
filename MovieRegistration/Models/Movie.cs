using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MovieRegistration.Models
{
    public class Movie
    {
        [Required(ErrorMessage = "Please enter an integer for ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter a title with 50 characters maximum")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a Run Time in minutes between 5 and 1200")]
        [Range(5, 1200, ErrorMessage = "Run Time must be between 5 and 1200 minutes")]
        public int RunTimeMins { get; set; }

        public string Genre { get; set; }

        [Required(ErrorMessage = "Please enter a date between 1880 and now")]
        [Range(1880, 3000, ErrorMessage = "Year must be between 1880 and the current year")]
        public int Year { get; set; }
        public string Actors { get; set; }
        public string Directors { get; set; }
        //public List<string> Actors { get; set; } = new List<string>();
        //public List<string> Directors { get; set; } = new List<string>();
        public Movie()
        {

        }
        public Movie(int ID, string Title, int RunTimeMins, string Genre, int Year, string Actors, string Directors)
        {
            this.ID = ID;
            this.Title = Title;
            this.RunTimeMins = RunTimeMins;
            this.Genre = Genre;
            this.Year = Year;
            this.Actors = Actors;
            this.Directors = Directors;
        }
    }
}
