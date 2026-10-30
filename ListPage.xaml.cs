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
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        public ObservableCollection<Training> Training { get; set; } = new();
        public Training? SelectedTraining { get; set; }

        public ListPage()
        {
            InitializeComponent();
            DataContext = this;
            Training = JsonServer.Load();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FormPage(Training));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            SelectedTraining = lvTraining.SelectedItem as Training;

            if (SelectedTraining == null)
            {
                MessageBox.Show("Выберите тренировку для удаления.", "Внимание");
                return;
            }

            bool result = MessageBox.Show(
                "Вы действительно хотите удалить запись?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
                ) == MessageBoxResult.Yes;

            if (result)
            {
                Training.Remove(SelectedTraining);
            }

            JsonServer.Save(Training);
        }

        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            SelectedTraining = lvTraining.SelectedItem as Training;

            if (SelectedTraining == null)
            {
                MessageBox.Show("Выберите тренировку для редактирования.", "Внимание");
                return;
            }

            NavigationService.Navigate(new FormPage(Training, SelectedTraining));
        }
    }
}
