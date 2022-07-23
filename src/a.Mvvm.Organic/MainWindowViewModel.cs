using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace a.Mvvm.Organic
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        // INotifyPropertyChanged の実装
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly UserRepository userRepository;
        private User user = new User();

        public string FirstName
        {
            get { return user.FirstName; }
            set
            {
                user.FirstName = value;
                OnPropertyChanged(); // 自プロパティの変更イベントを送信
                OnPropertyChanged(nameof(FullName)); // 関連プロパティの変更イベントを送信
                SubmitCommand.RaiseCanExecuteChanged();
                Status = string.Empty;
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
                SubmitCommand.RaiseCanExecuteChanged();
                Status = string.Empty;
            }
        }

        public string FullName => $"{FirstName} {LastName}".Trim();

        private Command submitCommand;

        public Command SubmitCommand => submitCommand ??= new Command(SubmitCommandExecute, SubmitCommandCanExecute);

        private string status = string.Empty;

        public string Status
        {
            get { return status; }
            private set
            {
                status = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            userRepository = new UserRepository();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SubmitCommandExecute()
        {
            userRepository.Create(user);
            Status = "Submitted";
        }

        private bool SubmitCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName);
        }
    }
}
