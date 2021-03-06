﻿using EmplloyeeManagement.Models;
using EmplloyeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmplloyeeManagement.Controllers
{
	public class HomeController :Controller
	{
		private readonly IEmployeeRepository _employeeRepository;
		private readonly IHostingEnvironment hostingEnvironment;

		public HomeController(IEmployeeRepository employeeRepository,
			                    IHostingEnvironment hostingEnvironment)
		{
			_employeeRepository = employeeRepository;
			this.hostingEnvironment = hostingEnvironment;
		}
		public ViewResult Index()
		{
			var model = _employeeRepository.GetAllEmployee();
			return View(model);
		}

		public ViewResult Details(int? id)
		{
			Employee employee = _employeeRepository.GetEmployee(id.Value);
			if(employee==null)
			{
				Response.StatusCode = 404;
				return View("EmployeeNotFound",id.Value);
			}

			HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
			{
				Employee = employee,
				PageTitle = "Employee Details"
			};


			return View(homeDetailsViewModel);
		}

		[HttpGet]
		[Authorize]
		public ViewResult Create()
		{
			return View();
		}

		[HttpGet]
		[Authorize]
		public ViewResult Edit(int id)
		{
			Employee employee = _employeeRepository.GetEmployee(id);
			EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
			{
				Id = employee.Id,
				Name = employee.Name,
				Email = employee.Email,
				Department=employee.Department,
				ExistingPhotoPath =employee.PhotoPath
			};
			return View(employeeEditViewModel);
		}

		[HttpPost]
		[Authorize]
		public IActionResult Edit(EmployeeEditViewModel model)
		{
			if (ModelState.IsValid)
			{
				Employee employee = _employeeRepository.GetEmployee(model.Id);
				employee.Name = model.Name;
				employee.Email = model.Email;
				employee.Department = model.Department;

				if(model.Photo !=null)
				{
					//check to see is user select new photo will delete older photo.
					if(model.ExistingPhotoPath !=null)
					{
						string filePath=Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
						System.IO.File.Delete(filePath);
					}
					employee.PhotoPath = ProcessUploadeFile(model);
				}

				_employeeRepository.Update(employee);
				return RedirectToAction("index");
			}
			return View();

		}

		private string ProcessUploadeFile(EmployeeCreateViewModel model)
		{
			string uniqueFileName = null;
			if (model.Photo != null)
			{
				string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
				uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
				string filePath = Path.Combine(uploadFolder, uniqueFileName);
				using(var filestream= new FileStream(filePath, FileMode.Create))
				{
						model.Photo.CopyTo(filestream);
				}
				
			}

			return uniqueFileName;
		}

		[HttpPost]
		[Authorize]
		public IActionResult Create(EmployeeCreateViewModel model)
		{
			if(ModelState.IsValid)
			{
				string uniqueFileName = ProcessUploadeFile(model);
				

				Employee newEmployee = new Employee
				{
					Name=model.Name,
					Email=model.Email,
					Department=model.Department,
					PhotoPath=uniqueFileName
				};

				_employeeRepository.Add(newEmployee);
			   return RedirectToAction("details", new { id = newEmployee.Id });
			}
			return View();
			
		}
	}
} 
