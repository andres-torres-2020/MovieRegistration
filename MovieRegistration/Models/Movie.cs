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

        [Required(ErrorMessage = "Please enter a title with 50 characters max")]
        public string Title { get; set; }
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
    }
}
