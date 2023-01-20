using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;

public interface IPersistentProgressService:  IService
{
    PlayerProgress Progress { get; set; }
}