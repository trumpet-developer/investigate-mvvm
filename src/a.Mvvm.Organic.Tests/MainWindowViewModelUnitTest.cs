using System.ComponentModel;

namespace a.Mvvm.Organic
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MainWindowViewModelUnitTest
    {
        private readonly List<PropertyChangedEventArgs> propertyChangedEvents = new ();

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
        }

        [TestMethod]
        public void FistNameを設定取得すること()
        {
            //// Arrange
            var target = new MainWindowViewModel();
            target.PropertyChanged += Target_PropertyChanged;

            //// Act
            target.FirstName = "first";

            //// Assert
            Assert.AreEqual("first", target.FirstName);
            Assert.AreEqual(2, propertyChangedEvents.Count);
            Assert.IsTrue(IsRaisedPropertyChanged(nameof(target.FirstName)));
            Assert.IsTrue(IsRaisedPropertyChanged(nameof(target.FullName)));
        }

        [TestMethod]
        public void LastNameを設定取得すること()
        {
            //// Arrange
            var target = new MainWindowViewModel();
            target.PropertyChanged += Target_PropertyChanged;

            //// Act
            target.LastName = "last";

            //// Assert
            Assert.AreEqual("last", target.LastName);
            Assert.AreEqual(2, propertyChangedEvents.Count);
            Assert.IsTrue(IsRaisedPropertyChanged(nameof(target.LastName)));
            Assert.IsTrue(IsRaisedPropertyChanged(nameof(target.FullName)));
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