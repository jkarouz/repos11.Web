using DevExtreme.AspNet.Data.ResponseModel;
using repos11.BusinessLogic.Extension;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace repos11.BusinessLogic
{
    public interface IBusinessLogic<ModelDto>
    {
        Task<ModelDto> Save(ModelDto model, long UserId);
        Task<ModelDto> Get(long Id);
        Task<List<ModelDto>> GetAll();
        Task<LoadResult> GetAll(DataSourceLoadOptions loadOptions);
        Task<ModelDto> Delete(long Id, long UserId);
        Task<int> DeleteForce(long Id);
    }
}
