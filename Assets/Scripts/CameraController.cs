using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform playerPivotTransform;


    public IEnumerator ChangeRotationOverTime( float rotateTowards, GravityHandler gravityhandler)
    {
        
        float rotationStart = transform.eulerAngles.z;
        float t = 0;
        Vector3 rot = transform.eulerAngles;

        while (t < 1)
        {
            float rotationZ = Mathf.LerpAngle(rotationStart, rotateTowards, t);
            transform.eulerAngles = new Vector3(rot.x, rot.y, rotationZ);
            playerPivotTransform.eulerAngles = new Vector3(0, 0, rotationZ);

            t += Time.fixedDeltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        transform.eulerAngles = new Vector3(rot.x, rot.y, rotateTowards);
        gravityhandler.CameraFinishedRotating();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = playerTransform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
    }
}
