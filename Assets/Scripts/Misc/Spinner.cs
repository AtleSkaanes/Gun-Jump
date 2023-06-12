using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float xRotate = 0f;
    [SerializeField] float yRotate = 0f;
    [SerializeField] float zRotate = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 
        transform.Rotate(xRotate*Time.deltaTime, yRotate*Time.deltaTime, zRotate*Time.deltaTime);
    }
}
