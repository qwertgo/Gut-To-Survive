using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{

    [SerializeField] float yOffset;

    [SerializeField] Transform player;
    [SerializeField] GravityHandler gravityhandler;


    public IEnumerator ChangeRotationOverTime( float rotateTowards)
    {
        
        float rotationStart = transform.eulerAngles.z;
        float t = 0;
        Vector3 rot = transform.eulerAngles;

        while (t < 1)
        {
            float rotationZ = Mathf.LerpAngle(rotationStart, rotateTowards, t);
            transform.eulerAngles = new Vector3(rot.x, rot.y, rotationZ);

            t += Time.fixedDeltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        transform.eulerAngles = new Vector3(rot.x, rot.y, rotateTowards);
        gravityhandler.CameraFinishedRotating();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.position;
        transform.position = new Vector3(playerPos.x, playerPos.y + yOffset, transform.position.z);
    }
}
