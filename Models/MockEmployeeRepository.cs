using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmplloyeeManagement.Models
{
	public class MockEmployeeRepository : IEmployeeRepository
	{
		private List<Employee> _employeeList;

		public MockEmployeeRepository()
		{
			_employeeList = new List<Employee>()
			{
				new Employee(){Id=1,Name="Mary",Department="HR",Email="Mary@Email.com"},
				new Employee(){Id=2,Name="John",Department="HR",Email="John@Email.com"},
				new Employee(){Id=3,Name="Sam",Department="HR",Email="Sam@Email.com"}
			};
		}

		public Employee GetEmployee(int Id)
		{
			return _employeeList.FirstOrDefault(e => e.Id == Id);
		}
	}
}
