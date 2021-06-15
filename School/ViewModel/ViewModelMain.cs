using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AdonisUI.ViewModels;
using School.Commands;
using School.Model;

namespace School.ViewModel
{
    public class ViewModelMain : PropertyChangedBase
    {
        public RoomViewModel RoomViewModel { get; set; }
        private readonly SchoolEntities _school;
        public PositionViewModel PositionViewModel { get; set; }
        public PersonViewModel PersonViewModel { get; set; }
        public PupilViewModel PupilViewModel { get; set; }
        public ClassViewModel ClassViewModel { get; set; }
        public LessonTimeViewModel LessonTimeViewModel { get; set; }
        public LessonNameViewModel LessonNameViewModel { get; set; }
        public StartVM StartVm { get; set; }
        public AppContext AppContext { get; set; }
        public ScheduleViewModel ScheduleViewModel { get; set; }


        public ViewModelMain(SchoolEntities school, RoomViewModel roomViewModel, PositionViewModel positionViewModel,
            PersonViewModel personViewModel,
            PupilViewModel pupilViewModel, ClassViewModel classViewModel, LessonTimeViewModel lessonTimeViewModel,
            LessonNameViewModel lessonNameViewModel, StartVM startVm,
            AppContext appContext, ScheduleViewModel scheduleViewModel)
        {
            RoomViewModel = roomViewModel;
            _school = school;
            PositionViewModel = positionViewModel;
            PersonViewModel = personViewModel;
            ClassViewModel = classViewModel;
            LessonTimeViewModel = lessonTimeViewModel;
            LessonNameViewModel = lessonNameViewModel;
            StartVm = startVm;
            ScheduleViewModel = scheduleViewModel;
            AppContext = appContext;
            PupilViewModel = pupilViewModel;
        }

    }
}