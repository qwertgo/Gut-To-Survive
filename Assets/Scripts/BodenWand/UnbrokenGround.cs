using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;


public class UnbrokenGround : MonoBehaviour
{
  [SerializeField] GameObject brokenGroundObj;
  [SerializeField] float timePlayerCanStandOnPlattform;

  public Quaternion originRotation; 
  public Vector3 originPosition; 
  public float shake_decay = 0.002f;
	public float shake_intensity = .3f;

	private float temp_shake_intensity = 0;


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
    
      Debug.Log("test");

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
  
 /*void Update (){
		if (temp_shake_intensity > 0){
			transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
			transform.rotation = new Quaternion(
				originRotation.x + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
				originRotation.y + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
				originRotation.z + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
				originRotation.w + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f);
			temp_shake_intensity -= shake_decay;
		}
	}
	
	public void Shake()
  {
    
		originPosition = transform.position;
		originRotation = transform.rotation;
		temp_shake_intensity = shake_intensity;
    
  }*/
}
