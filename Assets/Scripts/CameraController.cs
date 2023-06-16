using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : GravityObject
{

    [SerializeField] Transform player;

    private void Start()
    {
        if (gravityChangedEvent == null)
            gravityChangedEvent = new UnityEngine.Events.UnityEvent();

        gravityChangedEvent.AddListener(ChangeRotation);
    }

    void ChangeRotation()
    {
        float rotationZ = Vector2.SignedAngle(Vector2.down, gravityDirection);
        StartCoroutine(ChangeRotationOverTime(transform.eulerAngles.z, rotationZ));
        //Vector3 rot = transform.eulerAngles;
        //transform.eulerAngles = new Vector3(rot.x, rot.y, rotationZ);
    }

    IEnumerator ChangeRotationOverTime(float rotationStart, float rotationEnd)
    {
        float t = 0;
        while(t < 1)
        {
            float rotationZ = Mathf.LerpAngle(rotationStart, rotationEnd, t);

            Vector3 rot = transform.eulerAngles;
            transform.eulerAngles = new Vector3(rot.x, rot.y, rotationZ);

            t += Time.fixedDeltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
    }
}
