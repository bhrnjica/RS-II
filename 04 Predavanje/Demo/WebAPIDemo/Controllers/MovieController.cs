﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Model;
using WebAPIDemo.util;

namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        static List<Movie> m_Movies = generateMovies();

        private static List<Movie> generateMovies()
        {
            var lst = new List<Movie>()
            {
                new Movie(){ Id=1, Genre="Action", Price=12.50m, Rating="****", ReleaseDate= new DateTime(1984, 12,12), Title="Terminator "},
                new Movie(){ Id=2, Genre="Comedy", Price=11.50m, Rating="*****", ReleaseDate= new DateTime(1984, 12,12), Title="Glup glupllji "},
                new Movie(){ Id=3, Genre="Sci", Price=12.50m, Rating="****", ReleaseDate= new DateTime(1999, 12,12), Title="Matrix "},
                new Movie(){ Id=4, Genre="Domestic", Price=12.50m, Rating="****", ReleaseDate= new DateTime(1984, 12,12), Title="Ničija zemlja"},
            };

            return lst;
        }

        // GET: api/movie
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return m_Movies;
        }

        // GET: api/movie/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Movie> Get(int id)
        {
            var foundMoview = m_Movies.Where(x => x.Id == id).FirstOrDefault();
            if (foundMoview == null)
                return NotFound($"Moview ({id}) not found!");
            else
                return Ok(foundMoview);
        }

        // GET: api/movie/5
        //[HttpGet("{id}", Name = "Get")]
        //[HandleException]
        //public ActionResult<Movie> Get(int id)
        //{
        //    var foundMoview = m_Movies.Where(x => x.Id == id).FirstOrDefault();
        //    if (foundMoview == null)
        //        throw new Exception("This is exception!");

        //    return foundMoview;
        //}

        //// POST: api/movie
        //[HttpPost]
        //public void Post([FromBody] Movie movie)
        //{
        //    m_Movies.Add(movie);
        //}

        // POST: api/movie
        [HttpPost]
        public IActionResult Post([FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                m_Movies.Add(movie);

                return CreatedAtAction("Get", movie.Id);
            }
               
        }

        // PUT: api/movie/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Movie moview)
        {
           var m  =  m_Movies.Where(x => x.Id == id).FirstOrDefault();

            if (m != null)
            {
                m.Genre = moview.Genre;
                m.Price = moview.Price;
                m.Rating = moview.Rating;
                m.ReleaseDate = moview.ReleaseDate;
                m.Title = moview.Title;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            m_Movies.RemoveAll(x=>x.Id==id);
        }
    }
}
