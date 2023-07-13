using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
   public float time =2.0f;
   public PlatformSwitch ps;    
   public float timeLeft;


 public  void Update()
  {
      timeLeft = time-Time.deltaTime;
  }
    
    public void StartTimer()
    {
       
        Debug.Log(timeLeft);
        
        if (timeLeft <= 0.0f)
        {
          ps.timerEnded();
        }
    }
}
