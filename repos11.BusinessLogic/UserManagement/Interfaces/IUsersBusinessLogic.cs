using DevExtreme.AspNet.Data.ResponseModel;
using repos11.BusinessLogic.Dtos.Systems;
using repos11.BusinessLogic.Dtos.UserManagement;
using repos11.BusinessLogic.Extension;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace repos11.BusinessLogic.UserManagement.Interfaces
{
    public interface IUsersBusinessLogic : IBusinessLogic<UsersDto>
    {
        Task<UsersDto> GetByUserName(string username);
        Task<List<string>> GetRoles(long UserId);
        Task<List<FnGetSystemActionDto>> GetSystemActionByUser(long UserId);
        Task<LoadResult> FnGetUsers(long UserId, DataSourceLoadOptions loadOptions);
        Task<int> FnUsersCount(long UserId);
        Task<IEnumerable> SPGetUsers(long UserId);
        Task<int> SPUsersCount(long UserId);
        Task<int> SPInsertUser(long UserId);
    }
}
