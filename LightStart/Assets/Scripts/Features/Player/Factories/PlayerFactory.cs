using Features.Player.Data;
using UnityEngine;
using Zenject;

public struct PlayerData
{
    public float Speed { get; }
    public float JumpForce { get; }
    public float HealthPoints { get; }
    
    public Vector3 Position { get; }
    
    public PlayerData(float speed, float jumpForce, float healthPoints, Vector3 position)
    {
        Speed = speed;
        JumpForce = jumpForce;
        HealthPoints = healthPoints;
        Position = position;
    }
}

public class PlayerFactory : IFactory<PlayerData, PlayerView>
{
    private readonly DiContainer _container;

    public PlayerFactory(DiContainer container)
    {
        _container = container;
    }

    public PlayerView Create(PlayerData data)
    {
        var newPlayer =
            _container
                .InstantiatePrefabResourceForComponent<PlayerView>(
                    PlayerResources.PlayerPrefab);
        Debug.Log("[GreeterFactory] Creating Player");
        newPlayer.enabled = true;
        newPlayer.AttachValues(data);
        
        return newPlayer;
    }
}
