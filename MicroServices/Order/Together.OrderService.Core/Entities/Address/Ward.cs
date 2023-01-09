using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Together.OrderService.Core.Entities.Address
{
    public class Ward
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string NameEN { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string FullNameEN { get; set; } = default!;
        public string CodeName { get; set; } = default!;
        public string DistrictCode { get; set; } = default!;
        public int? AdministrativeUnitId { get; set; } = default!;
    }
}
