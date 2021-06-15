using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
using System.Windows.Input;
using AdonisUI.ViewModels;
using School.Commands;
using School.Model;
using School.View;
using School.View.Add;
using School.View.Update;
using Pupil = School.Model.Pupil;

namespace School.ViewModel
{
    public class PupilViewModel : PropertyChangedBase
    {
        public AppContext AppContext { get; set; }
        private readonly SchoolEntities _schoolEnt;
        private Class _class;

        public Class Class
        {
            get => _class;
            set => SetProperty(ref _class, value);
        }

        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private string _phone;

        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        private string _address;

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private string _login;

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        private string _pass;

        public string Pass
        {
            get => _pass;
            set => SetProperty(ref _pass, value);
        }

        private string _patronymic;

        public string Patronymic
        {
            get => _patronymic;
            set => SetProperty(ref _patronymic, value);
        }

        private Pupil _pupil;

        public Pupil Pupil
        {
            get => _pupil;
            set
            {
                SetProperty(ref _pupil, value);
                if (value is null) return;
            }
        }

        public PupilViewModel(SchoolEntities schoolEnt, AppContext appContext)
        {
            AppContext = appContext;
            _schoolEnt = schoolEnt;
            AppContext.Classes = new ObservableCollection<Class>(_schoolEnt.Class);
            AppContext.Pupils = new ObservableCollection<Pupil>(schoolEnt.Pupil); AddCommand = new LightCommand(AddMethod);
            OpenAdd = new LightCommand(OpenAddMethod);
            DeleteCommand = new LightCommand(DeleteMethod);
            Pupil = AppContext.Pupils.FirstOrDefault();
            UpdateOpenCommand = new LightCommand(UpdateOpenMethod);
        }

        public PupilViewModel(SchoolEntities schoolEnt, AppContext appContext, Pupil pupil)
        {
            AppContext = appContext;
            _schoolEnt = schoolEnt;
            AppContext.Classes = new ObservableCollection<Class>(_schoolEnt.Class);
            AppContext.Pupils = new ObservableCollection<Pupil>(schoolEnt.Pupil); AddCommand = new LightCommand(AddMethod);
            OpenAdd = new LightCommand(OpenAddMethod);
            DeleteCommand = new LightCommand(DeleteMethod);
            Pupil = pupil;
            SaveCommand = new LightCommand(SaveMethod);
        }

        private DateTime _date;

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public ICommand AddCommand { get; }

        private void AddMethod(object obj)
        {
            _schoolEnt.Pupil.Add(new Pupil
            {
                name = Name,
                lastName = LastName,
                address = Address,
                login = Login,
                password = Pass,
                classId = Class.id,
                phone = Phone,
                birthday = Date,
                patronymic = Patronymic
            });
            _schoolEnt.SaveChanges();
            AppContext.Pupils = new ObservableCollection<Pupil>(_schoolEnt.Pupil);
        }

        public ICommand OpenAdd { get; }

        private void OpenAddMethod(object obj)
        {
            var window = new PupilAdd();
            window.DataContext = new PupilViewModel(_schoolEnt, AppContext);
            window.ShowDialog();
        }

        public ICommand DeleteCommand { get; }

        private void DeleteMethod(object obj)
        {
            _schoolEnt.Pupil.Remove(Pupil);
            _schoolEnt.SaveChanges();
            AppContext.Pupils = new ObservableCollection<Pupil>(_schoolEnt.Pupil);
        }
        public ICommand UpdateOpenCommand { get; }

        private void UpdateOpenMethod(object obj)
        {
            var window = new PupilUpdate();
            window.DataContext = new PupilViewModel(_schoolEnt, AppContext, Pupil);
            window.ShowDialog();
        }

        public ICommand SaveCommand { get; }

        private void SaveMethod(object obj)
        {
            _schoolEnt.SaveChanges();
            AppContext.Pupils = new ObservableCollection<Pupil>(_schoolEnt.Pupil);
            AppContext.Classes = new ObservableCollection<Class>(_schoolEnt.Class);
        }
    }
}