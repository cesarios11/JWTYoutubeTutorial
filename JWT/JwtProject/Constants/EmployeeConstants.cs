using JwtProject.Models;

namespace JwtProject.Constants
{
    public class EmployeeConstants
    {
        public static List<EmployeeModel> Employees = new List<EmployeeModel>()
        {
            new EmployeeModel(){FirstName = "Tomas", LastName= "Rios", Email = "tomasito11@gmail.com"  },
            new EmployeeModel(){FirstName = "Gabriela", LastName= "Rios", Email = "gaby11@gmail.com"  },
            new EmployeeModel(){FirstName = "Mariana", LastName= "Diaz", Email = "marianita11@gmail.com"  }
        };
    }
}
