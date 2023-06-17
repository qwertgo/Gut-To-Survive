using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] GravityHandler gravityhandler;

    //public void StartGravityChange(Vector2 gravityDirection, UnityEvent gravityChangedEvent, PlayerController pController)
    //{
    //    float rotationZ = Vector2.SignedAngle(Vector2.down, gravityDirection);
    //    pController.enabled = false;
    //    if(rotationZ != transform.eulerAngles.z)
    //    {
    //        StartCoroutine(ChangeRotationOverTime(transform.eulerAngles.z, rotationZ, gravityChangedEvent));
    //        pController.enabled = false;
    //    }
    //}

    public IEnumerator ChangeRotationOverTime( float rotationEnd)
    {
        
        float rotationStart = transform.eulerAngles.z;
        float t = 0;
        while(t < 1)
        {
            float rotationZ = Mathf.LerpAngle(rotationStart, rotationEnd, t);

            Vector3 rot = transform.eulerAngles;
            transform.eulerAngles = new Vector3(rot.x, rot.y, rotationZ);

            t += Time.fixedDeltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        gravityhandler.CameraFinishedRotating();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
    }
}
