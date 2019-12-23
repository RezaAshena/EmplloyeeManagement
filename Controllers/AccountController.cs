using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmplloyeeManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmplloyeeManagement.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<IdentityUser> usermanager;
		private readonly SignInManager<IdentityUser> signInManager;

		public AccountController(UserManager<IdentityUser> usermanager,
																SignInManager<IdentityUser> signInManager)
		{
			this.usermanager = usermanager;
			this.signInManager = signInManager;
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new IdentityUser { UserName = model.Email, Email = model.Email };
				var result = await usermanager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					await signInManager.SignInAsync(user, isPersistent: false);
					return RedirectToAction("index", "home");

				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			return View(model);
		}
	}
}