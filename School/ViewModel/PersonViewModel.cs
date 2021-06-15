using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AdonisUI.ViewModels;
using School.Commands;
using School.Model;
using School.View.Add;
using School.View.Update;

namespace School.ViewModel
{
    public class PersonViewModel : PropertyChangedBase
    {
        public AppContext AppContext { get; set; }
        private readonly SchoolEntities _schoolEnt;

        private string _lessonsPosition;

        public string LessonsPosition
        {
            get => _lessonsPosition;
            set => SetProperty(ref _lessonsPosition, value);
        }

        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _classesPerson;

        public string ClassesPerson
        {
            get => _classesPerson;
            set => SetProperty(ref _classesPerson, value);
        }

        private string _patronymic;

        public string Patronymic
        {
            get => _patronymic;
            set => SetProperty(ref _patronymic, value);
        }

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
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

        private string _phone;

        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        private Person _person;

        public Person Person
        {
            get => _person;
            set
            {
                SetProperty(ref _person, value);
                if (value is null) return;
                LessonsPosition = string.Join(",", value.Lesson);
                if (value.Class.Count < 1) return;
                ClassesPerson = string.Join(", ", value.Class.Select(item => item.classNumber + item.letter).ToList());
            }
        }

        private DateTime _birthday;

        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                SetProperty(ref _birthday, value);
            }
        }

        private string _passport;

        public string Passport
        {
            get => _passport;
            set => SetProperty(ref _passport, value);
        }

        private Room _room;

        public Room Room
        {
            get => _room;
            set => SetProperty(ref _room, value);
        }
        private Position _position;

        public Position Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }
        public PersonViewModel(SchoolEntities schoolEnt, AppContext appContext)
        {
            AppContext = appContext;
            _schoolEnt = schoolEnt;
            AppContext.Persons = new ObservableCollection<Person>(schoolEnt.Person);
            AppContext.Positions = new ObservableCollection<Position>(schoolEnt.Position);
            AddCommand = new LightCommand(AddMethod);
            DeleteCommand = new LightCommand(DeleteMethod);
            OpenAdd = new LightCommand(OpenAddMethod);
            Person = AppContext.Persons.FirstOrDefault();
            UpdateOpenCommand = new LightCommand(UpdateOpenMethod);
        }
        public PersonViewModel(SchoolEntities schoolEnt, AppContext appContext, Person person)
        {
            AppContext = appContext;
            _schoolEnt = schoolEnt;
            AppContext.Persons = new ObservableCollection<Person>(schoolEnt.Person);
            AppContext.Positions = new ObservableCollection<Position>(schoolEnt.Position);
            AddCommand = new LightCommand(AddMethod);
            DeleteCommand = new LightCommand(DeleteMethod);
            OpenAdd = new LightCommand(OpenAddMethod);
            Person = person;
            SaveCommand = new LightCommand(SaveMethod);
        }

        public ICommand AddCommand { get; }

        private void AddMethod(object obj)
        {
            if (Room is not null)
                _schoolEnt.Person.Add(new Person
                {
                    name = Name,
                    lastName = LastName,
                    address = Address,
                    passport = Passport,
                    positionId = Position.id,
                    login = Login,
                    password = Pass,
                    phone = Phone,
                    patronymic = Patronymic
                });
            else
                _schoolEnt.Person.Add(new Person
                {
                    name = Name,
                    lastName = LastName,
                    address = Address,
                    passport = Passport,
                    positionId = Position.id,
                    login = Login,
                    password = Pass,
                    patronymic = Patronymic,
                    phone = Phone
                });
            _schoolEnt.SaveChanges();
            AppContext.Persons = new ObservableCollection<Person>(_schoolEnt.Person);
        }

        public ICommand DeleteCommand { get; }

        private void DeleteMethod(object obj)
        {
            _schoolEnt.Person.Remove(Person);
            _schoolEnt.SaveChanges();
            AppContext.Persons = new ObservableCollection<Person>(_schoolEnt.Person);

        }

        public ICommand OpenAdd { get; }

        private void OpenAddMethod(object obj)
        {
            var window = new PersonAdd();
            window.DataContext = new PersonViewModel(_schoolEnt, AppContext);
            window.ShowDialog();
        }

        public ICommand UpdateOpenCommand { get; }

        private void UpdateOpenMethod(object obj)
        {
            var window = new PersonUpdate();
            window.DataContext = new PersonViewModel(_schoolEnt, AppContext, Person);
            window.ShowDialog();
        }

        public ICommand SaveCommand { get; }

        private void SaveMethod(object obj)
        {
            _schoolEnt.SaveChanges();
            AppContext.Persons = new ObservableCollection<Person>(_schoolEnt.Person);
        }
    }
}