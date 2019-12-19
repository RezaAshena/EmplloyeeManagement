using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmplloyeeManagement.Models
{
	public static class ModelBuilderExtentions
	{
		public static void Seed(this ModelBuilder  modelBuilder)
		{
			modelBuilder.Entity<Employee>().HasData(
				new Employee
				{
					Id = 1,
					Name = "Reza",
					Department = Dept.IT,
					Email = "Reza@Email.com"
				},
				new Employee
				{
					Id = 2,
					Name = "John",
					Department = Dept.HR,
					Email = "John@Email.com"
				}
				);
		}
	}
}
