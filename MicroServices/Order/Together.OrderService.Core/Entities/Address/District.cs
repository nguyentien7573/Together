using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Together.OrderService.Core.Entities.Address
{
    public class District
    {
        public string Code { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public string NameEN { get; private set; } = default!;
        public string FullName { get; private set; } = default!;
        public string FullNameEN { get; private set; } = default!;
        public string CodeName { get; private set; } = default!;
        public string ProvinceCode { get; private set; } = default!;
        public int? AdministrativeUnitId { get; private set; }
    }
}
