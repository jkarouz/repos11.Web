using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using repos11.BusinessLogic.Dtos.Systems;
using repos11.BusinessLogic.Dtos.UserManagement;
using repos11.BusinessLogic.Extension;
using repos11.BusinessLogic.UserManagement.Interfaces;
using repos11.Repository.Entity.Function;
using repos11.Repository.Entity.UserManagement;
using repos11.Repository.UserManagement.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace repos11.BusinessLogic.UserManagement
{
    public class UsersBusinessLogic : BusinessLogic<UsersDto, Users>, IUsersBusinessLogic
    {
        private IUsersRepository _repo;

        public UsersBusinessLogic(IUsersRepository repo)
        {
            _repo = repo;
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UsersDto, Users>().ReverseMap();
                cfg.CreateMap<FnGetSystemActionModel, FnGetSystemActionDto>().ReverseMap();
            }).CreateMapper();
        }

        public async Task<UsersDto> Get(long Id)
        {
            var entity = await _repo.Get(Id);

            return Mapper.Map<UsersDto>(entity);
        }

        public async Task<UsersDto> GetByUserName(string username)
        {
            var entity = await _repo.GetByUserName(username);

            return Mapper.Map<UsersDto>(entity);
        }

        public async Task<List<string>> GetRoles(long UserId)
        {
            return await _repo.GetRoles(UserId).ToListAsync();
        }

        public async Task<List<UsersDto>> GetAll()
        {
            var entities = await _repo.GetAll().ToListAsync();

            return Mapper.Map<List<UsersDto>>(entities);
        }

        public async Task<LoadResult> GetAll(DataSourceLoadOptions loadOptions)
        {
            var query = Mapper.ProjectTo<UsersDto>(_repo.GetAll());

            return await DataSourceLoader.LoadAsync(query, loadOptions); ;
        }

        public async Task<LoadResult> FnGetUsers(long UserId, DataSourceLoadOptions loadOptions)
        {
            var query = _repo.FnGetUsers(UserId);
            return await Task.Run(() => { return DataSourceLoader.Load(query, loadOptions); });
        }

        public async Task<List<FnGetSystemActionDto>> GetSystemActionByUser(long UserId)
        {
            var entities = await _repo.GetRolePermission(UserId).ToListAsync();

            return Mapper.Map<List<FnGetSystemActionDto>>(entities);
        }

        public async Task<int> FnUsersCount(long UserId)
        {
            return await _repo.FnUsersCount(UserId);
        }

        public async Task<IEnumerable> SPGetUsers(long UserId)
        {
            return (await _repo.SPGetUsers(UserId)).ToList();
        }

        public async Task<int> SPUsersCount(long UserId)
        {
            return await _repo.SPUsersCount(UserId);
        }

        public async Task<int> SPInsertUser(long UserId)
        {
            return await _repo.SPInsertUser(UserId);
        }

        public async Task<UsersDto> Save(UsersDto model, long UserId)
        {
            var entity = await _repo.Get(model.Id);
            if (entity == null)
            {
                entity = Mapper.Map<Users>(model);
                await _repo.Add(entity, UserId);
            }
            else
            {
                model.CreatedBy = entity.CreatedBy;
                model.CreatedDate = entity.CreatedDate;
                entity = Mapper.Map<Users>(model);
                await _repo.Update(entity, UserId);
            }

            return Mapper.Map<UsersDto>(entity);
        }

        public async Task<UsersDto> Delete(long Id, long UserId)
        {
            var entity = await _repo.Delete(Id, UserId);

            return Mapper.Map<UsersDto>(entity);
        }

        public async Task<int> DeleteForce(long Id)
        {
            return await _repo.ForceDelete(Id);
        }
    }
}
