public record ProductDto
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int? CompanyId { get; set; }

    public string ProductName { get; set; } = null!;

    public bool? IsInPackage { get; set; }

    public int? AmountInPackage { get; set; }

    public int? Weight { get; set; }

    public string WeightType { get; set; } = null!;

    public string? Img { get; set; }
}
