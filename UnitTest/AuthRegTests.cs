using kind_farm.admin.users;
using kind_farm.auth;
using kind_farm.Conn_DB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Controls;

namespace UnitTest
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestAuthUser()
        {
            var page = new authorization();
            Assert.IsTrue(page.Auth("11", "11"));
            Assert.IsTrue(page.Auth("22", "22"));
            Assert.IsTrue(page.Auth("33", "33"));
        }

        [TestMethod]
        public void RegisterUser()
        {
            var pager = new registration();
            Assert.IsTrue(pager.Register("PROVERKA", "re", "proverka", "password", "+712345678", "email@email.com", 3));
        }

    }
}
