using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityHandler : MonoBehaviour
{
    [SerializeField] CameraController cam;
    [SerializeField] Transform camTransform;

    UnityEvent gravityChangedEvent;


    public void StartGravityChange(float gravityAngle,UnityEvent prepareGravityChangeEvent, UnityEvent gravityChangedEvent)
    {
        float cameraRotation = camTransform.eulerAngles.z;

        //Keep Rotation inbetween 0 and 360
        cameraRotation = Modulo(cameraRotation, 360);
        gravityAngle = Modulo(gravityAngle, 360);


        if (Mathf.RoundToInt(gravityAngle) == Mathf.RoundToInt(cameraRotation))
            return;

        prepareGravityChangeEvent.Invoke();
        GravityObject.gravityAngle = gravityAngle;

        this.gravityChangedEvent = gravityChangedEvent;

        StartCoroutine(cam.ChangeRotationOverTime(gravityAngle, this));
    }

    public void CameraFinishedRotating()
    {
        gravityChangedEvent.Invoke();
    }

    float Modulo(float a, float n)
    {
        return ((a % n) + n) % n;
    }
}
