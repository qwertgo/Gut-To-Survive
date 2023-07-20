using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;


public class PlatformSwitch : MonoBehaviour
{
  public GameObject unbroken;
  public GameObject broken;
  public Timer time;
  public Break breakable;
  public GameObject Ground;

void Start()
{

time.enabled = false;
}

  bool GroundCheck()
  {
    if(Physics2D.Raycast(transform.position, Vector2.down, 1.1f, ~LayerMask.GetMask("Player")))
    {
      return true;
    }

    else
    {
      return false;
    }
  }
  	
  
 public void OnCollisionExit2D(Collision2D collision)
  {
    if(collision.gameObject.CompareTag("Player"))
    {
      if(GroundCheck()==true)
      {
        unbroken.SetActive(false);
        broken.SetActive(true);
        Destroy(Ground);   
      }
    }
  }

  public void OnCollisionEnter2D(Collision2D coll)
  { 
    if(coll.gameObject.CompareTag("Player"))
    {
      time.enabled= true;
      time.Update();
    }   
  }

  public void timerEnded()
  {
    
    unbroken.SetActive(false);
    broken.SetActive(true);
    breakable.Fade();

  }
}
