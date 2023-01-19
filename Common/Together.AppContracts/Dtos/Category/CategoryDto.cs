namespace Together.AppContracts.Dtos.Category
{
    public class CategoryDto : BaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
