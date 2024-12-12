using kind_farm.admin.products;
using kind_farm.Conn_DB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Controls;
using System.Windows;
using System.Net;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnitTest
{
    [TestClass]
    public class CheckInputs
    {
        [TestMethod]
        public void CheckEmail()
        {
            string email = "email@email.com";

            Assert.IsTrue(Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"));
        }
        
        [TestMethod]
        public void CheckNumbers()
        {
            string phone = "+75645689533";
            Assert.IsTrue(phone.Length == 12);
        }

        [TestMethod]
        public void CheckName()
        {
            string name_product = "nameproduct";
            bool isOnlyLetters = name_product.All(char.IsLetter);
            Assert.IsTrue(isOnlyLetters, "Имя продукта должно содержать только буквы.");
        }
        }
    }
