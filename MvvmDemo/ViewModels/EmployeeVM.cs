using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmDemo.Models;
using MvvmDemo.Commands;

using System.Collections.ObjectModel;

namespace MvvmDemo.ViewModels
{
    public class EmployeeVM : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        EmployeeService ObjEmployeeService;
        public EmployeeVM()
        {
            ObjEmployeeService = new EmployeeService();
            LoadData();
            CurrentEmployee = new Employee();
            _SaveCommand = new RelayCommand(Save);
        }

        #region DisplayOperation
        private ObservableCollection<Employee> _EmployeesList;

        public ObservableCollection<Employee> EmployeesList
        {
            get { return _EmployeesList; }
            set { _EmployeesList = value; OnPropertyChange("EmployeesList"); }
        }

        private void LoadData()
        {
            EmployeesList = new ObservableCollection<Employee>(ObjEmployeeService.GetAll());
        }
        #endregion 

        private Employee _CurrentEmployee;

        public Employee CurrentEmployee
        {
            get { return _CurrentEmployee; }
            set { _CurrentEmployee = value; OnPropertyChange("CurrentEmployee"); }
        }

        private string _Message;

        public string Message
        {
            get { return _Message; }
            set { _Message = value; OnPropertyChange("Message"); }
        }

        #region SaveOperation
        private RelayCommand _SaveCommand;

        public RelayCommand SaveCommand
        {
            get { return _SaveCommand; }
            
        }

        public void Save()
        {
            try
            {
                var IsSaved = ObjEmployeeService.Add(CurrentEmployee);
                LoadData();
                if (IsSaved)
                    Message = "Employee saved";
                else
                    Message = "Save operation failed";
            }
            catch(Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion 

        private RelayCommand _SearchCommand;

        public RelayCommand SearchCommand
        {
            get { return _SearchCommand; }
            
        }

        public void Search(int id)
        {
            try
            {
                var ObjEmployee = ObjEmployeeService.Search(CurrentEmployee.Id); 
                if(ObjEmployee!=null)
                {
                    CurrentEmployee = ObjEmployee;
                }else
                {
                    Message = "Employee Not Found";
                }

            }catch(Exception ex)
            {

            }
        }

    }
}
