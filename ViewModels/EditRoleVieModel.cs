using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmplloyeeManagement.ViewModels
{
    public class EditRoleVieModel
    {
        public EditRoleVieModel()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }

        [Required(ErrorMessage = "Role Name is required")]
        public String RoleName { get; set; }

        public List<string> Users { get; set; }

    }
}
