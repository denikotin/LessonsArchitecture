using Assets.Scripts.Data.PlayerProgressFolder;
using Assets.Scripts.Infrastructure.Services;

public interface IPersistentProgressService:  IService
{
    PlayerProgress Progress { get; set; }
}