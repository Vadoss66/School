using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
using System.Windows.Input;
using AdonisUI.ViewModels;
using School.Commands;
using School.Model;
using School.View.Add;
using School.View.Update;

namespace School.ViewModel
{
    public class ClassViewModel : PropertyChangedBase
    {
        public AppContext AppContext { get; set; }
        private readonly SchoolEntities _schoolEnt;

        private Class _class;

        public Class Class
        {
            get => _class;
            set => SetProperty(ref _class, value);
        }

        private Pupil _pupil;

        public Pupil Pupil
        {
            get => _pupil;
            set => SetProperty(ref _pupil, value);
        }

        private Person _teacher;

        public Person Teacher
        {
            get => _teacher;
            set => SetProperty(ref _teacher, value);
        }

        private int _classNumber;

        public int ClassNumber
        {
            get => _classNumber;
            set => SetProperty(ref _classNumber, value);
        }

        private string _classLetter;

        public string ClassLetter
        {
            get => _classLetter;
            set => SetProperty(ref _classLetter, value);
        }

        public ClassViewModel(SchoolEntities schoolEnt, AppContext appContext)
        {
            AppContext = appContext;
            _schoolEnt = schoolEnt;
            AppContext.Classes = new ObservableCollection<Class>(schoolEnt.Class);
            AppContext.Persons = new ObservableCollection<Person>(schoolEnt.Person);
            Class = AppContext.Classes.FirstOrDefault();
            AddCommand = new LightCommand(AddMethod);
            DeletePupilOfClass = new LightCommand(DeletePupilOfClassMethod);
            DeleteClass = new LightCommand(DeleteClassMethod);
            OpenAdd = new LightCommand(OpenAddMethod);
        }

        public ClassViewModel(SchoolEntities schoolEnt, AppContext appContext, Class @class)
        {
            AppContext = appContext;
            _schoolEnt = schoolEnt;
            AppContext.Classes = new ObservableCollection<Class>(schoolEnt.Class);
            AppContext.Persons = new ObservableCollection<Person>(schoolEnt.Person);
            Class = @class;
            SaveCommand = new LightCommand(SaveMethod);
            UpdateOpenCommand = new LightCommand(UpdateOpenMethod);
        }

        public ICommand AddCommand { get; }

        private void AddMethod(object obj)
        {
            _schoolEnt.Class.Add(new Class() { letter = ClassLetter, classNumber = ClassNumber, classTeacherId = Teacher.id });
            _schoolEnt.SaveChanges();
            AppContext.Classes = new ObservableCollection<Class>(_schoolEnt.Class);
        }


        public ICommand DeletePupilOfClass { get; }

        private void DeletePupilOfClassMethod(object obj)
        {
            Class.Pupil.Remove(Pupil);
            _schoolEnt.SaveChanges();
            AppContext.Classes = new ObservableCollection<Class>(_schoolEnt.Class);
        }

        public ICommand DeleteClass { get; }

        private void DeleteClassMethod(object obj)
        {
            _schoolEnt.Class.Remove(Class);
            _schoolEnt.SaveChanges();
            AppContext.Classes = new ObservableCollection<Class>(_schoolEnt.Class);
        }

        public ICommand UpdateOpenCommand { get; }

        private void UpdateOpenMethod(object obj)
        {
            var window = new ClassUpdate();
            window.DataContext = new ClassViewModel(_schoolEnt, AppContext, Class);
            window.ShowDialog();
        }

        public ICommand SaveCommand { get; }

        private void SaveMethod(object obj)
        {
            _schoolEnt.SaveChanges();
            AppContext.Classes = new ObservableCollection<Class>(_schoolEnt.Class);

        }
        public ICommand OpenAdd { get; }

        private void OpenAddMethod(object obj)
        {
            var window = new ClassAdd();
            window.DataContext = new ClassViewModel(_schoolEnt, AppContext);
            window.ShowDialog();
        }
    }
}