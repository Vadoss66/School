using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AdonisUI.ViewModels;
using School.Commands;
using School.Model;
using School.Static;

namespace School
{
    public enum Access
    {
        Student,
        Teacher,
        Accountant,
        Admin
    }
    public class AppContext : PropertyChangedBase
    {
        private readonly SchoolEntities _schoolEnt;
        private ObservableCollection<LessonName> _lessonNames;

        public ObservableCollection<LessonName> LessonNames
        {
            get => _lessonNames;
            set => SetProperty(ref _lessonNames, value);
        }

        private ObservableCollection<LessonTime> _lessonTimes;

        public ObservableCollection<LessonTime> LessonTimes
        {
            get => _lessonTimes;
            set => SetProperty(ref _lessonTimes, value);
        }

        private ObservableCollection<Lesson> _lessons;

        public ObservableCollection<Lesson> Lessons
        {
            get => _lessons;
            set => SetProperty(ref _lessons, value);
        }

        private ObservableCollection<Pupil> _pupils;

        public ObservableCollection<Pupil> Pupils
        {
            get => _pupils;
            set => SetProperty(ref _pupils, value);
        }

        private ObservableCollection<Person> _persons;

        public ObservableCollection<Person> Persons
        {
            get => _persons;
            set => SetProperty(ref _persons, value);
        }

        private ObservableCollection<Position> _positions;

        public ObservableCollection<Position> Positions
        {
            get => _positions;
            set => SetProperty(ref _positions, value);
        }

        private ObservableCollection<Room> _rooms;

        public ObservableCollection<Room> Rooms
        {
            get => _rooms;
            set => SetProperty(ref _rooms, value);
        }

        private ObservableCollection<Class> _classes;

        public ObservableCollection<Class> Classes
        {
            get => _classes;
            set => SetProperty(ref _classes, value);
        }

        private ObservableCollection<Days> _days;

        public ObservableCollection<Days> Days
        {
            get => _days;
            set => SetProperty(ref _days, value);

        }

        public Access Access { get; set; }

        public ObservableCollection<AccountModel> GetAllAccounts()
        {
            var accountModels = new ObservableCollection<AccountModel>();
            foreach (var item in _schoolEnt.Pupil)
            {
                accountModels.Add(new AccountModel(item.login, item.password));
            }

            foreach (var item in _schoolEnt.Person)
            {
                accountModels.Add(new AccountModel(item.login, item.password));
            }

            return accountModels;
        }

        private ObservableCollection<AccountModel> _accounts;
        public ObservableCollection<AccountModel> Accounts
        {
            get => _accounts;
            set => SetProperty(ref _accounts, value);
        }

        public AppContext(SchoolEntities schoolEnt)
        {
            _schoolEnt = schoolEnt;
            LessonNames = new ObservableCollection<LessonName>(schoolEnt.LessonName);
            LessonTimes = new ObservableCollection<LessonTime>(schoolEnt.LessonTime);
            Lessons = new ObservableCollection<Lesson>(schoolEnt.Lesson);
            Pupils = new ObservableCollection<Pupil>(schoolEnt.Pupil);
            Persons = new ObservableCollection<Person>(schoolEnt.Person);
            Rooms = new ObservableCollection<Room>(schoolEnt.Room);
            Classes = new ObservableCollection<Class>(schoolEnt.Class);
            Positions = new ObservableCollection<Position>(schoolEnt.Position);
            Days = new ObservableCollection<Days>();
            Accounts = GetAllAccounts();
            foreach (var day in AllDays.DaysList)
                Days.Add(new Days
                {
                    Name = day
                });

        }

        private void AccessAccount(string vacation)
        {
            if (vacation.ToLower().Contains("учитель") || vacation.ToLower().Contains("преподаватель"))
                Access = Access.Teacher;
            else if (vacation.ToLower().Contains("завуч") || vacation.ToLower().Contains("директор") || vacation.ToLower().Contains("заместитель"))
                Access = Access.Admin;
            else if (vacation.ToLower().Contains("бухгалтер"))
                Access = Access.Accountant;
            else
                Access = Access.Student;
        }
    }
}