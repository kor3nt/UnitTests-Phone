using ClassLibrary;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace TestPhone
{
    // Sprawdzanie tworzenia telefonu
    [TestClass]
    public class TestCreatePhone
    {
        // Test Konstruktora - poprawnosc danych
        [TestMethod]
        public void Test_Constructor_Correct_Data()
        {
            // AAA

            // Arrange
            string owner = "Jedrzejczyk";
            string phoneNum = "123456789";

            // Act
            Phone p = new Phone(owner, phoneNum);

            // Assert
            Assert.AreEqual(owner, p.Owner, "Owner's name is not valid");
            Assert.AreEqual(phoneNum, p.PhoneNumber, "Phone number is not valid");
        }

        // Test w³aœciciel jest pusty
        [TestMethod]
        public void Test_Empty_Owner()
        {
            string owner = "";
            string phoneNum = "123456789";
            try
            {
                Phone p = new Phone(owner, phoneNum);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains($"Owner name is empty or null!"), "Error, Something went wrong");
            }
        }

        // Test numer telefonu jest pusty
        [TestMethod]
        public void Test_Empty_Phone()
        {
            string owner = "Jedrzejczyk";
            string phoneNum = "";
            try
            {
                Phone p = new Phone(owner, phoneNum);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains($"Phone number is empty or null!"), "Error, Something went wrong");
            }
        }

        // Test numer telefonu nie jest cyfra
        [TestMethod]
        public void Test_Phone_Are_Letters()
        {
            string owner = "Jedrzejczyk";
            string phoneNum = "abcdefghi";
            try
            {
                Phone p = new Phone(owner, phoneNum);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains($"Invalid phone number!"), "Error, Something went wrong");
            }
        }

        // Test numer telefonu jest za d³ugi
        [TestMethod]
        public void Test_Phone_Too_Long()
        {
            string owner = "Jedrzejczyk";
            string phoneNum = "1234567890";
            try
            {
                Phone p = new Phone(owner, phoneNum);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.ToString().Contains($"Invalid phone number!"), "Error, Something went wrong");
            }
        }
    }

    // Sprawdzanie ksiazki telefonicznej
    [TestClass]
    public class TestPhoneBook
    {
        // Poprawne dane 
        [TestMethod]
        public void Test_Add_Correct_Data()
        {
            Phone p1 = new Phone("Jedrzejczyk", "123456789");
            Phone p2 = new Phone("Kowalski", "234567891");
            Phone p3 = new Phone("Nowak", "345678912");

            p1.AddContact(p2.Owner, p2.PhoneNumber);
            p1.AddContact(p3.Owner, p3.PhoneNumber);

            int Expected_Count = 2;

            Assert.AreEqual(Expected_Count, p1.Count, "Count is not valid");
        }

        // Te same imiona 
        [TestMethod]
        public void Test_Add_The_Same_Names()
        {
            Phone p1 = new Phone("Jedrzejczyk", "123456789");
            Phone p2 = new Phone("Kowalski", "234567891");
            Phone p3 = new Phone("Kowalski", "345678912");

            p1.AddContact(p2.Owner, p2.PhoneNumber);
            p1.AddContact(p3.Owner, p3.PhoneNumber);

            int Expected_Count = 1;

            Assert.AreEqual(Expected_Count, p1.Count, "Count is not valid");
        }

        // Te same numery
        [TestMethod]
        public void Test_Add_The_Same_Numbers()
        {
            Phone p1 = new Phone("Jedrzejczyk", "123456789");
            Phone p2 = new Phone("Kowalski", "234567891");
            Phone p3 = new Phone("Nowak", "234567891");

            p1.AddContact(p2.Owner, p2.PhoneNumber);
            p1.AddContact(p3.Owner, p3.PhoneNumber);

            int Expected_Count = 2;

            Assert.AreEqual(Expected_Count, p1.Count, "Count is not valid");
        }

        // Sprawdzanie limitow
        [TestMethod]
        public void PhoneBook_Check_Phone_Book_Limit()
        {
            Phone p1 = new Phone("Jedrzejczyk", "123456789");
            string phoneNum = "234567891";

            try
            {
                for (int i = 0; i <= 100; i++)
                {
                    p1.AddContact(i.ToString(), phoneNum);
                }
                int Expected_Count = 101;
                Assert.AreEqual(Expected_Count, p1.Count, "Count is not valid!");
            }
            catch (InvalidOperationException e)
            {
                Assert.IsTrue(e.ToString().Contains("Phonebook is full!"), "Error, Something went wrong");
            }
        }
    }

    // Sprawdzenie dzwonienia
    [TestClass]
    public class TestCallPhone
    {
        // Poprawne dane 
        [TestMethod]
        public void Test_Correct_Call()
        {
            Phone p1 = new Phone("Jedrzejczyk", "123456789");

            string owner = "Kowalski";
            string phoneNum = "234567891";
            Phone p2 = new Phone(owner, phoneNum);

            p1.AddContact(p2.Owner, p2.PhoneNumber);
            string result = p1.Call(p2.Owner);

            Assert.AreEqual($"Calling {phoneNum} ({owner}) ...", result, "Error, Something went wrong");
        }

        // Osoba nie isnieje w naszych kontaktach 
        [TestMethod]
        public void Test_Person_Dont_Exist()
        {
            Phone p1 = new Phone("Jedrzejczyk", "123456789");

            string owner = "Kowalski";
            string phoneNum = "234567891";

            try
            {
                string result = p1.Call(owner);
            }
            catch (InvalidOperationException e)
            {
                Assert.IsTrue(e.ToString().Contains("Person doesn't exists!"), "Error, Something went wrong");
            }
        }
    }
}