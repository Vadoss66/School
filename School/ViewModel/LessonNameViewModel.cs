using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AdonisUI.ViewModels;
using School.Commands;
using School.Model;
using School.View.Add;
using School.View.Update;

namespace School.ViewModel
{
    public class LessonNameViewModel : PropertyChangedBase
    {
        public AppContext AppContext { get; set; }
        private readonly SchoolEntities _schoolEnt;

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        private LessonName _lessonName;
        public LessonName LessonName
        {
            get => _lessonName;
            set => SetProperty(ref _lessonName, value);
        }
        public LessonNameViewModel(SchoolEntities schoolEnt, AppContext appContext)
        {
            AppContext = appContext;
            _schoolEnt = schoolEnt;
            AddCommand = new LightCommand(AddMethod);
            OpenAdd = new LightCommand(OpenAddMethod);
            DeleteCommand = new LightCommand(DeleteMethod);
            UpdateOpenCommand = new LightCommand(UpdateOpenMethod);
        }
        public LessonNameViewModel(SchoolEntities schoolEnt, AppContext appContext, LessonName lessonName)
        {
            AppContext = appContext;
            _schoolEnt = schoolEnt;
            AddCommand = new LightCommand(AddMethod);
            OpenAdd = new LightCommand(OpenAddMethod);
            DeleteCommand = new LightCommand(DeleteMethod);
            LessonName = lessonName;
            SaveCommand = new LightCommand(SaveMethod);
        }
        public ICommand AddCommand { get; }
        private void AddMethod(object obj)
        {
            _schoolEnt.LessonName.Add(new LessonName()
            {
                name = Name
            });
            _schoolEnt.SaveChanges();
            AppContext.LessonNames = new ObservableCollection<LessonName>(_schoolEnt.LessonName);
        }
        public ICommand DeleteCommand { get; }
        private void DeleteMethod(object obj)
        {
            _schoolEnt.LessonName.Remove(LessonName);
            _schoolEnt.SaveChanges();
            AppContext.LessonNames = new ObservableCollection<LessonName>(_schoolEnt.LessonName);
        }

        public ICommand OpenAdd { get; }

        private void OpenAddMethod(object obj)
        {
            var window = new LessonNameAdd();
            window.DataContext = new LessonNameViewModel(_schoolEnt, AppContext);
            window.ShowDialog();
        }

        public ICommand UpdateOpenCommand { get; }

        private void UpdateOpenMethod(object obj)
        {
            var window = new LessonNameUpdate();
            window.DataContext = new LessonNameViewModel(_schoolEnt, AppContext, LessonName);
            window.ShowDialog();
        }

        public ICommand SaveCommand { get; }

        private void SaveMethod(object obj)
        {
            _schoolEnt.SaveChanges();
            AppContext.LessonNames = new ObservableCollection<LessonName>(_schoolEnt.LessonName);

        }
    }
}