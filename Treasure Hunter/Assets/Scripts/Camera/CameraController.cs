using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target;
    Vector3 velocity = Vector3.zero;

    [Range(0f, 1f)]
    public float smoothTime;

    public Vector3 positionOffset;

    [Header("Axis Limitation")]
    public Vector2 minAxis; // minimum axis limitation (Batas Minimal Kamera untuk X dan Y Axis)
    public Vector2 maxAxis; // maximum axis limitation (Batas Maksimal Kamera untuk X dan Y Axis)

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + positionOffset;
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, minAxis.x, maxAxis.x), Mathf.Clamp(targetPosition.y, minAxis.y, maxAxis.y), targetPosition.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
