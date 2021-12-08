using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace p1Client.Class
{
    class MenuViewModel
    {
        public RelayCommand Connect { get; set; }

        public MenuViewModel()
        {
            Connect = new RelayCommand(Execute, CanExecute);
        }

        bool CanExecute(object obj)
        {
            return true;
        }

        void Execute(object obj)
        {
            try
            {
                MessageBox.Show(UserIp.User + "님 ㅎㅇ");
            }
            catch
            {
                MessageBox.Show("음.. 왜이럴까?");
            }
        }
    }
}
