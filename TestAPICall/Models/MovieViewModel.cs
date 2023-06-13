using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPICall.Models
{
    public class MovieViewModel
    {
        public List<MovieModel> MovieModelList { get; set; }
        public MovieModel MovieModelSingle { get; set; }
    }
}
