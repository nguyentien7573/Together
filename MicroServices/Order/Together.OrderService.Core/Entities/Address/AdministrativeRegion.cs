using System.ComponentModel.DataAnnotations;

namespace Together.OrderService.Core.Entities.Address
{
    public class AdministrativeRegion
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string NameEN { get; set; } = default!;
        public string CodeName { get; set; } = default!;
        public string CodeNameEN { get; set; } = default!;
    }
}
