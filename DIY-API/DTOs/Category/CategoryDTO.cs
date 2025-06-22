namespace DIY_API.DTOs.Category
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }

        public string CategoryNameEn { get; set; } = null!;

        public string CategoryNameAr { get; set; } = null!;

        public string? Image { get; set; }

        public string? CreatedBy { get; set; }

    }
}
