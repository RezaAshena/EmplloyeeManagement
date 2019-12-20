﻿using EmplloyeeManagement.Models;
using EmplloyeeManagement.ViewModels;
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

		public ViewResult Details(int id)
		{
			HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
			{
				Employee = _employeeRepository.GetEmployee(id),
				PageTitle = "Employee Details"
			};


			return View(homeDetailsViewModel);
		}

		[HttpGet]
		public ViewResult Create()
		{
			return View();
		}

		[HttpGet]
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
		public IActionResult Create(EmployeeCreateViewModel model)
		{
			if(ModelState.IsValid)
			{
				string uniqueFileName = null;
				if(model.Photo !=null)
				{
				  string uploadFolder =	Path.Combine(hostingEnvironment.WebRootPath, "images");
					uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
				  string filePath =	Path.Combine(uploadFolder, uniqueFileName);
					model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
				}

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
