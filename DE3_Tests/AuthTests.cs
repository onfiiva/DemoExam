using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using DE3;

namespace DE3_Tests
{
    [TestClass]
    public class AuthTests
    {

        private Authorization authorization;

        [TestInitialize] public void Init() 
        {
            authorization = new Authorization(); 
        }

        [TestMethod]
        public void Authenticate_ValidCreditnails_ReturnsTrue()
        {
            string login = "user";
            string password = "password1";

            bool result = authorization.Authenticate(login, password);
            
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void Authenticate_ValidCreditnails_ReturnsFalse()
        {
            string login = "user123";
            string password = "password1123";

            bool result = authorization.Authenticate(login, password);

            Assert.IsFalse(result);

        }
    }
}
