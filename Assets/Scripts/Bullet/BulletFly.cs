using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float disappearDelay = 5f;

    [SerializeField] string exceptionTag;

    [SerializeField] GameObject disappearParticleObject;
    [SerializeField] ParticleSystem disappearParticle;
    private Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(bulletSpeed * Time.deltaTime,0,0);
        Invoke("Disappear", disappearDelay);
    }

    void Disappear()
    {
        bulletSpeed = 0;
        disappearParticle.Play();
        disappearParticleObject.transform.SetParent(null);
        Destroy(gameObject);
        Destroy(disappearParticleObject, disappearDelay);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag(exceptionTag)) {
            Disappear();
        }
    }
}