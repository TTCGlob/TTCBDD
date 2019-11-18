using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTCBDD.ComponentHelper;
using TTCBDD.PageObject;

namespace TTCBDD.Public_Var
{
    public class PublicVar
    {
        
        public static string BaseUrl { get; set; }
        private static Employee _employee;
        public static Employee employee
        {
            get => _employee;
            set
            {
                createdEmployees = createdEmployees ?? new List<Employee>();
                createdEmployees.Add(value);
                _employee = value;
            }
        }
        public static List<Employee> createdEmployees;
        public static IEnumerable<Employee> employees { get; set; }
        public static string Response { get; set; }
        public static string RequestType { get; set; }
        public static string UniqueId { get; set; }

        public static string StepStatus { get; set; }

        public static string StepMsg { get; set; }
    }
}
