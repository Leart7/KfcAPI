namespace KfcApi.DTOs
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string AddressName { get; set; }
        public string? HouseNumber { get; set; }
        public string? AddressNotes { get; set; }
        public string Type { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
