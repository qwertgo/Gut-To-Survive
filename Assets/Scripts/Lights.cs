using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lights : MonoBehaviour
{
    [SerializeField] Vector2 direction;
    [SerializeField] Light2D l;

    private void Start()
    {
        GameEvents.gravityChangedEvent.AddListener(ChangedGravity);
    }

    void ChangedGravity()
    {
        if (Vector2.SignedAngle(Vector2.down, direction) == GravityObject.gravityAngle)
            l.intensity = 1;
        else
            l.intensity = 0;

    }

    
}
