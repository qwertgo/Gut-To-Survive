using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static MathExtention;

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
        float ownAgnle = Vector2.SignedAngle(Vector2.down, direction);
        ownAgnle = Modulo(ownAgnle, 360);

        float gravAngle = Modulo(GravityObject.gravityAngle, 360);

        if (AreCloseTogether(ownAgnle, gravAngle, 5))
            l.intensity = 1;
        else
            l.intensity = 0;
    }

    
}
