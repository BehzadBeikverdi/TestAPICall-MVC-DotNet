using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TestAPICall.Models;

namespace TestAPICall.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string Baseurl = "https://localhost:44305/api/movie/";
        /*     string Baseurl = "https://6110bf5bc38a0900171f0d86.mockapi.io/api/v2/";*/
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> MovieList()
        {
            List<MovieModel> movieModels = new List<MovieModel>();
            MovieViewModel mvm = new MovieViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("GetMovies");
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    movieModels = JsonConvert.DeserializeObject<List<MovieModel>>(Response);
                }

                mvm.MovieModelList = movieModels;

                return View(mvm);
            }
        }

        public async Task<IActionResult> GetMovieById(int id)
        {
            var movieModel = new MovieModel();
            MovieViewModel mvm = new MovieViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"/api/Movie/GetMovieById/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    movieModel = JsonConvert.DeserializeObject<MovieModel>(Response);
                }

                mvm.MovieModelSingle = movieModel;

                return View("EditMoviePage", mvm);
            }
        }

        public ActionResult CreateMovie(MovieViewModel movie)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var postTask = client.PostAsJsonAsync<MovieModel>("AddMovie", movie.MovieModelSingle);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("MovieList");
                }
            }

            return RedirectToAction("MovieList");
        }

       
        public ActionResult EditMovie(MovieViewModel movie, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var ss = id;
                var putTask = client.PostAsJsonAsync<MovieModel>($"EditMovie?id={id}", movie.MovieModelSingle);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("MovieList");
                }
            }

            return RedirectToAction("MovieList");
        }


        public ActionResult DeleteMovie(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var deleteTask = client.DeleteAsync($"DeleteMovieById/{id}");
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("MovieList");
                }
            }

            return RedirectToAction("MovieList");
        }

        public IActionResult CreateMoviePage()
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
