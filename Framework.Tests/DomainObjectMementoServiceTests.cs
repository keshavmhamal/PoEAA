﻿using System;
using Framework.Data_Manipulation;
using Framework.Domain;
using Framework.Tests.CustomerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests
{
    [TestClass]
    public class DomainObjectMementoServiceTests
    {
        [TestMethod]
        public void TestCreateMemento()
        {
            IDomainObjectMementoService service = DomainObjectMementoService.GetInstance();
            Customer customer = new Customer(null) {Name = "John Doe", Number = "123"};
            IDomainObjectMemento memento = service.CreateMemento(customer);

            Assert.AreEqual("John Doe", memento.GetPropertyValue("Name"));
            Assert.AreEqual("123", memento.GetPropertyValue("Number"));
        }

        [TestMethod]
        public void TestSetMemento()
        {
            IDomainObjectMementoService service = DomainObjectMementoService.GetInstance();
            Customer customer = new Customer(null) { Name = "John Doe", Number = "123" };
            IDomainObjectMemento snapshotMemento = service.CreateMemento(customer);

            //Test original
            Assert.AreEqual("John Doe", customer.Name);
            Assert.AreEqual("123", customer.Number);

            //Alter
            customer.Name = "Juan dela Cruz";
            customer.Number = "345";

            //Test altered persistency
            Assert.AreEqual("Juan dela Cruz", customer.Name);
            Assert.AreEqual("345", customer.Number);

            //Restore
            IDomainObject domainObject = customer;
            service.SetMemento(ref domainObject, snapshotMemento);

            //Test original
            Assert.AreEqual("John Doe", customer.Name);
            Assert.AreEqual("123", customer.Number);
        }
    }
}
