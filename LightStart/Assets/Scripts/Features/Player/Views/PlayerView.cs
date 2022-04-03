using Features.Config;
using UnityEngine;
using Zenject;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    private PlayerData _playerData;
    private LevelConfig _levelConfig;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [Inject]
    public void Construct(LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
    }

    public void AttachValues(PlayerData data)
    {
        _playerData = data;
    }
    
    private void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(move * Time.deltaTime * _playerData.Speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(_playerData.JumpForce * -3.0f * _levelConfig.GravityValue);
        }

        playerVelocity.y += _levelConfig.GravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
        
    }
}
