using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Services.Interfaces;
using Newtonsoft.Json;

namespace FlightManager.Services.Implementations.Helpers
{
    public class GeoLocationHelper : IGeoLocationHelper
    {
        private const string CitiesFileName = "cities.json";
        
        private List<City> cities;
        
        private readonly IFileSystem fileSystem;
        
        public GeoLocationHelper(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
            this.DeserializeFromFile(CitiesFileName);
        }
        
        public string GetCityName(double latitude, double longitude)
        {
            //https://en.wikipedia.org/wiki/Euclidean_distance
            
            var nearestCity = cities
                .OrderBy(c =>
                    Math.Sqrt(Math.Pow(latitude - c.Latitude, 2) + Math.Pow(longitude - c.Longitude, 2)))
                .FirstOrDefault()
                ?.Name;

            return nearestCity;
        }
        
        private void DeserializeFromFile(string fileName)
        {
            var jsonText = this.fileSystem.File.ReadAllText(fileName);

            this.cities = JsonConvert.DeserializeObject<List<City>>(jsonText);
        }
        
        private class City
        {
            public string Name { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
    }
}