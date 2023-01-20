using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;
using UnityEngine;

public class PersistentProgressService : IPersistentProgressService
{
    public PlayerProgress Progress { get; set; }
}
