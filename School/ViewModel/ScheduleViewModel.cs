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
    public class ScheduleViewModel : PropertyChangedBase
    {
        public AppContext AppContext { get; set; }
        private readonly SchoolEntities _schoolEnt;

        private Lesson _lesson;

        public Lesson Lesson
        {
            get => _lesson;
            set => SetProperty(ref _lesson, value);
        }

        private Person _person;

        public Person Person
        {
            get => _person;
            set => SetProperty(ref _person, value);
        }

        private LessonTime _lessonTime;

        public LessonTime LessonTime
        {
            get => _lessonTime;
            set => SetProperty(ref _lessonTime, value);
        }

        private Room _room;

        public Room Room
        {
            get => _room;
            set => SetProperty(ref _room, value);
        }

        private Class _class;

        public Class Class
        {
            get => _class;
            set
            {
                SetProperty(ref _class, value);
                if (value is not null
                    && CurrentDay is not null)
                    LessonsFiltered =
                        new ObservableCollection<Lesson>(Class.Lesson.Where(x => CurrentDay.Name.Equals(x.day)));
            }
        }

        private LessonName _lessonName;

        public LessonName LessonName
        {
            get => _lessonName;
            set => SetProperty(ref _lessonName, value);
        }

        private Days _currentDay;

        public Days CurrentDay
        {
            get => _currentDay;
            set => SetProperty(ref _currentDay, value);
        }

        private ObservableCollection<Lesson> _lessonsFiltered;

        public ObservableCollection<Lesson> LessonsFiltered
        {
            get => _lessonsFiltered;
            set => SetProperty(ref _lessonsFiltered, value);
        }

        public ScheduleViewModel(SchoolEntities schoolEnt, AppContext appContext, Lesson lesson, Days currentDay)
        {
            AppContext = appContext;
            _schoolEnt = schoolEnt;
            AddCommand = new LightCommand(AddMethod);
            Day1 = new LightCommand(Day1Method);
            Day2 = new LightCommand(Day2Method);
            Day3 = new LightCommand(Day3Method);
            Day4 = new LightCommand(Day4Method);
            Day5 = new LightCommand(Day5Method);
            Day6 = new LightCommand(Day6Method);
            Day7 = new LightCommand(Day7Method);
            SaveCommand = new LightCommand(SaveMethod);
            Lesson = lesson;
            CurrentDay = currentDay;
        }

        public ScheduleViewModel(SchoolEntities schoolEnt, AppContext appContext)
        {
            AppContext = appContext;
            _schoolEnt = schoolEnt;
            AddCommand = new LightCommand(AddMethod);
            OpenAdd = new LightCommand(OpenAddMethod);
            DeleteCommand = new LightCommand(DeleteMethod);
            UpdateOpenCommand = new LightCommand(UpdateOpenMethod);
            CurrentDay = AppContext.Days[0];
            Day1 = new LightCommand(Day1Method);
            Day2 = new LightCommand(Day2Method);
            Day3 = new LightCommand(Day3Method);
            Day4 = new LightCommand(Day4Method);
            Day5 = new LightCommand(Day5Method);
            Day6 = new LightCommand(Day6Method);
            Day7 = new LightCommand(Day7Method);
        }

        public ICommand AddCommand { get; }

        private void AddMethod(object obj)
        {
            _schoolEnt.Lesson.Add(new Lesson
            {
                roomId = Room.number,
                personId = Person.id,
                numberId = LessonTime.id,
                day = CurrentDay.Name,
                nameId = LessonName.id,
                classId = Class.id
            });
            _schoolEnt.SaveChanges();
            AppContext.Lessons = new ObservableCollection<Lesson>(_schoolEnt.Lesson);
            LessonsFiltered = new ObservableCollection<Lesson>(Class.Lesson.Where(x => CurrentDay.Name.Equals(x.day)));

        }

        public ICommand DeleteCommand { get; }

        private void DeleteMethod(object obj)
        {
            _schoolEnt.Lesson.Remove(Lesson);
            _schoolEnt.SaveChanges();
            AppContext.Lessons = new ObservableCollection<Lesson>(_schoolEnt.Lesson);
            LessonsFiltered = new ObservableCollection<Lesson>(Class.Lesson.Where(x => CurrentDay.Name.Equals(x.day)));
        }

        public ICommand OpenAdd { get; }

        private void OpenAddMethod(object obj)
        {
            var window = new ScheduleAdd();
            window.DataContext = new ScheduleViewModel(_schoolEnt, AppContext);
            window.ShowDialog();
        }

        public ICommand UpdateOpenCommand { get; }

        private void UpdateOpenMethod(object obj)
        {
            var window = new ScheduleUpdate();
            window.DataContext = new ScheduleViewModel(_schoolEnt, AppContext, Lesson, CurrentDay);
            window.ShowDialog();
        }

        public ICommand SaveCommand { get; }

        private void SaveMethod(object obj)
        {
            _schoolEnt.SaveChanges();
            Lesson.day = CurrentDay.Name;
            AppContext.Lessons = new ObservableCollection<Lesson>(_schoolEnt.Lesson);
            Class = Lesson.Class;
            LessonsFiltered = new ObservableCollection<Lesson>(Class.Lesson.Where(x => CurrentDay.Name.Equals(x.day)));
        }

        public ICommand Day1 { get; }

        private void Day1Method(object obj)
        {
            CurrentDay = AppContext.Days[0];
            if (Class is not null)
                LessonsFiltered =
                    new ObservableCollection<Lesson>(Class.Lesson.Where(x => CurrentDay.Name.Equals(x.day)));
        }

        public ICommand Day2 { get; }

        private void Day2Method(object obj)
        {
            CurrentDay = AppContext.Days[1];
            if (Class is not null)
                LessonsFiltered =
                    new ObservableCollection<Lesson>(Class.Lesson.Where(x => CurrentDay.Name.Equals(x.day)));
        }

        public ICommand Day3 { get; }

        private void Day3Method(object obj)
        {
            CurrentDay = AppContext.Days[2];
            if (Class is not null)
                LessonsFiltered = new ObservableCollection<Lesson>(Class.Lesson.Where(x => CurrentDay.Name.Equals(x.day)));
        }

        public ICommand Day4 { get; }

        private void Day4Method(object obj)
        {
            CurrentDay = AppContext.Days[3];
            if (Class is not null)
                LessonsFiltered =
                    new ObservableCollection<Lesson>(Class.Lesson.Where(x => CurrentDay.Name.Equals(x.day)));
        }

        public ICommand Day5 { get; }

        private void Day5Method(object obj)
        {
            CurrentDay = AppContext.Days[4];
            if (Class is not null)
                LessonsFiltered =
                    new ObservableCollection<Lesson>(Class.Lesson.Where(x => CurrentDay.Name.Equals(x.day)));
        }

        public ICommand Day6 { get; }

        private void Day6Method(object obj)
        {
            CurrentDay = AppContext.Days[5];
            if (Class is not null)
                LessonsFiltered =
                    new ObservableCollection<Lesson>(Class.Lesson.Where(x => CurrentDay.Name.Equals(x.day)));
        }

        public ICommand Day7 { get; }


        private void Day7Method(object obj)
        {
            CurrentDay = AppContext.Days[6];
            if (Class is not null)
                LessonsFiltered =
                    new ObservableCollection<Lesson>(Class.Lesson.Where(x => CurrentDay.Name.Equals(x.day)));
        }
    }
}