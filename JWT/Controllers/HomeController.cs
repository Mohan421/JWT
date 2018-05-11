using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    public class HomeController: Controller
    {
    [HttpGet, Authorize]
    public IEnumerable<BooksModel> Get()
    {
      var currentUser = HttpContext.User;
      var booksList = new BooksModel[] {
        new BooksModel { Author = "The Alchemist",Title = "Paulo Coelho" },
        new BooksModel { Author = "Dr. A. P. J. Abdul Kalam", Title = "Wings of fire" },
        new BooksModel { Author = "Shashi Tharoor", Title = "An era of darkness" },
        new BooksModel { Author = "Satya Nadella", Title = "Hit refresh" }
      };

      return booksList;
    }

    public class BooksModel
    {
      public string Author { get; set; }
      public string Title { get; set; }
    }
  }
    
}