using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1Client.Class
{
    class ChattingRoomViewModelBase : INotifyPropertyChanged
    {
        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        string _user;
        public string User
        {
            get { return _user; }
            set
            {
                if (_user == value)
                    return;
                _user = value;
                OnPropertyChanged("User");
            }
        }

        string _user2;
        public string User2
        {
            get { return _user2; }
            set
            {
                if (_user2 == value)
                    return;
                _user2 = value;
                OnPropertyChanged("User2");
            }
        }

        string _userImg1;
        public string UserImg1
        {
            get { return _userImg1; }
            set
            {
                if (_userImg1 == value)
                    return;
                _userImg1 = value;
                OnPropertyChanged("UserImg1");
            }
        }

        string _userImg2;
        public string UserImg2
        {
            get { return _userImg2; }
            set
            {
                if (_userImg2 == value)
                    return;
                _userImg2 = value;
                OnPropertyChanged("UserImg2");
            }
        }

        string _sendBox;
        public string SendBox
        {
            get
            {
                return _sendBox;
            }
            set
            {
                if (_sendBox == value)
                    return;
                _sendBox = value;
                OnPropertyChanged("SendBox");
            }
        }

        string _msgBox;
        public string MsgBox
        {
            get
            {
                return _msgBox;
            }
            set
            {
                if (_msgBox == value)
                    return;
                _msgBox = value;
                OnPropertyChanged("MsgBox");
            }
        }
    }
}
