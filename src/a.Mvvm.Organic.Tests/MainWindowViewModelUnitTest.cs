namespace a.Mvvm.Organic
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MainWindowViewModelUnitTest
    {
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

            //// Act
            target.FirstName = "first";

            //// Assert
            Assert.AreEqual("first", target.FirstName);
        }

        [TestMethod]
        public void LastNameを設定取得すること()
        {
            //// Arrange
            var target = new MainWindowViewModel();

            //// Act
            target.LastName = "last";

            //// Assert
            Assert.AreEqual("last", target.LastName);
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
    }
}