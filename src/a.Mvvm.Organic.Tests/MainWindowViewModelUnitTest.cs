using System.ComponentModel;

namespace a.Mvvm.Organic
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MainWindowViewModelUnitTest
    {
        private readonly List<PropertyChangedEventArgs> propertyChangedEvents = new ();
        bool isRaisedSubmitCommandCanExecuteChanged;

        [TestMethod]
        public void コンストラクターで初期化すること()
        {
            //// Arrange

            //// Act
            var actual = new MainWindowViewModel();

            //// Assert
            Assert.AreEqual(string.Empty, actual.FirstName);
            Assert.AreEqual(string.Empty, actual.LastName);
            Assert.AreEqual(string.Empty, actual.FullName);
            Assert.AreEqual(string.Empty, actual.Status);
        }

        [TestMethod]
        public void FistNameを設定取得すること()
        {
            //// Arrange
            var target = new MainWindowViewModel();
            target.PropertyChanged += Target_PropertyChanged;
            target.SubmitCommand.CanExecuteChanged += SubmitCommand_CanExecuteChanged;

            //// Act
            target.FirstName = "first";

            //// Assert
            Assert.AreEqual("first", target.FirstName);
            Assert.AreEqual(3, propertyChangedEvents.Count);
            Assert.IsTrue(IsRaisedPropertyChanged(nameof(target.FirstName)));
            Assert.IsTrue(IsRaisedPropertyChanged(nameof(target.FullName)));
            Assert.IsTrue(IsRaisedPropertyChanged(nameof(target.Status)));
            Assert.IsTrue(isRaisedSubmitCommandCanExecuteChanged);
        }

        [TestMethod]
        public void LastNameを設定取得すること()
        {
            //// Arrange
            var target = new MainWindowViewModel();
            target.PropertyChanged += Target_PropertyChanged;
            target.SubmitCommand.CanExecuteChanged += SubmitCommand_CanExecuteChanged;

            //// Act
            target.LastName = "last";

            //// Assert
            Assert.AreEqual("last", target.LastName);
            Assert.AreEqual(3, propertyChangedEvents.Count);
            Assert.IsTrue(IsRaisedPropertyChanged(nameof(target.LastName)));
            Assert.IsTrue(IsRaisedPropertyChanged(nameof(target.FullName)));
            Assert.IsTrue(IsRaisedPropertyChanged(nameof(target.Status)));
            Assert.IsTrue(isRaisedSubmitCommandCanExecuteChanged);
        }

        [TestMethod]
        public void FullNameを取得すること()
        {
            //// Arrange
            var target = new MainWindowViewModel
            {
                FirstName = "first",
                LastName = "last"
            };

            //// Assert
            Assert.AreEqual("first last", target.FullName);
        }

        [TestMethod]
        public void SubmitCommandExecuteで実行すること()
        {
            //// Arrange
            var target = new MainWindowViewModel
            {
                FirstName = "first",
                LastName = "last"
            };

            //// Act
            target.SubmitCommand.Execute(null);

            //// Assert
            Assert.AreEqual("Submitted", target.Status);
        }

        [TestMethod]
        [DataRow("first", "last", true)]
        [DataRow("first", "", false)]
        [DataRow("", "last", false)]
        [DataRow("", "", false)]
        public void SubmitCommandCanExecuteで実行可否を取得すること(string first, string last, bool expected)
        {
            //// Arrange
            var target = new MainWindowViewModel
            {
                FirstName = first,
                LastName = last
            };

            //// Act
            var actual = target.SubmitCommand.CanExecute(null);

            //// Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Statusを取得すること()
        {
            //// Arrange
            var target = new MainWindowViewModel
            {
                FirstName = "first",
                LastName = "last"
            };
            target.SubmitCommand.Execute(null);

            //// Assert
            Assert.AreEqual("Submitted", target.Status);
        }

        [TestMethod]
        public void StatusがFirstNameの設定でクリアすること()
        {
            //// Arrange
            var target = new MainWindowViewModel()
            {
                FirstName = "first",
                LastName = "last"
            };
            target.SubmitCommand.Execute(null);
            target.FirstName = "first";

            //// Assert
            Assert.AreEqual(string.Empty, target.Status);
        }

        [TestMethod]
        public void StatusがLastNameの設定でクリアすること()
        {
            //// Arrange
            var target = new MainWindowViewModel()
            {
                FirstName = "first",
                LastName = "last"
            };
            target.SubmitCommand.Execute(null);
            target.LastName = "last";

            //// Assert
            Assert.AreEqual(string.Empty, target.Status);
        }

        private void SubmitCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            isRaisedSubmitCommandCanExecuteChanged = true;
        }

        private void Target_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            propertyChangedEvents.Add(e);
        }

        private bool IsRaisedPropertyChanged(string propertyName)
        {
            return propertyChangedEvents.Exists(e => e.PropertyName == propertyName);
        }
    }
}