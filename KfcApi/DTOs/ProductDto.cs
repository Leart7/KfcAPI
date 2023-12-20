﻿namespace KfcApi.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public bool HasMenu { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
        public string? HomeCategory { get; set; }
    }
}
