using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    DisplayDamage displayDamage;

    private void Start()
    {
        displayDamage = GetComponent<DisplayDamage>();
    }
    private void Update()
    {
        Death();
    }


    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        displayDamage.DamageDisplay();
    }

    void Death()
    {
        
        if (hitPoints <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
            FindObjectOfType<WeaponMain>().showCrosshair = false;
        }
    }

}
