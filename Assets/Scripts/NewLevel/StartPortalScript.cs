using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPortalScript : MonoBehaviour
{

    [SerializeField] Vector2 forcePush;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D (Collision2D other)
    {
        other.gameObject.GetComponent<Rigidbody2D>().AddForce(forcePush * Time.deltaTime);

        if (other.gameObject.CompareTag("Transport"))
        {
            other.gameObject.transform.DetachChildren();
            Destroy(other.gameObject);
        }

    }
}
