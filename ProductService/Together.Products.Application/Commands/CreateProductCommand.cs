using MediatR;
using Together.Products.Application.Responses;

namespace Together.Products.Application.Commands
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public float Amount { get; set; }
    }
}
