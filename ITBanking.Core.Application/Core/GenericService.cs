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

  public async Task<IEnumerable<EntityVm>> GetAll() {
    var query = from entity in await _repository.GetAll() 
                select _mapper.Map<EntityVm>(entity);

    return query.ToList();
  }

  public async Task<EntityVm> GetById(int id) {
    var entity = await _repository.GetEntity(id);
    return _mapper.Map<EntityVm>(entity);
  }

  public async Task<SaveEntityVm> GetEntity(int id) {
    var entity = await _repository.GetEntity(id);
    return _mapper.Map<SaveEntityVm>(entity);
  }

  public async Task<SaveEntityVm> Save(SaveEntityVm vm) {
    var entity = _mapper.Map<Entity>(vm);
    await _repository.Save(entity);
    return _mapper.Map<SaveEntityVm>(entity);
  }

  public async Task Edit(SaveEntityVm vm) {
    var entity = _mapper.Map<Entity>(vm);
    await _repository.Update(entity);
  }

  public async Task Delete(int id) {
    try {
      var entity = await _repository.GetEntity(id);
      await _repository.Delete(entity);
    } catch (Exception ex) {
      throw new Exception(ex.Message);
    }
  }
}
