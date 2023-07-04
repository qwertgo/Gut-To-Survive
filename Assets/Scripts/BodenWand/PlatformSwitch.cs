using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitch : MonoBehaviour
{
     public GameObject unbroken;
    public GameObject broken;

  bool GroundCheck()
    {
        if(Physics2D.Raycast(transform.position, Vector2.down, 0.86f, ~LayerMask.GetMask("Player")))
        {
            return true;
           
        }

        else
        {
            return false;
        }
    }
    private void OnCollisionExit2D( Collision2D collision)
    {
    if(collision.gameObject.CompareTag("Player"))
    {
      if(GroundCheck()==true)
      unbroken.SetActive(false);
      broken.SetActive(true);
     
      
    }
    }

    
}
