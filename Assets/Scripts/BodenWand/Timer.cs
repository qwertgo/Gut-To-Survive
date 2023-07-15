using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
  public float time;
  public PlatformSwitch ps;   


  public void Update()
  {
    time -= Time.deltaTime;
    if (time <= 0.0f)
    {
      ps.timerEnded();
      
    }
  }
}