# B. MVVM with Prism

Prismライブラリで作成

- [A. MVVM with .NET](#a-mvvm-with-net)
  - [INotifyPropertyChangedによるデータバインディング](#inotifypropertychangedによるデータバインディング)
    - [Modelの実装](#modelの実装)

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
