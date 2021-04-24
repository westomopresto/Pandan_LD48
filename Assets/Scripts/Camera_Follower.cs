using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follower : MonoBehaviour
{

    public Transform target;
    public float cameraRotationRate = 180;
    public float cameraDistance = 10;
    public float cameraHeightOffset = 0.9f;

    [Header("Tilt Limit")]
    public float tiltLimit = 15;
    public float tiltLimitEnd = 50;

    private float tilt = 15;

    void LateUpdate()
    {
        float newtilt = tilt * cameraRotationRate;

        tilt = Mathf.Lerp(tilt, newtilt, Time.deltaTime * 0.5f);
        tilt = Mathf.Clamp(tilt, tiltLimit, tiltLimitEnd);

        transform.rotation = Quaternion.Euler(new Vector3(tilt, 0f, 0f));

        transform.position = target.position - transform.forward * cameraDistance + Vector3.up*cameraHeightOffset;
    }
}
