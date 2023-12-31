﻿using KfcApi.Models.AbstractModelClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace KfcApi.Models
{
    public class Address : BaseModel
    {
        public string UserId { get; set; }
        public string AddressName { get; set; }
        public string? HouseNumber { get; set; }
        public string? AddressNotes { get; set; }
        public string Type { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }


    }
}
