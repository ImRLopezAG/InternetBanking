using ITBanking.Core.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITBanking.Core.Domain.Entities
{
    public class Beneficiary:BaseEntity
    {
        public string UserId { get; set; } = null!;
        public string? BAccount { get; set; }
    }
}
