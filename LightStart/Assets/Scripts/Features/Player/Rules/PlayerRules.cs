using System;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerRules : ITickable, IDisposable
{
    private readonly CompositeDisposable _compositeDisposable;
    private PlayerFactory _playerFactory;
    private PlayerConfig _playerConfig;
    public bool PlayerShouldSpawn = true;

    public PlayerRules(PlayerFactory playerFactory, PlayerConfig playerConfig)
    {
        _playerFactory = playerFactory;
        _playerConfig = playerConfig;
            
        _compositeDisposable = new CompositeDisposable();
    }

    public void Tick()
    {
        if (!PlayerShouldSpawn) return;

        var data = new PlayerData(_playerConfig.Speed, _playerConfig.JumpForce, _playerConfig.HealthPoints, Vector3.zero);
        //Spawn player
        _playerFactory.Create(data);
        PlayerShouldSpawn = false;
    }
        
    public void Dispose()
    {
        _compositeDisposable?.Dispose();
    }
}
