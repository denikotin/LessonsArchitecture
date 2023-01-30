namespace Assets.Scripts.Infrastructure.Services.RandomService
{
    public interface IRandomService:IService
    {
        public int Next(int min, int max);
    }
}
