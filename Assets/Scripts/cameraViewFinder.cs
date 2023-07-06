using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraViewFinder : GravityObject
{
    [SerializeField] float upwardOffset;
    [SerializeField] Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2)player.transform.position + -gravityDirection * upwardOffset; 
    }
}
