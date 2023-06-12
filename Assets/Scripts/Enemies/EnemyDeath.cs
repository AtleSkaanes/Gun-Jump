using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] float disappearDelay;

    bool isDead = false;

    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] GameObject deathParticleObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Die();
        }

    }

    void Die()
    {
        if (!isDead)
        {
            isDead = false;
            deathParticles.Play();
            deathParticleObject.transform.SetParent(null);
            Destroy(gameObject);
            Destroy(deathParticleObject, disappearDelay);
        }
    }
}