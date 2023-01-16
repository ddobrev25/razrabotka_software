using Microsoft.AspNetCore.Mvc;
using dani.Models.DB;
using dani.Models.Request;
using dani.Models.Response;

namespace dani.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private DemoDbContext _dbContext;
        public UserController(DemoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<UserGetResponse>> GetAll()
        {
            var response = _dbContext.Users.Select(x => new UserGetResponse
            {
                Age = x.Age,
                City = x.City,
                Email = x.Email,
                FirstName = x.FirstName,
                Id = x.Id,
                LastName = x.LastName,
                Password = x.Password,
                Username = x.Username
            }).ToList();

            return Ok(response);
        }
        [HttpGet]
        public ActionResult<UserGetResponse> GetById(int id)
        {
            var user = _dbContext.Users
                .Where(x => x.Id == id)
                .FirstOrDefault();
            if(user == null)
            {
                return BadRequest("Invalid id");
            }
            UserGetResponse response = new UserGetResponse
            {
                Age = user.Age,
                City = user.City,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                Password = user.Password,
                Username = user.Username
            };

            return Ok(response);
        }
        [HttpPost]
        public ActionResult Register(UserRegisterRequest request)
        {
            Models.DB.User user = new Models.DB.User()
            {
                Username = request.Username,
                Password = request.Password,
                Age = request.Age,
                City = request.City,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            if(_dbContext.Users.Any(x => x.Username == user.Username))
            {
                return BadRequest("Username duplicate");
            }

            if(user.Password.Length < 8)
            {
                return BadRequest("Weak password");
            }

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return Ok();
        }


        [HttpPost]
        public ActionResult Login(UserLoginRequest request)
        {
            var user = _dbContext.Users
                .Where(x => x.Username == request.Username)
                .FirstOrDefault();

            if(user == null)
            {
                return BadRequest("Incorrect credentials");
            }

            if(user.Password == request.Password)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Incorrect credentials");
            }
        }
        [HttpPut]
        public ActionResult Update(UserUpdateRequest request)
        {
            var userForUpdate = _dbContext.Users
                .Where(x => x.Id == request.Id)
                .FirstOrDefault();

            if(userForUpdate == null)
            {
                return BadRequest("Invalid Id");
            }
            if(request.FirstName == string.Empty)
            {
                return BadRequest("First name can not be a empty string");
            }
            if (request.Age < 0)
            {
                return BadRequest("...");
            }
            if(request.Username == string.Empty 
                || request.Username.Count() < 2)
            {
                return BadRequest("Invalid username");
            }
            userForUpdate.FirstName = request.FirstName;
            userForUpdate.LastName = request.LastName;
            userForUpdate.Email = request.Email;
            userForUpdate.City = request.City;
            userForUpdate.Age = request.Age;
            userForUpdate.Password = request.Password;
            userForUpdate.Username = request.Username;

            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var userForDelete = _dbContext.Users
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if(userForDelete is null)
            {
                return BadRequest("Invalid id");
            }

            _dbContext.Users.Remove(userForDelete);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
