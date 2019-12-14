namespace WpfApp1.Models
{
    using System.Windows;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public class Employee
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public ICommand EmployeeCommand { get; } = new RelayCommand(Test);

        private static void Test()
        {
            MessageBox.Show("test");
        }
    }
}
