﻿namespace DotNetCoreAsysnSample.Models.DTO
{
    public class CityDTO
    {
        public CityDTO() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
