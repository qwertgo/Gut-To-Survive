using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameEvents 
{
    public static UnityEvent HitSavePoint = new UnityEvent();
    public static UnityEvent Respawn = new UnityEvent();

    public static UnityEvent gravityChangedEvent = new UnityEvent();
    public static UnityEvent prepareGravityChangeEvent = new UnityEvent();
}
