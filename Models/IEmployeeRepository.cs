using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmplloyeeManagement.Models
{
	public interface IEmployeeRepository
	{
		Employee GetEmployee(int id);

		IEnumerable<Employee> GetAllEmployee();

		Employee Add(Employee employee);

		Employee Update(Employee employeeInDB);

		Employee Delete(int id);
	}
}
