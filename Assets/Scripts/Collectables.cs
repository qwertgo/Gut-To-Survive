using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public PlayerController pc;
    
        

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        
        

        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {  
            
            Destroy(gameObject);
          
        }
    }
}
   
