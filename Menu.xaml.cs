using p1Client.Class;
using System;
using System.Collections.Generic;
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

namespace p1Client
{
    /// <summary>
    /// Menu.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ChattingRoom.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Pacman p = new Pacman();
            p.Owner = Application.Current.MainWindow;
            p.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            p.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BirdGame b = new BirdGame();
            b.Owner = Application.Current.MainWindow;
            b.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            b.ShowDialog();
        }
    }
}
