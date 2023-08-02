using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;


public class BrokenGround : MonoBehaviour
{
  [SerializeField] List<Rigidbody2D> bodyParts;




  void Start()
  {
    
    for(int i = 0; i < bodyParts.Count; i++)
    {
      bodyParts[i].AddTorque(200);
      
    }
    //Debug.Log(bodyParts[0].angularVelocity);

    StartCoroutine(DestroyAfterTime());
   
  }

  IEnumerator DestroyAfterTime()
  {
    yield return new WaitForSeconds(2);
    Destroy(gameObject);
  }


}