using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using AutoMapper;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Infrastructure.Data;
using System.Dynamic;
using API.Helpers;
using API.Dto;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;

        ApplicationDbContext _context;
        public UsersController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet("GetUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> GetUsers()
        {
            var editUsersDto = new List<EditUserDto>();
            EditUserDto eudto = null;
            var users = _userManager.Users.ToList();

            foreach(AppUser u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);
                eudto = new EditUserDto()
                {
                    Id = u.Id,
                    Email = u.Email,
                    DisplayName = u.DisplayName,
                    Role = roles[0]
                };
                editUsersDto.Add(eudto);
            }


            var usersDto = _mapper.Map<IEnumerable<AppUser>, IEnumerable<EditUserDto>>(users);
            ApiResponse response = new ApiResponse(200, "GetUsers()", editUsersDto);
            return response;
        }

        [HttpGet("GetRoles")]
        [Authorize(Roles = "User")]
        public async Task<ApiResponse> GetRoles()
        {
            var roles =  _context.Roles.ToList();
            var rolesDto = _mapper.Map<IEnumerable<AppRole>, IEnumerable<RoleDto>>(roles);
            ApiResponse response = new ApiResponse(200, "GetRoles()", rolesDto);
            return response;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUserById(string id)
        {
            try
            {
                var user = (await _userManager.FindByIdAsync(id));
                if (user == null)
                    return NotFound();

                var userDto = _mapper.Map<AppUser, UserDto>(user);    

                return userDto;
            }
            catch (Exception e)
            {
                return NoContent();
            }
        }

        [HttpPost("AddUser")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> AddUser(UserDto userDto)
        {
            var userInDb = await _userManager.FindByEmailAsync(userDto.Email);
            if(userInDb != null)
            {
                return new ApiResponse(201, $"User {userInDb.Email} existst in DB", userInDb);
            }
            var user = new AppUser
            {
                DisplayName = userDto.DisplayName,
                Email = userDto.Email,
                UserName = userDto.Email
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                return new ApiResponse(402, $"Can not add user in DB", userDto);
            }
            else
            {
                string role = userDto.Role;
                await _userManager.AddToRoleAsync(user, role);
            }
            return new ApiResponse(200, $"User {user.Email} was successfull added in db", user);
        }

        [HttpPut("UpdateUser")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> UpdateUser(string id, string email, string displayName, string role )
        {
            var userInDb = await _userManager.FindByIdAsync(id);
            
            if(userInDb != null)
            {
                var userRoles = await _userManager.GetRolesAsync(userInDb);
                try{
                    userInDb.UserName = email;
                    userInDb.DisplayName = displayName;
                    userInDb.Email = email;
                    userInDb.NormalizedEmail = email.ToUpper();
                    await _userManager.UpdateAsync(userInDb);
                    await _userManager.RemoveFromRolesAsync(userInDb, userRoles);
                    await _userManager.AddToRoleAsync(userInDb, role);
                    return new ApiResponse(200, $"Update Successfull", userInDb);
                }catch(Exception e)
                {
                    return new ApiResponse(201, $"User {userInDb.Email} existst in DB", new {exc = e});
                }
            }
 
            return new ApiResponse(200, $"result");
        }
        
        [HttpDelete("DeleteUser")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> DeleteUser(string id)
        {
            var userInDb = await _userManager.FindByIdAsync(id);
            if(userInDb != null)
            {
                try{
                    await _userManager.DeleteAsync(userInDb);
                    return new ApiResponse(201, $"User {userInDb.Email} existst in DB", userInDb);
                }catch(Exception e)
                {
                    return new ApiResponse(201, $"User {userInDb.Email} existst in DB", new {exc = e});
                }
                
            }
            return new ApiResponse(201, $"delete succesfull", userInDb);
        }
    }
}