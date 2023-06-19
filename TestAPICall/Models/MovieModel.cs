using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPICall.Models
{
    public class MovieModel
    {
        public int Id { get; set;  }
        public string MovieName { get; set; }
        public string MovieIMDB { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string MovieDuration { get; set; }
        public string MovieDirector { get; set; }
        public string MovieActors { get; set; }
        public string MovieDescription { get; set; }
    }
}
