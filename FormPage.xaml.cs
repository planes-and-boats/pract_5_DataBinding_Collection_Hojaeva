using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pract_5_Hojaeva
{
    /// <summary>
    /// Логика взаимодействия для FormPage.xaml
    /// </summary>
    public partial class FormPage : Page
    {
        private Training _originTraining;
        private Training _training;
        private ObservableCollection<Training> _trainingList;

        public FormPage(ObservableCollection<Training> trainingList, Training? training = null)
        {
            InitializeComponent();
            _trainingList = trainingList;

            if (training != null)
            {
                _originTraining = training;
                _training = new Training
                {
                    ID = _originTraining.ID,
                    Name = _originTraining.Name,
                    Date = _originTraining.Date,
                    Type = _originTraining.Type,
                    Exercises = new ObservableCollection<Exercise>()
                };
                this.Title = "Редактирование";

                for (int i = 0; i < _originTraining.Exercises.Count; i++)
                    _training.Exercises.Add(_originTraining.Exercises[i]);
            }
            else
            {
                _training = new Training();
                _training.ID = _trainingList.Count + 1;
                this.Title = "Новая тренировка";
            }

            DataContext = _training;
        }

        private void btnAddExercise_Click(object sender, RoutedEventArgs e)
        {
            _training.Exercises.Add(new Exercise("упражнение", 0, 0));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_training.Name == string.Empty || _training.Type == string.Empty)
            {
                MessageBox.Show("Введите все данные об тренировке.", "Внимание");
                return;
            }

            if (_training.Exercises.Count == 0)
            {
                MessageBox.Show("Тренировка должна содержать хотя бы одно упражнение.", "Внимание");
                return;
            }

            if (_originTraining != null)
            {
                _originTraining.ID = _training.ID;
                _originTraining.Name = _training.Name;
                _originTraining.Date = _training.Date;
                _originTraining.Type = _training.Type;
                _originTraining.Exercises.Clear();
                for (int i = 0; i < _training.Exercises.Count; i++)
                    _originTraining.Exercises.Add(_training.Exercises[i]);
            }
            else
            {
                _trainingList.Add(_training);
            }

            JsonServer.Save(_trainingList);
            NavigationService.GoBack();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnDeleteExercise_Click(object sender, RoutedEventArgs e)
        {
            if (lvExercises.SelectedItem == null)
            {
                MessageBox.Show("Выберите упражнение для удаления.", "Внимание");
                return;
            }

            if (MessageBox.Show(
                 "Вы действительно хотите удалить упаржнение?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _training.Exercises.Remove((Exercise) lvExercises.SelectedItem);
            }
        }
    }
}
