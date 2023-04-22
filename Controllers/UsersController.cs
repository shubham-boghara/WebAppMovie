using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAppMovie.Data;
using WebAppMovie.Models;

namespace WebAppMovie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserAsyncAPIRepo _repo;
        private readonly IMapper _mapper;
        public UsersController(IUserAsyncAPIRepo repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return NotFound();
        }
    }
}
