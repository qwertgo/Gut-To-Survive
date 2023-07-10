using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MathExtention;

public class GravityHandler : MonoBehaviour
{
    [SerializeField] Transform camTransform;

    Vector2 newGravityDirection;


    public void StartGravityChange(float gravityAngle, Vector2 newGravityDirection)
    {
        float cameraRotation = camTransform.eulerAngles.z;

        //Keep Rotation inbetween 0 and 360
        cameraRotation = Modulo(cameraRotation, 360);
        gravityAngle = Modulo(gravityAngle, 360);


        if (AreCloseTogether(gravityAngle, cameraRotation, 1))
            return;

        GameEvents.prepareGravityChangeEvent.Invoke();
        GravityObject.gravityAngle = gravityAngle;
        this.newGravityDirection = newGravityDirection;

        StartCoroutine(rotateGravityOverTime());
    }

    IEnumerator rotateGravityOverTime()
    {
        float rotationStart = camTransform.eulerAngles.z;
        float rotationGoal = Vector2.SignedAngle(Vector2.down, newGravityDirection);
        float t = 0;

        while (t < 1)
        {
            float rotationZ = Mathf.LerpAngle(rotationStart, rotationGoal, t);
            camTransform.eulerAngles = new Vector3(0, 0, rotationZ);

            GravityObject.SetGravityDirection(Quaternion.Euler(0f, 0f, rotationZ) * Vector2.down);

            t += Time.deltaTime;
            yield return 0;
        }

        camTransform.eulerAngles = new Vector3(0, 0, rotationGoal);
        GravityObject.SetGravityDirection(newGravityDirection);

        GameEvents.gravityChangedEvent.Invoke();
    }
}
