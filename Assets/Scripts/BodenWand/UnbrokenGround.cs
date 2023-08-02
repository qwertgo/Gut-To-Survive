using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;


public class UnbrokenGround : MonoBehaviour
{
  [SerializeField] GameObject brokenGroundObj;
  [SerializeField] float timePlayerCanStandOnPlattform;




  void Start()
  {
    GameEvents.Respawn.AddListener(Activate);
  }

  	
  
 public void OnCollisionExit2D(Collision2D collision)
  {
    if(collision.gameObject.CompareTag("Player"))
    {       
      StopAllCoroutines();
      Deactivate();
    }
  }

  public void OnCollisionEnter2D(Collision2D coll)
  { 
    if(coll.gameObject.CompareTag("Player"))
    {
      StartCoroutine(WaitToDestroyYourself());
    
      //Debug.Log("test");

    }   
  }

  IEnumerator WaitToDestroyYourself()
  {
    float timeElapsed = 0;
    while(timeElapsed < timePlayerCanStandOnPlattform)
    {
        timeElapsed += Time.deltaTime;
        yield return 0;
    }
    
    Deactivate();

  }

  void Activate()
  {
    gameObject.SetActive(true);
  }

  void Deactivate()
  {
    if(gameObject.activeInHierarchy){
      Instantiate(brokenGroundObj, transform.position, Quaternion.identity);
      GetComponentInParent<AudioSource>().Play();
      gameObject.SetActive(false);
    }

  }

}
