using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using repos11.BusinessLogic.Dtos.Systems;
using repos11.BusinessLogic.Extension;
using repos11.BusinessLogic.Systems.Interfaces;
using repos11.Repository.Entity.Systems;
using repos11.Repository.Systems.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace repos11.BusinessLogic.Systems
{
    public class SystemActionsBusinessLogic : BusinessLogic<SystemActionsDto, SystemActions>, ISystemActionsBusinessLogic
    {
        private ISystemActionsRepository _repo;

        public SystemActionsBusinessLogic(ISystemActionsRepository repo)
        {
            _repo = repo;
        }

        public async Task<SystemActionsDto> Get(long Id)
        {
            var entity = await _repo.Get(Id);

            return Mapper.Map<SystemActionsDto>(entity);
        }

        public SystemActionsDto Get(string ctrl, string action)
        {
            var entity = _repo.FindByCondition(w => w.Controller == ctrl && w.Action == action).FirstOrDefault();

            return Mapper.Map<SystemActionsDto>(entity);
        }

        public async Task<SystemActionsDto> GetAsync(string ctrl, string action)
        {
            var entity = await _repo.FindByCondition(w => w.Controller == ctrl && w.Action == action).FirstOrDefaultAsync();

            return Mapper.Map<SystemActionsDto>(entity);
        }

        public async Task<List<SystemActionsDto>> GetAll()
        {
            var entities = await _repo.GetAll().ToListAsync();

            return Mapper.Map<List<SystemActionsDto>>(entities);
        }

        public async Task<LoadResult> GetAll(DataSourceLoadOptions loadOptions)
        {
            var query = Mapper.ProjectTo<SystemActionsDto>(_repo.GetAll());

            return await DataSourceLoader.LoadAsync(query, loadOptions); ;
        }

        public async Task<SystemActionsDto> Save(SystemActionsDto model, long UserId)
        {
            var entity = await _repo.Get(model.Id);
            if (entity == null)
            {
                entity = Mapper.Map<SystemActions>(model);
                await _repo.Add(entity, UserId);
            }
            else
            {
                model.CreatedBy = entity.CreatedBy;
                model.CreatedDate = entity.CreatedDate;
                entity = Mapper.Map<SystemActions>(model);
                await _repo.Update(entity, UserId);
            }

            return Mapper.Map<SystemActionsDto>(entity);
        }

        public async Task SaveBatch(List<SystemActionsDto> items, long UserId)
        {
            foreach (var item in items)
            {
                var entity = await _repo.FindByCondition(w => w.Controller == item.Controller && w.Action == item.Action).FirstOrDefaultAsync();
                if (entity == null)
                {
                    entity = Mapper.Map<SystemActions>(item);
                    await _repo.Add(entity, UserId);
                }
                else
                {
                    entity.Description = item.Description;
                    await _repo.Update(entity, UserId);
                }
            }
        }

        public async Task<SystemActionsDto> Delete(long Id, long UserId)
        {
            var entity = await _repo.Delete(Id, UserId);

            return Mapper.Map<SystemActionsDto>(entity);
        }

        public async Task<int> DeleteForce(long Id)
        {
            return await _repo.ForceDelete(Id);
        }
    }
}
