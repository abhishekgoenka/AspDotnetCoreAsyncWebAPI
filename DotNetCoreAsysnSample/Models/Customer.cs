﻿using System.Collections.Generic;

namespace DotNetCoreAsysnSample.Models
{
    /// <summary>
    ///     Customer model
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public int Zip { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}