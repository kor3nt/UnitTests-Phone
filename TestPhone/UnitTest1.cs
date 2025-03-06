using ClassLibrary;
using Newtonsoft.Json.Linq;

namespace TestPhone
{
    [TestClass]
    public class UnitTest1
    {
        // Test Konstruktora - poprawnosc danych
        [TestMethod]
        public void Test_Constructor_Correct_Data()
        {
            // AAA

            // Arrange
            var owner = "Jedrzejczyk";
            var phoneNum = "123456789";

            // Act
            var p = new Phone(owner, phoneNum);

            // Assert
            Assert.AreEqual(owner, p.Owner);
            Assert.AreEqual(phoneNum, p.PhoneNumber);
        }
    }
}