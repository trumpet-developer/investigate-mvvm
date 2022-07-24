namespace a.Mvvm.Organic.Tests
{
    [TestClass]
    public class UserUnitTest
    {
        [TestMethod]
        public void コンストラクターで初期化すること()
        {
            //// Arrange

            //// Act
            var actual = new User();

            //// Assert
            Assert.AreEqual(string.Empty, actual.FirstName);
            Assert.AreEqual(string.Empty, actual.LastName);
        }

        [TestMethod]
        public void FistNameを設定取得すること()
        {
            //// Arrange
            var target = new User();

            //// Act
            target.FirstName = "first";

            //// Assert
            Assert.AreEqual("first", target.FirstName);
        }

        [TestMethod]
        public void LastNameを設定取得すること()
        {
            //// Arrange
            var target = new User();

            //// Act
            target.LastName = "last";

            //// Assert
            Assert.AreEqual("last", target.LastName);
        }
    }
}
