# A. MVVM with .NET

.NET標準クラスライブラリのみで作成

- [A. MVVM with .NET](#a-mvvm-with-net)
  - [INotifyPropertyChangedによるデータバインディング](#inotifypropertychangedによるデータバインディング)
    - [Modelの実装](#modelの実装)
    - [ViewModelクラスの作成](#viewmodelクラスの作成)
    - [INotifyPropertyChangedの実装](#inotifypropertychangedの実装)
    - [View-ViewModelのデータバインディング](#view-viewmodelのデータバインディング)
    - [ICommandの実装](#icommandの実装)

## INotifyPropertyChangedによるデータバインディング

### Modelの実装

```mermaid
classDiagram
  IUserRepository <|.. UserRepository : realization
  UserRepository --|> User
  class User {
    +string FirstName
    +string LastName
  }
  class UserRepository {
    <<IUserRepository>>
    +User Create(User)
  }
  class IUserRepository {
    +User Create(User)
  }
```

### ViewModelクラスの作成

ウィンドウ名+`ViewModel`クラスを作成する  

```mermaid
classDiagram
  MainWindowViewModel --> User
  class MainWindowViewModel {
    +string FirstName
    +string LastName
    +string FullName
    -User user
  }
  IUserRepository <|.. UserRepository : realization
  UserRepository --|> User
  class User {
    +string FirstName
    +string LastName
  }
  class UserRepository {
    +User Create(User)
  }
  class IUserRepository {
    +User Create(User)
  }
```

### INotifyPropertyChangedの実装

ViewModelクラスにINotifyPropertyChangedを実装する  

- ViewModel:`MainWindowViewModel`  

```mermaid
classDiagram
  INotifyPropertyChanged <|.. MainWindowViewModel : realization
  class MainWindowViewModel {
    <<INotifyPropertyChanged>>
    +PropertyChangedEventHandler PropertyChanged
    +string FirstName
    +string LastName
    +string FullName
    -User user
    -void OnPropertyChanged(string propertyName)
  }
  class INotifyPropertyChanged {
    +PropertyChangedEventHandler PropertyChanged
  }
  MainWindowViewModel --> User
  IUserRepository <|.. UserRepository : realization
  UserRepository --|> User
  class User {
    +string FirstName
    +string LastName
  }
  class UserRepository {
    +User Create(User)
  }
  class IUserRepository {
    +User Create(User)
  }
```

```csharp
internal class MainWindowViewModel : INotifyPropertyChanged
{
    // INotifyPropertyChanged の実装
    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

```csharp
internal class MainWindowViewModel : INotifyPropertyChanged
{
    private string firstName;

    public string FirstName
    {
        get { return firstName; }
        set
        {
            firstName = value;
            OnPropertyChanged(); // 自プロパティの変更イベントを送信
            OnPropertyChanged(nameof(FullName)); // 関連プロパティの変更イベントを送信
        }
    }
}
```

### View-ViewModelのデータバインディング

ViewとViewModelをバインドする  

- View：`MainWindow`  
- ViewModel:`MainWindowViewModel`

```mermaid
classDiagram
  MainWindow --> MainWindowViewModel : creation
  INotifyPropertyChanged <|.. MainWindowViewModel : realization
  class MainWindow {
    <<Window>>
    ~TextBox FirstName_TextBox
    ~TextBox LastName_TextBox
    ~TextBox FullName_TextBox
  }
  class MainWindowViewModel {
    <<INotifyPropertyChanged>>
    +PropertyChangedEventHandler PropertyChanged
    +string FirstName
    +string LastName
    +string FullName
    -User user
    #void OnPropertyChanged(string propertyName)
  }
  class INotifyPropertyChanged {
    +PropertyChangedEventHandler PropertyChanged
  }
  MainWindowViewModel --> User
  IUserRepository <|.. UserRepository : realization
  UserRepository --|> User
  class User {
    +string FirstName
    +string LastName
  }
  class UserRepository {
    +User Create(User)
  }
  class IUserRepository {
    +User Create(User)
  }
```

```mermaid
sequenceDiagram
  actor User
  participant MainWindow
  participant FirstName_TextBox
  participant FullName_TextBox
  participant MainWindowViewModel
  User ->> MainWindow : Show
  activate MainWindow
  MainWindow ->> MainWindowViewModel : Create
  activate MainWindowViewModel
  
  User ->> FirstName_TextBox : Input text
  rect rgb(232,209,157)
  FirstName_TextBox ->> MainWindowViewModel : Set property
  MainWindowViewModel -) FirstName_TextBox : PropertyChanged
  MainWindowViewModel -) FullName_TextBox : PropertyChanged
  end

```

```xaml
<Window.DataContext>
    <local:MainWindowViewModel />
</Window.DataContext>
```

```xaml
<TextBox x:Name="FirstName_TextBox"
         Text="{Binding FirstName, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" />
<TextBlock x:Name="FullName_TextBox"
           Text="{Binding FullName}" />
```

### ICommandの実装

- View：`MainWindow`  
- ViewModel:`MainWindowViewModel`
- 

```mermaid
classDiagram
  MainWindow --> MainWindowViewModel : creation
  INotifyPropertyChanged <|.. MainWindowViewModel : realization
  class MainWindow {
    <<Window>>
    ~TextBox FirstName_TextBox
    ~TextBox LastName_TextBox
    ~TextBox FullName_TextBox
  }
  class MainWindowViewModel {
    <<INotifyPropertyChanged>>
    +PropertyChangedEventHandler PropertyChanged
    +string FirstName
    +string LastName
    +string FullName
    +Command SubmitCommand
    -User user
    -void SubmitCommandExecute()
    -bool SubmitCommandCanExecute()
    #void OnPropertyChanged(string propertyName)
  }
  class INotifyPropertyChanged {
    +PropertyChangedEventHandler PropertyChanged
  }
  ICommand <|.. Command : realization
  MainWindowViewModel --> Command
  class ICommand {
    +EventHandler? CanExecuteChanged
    +void Execute(object? parameter)
    +bool CanExecute(object? parameter)
  }
  class Command {
    <<ICommand>>
    +EventHandler CanExecuteChanged
    +void Execute(object parameter)
    +bool CanExecute(object parameter)
    -void RaiseCanExecuteChanged()
  }
  MainWindowViewModel --> User
  IUserRepository <|.. UserRepository : realization
  UserRepository --|> User
  class User {
    +string FirstName
    +string LastName
  }
  class UserRepository {
    +User Create(User)
  }
  class IUserRepository {
    +User Create(User)
  }
```

```mermaid
sequenceDiagram
  actor User
  participant MainWindow
  participant FirstName_TextBox
  participant Submit_Button
  participant MainWindowViewModel
  participant SubmitCommand
  User ->> MainWindow : Show
  activate MainWindow
  MainWindow ->> MainWindowViewModel : Create
  activate MainWindowViewModel
  MainWindowViewModel ->> SubmitCommand : Create
  activate SubmitCommand
  
  User ->> FirstName_TextBox : Input text
  FirstName_TextBox ->> MainWindowViewModel : Set property
  MainWindowViewModel ->> SubmitCommand : Raise CanExecuteChanged
  rect rgb(232,209,157)
  SubmitCommand -) Submit_Button : CanExecuteChanged
  Submit_Button ->> SubmitCommand : CanExecute command
  SubmitCommand -->> Submit_Button : ture or false
  end

  User ->> Submit_Button : Click
  rect rgb(232,209,157)
  Submit_Button ->> SubmitCommand : Execute command
  end

```
