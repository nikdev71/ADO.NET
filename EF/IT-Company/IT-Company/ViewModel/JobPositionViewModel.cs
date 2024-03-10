using AcademyGroupMVVM.ViewModels;
using IT_Company.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Company.ViewModel
{
    public class JobPositionViewModel : ViewModelBase
    {
        JobPosition post;
        public JobPositionViewModel(JobPosition _post)
        {
            post = _post;
        }

        public string JobTitle
        {
            get { return post.Title; }
            set
            {
                post.Title = value;
                OnPropertyChanged(nameof(JobTitle));
            }
        }
    }
}
