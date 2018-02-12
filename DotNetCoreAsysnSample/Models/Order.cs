﻿namespace DotNetCoreAsysnSample.Models
{
    /// <summary>
    ///     Order model
    /// </summary>
    public class Order
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}