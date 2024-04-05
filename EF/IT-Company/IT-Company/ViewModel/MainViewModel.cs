using AcademyGroupMVVM.ViewModels;
using AuthorsAndBooks_DB_.Commands;
using IT_Company.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;

namespace IT_Company.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {
        ObservableCollection<EmployeeViewModel> originalStaff { get; set; }
        public ObservableCollection<EmployeeViewModel> staff { get; set; }
        public ObservableCollection<JobPositionViewModel> jobs { get; set; }
        private ObservableCollection<string> entityTypes;
        static EmployeeViewModel? currentEmp;
        public MainViewModel(IQueryable<JobPosition> jbs, IQueryable<Staff> emps)
        {
            Jobs = new ObservableCollection<JobPositionViewModel>(jbs.Select(j => new JobPositionViewModel(j)));
            originalStaff = new ObservableCollection<EmployeeViewModel>(emps.Select(e => new EmployeeViewModel(e)));
            Staff = new ObservableCollection<EmployeeViewModel>(originalStaff);
            EntityTypes = new ObservableCollection<string>() { "Staff", "Jobs" };
        }

        public ObservableCollection<string> EntityTypes
        {
            get { return entityTypes; }
            set { entityTypes = value; OnPropertyChanged(nameof(EntityTypes)); }
        }

        public ObservableCollection<EmployeeViewModel> Staff
        {
            get { return staff; }
            set { staff = value; OnPropertyChanged(nameof(Staff)); }
        }

        public ObservableCollection<JobPositionViewModel> Jobs
        {
            get { return jobs; }
            set { jobs = value; OnPropertyChanged(nameof(Jobs)); }
        }

        private EmployeeViewModel selectedEmployee;
        public EmployeeViewModel SelectedEmployee
        {
            get { return selectedEmployee; }
            set { selectedEmployee = value; OnPropertyChanged(nameof(SelectedEmployee)); }
        }

        private int selectedJobIndex = -1;
        public int SelectedJobIndex
        {
            get { return selectedJobIndex; }
            set
            {
                selectedJobIndex = value;
                OnPropertyChanged(nameof(SelectedJobIndex));
            }
        }

        private string selectedEntityType = "Staff";
        public string SelectedEntityType
        {
            get { return selectedEntityType; }
            set {
                selectedEntityType = value;
                OnPropertyChanged(nameof(SelectedEntityType));
                IsAddJobPanel = "Collapsed";
                IsAddPanelVisible = "Collapsed";
                IsEditJobs = "Collapsed";
            }
        }

        private string searchQuery;
        public string SearchQuery
        {
            get { return searchQuery; }
            set { searchQuery = value; OnPropertyChanged(nameof(SearchQuery)); }
        }

        private string newEmployeeName;
        public string NewEmployeeName
        {
            get { return newEmployeeName; }
            set { newEmployeeName = value; OnPropertyChanged(nameof(NewEmployeeName)); }
        }

        private string newEmployeeSurname;
        public string NewEmployeeSurname
        {
            get { return newEmployeeSurname; }
            set { newEmployeeSurname = value; OnPropertyChanged(nameof(NewEmployeeSurname)); }
        }

        private string newEmployeeAge;
        public string NewEmployeeAge
        {
            get { return newEmployeeAge; }
            set { newEmployeeAge = value; OnPropertyChanged(nameof(NewEmployeeAge)); }
        }

        private string newJobPosition;
        public string NewJobPosition
        {
            get { return newJobPosition; }
            set { newJobPosition = value; OnPropertyChanged(nameof(NewJobPosition)); }
        }

        private string isAddPanelVisible = "Collapsed";
        public string IsAddPanelVisible
        {
            get { return isAddPanelVisible; }
            set
            {
                isAddPanelVisible = value;
                OnPropertyChanged(nameof(IsAddPanelVisible));
            }
        }
        private string isAddJobPanel = "Collapsed";
        public string IsAddJobPanel
        {
            get { return isAddJobPanel; }
            set
            {
                isAddJobPanel = value;
                OnPropertyChanged(nameof(IsAddJobPanel));
            }
        }
        private string isEditJobs = "Collapsed";
        public string IsEditJobs
        {
            get { return isEditJobs; }
            set
            {
                isEditJobs = value;
                OnPropertyChanged(nameof(IsEditJobs));
            }
        }


        private string myCommandParameter;
        public string MyCommandParameter
        {
            get { return myCommandParameter; }
            set
            {
                myCommandParameter = value;
                OnPropertyChanged(nameof(MyCommandParameter));
            }
        }


        #region Search

        private DelegateCommand searchCommand;
        public DelegateCommand SearchCommand
        {
            get {
                if (searchCommand == null)
                {
                    searchCommand = new DelegateCommand(param => ExecuteSearch(), param => CanSearch());
                }
                return searchCommand;
            }
        }
        void ExecuteSearch()
        {
            if (SearchQuery == ".")
            {
                Staff = new ObservableCollection<EmployeeViewModel>(originalStaff);
                return;
            }
            Staff.Clear();
            foreach (var employeeViewModel in originalStaff)
            {
                if (employeeViewModel.Name.ToLower().Contains(SearchQuery.ToLower()) ||
                    employeeViewModel.Surname.ToLower().Contains(SearchQuery.ToLower()) ||
                    employeeViewModel.Age.ToString().Contains(SearchQuery) ||
                    employeeViewModel.JobTitle.ToLower().Contains(SearchQuery.ToLower()))
                {
                    Staff.Add(employeeViewModel);
                }
            }
        }
        bool CanSearch()
        {
            return !string.IsNullOrEmpty(SearchQuery);
        }
        #endregion

        private DelegateCommand addoperationCommand;
        public DelegateCommand AddOperationCommand
        {
            get
            {
                if (addoperationCommand == null)
                {
                    addoperationCommand = new DelegateCommand(param => Operation("add"), param => CanExec());
                }
                return addoperationCommand;
            }
        }
        private DelegateCommand editoperationCommand;
        public DelegateCommand EditOperationCommand
        {
            get
            {
                if (editoperationCommand == null)
                {
                    editoperationCommand = new DelegateCommand(param => Operation("edit"), param => CanExecEdit());
                }
                return editoperationCommand;
            }
        }
        private DelegateCommand deleteOperationCommand;
        public DelegateCommand DeleteOperationCommand
        {
            get
            {
                if (deleteOperationCommand == null)
                {
                    deleteOperationCommand = new DelegateCommand(param => Delete(), param => CanExecEdit());
                }
                return deleteOperationCommand;
            }
        }
        bool CanExec()
        {
            return IsAddPanelVisible == "Collapsed";
        }
        bool CanExecEdit()
        {
            return IsAddPanelVisible == "Collapsed" && SelectedEmployee != null || SelectedEntityType == "Jobs";
        }

        void Operation(string state)
        {
            switch (SelectedEntityType)
            {
                case "Staff":
                    IsAddPanelVisible = "Visible";
                    IsAddJobPanel = "Collapsed";
                    switch (state)
                    {
                        case "add":
                            MyCommandParameter = "add";
                            break;
                        case "edit":
                            currentEmp = SelectedEmployee;
                            NewEmployeeName = currentEmp.Name;
                            NewEmployeeSurname = currentEmp.Surname;
                            NewEmployeeAge = currentEmp.Age.ToString();
                            SelectedJobIndex = Jobs.IndexOf(Jobs.FirstOrDefault(job => job.JobTitle == currentEmp.JobTitle)!);
                            MyCommandParameter = "edit";
                            break;
                    }
                    break;
                case "Jobs":
                    IsAddPanelVisible = "Collapsed";
                    IsAddJobPanel = "Visible";
                    switch (state)
                    {
                        case "add":
                            IsEditJobs = "Collapsed";
                            MyCommandParameter = "add";
                            break;
                        case "edit":
                            IsEditJobs = "Visible";
                            MyCommandParameter = "edit";
                            break;
                    }
                    break;
            }
        }
        void Delete()
        {
            if (SelectedEntityType == "Staff")
            {
                DeleteEmployee();
            }
            else
            {
                DeleteJP();
            }
        }
        void DeleteEmployee()
        {
            try
            {
                using (var db = new ITCompanyContext())
                {
                    currentEmp = SelectedEmployee;
                    var empTodelete = db.Employees.FirstOrDefault(emp => emp.Id == currentEmp!.EmpId);

                    DialogResult result = MessageBox.Show("Вы действительно желаете удалить сотрудника " + empTodelete.Name + " " + empTodelete.Surname +
                    " ?", "Delete employee", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Cancel)
                        return;

                    if (empTodelete != null)
                    {
                        db.Employees.RemoveRange(empTodelete);
                        db.SaveChanges();

                        originalStaff.Remove(currentEmp);
                        Staff = new ObservableCollection<EmployeeViewModel>(originalStaff);
                    }
                    MessageBox.Show("Employee deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void DeleteJP()
        {
            try
            {
                using (var db = new ITCompanyContext())
                {
                    currentEmp = SelectedEmployee;
                    var jpDelete = db.JobPositions.Where(j => j.Title == currentEmp.JobTitle).Single();

                    DialogResult result = MessageBox.Show("Вы действительно желаете удалить должность " + jpDelete.Title +
                    " ?", "Delete job position", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Cancel)
                        return;

                    if (jpDelete != null)
                    { 
                        db.JobPositions.RemoveRange(jpDelete);
                        db.SaveChanges();

                        var staffToDelete = originalStaff.Where(s => s.JobTitle == jpDelete.Title).ToList();
                        foreach(var staff in staffToDelete)
                        {
                            originalStaff.Remove(staff);

                        }
                        Staff = new ObservableCollection<EmployeeViewModel>(originalStaff);
                        Jobs = new ObservableCollection<JobPositionViewModel>(db.JobPositions.Select(s => new JobPositionViewModel(s)));
                    }
                    MessageBox.Show("Job position deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DelegateCommand operationStaff;
        public DelegateCommand OperationStaff
        {
            get
            {
                if (operationStaff == null)
                {
                    operationStaff = new DelegateCommand(param => SaveStaff(MyCommandParameter), param => CanAddStaff());
                }
                return operationStaff;
            }
        }
        bool CanAddStaff()
        {
            return !string.IsNullOrEmpty(NewEmployeeName) && !string.IsNullOrEmpty(NewEmployeeSurname)
                && NewEmployeeAge != null && SelectedJobIndex != -1;
        }
        void SaveStaff(string option)
        {
            try
            {
                switch (option)
                {
                    case "add":
                        AddEmploee();
                        break;
                    case "edit":
                        EditEmployee();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void AddEmploee()
        {
            try
            {
                using (var db = new ITCompanyContext())
                {
                    var currentPost = Jobs[SelectedJobIndex];
                    var query = db.JobPositions.Where(j => j.Title == currentPost.JobTitle).Single();
                    Staff emp = new Staff { Name = NewEmployeeName, Surname = NewEmployeeSurname, Age = int.Parse(NewEmployeeAge), JobPosition = query };
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    originalStaff.Add(new EmployeeViewModel(emp));
                    Staff = new ObservableCollection<EmployeeViewModel>(originalStaff);
                    IsAddPanelVisible = "Collapsed";
                    MessageBox.Show("Employee added");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void EditEmployee()
        {
            try
            {
                using (var db = new ITCompanyContext())
                {
                    var empToUpdate = db.Employees.FirstOrDefault(emp => emp.Id == currentEmp!.EmpId);
                    if (empToUpdate != null)
                    {
                        empToUpdate.Name = NewEmployeeName;
                        empToUpdate.Surname = NewEmployeeSurname;
                        empToUpdate.Age = int.Parse(NewEmployeeAge);
                        var currentPost = Jobs[SelectedJobIndex];
                        var query = db.JobPositions.FirstOrDefault(j => j.Title == currentPost.JobTitle);
                        empToUpdate.JobPosition = query;
                        db.Employees.Update(empToUpdate);

                        db.SaveChanges();
                    }
                    originalStaff = new ObservableCollection<EmployeeViewModel>(db.Employees.Select(e => new EmployeeViewModel(e)));
                    Staff = new ObservableCollection<EmployeeViewModel>(originalStaff);
                    IsAddPanelVisible = "Collapsed";
                    MessageBox.Show("Employee edited");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private DelegateCommand operationJobPostion;
        public DelegateCommand OperationJobPosition
        {
            get
            {
                if (operationJobPostion == null)
                {
                    operationJobPostion = new DelegateCommand(param => SaveJP(MyCommandParameter), param => CanJP());
                }
                return operationJobPostion;
            }
        }
        void SaveJobPosition()
        {
            try
            {
                using (var db = new ITCompanyContext())
                {
                    JobPosition jp = new JobPosition { Title = NewJobPosition };
                    db.JobPositions.Add(jp);
                    db.SaveChanges();
                    jobs.Add(new JobPositionViewModel(jp));
                    IsAddJobPanel = "Collapsed";
                    MessageBox.Show("Job position added");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SaveJP(string option)
        {
            try
            {
                switch (option)
                {
                    case "add":
                        SaveJobPosition();
                        break;
                    case "edit":
                        EditJobPosition();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        bool CanJP()
        {
            return !string.IsNullOrEmpty(NewJobPosition);
        }

        void EditJobPosition()
        {
            try
            {
                using (var db = new ITCompanyContext())
                {
                    if (SelectedJobIndex == -1) { MessageBox.Show("Select job to edit"); return; }
                    var currentPost = Jobs[SelectedJobIndex];
                    var query = db.JobPositions.FirstOrDefault(j => j.Title == currentPost.JobTitle);
                    query.Title = NewJobPosition;
                    db.JobPositions.Update(query);
                    db.SaveChanges();
                    Jobs[SelectedJobIndex] = new JobPositionViewModel(query);
                    originalStaff = new ObservableCollection<EmployeeViewModel>(db.Employees.Select(e => new EmployeeViewModel(e)));
                    Staff = new ObservableCollection<EmployeeViewModel>(originalStaff);
                    IsAddJobPanel = "Collapsed";
                    MessageBox.Show("Job position edited");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
