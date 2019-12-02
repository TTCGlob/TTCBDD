﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTCBDD.ComponentHelper;

namespace TTCBDD.PageObject
{
    public class Address
    {
        public int number { get; set; }
        public string street { get; set; }
        public string city { get; set; }

        public static Address Random()
        {
            return new Address()
            {
                number = new Random().Next(1, 100),
                street = BasicHelperMethods.RandomString(12)

            };
        }

        public override bool Equals(object obj)
        {
            return obj is Address other
                   && number == other.number
                   && street.Equals(other.street)
                   && city.Equals(other.city);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
