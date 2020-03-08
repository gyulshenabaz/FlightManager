using System.Collections.Generic;
using System.IO.Abstractions;
using FlightManager.Services.Interfaces;
using Newtonsoft.Json;

namespace FlightManager.Services.Implementations.Helpers
{
    public class CountriesHelper : ICountriesHelper
    {
        private const string CountriesFileName = "countries.json";
        
        private Country[] countries;
        
        private readonly IFileSystem fileSystem;

        public CountriesHelper(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
            this.DeserializeFromFile(CountriesFileName);
        }
        
        public ICollection<Country> GetAll()
        {
            return this.countries;
        }
        
        private void DeserializeFromFile(string fileName)
        {
            var jsonText = this.fileSystem.File.ReadAllText(fileName);

            this.countries = JsonConvert.DeserializeObject<Country[]>(jsonText);
        }
        
        public class Country
        {
            public string Ioc { get; set; }
            public string Name { get; set; }
        }
    }
}