using AuthenticationPlugin;
using ImageUploader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Store.Models.Context;
using Store.Models.Entities;
using Store.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private StoreDbContext _storeDbContext;
        private IConfiguration _configuration;
        private readonly AuthService _auth;
        public UserController(IConfiguration configuration, StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
            _configuration = configuration;
            _auth = new AuthService(_configuration);
        }

        ///////////////////////////////////////////// Post Methods For Products /////////////////////////////////////////////////////

        // POST api/<UserController> --> Register form
        [HttpPost]
        public IActionResult Register([FromForm] User user)
        {
            var userWithSameEmail = _storeDbContext.Users.Where(u => u.Email == user.Email).SingleOrDefault();
            if (userWithSameEmail != null)
            {
                return BadRequest("User with same email already exists");
            }
            var guid = Guid.NewGuid();
            var filepath = Path.Combine("wwwroot", guid + ".jpg");
            var userObj = new User()
            {
                Name = user.Name,
                Phone = user.Phone,
                Email = user.Email,
                Role = "Users",
                Password = SecurePasswordHasherHelper.Hash(user.Password),
                ConfirmPassword = SecurePasswordHasherHelper.Hash(user.Password),
            };

            if (userObj.Images != null)
            {
                var filestream = new FileStream(filepath, FileMode.Create);
                user.Images.CopyTo(filestream);
            }

            userObj.ImageUrl = filepath.Remove(0, 7);
            _storeDbContext.Users.Add(userObj);
            _storeDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        //Post api --> Login
        [HttpPost]
        public IActionResult Login([FromBody] Login login)
        {
            var userEmail = _storeDbContext.Users.FirstOrDefault(u => u.Email == login.Email);
            if (userEmail == null)
            {
                return NotFound();
            }
            if (!SecurePasswordHasherHelper.Verify(login.Password, userEmail.Password))
            {
                return Unauthorized();
            }

            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Email, login.Email),
               new Claim(ClaimTypes.Email, login.Email),
               new Claim(ClaimTypes.Role,userEmail.Role)
            };
            var token = _auth.GenerateAccessToken(claims);
            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                expires_in = token.ExpiresIn,
                token_type = token.TokenType,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                user_id = userEmail.ID
            });
        }

        //Post api --> ChangePassword
        [HttpPost]
        [Authorize]
        public IActionResult ChangePassword([FromBody] ChangePassword changePassword)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = _storeDbContext.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null)
            {
                return NotFound();
            }

            if (!SecurePasswordHasherHelper.Verify(changePassword.OldPassword, user.Password))
            {
                return Unauthorized("Sorry you can't change the password");
            }
            user.Password = SecurePasswordHasherHelper.Hash(changePassword.NewPassword);
            _storeDbContext.SaveChanges();
            return Ok("Your password has been changed");
        }

        //Post api --> ChangePhoneNumber
        [HttpPost]
        [Authorize]
        public IActionResult ChangePhoneNumber([FromBody] ChangePhoneNumber changePhoneNumber)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = _storeDbContext.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null)
            {
                return NotFound();
            }
            user.Phone = changePhoneNumber.Phone;
            _storeDbContext.SaveChanges();
            return Ok("Your phone number has been updated");
        }

        ///////////////////////////////////////////// Put Methods For Products /////////////////////////////////////////////////////


        //Put Method to upload images
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult EditProfile(int id, [FromForm] ChangeUserProfile imageobj)
        {
            var user = _storeDbContext.Users.Find(id);
            if (user == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                var guid = Guid.NewGuid();
                var filepath = Path.Combine("wwwroot", guid + ".jpg");
                if (imageobj.Images != null)
                {
                    var filestream = new FileStream(filepath, FileMode.Create);
                    imageobj.Images.CopyTo(filestream);
                    user.ImageUrl = filepath.Remove(0, 7);
                }
                _storeDbContext.SaveChanges();
                return Ok("Record updated successfully");
            }
        }

        //Update Roles By Admin
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Roles(int id, [FromForm] ChangeUsersRoles roleobj)
        {
            var role = _storeDbContext.Users.Find(id);
            if (role == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                role.Role = roleobj.Role;
                _storeDbContext.SaveChanges();
                return Ok("User Role Update Successfully.");
            }
        }

        ///////////////////////////////////////////// Get Methods For Products /////////////////////////////////////////////////////

        //Get All Users By Admin
        [Authorize(Roles = "Admin,Editor")]
        [HttpGet]
        public IActionResult AllUsers()
        {
            var users = from u in _storeDbContext.Users
                        select new
                        {
                            Name = u.Name,
                            Phone = u.Phone,
                            Email = u.Email,
                            Role = u.Role
                        };
            return Ok(users);
        }

        //Get Users Detail by one
        [Authorize(Roles = "Admin,Editor")]
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _storeDbContext.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        ///////////////////////////////////////////// Delete Methods For Products /////////////////////////////////////////////////////

        //Delete User
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _storeDbContext.Users.Find(id);
            if (user == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                _storeDbContext.Users.Remove(user);
                _storeDbContext.SaveChanges();
                return Ok("User deleted");
            }
        }
    }
}

