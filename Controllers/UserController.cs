using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Newtonsoft.Json;
using RoshanDemo1.Data;
using RoshanDemo1.Models;
using System.Diagnostics.Metrics;


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
                Country = _dbContext.Country.ToList(),
                States = _dbContext.States.ToList(),
                Cities = _dbContext.Cities.ToList()
            };

            ViewBag.Fruits = new List<string> { "Orange", "Apple" };
            ViewBag.InterestsList = new List<string> { "Sports", "Music", "Movies" };
            
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

               
                if (model.Id == null)
                {
                    _dbContext.Users.Add(model);
                }
                else
                {
                    //Update related code
                    var existingUser = await _dbContext.Users.FindAsync(model.Id);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }
                    

                    existingUser.Username = model.Username;
                    existingUser.Gender = model.Gender;
                    existingUser.InterestsString = model.InterestsString;
                    existingUser.Hobbies = model.Hobbies;
                    existingUser.ImagePath = model.ImagePath;
                    existingUser.Password = model.Password;
                    existingUser.Fruits = model.Fruits;
                    existingUser.Address = model.Address;
                    existingUser.SelectedStateId = model.SelectedStateId;
                    existingUser.SelectedCityId = model.SelectedCityId;
                    existingUser.SelectedCountryId = model.SelectedCountryId;
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
            
            user.Country = _dbContext.Country.ToList();
            user.States = _dbContext.States.Where(c => c.CountryId == user.SelectedCountryId).ToList();
            user.Cities= _dbContext.Cities.Where(c => c.StateId == user.SelectedStateId).ToList();

            ViewBag.Fruits = new List<string> { "Orange", "Apple" };
            ViewBag.InterestsList = new List<string> { "Sports", "Music", "Movies" };

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

        [HttpGet]
        public async Task<IActionResult> GetStateByCoutryId (int CountryId)
        {
            var state = await _dbContext.States.Where(c => c.CountryId == CountryId).ToListAsync();
            return Json(state);
        }

    }
}
