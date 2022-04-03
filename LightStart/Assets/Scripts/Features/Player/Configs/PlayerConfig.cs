using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float healthPoints;

    public float Speed => speed;
    public float JumpForce => jumpForce;
    public float HealthPoints => healthPoints;
}
