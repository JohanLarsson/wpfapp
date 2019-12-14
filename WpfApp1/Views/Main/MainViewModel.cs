using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Views.Base;

namespace WpfApp1.Views.Main
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Employee> _employees;

        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set => Set(ref _employees, value);
        }

        public MainViewModel()
        {
            _employees = new ObservableCollection<Employee>();

            var employee = new Employee
            {
                Name = "example",
                LastName = "example"
            };

            Employees.Add(employee);
        }

        public ICommand EmployeeCommand => new RelayCommand(Test);

        private void Test()
        {
            MessageBox.Show("test");
        }
    }

}
