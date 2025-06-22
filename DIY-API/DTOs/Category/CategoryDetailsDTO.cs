namespace DIY_API.DTOs.Category
{
    public class CategoryDetailsDTO
    {
        public string CategoryNameEn { get; set; } = null!;

        public string CategoryNameAr { get; set; } = null!;

        public string? Image { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool? IsActive { get; set; }
    }
}
