using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace a.Mvvm.Organic
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        // INotifyPropertyChanged の実装
        public event PropertyChangedEventHandler PropertyChanged;

        private User user = new User();

        public string FirstName
        {
            get { return user.FirstName; }
            set
            {
                user.FirstName = value;
                OnPropertyChanged(); // 自プロパティの変更イベントを送信
                OnPropertyChanged(nameof(FullName)); // 関連プロパティの変更イベントを送信
            }
        }

        public string LastName
        {
            get { return user.LastName; }
            set
            {
                user.LastName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string FullName => $"{FirstName} {LastName}".Trim();

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
