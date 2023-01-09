using System.ComponentModel.DataAnnotations;

namespace Together.OrderService.Core.Entities.Address
{
    public class AdministrativeUnit
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string FullNameEN { get; set; } = default!;
        public string ShortName { get; set; } = default!;
        public string ShortNameEN { get; set; } = default!;
        public string CodeName { get; set; } = default!;
        public string CodeNameEN { get; set; } = default!;
    }
}
