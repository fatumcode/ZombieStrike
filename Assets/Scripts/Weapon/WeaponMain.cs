using TMPro;
using UnityEngine;

public class WeaponMain : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 20f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] float bugFix = 0;
    [SerializeField] TextMeshProUGUI ammoText;

    // Crosshairs
    public bool showCrosshair = true;                   
    public Texture2D crosshairTexture;                  
    private int crosshairLength = 10;                   
    private int crosshairWidth = 4;                     
    private float startingCrosshairSize = 10.0f;        
    private float currentCrosshairSize;

    // Audio
    public AudioClip fireSound;
    public AudioClip reloadSound;

    // Other

    private void OnEnable()
    {
    }

    private void Start()
    {

        // Initialize the current crosshair size variable to the starting value specified by the user
        currentCrosshairSize = startingCrosshairSize;

        // Make sure crosshairTexture isn't null
        if (crosshairTexture == null)
            crosshairTexture = new Texture2D(0, 0);
    }
    void Update()
    {
        DisplayAmmo();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    private void Shoot()
    {
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMazzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceAmmo(ammoType);
            GetComponent<AudioSource>().PlayOneShot(fireSound);
        }
    }

    private void PlayMazzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
    }

    void OnGUI()
    {
        if (showCrosshair)
        {
            // Hold the location of the center of the screen in a variable
            Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);

            // Draw the crosshairs based on the weapon's inaccuracy
            // Left
            Rect leftRect = new Rect(center.x - crosshairLength - currentCrosshairSize, center.y - (crosshairWidth / 2), crosshairLength, crosshairWidth);
            GUI.DrawTexture(leftRect, crosshairTexture, ScaleMode.StretchToFill);
            // Right
            Rect rightRect = new Rect(center.x + currentCrosshairSize, center.y - (crosshairWidth / 2), crosshairLength, crosshairWidth);
            GUI.DrawTexture(rightRect, crosshairTexture, ScaleMode.StretchToFill);
            // Top
            Rect topRect = new Rect(center.x - (crosshairWidth / 2), center.y - crosshairLength - currentCrosshairSize, crosshairWidth, crosshairLength);
            GUI.DrawTexture(topRect, crosshairTexture, ScaleMode.StretchToFill);
            // Bottom
            Rect bottomRect = new Rect(center.x - (crosshairWidth / 2), center.y + currentCrosshairSize, crosshairWidth, crosshairLength);
            GUI.DrawTexture(bottomRect, crosshairTexture, ScaleMode.StretchToFill);
        }
    }
}
