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
    public class RoomViewModel : PropertyChangedBase
    {
        public AppContext AppContext { get; set; }
        private readonly SchoolEntities _school;

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private Room _room;
        public Room Room
        {
            get => _room;
            set => SetProperty(ref _room, value);
        }
        public RoomViewModel(SchoolEntities school, AppContext appContext)
        {
            AppContext = appContext;
            _school = school;
            AppContext.Rooms = new ObservableCollection<Room>(_school.Room);
            AddCommand = new LightCommand(AddMethod);
            DeleteCommand = new LightCommand(DeleteMethod);
            OpenAdd = new LightCommand(OpenAddMethod);
            UpdateOpenCommand = new LightCommand(UpdateOpenMethod);
        }

        public RoomViewModel(SchoolEntities school, AppContext appContext, Room room)
        {
            AppContext = appContext;
            _school = school;
            AppContext.Rooms = new ObservableCollection<Room>(_school.Room);
            Room = room;
            SaveCommand = new LightCommand(SaveMethod);
        }

        public ICommand OpenAdd { get; }

        private void OpenAddMethod(object obj)
        {
            var window = new RoomAdd();
            window.DataContext = new RoomViewModel(_school, AppContext);
            window.ShowDialog();
        }

        public ICommand AddCommand { get; }
        private void AddMethod(object obj)
        {
            if (_school.Room.Select(x => x.number).Contains(int.Parse(Text))) return;
            _school.Room.Add(new Room { number = int.Parse(Text) });
            _school.SaveChanges();
            AppContext.Rooms = new ObservableCollection<Room>(_school.Room);
        }

        public ICommand DeleteCommand { get; }
        private void DeleteMethod(object obj)
        {
            if (!_school.Room.Select(x => x.number).Contains(Room.number)) return;
            _school.Room.Remove(Room);
            _school.SaveChanges();
            AppContext.Rooms = new ObservableCollection<Room>(_school.Room);
            if (AppContext.Rooms is { Count: > 0 })
                Room = AppContext.Rooms.FirstOrDefault();
        }
        public ICommand UpdateOpenCommand { get; }

        private void UpdateOpenMethod(object obj)
        {
            var window = new RoomUpdate();
            window.DataContext = new RoomViewModel(_school, AppContext, Room);
            window.ShowDialog();
        }

        public ICommand SaveCommand { get; }

        private void SaveMethod(object obj)
        {
            _school.SaveChanges();
            AppContext.Pupils = new ObservableCollection<Pupil>(_school.Pupil);
            AppContext.Classes = new ObservableCollection<Class>(_school.Class);
        }

    }
}