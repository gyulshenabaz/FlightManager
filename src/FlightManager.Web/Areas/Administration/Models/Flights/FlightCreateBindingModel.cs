using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FlightManager.Common.AutoMapping.Interfaces;
using FlightManager.Services.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FlightManager.Web.Areas.Administration.Models.Flights
{
    public class FlightCreateBindingModel : IMappableTo<FlightServiceModel>, IValidatableObject
    {
        [Required(ErrorMessage = "Въведете място на излитане")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Това поле трябва да съдържа поне 4 букви")]
        public string From { get; set; }

        [Required(ErrorMessage = "Въведете дестинация")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Това поле трябва да съдържа поне 4 букви")]
        public string To { get; set; }
        
        [Required(ErrorMessage = "Изберете дата и час на излитане")]
        public DateTime DepartureDate { get; set; }
        
        [Required(ErrorMessage = "Изберете дата и час на пристигане")]
        public DateTime ArrivalDate { get; set; }
        
        [Required(ErrorMessage = "Посочете вида на самолета")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Това поле трябва да съдържа поне 4 букви")]
        public string AircraftType { get; set; }
        
        [Required(ErrorMessage = "Моля въведете уникалния номер на самолета")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Това поле трябва да съдържа поне 4 букви")]
        public string AircraftRegistration { get; set; }
        
        [Required(ErrorMessage = "Моля въведете името на пилота")]
        [StringLength(60, MinimumLength = 4, ErrorMessage = "Това поле трябва да съдържа поне 2 букви")]
        public string PilotName { get; set; }
        
        [Required(ErrorMessage = "Моля въведете брой свободни места на самолета")]
        [Range(5, 400, ErrorMessage = "Самолетът трябва да има над 5 свободни места")]
        public int AvailableSeats { get; set; }
        
        [Required(ErrorMessage = "Моля въведете брой свободни места в бизнес класа")]
        [Range(5, 400, ErrorMessage = "Бизнес капацитетът трябва да има над 5 свободни места")]
        public int BusinessClassSeats { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.BusinessClassSeats > this.AvailableSeats)
            {
                yield return new ValidationResult("Свободните места в бизнес класата не трябва да надвишат общия", new[] {"BusinessClassCapacity"});
            }
            if (this.DepartureDate > this.ArrivalDate)
            {
                yield return new ValidationResult("Датата и часът на пристигане не трябва да са след датата на заминаване", new[] {"ArrivalDate"});
            }
        }
    }
}