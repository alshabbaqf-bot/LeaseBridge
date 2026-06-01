using LeaseBridge.API.Data;
using LeaseBridge.API.DTOs.Auth;
using LeaseBridge.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LeaseBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
        }
        private async Task<string> GenerateJwtToken(IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            // Add roles to claims
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            // Validate request
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if email already exists
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);

            if (existingUser != null)
                return BadRequest("Email already exists.");

            // Create roles if they do not exist
            if (!await _roleManager.RoleExistsAsync(dto.Role))
            {
                await _roleManager.CreateAsync(
                    new IdentityRole(dto.Role));
            }

            // Create Identity user
            var identityUser = new IdentityUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };

            // Save Identity user
            var result = await _userManager.CreateAsync(
                identityUser,
                dto.Password
            );

            // Check creation result
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Assign role
            await _userManager.AddToRoleAsync(
                identityUser,
                dto.Role
            );

            // Create AppUser record
            var appUser = new AppUser
            {
                IdentityUserId = identityUser.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                IsAvailable = dto.Role == "Staff"
            };

            // Save AppUser
            _context.AppUsers.Add(appUser);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            // Validate request
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Find user by email
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return Unauthorized("Invalid email or password.");

            // Check password
            var passwordValid = await _userManager.CheckPasswordAsync(
                user,
                dto.Password
            );

            if (!passwordValid)
                return Unauthorized("Invalid email or password.");

            // Generate JWT token
            var token = await GenerateJwtToken(user);

            // Get user roles
            var roles = await _userManager.GetRolesAsync(user);

            // Store the expiration date
            var expiry = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:DurationInMinutes"]));

            return Ok(new
            {
                Token = token,
                ExpiresAt = expiry,
                Email = user.Email,
                Roles = roles
            });
        }
    }
}