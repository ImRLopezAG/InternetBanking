using ITBanking.Core.Application.Core.Models;

namespace ITBanking.Core.Application.Core;

public interface IBaseService<EntityVm> where EntityVm : BaseVm {
  Task<IEnumerable<EntityVm>> GetAll();
  Task<EntityVm> GetById(int id);
}
