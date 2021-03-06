﻿using EmplloyeeManagement.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmplloyeeManagement.ViewModels
{
	public class EmployeeCreateViewModel
	{

		[Required]
		[MaxLength(50, ErrorMessage = "Name cannot excees 50 character")]
		public string Name { get; set; }
		[Required]
		[RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Format")]
		public string Email { get; set; }
		[Required]
		public Dept? Department { get; set; }

		public IFormFile Photo { get; set; }
	}
}
