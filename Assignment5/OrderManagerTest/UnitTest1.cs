using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OrderManager;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace OrderManagerTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRegisterCustomer()
        {
            Customer a = OrderService.RegisterCustomer("dty");
            string result = "{id:0,name:dty}";
            Assert.AreEqual(a.ToString(), result);
        }
        [TestMethod]
        public void TestAddOrder()
        {
            Customer a = OrderService.RegisterCustomer("dty");
            string id = "qwq";
            int n = 2;
            List<string> names =new List<string>() { "awa","TuT"};
            List<int> nums=new List<int>() { 2,3};
            List<int> unitPrices=new List<int> { 2,1};
            OrderService.AddOrder(a, id, n, names, nums, unitPrices);
            string result= "{id:qwq,details:{goods:[{name:awa,num:2,unitPrice:2},{name:TuT,num:3,unitPrice:1}],price:7,customer:{id:0,name:dty}}}";
            Assert.AreEqual(OrderService.ToStringAll(), result);
        }
    }
}
