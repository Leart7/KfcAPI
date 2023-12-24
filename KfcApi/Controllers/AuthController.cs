using KfcApi.DTOs;
using KfcApi.Models;
using KfcApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KfcApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ITokenRepository tokenRepository, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenRepository = tokenRepository;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
        {
            if (!await _roleManager.RoleExistsAsync("user"))
            {
                var userRole = new IdentityRole("user");
                await _roleManager.CreateAsync(userRole);
            }

            var newUser = new ApplicationUser
            {
                Email = registerRequestDto.Email,
                UserName = registerRequestDto.Email,
                FirstName = registerRequestDto.FirstName,
                
            };

            var identityResult = await _userManager.CreateAsync(newUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "user");

                return Ok(newUser);
            }

            return BadRequest($"Registration failed: {string.Join(", ", identityResult.Errors.Select(e => e.Description))}");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);

            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

                if (checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                            UserId = user.Id,
                            Email = user.Email,
                            FirstName = (user as ApplicationUser)?.FirstName,
                            LastName = (user as ApplicationUser)?.LastName,
                            PhoneNumber = user.PhoneNumber,
                        };

                        return Ok(response);
                    }
                }
            }

            return BadRequest("Login Failed");
        }

        [HttpGet]
        [Route("{email}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user == null)
            {
                return NotFound();
            }

            var response = new
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = (user as ApplicationUser)?.FirstName,
                LastName = (user as ApplicationUser)?.LastName,
                PhoneNumber = user.PhoneNumber,
                ImageUrl = (user as ApplicationUser)?.ImageUrl,
            };


            return Ok(response);
        }

        [HttpPut]
        [Route("{userId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateUser([FromRoute] string userId, [FromForm] UpdateUserRequestDto updateUserRequestDto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            // Update user properties
            user.Email = updateUserRequestDto.Email;
            user.UserName = updateUserRequestDto.Email;
            user.PhoneNumber = updateUserRequestDto.PhoneNumber;

            if (user is ApplicationUser applicationUser)
            {
                applicationUser.FirstName = updateUserRequestDto.FirstName;
                applicationUser.LastName = updateUserRequestDto.LastName;
            }

            if (updateUserRequestDto.Image != null && user is ApplicationUser applicationUser1)
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "UserImages");
                string uniqueFileName = $"{Guid.NewGuid().ToString()}_{updateUserRequestDto.Image.FileName}";

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    updateUserRequestDto.Image.CopyTo(fileStream);
                }

                var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}";
                applicationUser1.ImageUrl = $"{baseUrl}/UserImages/{uniqueFileName}";
            }

            // Update password if provided
            if (!string.IsNullOrEmpty(updateUserRequestDto.NewPassword))
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, updateUserRequestDto.Password);
                if (checkPasswordResult)
                {
                    await _userManager.ChangePasswordAsync(user, updateUserRequestDto.Password, updateUserRequestDto.NewPassword);
                }

                if (!checkPasswordResult)
                {
                    return BadRequest();
                }
            }

            var updateResult = await _userManager.UpdateAsync(user);

            if (updateResult.Succeeded)
            {
                var updatedUserResponse = new UpdatedUserResponseDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = (user as ApplicationUser)?.FirstName,
                    LastName = (user as ApplicationUser)?.LastName,
                    PhoneNumber = user.PhoneNumber,
                    ImageUrl = (user as ApplicationUser)?.ImageUrl,
                };

                return Ok(updatedUserResponse);
            }

            return BadRequest();
        }




    }
}
