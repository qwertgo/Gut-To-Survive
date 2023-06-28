using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityHandler : MonoBehaviour
{
    public bool motionSicknessSafeMode = false;
    [SerializeField] CameraController cam;
    [SerializeField] Transform camTransform;

    UnityEvent gravityChangedEvent;


    public void StartGravityChange(PlayerController player, float gravityAngle, UnityEvent gravityChangedEvent, Rigidbody2D playerRb)
    {
        float cameraRotation = camTransform.eulerAngles.z;

        //Keep Rotation inbetween 0 and 360
        cameraRotation = Modulo(cameraRotation, 360);
        gravityAngle = Modulo(gravityAngle, 360);


        if (Mathf.RoundToInt(gravityAngle) == Mathf.RoundToInt(cameraRotation))
            return;

        player.StopAllCoroutines();
        GravityObject.gravityAngle = gravityAngle;

        if (!motionSicknessSafeMode)
        {
            player.isSleeping = true;
            playerRb.velocity = Vector2.zero;

            this.gravityChangedEvent = gravityChangedEvent;

            StartCoroutine(cam.ChangeRotationOverTime(gravityAngle));
        }
        else
        {
            gravityChangedEvent.Invoke();
        }
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
