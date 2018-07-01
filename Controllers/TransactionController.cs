using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BankAccounts.Models;

namespace Bank
{
    public class TransactionController : Controller
    {
        private BankAccountContext _context;
        public TransactionController(BankAccountContext context)
        {
            _context = context;
        }
        // GET: /account/id
        [HttpGet("accounts/{id}")]
        public IActionResult Home(int id)
        {
            // check if user id is in session, if false redirect to login 
            if(HttpContext.Session.GetInt32("userID") == null)
            {
                return RedirectToAction("Login", "User");
            }
            // check if route id matches session id to prevent logged user from accessing another users' account
            if(id != HttpContext.Session.GetInt32("userID"))
            {
                return RedirectToAction("Home", new{id = HttpContext.Session.GetInt32("userID")});
            }
            return View(_context.users.Include(u => u.transactions).Where(u => u.ID == id).SingleOrDefault());
        }
        // POST: /accounts/id/process
        [HttpPost("accounts/{id}/process")]
        public IActionResult Process(TransValidation model, int id)
        {
            if(ModelState.IsValid)
            {
                User user = _context.users.Where(u => u.ID == id).SingleOrDefault();
                // Check if amount is a withdrawl and if there are available funds to pull
                if(model.total < 0 && ((model.total * -1) > user.balance))
                {
                    TempData["errors"] = "Insufficient funds";
                    return RedirectToAction("Home", new{id = id});
                }
                else
                {
                    Transaction withdraw = new Transaction
                    {
                        total = model.total,
                        User = user
                    };
                    user.balance += model.total;
                    _context.transactions.Add(withdraw);
                    _context.SaveChanges();
                    return RedirectToAction("Home", new{id = id});
                }
                
            }
            foreach(var modelState in ModelState.Values)
            {
                foreach(var error in modelState.Errors)
                {
                    TempData["errors"] = error.ErrorMessage;
                }
            }
            return RedirectToAction("Home", new{id = id});
        }
        // GET: /logout
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            // clear session and redirect to login
            HttpContext.Session.Clear();
            return RedirectToAction("UserLogin", "User");
        }
    }
}