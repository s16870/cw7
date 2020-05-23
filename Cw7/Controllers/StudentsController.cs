using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cw7.Models;
using Cw7.DAL;
using System.Data.SqlClient;
using Cw7.Services;
using Cw7.DTO.Requests;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace Cw7.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {

        private IStudentDbService _service;
        private IJwtAuthorizationService _jwtService;
        public IConfiguration _configuration { get; set; }
        public StudentsController(IStudentDbService service, IConfiguration configuration, IJwtAuthorizationService jwtService)
        {
            _service = service;
            _configuration = configuration;
            _jwtService = jwtService;
        }

        public string GetStudent()
        {
            return "Kowalski, Malewski, Andrzejewski";
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(string id)
        {
            Student resp = _service.GetStudent(id);
            if (resp != null)
            {
                return Ok(resp);
            }
            else
            {
                return NotFound("Nie znaleziono studenta");
            }
        }
        [HttpGet]
        public IActionResult GetStudents()
        {
            var resp = _service.GetStudents();
            if (resp != null)
            {
                return Ok(resp);
            }
            else
            {
                return NotFound("Brak studentów");
            }
        }
        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, Student student)
        {
            return Ok($"Aktualizacja dokończona");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok($"Usuwanie ukończone");
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login(LoginRequest request)
        {
            var token = _jwtService.LogIn(request);
            if (token != null)
            {
                return Ok(token);
            }
            else
            {
                return Unauthorized("Incorrect login or password!");
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("refreshToken")]
        public IActionResult RefreshToken(String request)
        {
            var token = _jwtService.RefreshToken(request);
            if (token != null)
            {
                return Ok(token);
            }
            else
            {
                return NotFound("Token does not exist!");
            }
        }

    }
}