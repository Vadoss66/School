using System.Collections.ObjectModel;
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
    public class PositionViewModel : PropertyChangedBase
    {
        public AppContext AppContext { get; set; }
        private readonly SchoolEntities _schoolEnt;

        private Position _position;

        public Position Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private decimal _salary;

        public decimal Salary
        {
            get => _salary;
            set => SetProperty(ref _salary, value);
        }
        public PositionViewModel(SchoolEntities schoolEnt, AppContext appContext)
        {
            AppContext = appContext;
            _schoolEnt = schoolEnt;
            AddCommand = new LightCommand(AddMethod);
            OpenAdd = new LightCommand(OpenAddMethod);
            UpdateOpenCommand = new LightCommand(UpdateOpenMethod);
            DeleteCommand = new LightCommand(DeleteMethod);
        }
        public PositionViewModel(SchoolEntities schoolEnt, AppContext appContext, Position position)
        {
            AppContext = appContext;
            _schoolEnt = schoolEnt;
            AddCommand = new LightCommand(AddMethod);
            Position = position;
            SaveCommand = new LightCommand(SaveMethod);
        }
        public ICommand AddCommand { get; }
        private void AddMethod(object obj)
        {
            _schoolEnt.Position.Add(new Position
            {
                positionName = Name,
                salary = Salary
            });
            _schoolEnt.SaveChanges();
            AppContext.Positions = new ObservableCollection<Position>(_schoolEnt.Position);
        }

        public ICommand UpdateOpenCommand { get; }

        private void UpdateOpenMethod(object obj)
        {
            var window = new PositionUpdate();
            window.DataContext = new PositionViewModel(_schoolEnt, AppContext, Position);
            window.ShowDialog();
        }

        public ICommand SaveCommand { get; }

        private void SaveMethod(object obj)
        {
            _schoolEnt.SaveChanges();
            AppContext.Positions = new ObservableCollection<Position>(_schoolEnt.Position);
        }

        public ICommand DeleteCommand { get; }

        private void DeleteMethod(object obj)
        {
            _schoolEnt.Position.Remove(Position);
            _schoolEnt.SaveChanges();
            AppContext.Positions = new ObservableCollection<Position>(_schoolEnt.Position);
        }

        public ICommand OpenAdd { get; }

        private void OpenAddMethod(object obj)
        {
            var window = new PositionAdd();
            window.DataContext = new PositionViewModel(_schoolEnt, AppContext);
            window.ShowDialog();
        }
    }
}