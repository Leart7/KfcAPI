using KfcApi.Models.AbstractModelClasses;

namespace KfcApi.Models
{
    public class Location : BaseModel
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string OpeningHour {  get; set; }
        public string ClosingHour { get; set; }
        public string? City { get; set; }

    }
}
