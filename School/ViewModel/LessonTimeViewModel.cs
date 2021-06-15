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
    public class LessonTimeViewModel : PropertyChangedBase
    {
        public AppContext AppContext { get; set; }
        private readonly SchoolEntities _schoolEnt;

        private LessonTime _lessonTime;
        public LessonTime LessonTime
        {
            get => _lessonTime;
            set => SetProperty(ref _lessonTime, value);
        }
        private string _startLesson;
        public string StartLesson
        {
            get => _startLesson;
            set => SetProperty(ref _startLesson, value);
        }
        private string _endLesson;
        public string EndLesson
        {
            get => _endLesson;
            set => SetProperty(ref _endLesson, value);
        }
        private int _numberLesson;
        public int NumberLesson
        {
            get => _numberLesson;
            set => SetProperty(ref _numberLesson, value);
        }

        public LessonTimeViewModel(SchoolEntities schoolEnt, AppContext appContext)
        {
            AppContext = appContext;
            _schoolEnt = schoolEnt;
            AddCommand = new LightCommand(AddMethod);
            OpenAdd = new LightCommand(OpenAddMethod);
            DeleteCommand = new LightCommand(DeleteMethod);
            UpdateOpenCommand = new LightCommand(UpdateOpenMethod);
        }

        public LessonTimeViewModel(SchoolEntities schoolEnt, AppContext appContext, LessonTime lessonTime)
        {
            AppContext = appContext;
            _schoolEnt = schoolEnt;
            AddCommand = new LightCommand(AddMethod);
            OpenAdd = new LightCommand(OpenAddMethod);
            DeleteCommand = new LightCommand(DeleteMethod);
            SaveCommand = new LightCommand(SaveMethod);
            LessonTime = lessonTime;
        }

        public ICommand AddCommand { get; }
        private void AddMethod(object obj)
        {
            var start = StartLesson.Split(':');
            var end = StartLesson.Split(':');
            _schoolEnt.LessonTime.Add(new LessonTime()
            {
                number = NumberLesson,
                start = new TimeSpan(int.Parse(start[0]), int.Parse(start[1]), 0),
                end = new TimeSpan(int.Parse(end[0]), int.Parse(end[1]), 0),
            });
            _schoolEnt.SaveChanges();
            AppContext.LessonTimes = new ObservableCollection<LessonTime>(_schoolEnt.LessonTime);
        }
        public ICommand DeleteCommand { get; }
        private void DeleteMethod(object obj)
        {
            _schoolEnt.LessonTime.Remove(LessonTime);
            _schoolEnt.SaveChanges();
            AppContext.LessonTimes = new ObservableCollection<LessonTime>(_schoolEnt.LessonTime);
        }
        public ICommand OpenAdd { get; }

        private void OpenAddMethod(object obj)
        {
            var window = new LessonTimeAdd();
            window.DataContext = new LessonTimeViewModel(_schoolEnt, AppContext);
            window.ShowDialog();
        }

        public ICommand UpdateOpenCommand { get; }

        private void UpdateOpenMethod(object obj)
        {
            var window = new LessonTimeUpdate();
            window.DataContext = new LessonTimeViewModel(_schoolEnt, AppContext, LessonTime);
            window.ShowDialog();
        }

        public ICommand SaveCommand { get; }

        private void SaveMethod(object obj)
        {
            _schoolEnt.SaveChanges();
            AppContext.LessonTimes = new ObservableCollection<LessonTime>(_schoolEnt.LessonTime);

        }
    }
}