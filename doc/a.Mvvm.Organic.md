# A. MVVM with .NET

.NET標準クラスライブラリのみで作成

- [A. MVVM with .NET](#a-mvvm-with-net)
  - [INotifyPropertyChangedによるデータバインディング](#inotifypropertychangedによるデータバインディング)
    - [Modelの実装](#modelの実装)
    - [ViewModelクラスの作成](#viewmodelクラスの作成)

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
