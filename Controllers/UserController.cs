using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using RoshanDemo1.Data;
using RoshanDemo1.Models;

namespace RoshanDemo1.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var users = _dbContext.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new UserModel
            {
                States = _dbContext.States.ToList(),
                Cities = _dbContext.Cities.ToList(),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //File related code we can get using and radom filename  
                #region file realted code
                if (model.ProfileImage != null)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "image");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.ProfileImage?.CopyToAsync(fileStream);
                    }
                    model.ImagePath = $"Image/{uniqueFileName}";
                }
                #endregion

                //Here create ralated code if Id=0 
                if (model.Id == 0 || model.Id == null)
                {
                    _dbContext.Users.Add(new UserModel
                    {
                        Username = model.Username,
                        Gender = model.Gender,
                        InterestsString = model.InterestsString,
                        ImagePath = model.ImagePath,
                        Password = model.Password,
                        Fruits = model.Fruits
                    });
                }
                else
                {
                    //Update related code
                    var existingUser = await _dbContext.Users.FindAsync(model.Id);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    //Here we can also use autoapper below code short

                    existingUser.Username = model.Username;
                    existingUser.Gender = model.Gender;
                    existingUser.InterestsString = model.InterestsString;
                    existingUser.ImagePath = model.ImagePath;
                    existingUser.Password = model.Password;
                    existingUser.Fruits = model.Fruits;
                }
                await _dbContext.SaveChangesAsync();

                return Ok(model);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex);
            }
        }

        // Edit related code 
        public IActionResult Edit(int id)
        {
            var user = _dbContext.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View("Create", user);
        }

        // Delete related code 
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCitiesByStateId(int stateId)
        {
            var cities = await _dbContext.Cities.Where(c => c.StateId == stateId).ToListAsync();
            return Json(cities);
        }

    }
}
