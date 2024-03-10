using AcademyGroupMVVM.ViewModels;
using IT_Company.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Company.ViewModel
{
    internal class EmployeeViewModel :ViewModelBase
    {
        Staff employee;
        public EmployeeViewModel(Staff emp) 
        {
            employee = emp;
        }
        public int EmpId
        {
            get { return employee.Id; }
            set { employee.Id = value; OnPropertyChanged(nameof(EmpId)); }
        }
        public string Name
        {
            get { return employee.Name; }
            set
            {
                employee.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Surname
        {
            get { return employee.Surname; }
            set
            {
                employee.Surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }
        public int Age
        {
            get { return employee.Age; }
            set
            {
                employee.Age = value;
                OnPropertyChanged(nameof(Age));
            }
        }
        public string JobTitle
        {
            get { return employee.JobPosition.Title; }
            set
            {
                employee.JobPosition.Title = value;
                OnPropertyChanged(nameof(JobTitle));
            }
        }
    }
}
