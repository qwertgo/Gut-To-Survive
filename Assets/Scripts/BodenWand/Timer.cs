using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
   public float time =10.0f;
   public PlatformSwitch ps;    
   public float TimeLeft;


    void Update()
    {
          time -= Time.deltaTime;
      }

    


    public void StartTimer()
    {
        if (time <= 0.0f)
        {
          ps.timerEnded();
        }
    }
}
