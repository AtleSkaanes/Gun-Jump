using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{ 
    [SerializeField] float shootDelay;
    float shootCooldown;

    [SerializeField] GameObject enemyBulletObject;
    [SerializeField] Transform barrelEnd;
    

    // Start is called before the first frame update
    void Start()
    {
        shootCooldown = shootDelay;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        CooldownDeplete();
    }

    void Shoot()
    {
        if (shootCooldown < Mathf.Epsilon)
        {
            shootCooldown = shootDelay;
            Instantiate(enemyBulletObject, barrelEnd.position, barrelEnd.rotation); // shoot bullet
        }
    }

    void CooldownDeplete()
    {
        shootCooldown = shootCooldown - 1 * Time.deltaTime;
    }
}
