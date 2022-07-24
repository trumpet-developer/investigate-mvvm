namespace a.Mvvm.Organic.Tests
{
    [TestClass]
    public class UserRepositoryUnitTest
    {
        [TestMethod]
        public void Createで作成すること()
        {
            //// Arrange
            var tareget = new UserRepository();
            var expected = new User
            {
                FirstName = "first",
                LastName = "last"
            };
            
            //// Act
            var actual = tareget.Create(expected);

            //// Assert
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
        }
    }
}
