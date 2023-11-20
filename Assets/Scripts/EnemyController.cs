using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;
    public float speed = 2.0f;

    private float progress = 0;
    private bool movingToWaypoint1 = true;

    void Update()
    {
        progress += speed * Time.deltaTime * (movingToWaypoint1 ? 1 : -1);
        progress = Mathf.Clamp01(progress);

        transform.position = Vector3.Lerp(waypoint1.position, waypoint2.position, progress);

        if (progress <= 0 || progress >= 1)
        {
            movingToWaypoint1 = !movingToWaypoint1;
        }
    }
}
