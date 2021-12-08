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
    /// Join.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Join : Page
    {
        MySQLManager manager = new MySQLManager();

        public Join()
        {
            Loaded += Join_Loaded;
        }

        private void Join_Loaded(object sender, RoutedEventArgs e)
        {
            // DB Connection
            manager.Initialize();
        }

        #region SignUp

        public class SignUpEventArgs : EventArgs
        {
            public bool isSignUp;
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUpEventArgs args = new SignUpEventArgs();

            string query = "INSERT INTO user(Id, Password)" + "VALUES('" + tbId.Text + "', '" + tbPw.Text + "')";
            manager.MySqlQueryExecuter(query);

            if (App.DataSaveResult == true)
            {
                args.isSignUp = true;
            }

            if (args.isSignUp == true)
            {
                MessageBox.Show("회원가입에 성공하셨습니다!");
                tbId.Text = string.Empty;
                tbPw.Text = string.Empty;
                NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("회원가입에 실패하셨습니다.");
            }
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }
    }
}
