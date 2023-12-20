namespace KfcApi.DTOs
{
    public class LocationDto
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string OpeningHour { get; set; }
        public string ClosingHour { get; set; }
        public string? City { get; set; }
    }
}
