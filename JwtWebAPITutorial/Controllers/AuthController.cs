using JwtWebAPITutorial.Entities;
using JwtWebAPITutorial.Entities_SubModel.User;
using JwtWebAPITutorial.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Security.Cryptography;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace JwtWebAPITutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IUserRepository _userRepository;
        //private readonly IConfiguration _configuration;

        public AuthController(IUserRepository userRepository)
            //IConfiguration configuration
        {
            //_configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(CreateUserModel userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_userRepository.CreateUser(userCreate, out string errorMessage))
            {
                ModelState.AddModelError("", errorMessage);
                return StatusCode(422, ModelState);
            }

            //_userRepository.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            //user.Email = request.Email;
            //user.PasswordHash = passwordHash;
            //user.PasswordSalt = passwordSalt;
            //user.Role = "Admin";

            return Ok("Successfully Added");
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginModel request)
        {
            //if(user.Email != request.Email)
            //{
            //    return BadRequest("User not found");
            //}
            //if(!_userRepository.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            //{
            //    return BadRequest("Password incorect");
            //}
            //string token = _userRepository.CreateToken(request.UserName);
            if (request == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.Login(request, out string errorMessage))
            {
                ModelState.AddModelError("", errorMessage);
                return StatusCode(422, ModelState);
            }
            string token = _userRepository.CreateToken(request.UserName);
            return Ok(token);
        }

        [HttpGet("generatePDF")]
        public async Task<IActionResult> GeneratePDF(string contractNo)
        {
            var document = new PdfDocument();
            string HtmlContent = "<h1> Hợp đồng thuê </h1>";
            PdfGenerator.AddPdfPages(document, HtmlContent, PageSize.A4);
            byte[]? response = null;
            using(MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }
            string fileName = "Contract_ " + contractNo+ ".pdf";
            return File(response, "application/pdf", fileName);
        }

        //private string CreateToken(User user)
        //{
        //    List<Claim> claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Email, user.Email),
        //        new Claim(ClaimTypes.Role, user.Role)
        //    };
        //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
        //        _configuration.GetSection("AppSettings:Token").Value));

        //    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //    var token = new JwtSecurityToken(
        //        claims: claims,
        //        expires: DateTime.Now.AddDays(1),
        //        signingCredentials: cred);

        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        //    return jwt;   
        //}

        //private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    using(var hmac = new HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key;
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        //    }
        //}

        //private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        //{
        //    using (var hmac = new HMACSHA512(passwordSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        return computedHash.SequenceEqual(passwordHash);
        //    }
        //}


    }
}
