using System.Threading.Tasks;

namespace Xlent.Lever.Libraries2.MoveTo.Core.Mapping
{
    public interface IMapper<TModel, in TLogic>
    {
        Task MapFromAsync(TModel source, TLogic logic);

        Task<TModel> CreateAndMapTo(TLogic logic);
    }
}
