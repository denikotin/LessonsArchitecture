using Assets.Scripts.Infrastructure.Services;
using System.Threading.Tasks;

namespace Assets.Scripts.UI.Services.Factory
{
    public interface IUIFactory:IService
    {
        void CreateShop();
        Task CreateUIRoot();
    }
}
