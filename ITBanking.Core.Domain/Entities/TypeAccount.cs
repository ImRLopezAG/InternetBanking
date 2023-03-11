using ITBanking.Core.Domain.Core;

namespace ITBanking.Core.Domain.Entities
{
    public class TypeAccount : BaseTypeEntity
    {
        public ICollection<Product> Products { get; set; } = null!;
        public ICollection<Card> Cards { get; set; } = null!;


    }
}
