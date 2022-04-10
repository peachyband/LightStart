using UnityEngine;
using AFPC;
using BigRookGames.Weapons;
using Features.Manager;
using Features.UIBehaviour;
using Plugins.AFPC.Scripts;

/// <summary>
/// Example of setup AFPC with Lifecycle, Movement and Overview classes.
/// </summary>
public class Hero : MonoBehaviour
{
    /* UI Reference */
    public HUD HUD;
    public float TimerValue;
    public GunfireController gunfireController;
    
    /* Lifecycle class. Damage, Heal, Death, Respawn... */
    public Lifecycle lifecycle;

    /* Movement class. Move, Jump, Run... */
    public Movement movement;

    /* Overview class. Look, Aim, Shake... */
    public Overview overview;
    
    public LevelManager LevelManager;

    public RecordKeeper RecordKeeper;
    /* Optional assign the HUD */
    private void Awake()
    {
        if (HUD)
        {
            HUD.hero = this;
        }
    }

    /* Some classes need to initizlize */
    private void Start()
    {
        /* a few apllication settings for more smooth. This is Optional. */
        QualitySettings.vSyncCount = 0;
        Cursor.lockState = CursorLockMode.Locked;

        /* Initialize lifecycle and add Damage FX */
        lifecycle.Initialize();
        lifecycle.AssignDamageAction(DamageFX);
        lifecycle.AssignHealAction(HealFX);
        lifecycle.BanHealthRecovery();
        
        /* Initialize movement and add camera shake when landing */
        movement.Initialize();
        movement.AssignLandingAction(() => overview.Shake(0.5f));
    }

    private void Update()
    {
        if (LevelManager.CaptureRecord) TimerValue += Time.deltaTime;
        LevelManager.FinishResult = TimerValue;
        /* Read player input before check availability */
        ReadInput();

        /* Block controller when unavailable */
        if (!lifecycle.Availability()) return;

        //HoldWeapon();
        
        /* Mouse look state */
        overview.Looking();

        /* Change camera FOV state */
        overview.Aiming();

        /* Shake camera state. Required "physical camera" mode on */
        overview.Shaking();

        /* Control the speed */
        movement.Running();

        /* Control the jumping, ground search... */
        movement.Jumping();

        /* Control the health and shield recovery */
        lifecycle.Runtime();
    }

    private void FixedUpdate()
    {
        
        /* Block controller when unavailable */
        if (!lifecycle.Availability()) return;

        /* Physical movement */
        movement.Accelerate();

        /* Physical rotation with camera */
        overview.RotateRigidbodyToLookDirection(movement.rb);
    }

    private void LateUpdate()
    {

        /* Block controller when unavailable */
        if (!lifecycle.Availability()) return;

        /* Camera following */
        overview.Follow(transform.position);
    }
    

    private void ReadInput () {
        //if (Input.GetKeyDown (KeyCode.R)) lifecycle.Damage(50);
        //if (Input.GetKeyDown (KeyCode.H)) lifecycle.Heal(50);
        //if (Input.GetKeyDown (KeyCode.T)) lifecycle.Respawn();
        //if (Input.GetMouseButton(0)) gunfireController.weaponShouldFire = true;
        overview.aimingInputValue = Input.GetMouseButton(1);
        overview.lookingInputValues.x = Input.GetAxis("Mouse X");
        overview.lookingInputValues.y = Input.GetAxis("Mouse Y");
        movement.movementInputValues.x = Input.GetAxis("Horizontal");
        movement.movementInputValues.y = Input.GetAxis("Vertical");
        movement.jumpingInputValue = Input.GetKeyDown(KeyCode.Space);
        movement.runningInputValue = Input.GetKeyDown(KeyCode.LeftShift);
    }
    
    public void ReactOnImpact(float impactForce, Vector3 direction){
        var impactVector = direction;
        impactVector *= impactForce * Mathf.Sin(Vector3.Angle(transform.up, direction)) / (0.5f * 70); 
        impactVector.Normalize();
        movement.Impact(impactVector);
    }

    public void GetDamage(float damage)
    {
        lifecycle.Damage(damage);
    }

    public void GetHeal(float healPoints)
    {
        lifecycle.Heal(healPoints);
    }
    
    private void DamageFX () {
        if (HUD) HUD.DamageFX();
        overview.Shake(0.75f);
    }

    private void HealFX()
    {
        if (HUD) HUD.HealFX();
    }
}
