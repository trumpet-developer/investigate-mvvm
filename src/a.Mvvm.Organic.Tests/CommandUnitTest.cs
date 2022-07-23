namespace a.Mvvm.Organic
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CommandUnitTest
    {
        [TestMethod]
        public void コンストラクターの実行メソッドのみで実行すること()
        {
            //// Arrange
            var actual = false;
            var target = new Command(() => actual = true);

            //// Act
            target.Execute(null);

            //// Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void コンストラクターの実行メソッドのみで実行可否を取得すること()
        {
            //// Arrange
            var target = new Command(() => { });

            //// Act
            var actual = target.CanExecute(null);

            //// Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void コンストラクターの実行メソッド実行可否メソッドで実行すること()
        {
            //// Arrange
            var actual = false;
            var target = new Command(() => actual = true, () => true);

            //// Act
            target.Execute(null);

            //// Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void コンストラクターの実行メソッド実行可否メソッドで実行可否を取得すること()
        {
            //// Arrange
            var target = new Command(() => { }, () => true);

            //// Act
            var actual = target.CanExecute(null);

            //// Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void コンストラクターの実行メソッドがnullでエラーを返すこと()
        {
            //// Act
            _= new Command(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void コンストラクターの実行メソッド実行可否メソッドで実行メソッドがnullでエラーを返すこと()
        {
            //// Act
            _ = new Command(null, () => true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void コンストラクターの実行メソッド実行可否メソッドで実行可否メソッドがnullでエラーを返すこと()
        {
            //// Act
            _ = new Command(() => { }, null);
        }
    }
}
