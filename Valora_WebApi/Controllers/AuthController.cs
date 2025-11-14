using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Valora.DTOs.Auth;
using Valora.Models;
using Valora.Services;

namespace Valora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IJwtTokenService jwtTokenService,
            IMapper mapper,
            ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if user already exists
            var existingUser = await _userManager.FindByNameAsync(model.UserName);
            if (existingUser != null)
                return BadRequest(new { message = "Username already exists" });

            var existingEmail = await _userManager.FindByEmailAsync(model.Email);
            if (existingEmail != null)
                return BadRequest(new { message = "Email already exists" });

            // Map DTO to entity
            var user = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors.Select(e => e.Description) });

            // Ensure User role exists
            await EnsureRoleExistsAsync("User");
            
            // Add default User role
            await _userManager.AddToRoleAsync(user, "User");

            _logger.LogInformation("User {UserName} registered successfully", user.UserName);

            // Generate token
            var token = await _jwtTokenService.GenerateTokenAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var userDto = _mapper.Map<UserDTO>(user);
            userDto.Roles = roles;

            var response = new AuthResponseDTO
            {
                Token = token,
                ExpiresAt = _jwtTokenService.GetTokenExpiration(),
                User = userDto
            };

            return Ok(response);
        }

        /// <summary>
        /// Login - Auto-assigns Admin role if username is "admin"
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return Unauthorized(new { message = "Invalid credentials" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
                return Unauthorized(new { message = "Invalid credentials" });

            // Auto-assign Admin role if username is "admin"
            if (user.UserName?.Equals("admin", StringComparison.OrdinalIgnoreCase) == true)
            {
                await EnsureRoleExistsAsync("Admin");
                
                var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                if (!isAdmin)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    _logger.LogInformation("Admin role automatically assigned to user: {UserName}", user.UserName);
                }
            }

            // Generate JWT token with roles
            var token = await _jwtTokenService.GenerateTokenAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var userDto = _mapper.Map<UserDTO>(user);
            userDto.Roles = roles;

            var response = new AuthResponseDTO
            {
                Token = token,
                ExpiresAt = _jwtTokenService.GetTokenExpiration(),
                User = userDto
            };

            _logger.LogInformation("User {UserName} logged in successfully", user.UserName);

            return Ok(response);
        }

        /// <summary>
        /// Get current user information
        /// </summary>
        [HttpGet("me")]
        [Authorize]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            var roles = await _userManager.GetRolesAsync(user);
            var userDto = _mapper.Map<UserDTO>(user);
            userDto.Roles = roles;

            return Ok(userDto);
        }

        /// <summary>
        /// Logout
        /// </summary>
        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logged out successfully" });
        }

        /// <summary>
        /// Helper method to ensure role exists
        /// </summary>
        private async Task EnsureRoleExistsAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new ApplicationRole { Name = roleName });
                _logger.LogInformation("Role {RoleName} created automatically", roleName);
            }
        }
    }
}
