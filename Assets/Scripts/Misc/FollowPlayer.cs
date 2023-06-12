using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = player.transform.position.x + xOffset;
        float yPos = player.transform.position.y + yOffset;
        
        transform.position = new Vector2(xPos, yPos);
    }
}
