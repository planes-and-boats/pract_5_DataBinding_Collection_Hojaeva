using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pract_5_Hojaeva
{
    public class Exercise : INotifyPropertyChanged
    {
        private string name;
        private int sets;
        private int setDurationMinutes;

        public Exercise() { }

        public Exercise(string name, int sets, int setDurationMinutes)
        {
            Name = name;
            Sets = sets;
            SetDurationMinutes = setDurationMinutes;
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

        public int Sets
        {
            get => sets;
            set
            {
                sets = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ExerciseDurationMinutes));
            }
        }

        public int SetDurationMinutes
        {
            get => setDurationMinutes;
            set
            {
                setDurationMinutes = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ExerciseDurationMinutes));
            }
        }

        public int ExerciseDurationMinutes => (sets * setDurationMinutes);

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
