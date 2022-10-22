using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtack : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] float damage = 25f;

    
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        if(target == null) return;
        target.TakeDamage(damage);
        GetComponent<EnemyMovement>().enabled = false;
    }

}