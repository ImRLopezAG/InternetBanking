using ITBanking.Core.Application.Core.Models;
using ITBanking.Core.Application.Core;
using ITBanking.Core.Domain.Core;

namespace ITBanking.Core.Application.Contracts.Core;
public interface IGenericService<EntityVm, SaveEntityVm, Entity> : IBaseService<EntityVm> where EntityVm : BaseVm where SaveEntityVm : BaseVm where Entity : BaseEntity {
  Task<SaveEntityVm> GetEntity(int id);
  Task<SaveEntityVm> Save(SaveEntityVm vm);
  Task Edit(SaveEntityVm vm);
  Task Delete(int id);
}
