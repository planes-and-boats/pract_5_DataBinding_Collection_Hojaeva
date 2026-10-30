using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pract_5_Hojaeva
{
    public class Training : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private DateTime date;
        private string type;
        private ObservableCollection<Exercise> exercises;

        public Training()
        {
            Date = DateTime.Now;
            Exercises = new ObservableCollection<Exercise>();
            Exercises.CollectionChanged += ExercisesCollectionChanged;
        }

        public Training(int id, string name, DateTime date, string type,
            ObservableCollection<Exercise> exerxises)
        {
            ID = id;
            Name = name;
            Date = date;
            Type = type;
            Exercises = exerxises;
        }

        public int ID
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

        public string Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Exercise> Exercises
        {
            get => exercises;
            set
            {
                if (exercises != null)
                {
                    exercises.CollectionChanged -= ExercisesCollectionChanged;
                    foreach (var exercise in exercises)
                        exercise.PropertyChanged -= ExercisesItemChanged;
                }
                exercises = value;
                if (exercises != null)
                {
                    OnPropertyChanged();
                    exercises.CollectionChanged += ExercisesCollectionChanged;
                    foreach (var exercise in exercises)
                        exercise.PropertyChanged += ExercisesItemChanged;
                }
                OnPropertyChanged(nameof(WorkoutDurationMinutes));
            }
        }

        private void ExercisesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach(INotifyPropertyChanged item in e.NewItems)
                {
                    if (item != null)
                        item.PropertyChanged += ExercisesItemChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    if (item != null)
                        item.PropertyChanged -= ExercisesItemChanged;
                }
            }

            OnPropertyChanged(nameof (WorkoutDurationMinutes));
        }

        private void ExercisesItemChanged(object sender, PropertyChangedEventArgs e)
        {
            var changedItem = sender as Exercise;

            if (e.PropertyName == nameof(Exercise.ExerciseDurationMinutes))
            {
                OnPropertyChanged(nameof(WorkoutDurationMinutes));
            }
        }

        public int WorkoutDurationMinutes => exercises.Sum(e => e.ExerciseDurationMinutes);


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string ? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
