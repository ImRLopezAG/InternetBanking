using AutoMapper;
using ITBanking.Core.Application.Contracts.Core;
using ITBanking.Core.Application.Core.Models;
using ITBanking.Core.Domain.Core;

namespace ITBanking.Core.Application.Core;

public class GenericService<EntityVm, SaveEntityVm, Entity> : IGenericService<EntityVm, SaveEntityVm, Entity> where EntityVm : BaseVm where SaveEntityVm : BaseVm where Entity : BaseEntity {
  private readonly IGenericRepository<Entity> _repository;
  private readonly IMapper _mapper;

  public GenericService(IGenericRepository<Entity> repository, IMapper mapper) {
    _repository = repository;
    _mapper = mapper;
  }

  public virtual async Task<IEnumerable<EntityVm>> GetAll() {
    var query = from entity in await _repository.GetAll()
                select _mapper.Map<EntityVm>(entity);

    return query.ToList();
  }

  public virtual async Task<EntityVm> GetById(int id) {
    var entity = await _repository.GetEntity(id);
    return _mapper.Map<EntityVm>(entity);
  }

  public virtual async Task<SaveEntityVm> GetEntity(int id) {
    var entity = await _repository.GetEntity(id);
    return _mapper.Map<SaveEntityVm>(entity);
  }

  public virtual async Task<SaveEntityVm> Save(SaveEntityVm vm) {
    try {
      var entity = _mapper.Map<Entity>(vm);
      await _repository.Save(entity);
      return _mapper.Map<SaveEntityVm>(entity);
    } catch (Exception ex) {
      vm.HasError = true;
      vm.Error = ex.Message;
      return vm;
    }
  }

  public virtual async Task Edit(SaveEntityVm vm) {
    var entity = _mapper.Map<Entity>(vm);
    await _repository.Update(entity);
  }

  public virtual async Task Delete(int id) {
    try {
      var entity = await _repository.GetEntity(id);
      await _repository.Delete(entity);
    } catch (Exception ex) {
      throw new Exception(ex.Message);
    }
  }
}
