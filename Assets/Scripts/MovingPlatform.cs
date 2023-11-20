using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;
    public float speed = 2.0f;

    private float progress = 0; 
    private bool movingToWaypoint1 = false;

    void Update()
    {
        if (movingToWaypoint1)
            progress -= speed * Time.deltaTime;
        else
            progress += speed * Time.deltaTime;

        progress = Mathf.Clamp01(progress);

        transform.position = Vector3.Lerp(waypoint1.position, waypoint2.position, Mathf.SmoothStep(0.0f, 1.0f, progress));

        if (progress >= 1.0f || progress <= 0.0f)
            movingToWaypoint1 = !movingToWaypoint1;
    }
}
