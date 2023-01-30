namespace Assets.Scripts.Infrastructure.Services
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;

        public static ServiceLocator Container => _instance ?? (_instance = new ServiceLocator());

        public void RegisterSingle<TService>(TService implementation) where TService: IService =>
            Implementaion<TService>.ServiceInstance = implementation;
        
        public TService Single<TService>() where TService : IService =>
            Implementaion<TService>.ServiceInstance;
        
        private static class Implementaion<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }

}
