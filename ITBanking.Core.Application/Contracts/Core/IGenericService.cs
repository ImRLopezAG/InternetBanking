using ITBanking.Core.Application.Core.Models;
using ITBanking.Core.Application.Core;
using ITBanking.Core.Domain.Core;

namespace ITBanking.Core.Application.Contracts.Core;
public interface IGenericService<EntityVm, SaveEntityVm, Entity> : IBaseService where EntityVm : BaseVm where SaveEntityVm : class where Entity : BaseEntity {
  Task<SaveEntityVm> Save(SaveEntityVm vm);
  Task Edit(SaveEntityVm vm);
  Task Delete(int id);
  Task<SaveEntityVm> GetEntity(int id);
}
