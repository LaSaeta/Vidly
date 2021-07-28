using AutoMapper;
using System;
using System.Linq;
using System.Web.Http;
using Vidly.Data;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class MoviesController : ApiController
    {
        private readonly DataContext _context;
        public MoviesController()
        {
            _context = new DataContext();
        }

        //GET /api/movies
        public IHttpActionResult GetMovies()
        {
            return Ok(_context.Movies.ToList().Select(Mapper.Map<Movie, MovieDTO>));
        }

        //Get /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDTO>(movie));
        }

        //POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDTO, Movie>(movieDTO);

            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDTO.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movieDTO.Id), movieDTO);
        }

        //PUT /api/movies/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(MovieDTO movieDTO, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieData = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieData == null)
                return NotFound();

            Mapper.Map(movieDTO, movieData);

            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/movies
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return Ok();
        }
    }
}