using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        

        //if(collision.gameObject.layer == LayerMask.GetMask("Collectable"))
        if(collision.gameObject.CompareTag("Collectable"))
        {  
        Destroy(collision.gameObject);
    }}
}
