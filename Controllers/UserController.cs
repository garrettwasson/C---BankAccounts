using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using BankAccounts.Models;

namespace Bank.Controllers
{
    public class UserController : Controller
    {
        private BankAccountContext _context;
        public UserController(BankAccountContext context)
        {
            _context = context;
        }
        // GET: / 
        [HttpGet("")]
        public IActionResult Home()
        {
            // check if user id is in session
            if(HttpContext.Session.GetInt32("userID") != null)
            {
                return RedirectToAction("Home", "Transaction", new{id = HttpContext.Session.GetInt32("userID")});
            }
            return View();
        }
        // POST /register/process
        [HttpPost("register/process")]
        public IActionResult Registration(UserRegister model)
        {
            if(ModelState.IsValid)
            {
                // Check for unique email 
                List<User> users = _context.users.Where(user => user.email == model.email).ToList();
                if(users.Count > 0)
                {
                    ModelState.AddModelError("email", "Email already exists");
                    return View("Home");
                }
                else 
                {
                    // Hash password and add to db
                    PasswordHasher<UserRegister> hasher = new PasswordHasher<UserRegister>();
                    string hashedPassword = hasher.HashPassword(model, model.password);
                    User user = new User
                    {
                        firstName = model.firstName,
                        lastName = model.lastName,
                        email = model.email,
                        password = hashedPassword
                    };
                    _context.Add(user);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("userID", (int)user.ID);
                    return RedirectToAction("Home", "Transaction", new{id = user.ID});
                }
            }
            return View("Home");
        }
        // GET: /login 
        [HttpGet("login")]
        public IActionResult UserLogin()
        {
            if(HttpContext.Session.GetInt32("userID") != null)
            {
                return RedirectToAction("Home", "Transaction", new{id = HttpContext.Session.GetInt32("userID")});
            }
            return View();
        }
        // POST: /login/process
        [HttpPost("login/process")]
        public IActionResult LoginProc(UserLogin model)
        {
            if(ModelState.IsValid)
            {
                // Check if user(s) is returned based on email input
                List<User> users = _context.users.Where(user => user.email == model.email).ToList();
                if(users.Count == 0)
                {
                    ModelState.AddModelError("password", "Invalid email/password");
                }
                else
                {
                    // Grab hashed password from db and match it against users input
                    PasswordHasher<UserLogin> hasher = new PasswordHasher<UserLogin>();
                    string hashedPassword = users[0].password;
                    PasswordVerificationResult result = hasher.VerifyHashedPassword(model, hashedPassword, model.password);
                    if(result == PasswordVerificationResult.Failed)
                    {
                        ModelState.AddModelError("password", "Invalid email/password");
                    }
                    else 
                    {
                        HttpContext.Session.SetInt32("userID", (int)users[0].ID);
                        return RedirectToAction("Home", "Transaction", new{id = users[0].ID});
                    }
                }
            }
            return View("Login");
        }
    }
}