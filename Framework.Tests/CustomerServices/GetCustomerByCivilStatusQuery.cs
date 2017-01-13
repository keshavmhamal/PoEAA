﻿using System;
using System.Collections.Generic;
using Framework.Data_Manipulation;

namespace Framework.Tests.CustomerServices
{
    class GetCustomerByCivilStatusQuery : BaseQueryObject<Customer, GetCustomerByCivilStatusQuery.Criteria, Tuple<string, string>>
    {
        public enum CivilStatus
        {
            Single,
            Married,
            Divorced
        }

        public class Criteria
        {
            public CivilStatus CurrentCivilStatus { get; set; }

            private Criteria()
            {
            }

            public static Criteria SearchById(CivilStatus status)
            {
                return new Criteria { CurrentCivilStatus = status };
            }
        }

        public override Tuple<string, string> PerformSearchOperation(Criteria searchInput)
        {
            Dictionary<CivilStatus, Tuple<string, string>> customerList = new Dictionary<CivilStatus, Tuple<string, string>>();

            
            customerList.Add(CivilStatus.Single, new Tuple<string, string>("4", "Test Single"));
            customerList.Add(CivilStatus.Married, new Tuple<string, string>("5", "Test Married"));
            customerList.Add(CivilStatus.Divorced, new Tuple<string, string>("6", "Test Divorced"));

            return customerList[searchInput.CurrentCivilStatus];
        }

        public override IList<Customer> ConstructOutput(Tuple<string, string> searchResult)
        {
            Customer customer = new Customer();

            customer.Number = searchResult.Item1;
            customer.Name = searchResult.Item2;

            return new List<Customer>(new[] { customer });
        }
    }
}
