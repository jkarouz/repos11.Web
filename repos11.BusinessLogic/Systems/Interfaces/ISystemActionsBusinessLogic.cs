using repos11.BusinessLogic.Dtos.Systems;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace repos11.BusinessLogic.Systems.Interfaces
{
    public interface ISystemActionsBusinessLogic : IBusinessLogic<SystemActionsDto>
    {
        Task SaveBatch(List<SystemActionsDto> items, long UserId);
        SystemActionsDto Get(string ctrl, string action);
        Task<SystemActionsDto> GetAsync(string ctrl, string action);
    }
}
