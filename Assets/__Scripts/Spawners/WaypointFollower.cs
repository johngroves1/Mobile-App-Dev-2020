using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaypointFollower : MonoBehaviour
{
   // == public properties ==
    public float Speed { get { return Speed; } set { speed = value; } }

    // == private variables ==
    private float speed;
    private Rigidbody2D rb;
    private IList<Vector3> waypoints = new List<Vector3>();
    private Vector3 currentWaypoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextWayPoint(); // sets currentWaypoint
    }

    private void FixedUpdate()
    {
        if(hasMorePoints())
        {
            MoveToWaypoint();
        }
    }

    private void MoveToWaypoint()
    {
        rb.position = Vector3.MoveTowards(rb.position, currentWaypoint, speed * Time.deltaTime);
        if(Vector3.Distance(rb.position, currentWaypoint) < 0.01f)
        {
            rb.position = new Vector2(currentWaypoint.x, currentWaypoint.y);
            waypoints.Remove(currentWaypoint);
            if(hasMorePoints())
            {
                nextWayPoint();
            }
            else{
                Destroy(gameObject);
            }
        }
    }

    private bool hasMorePoints()
    {
        return waypoints.Count > 0;
    }

    private void nextWayPoint()
    {
        if(hasMorePoints())
        {
            currentWaypoint = waypoints.First();
        }
    }

    public void AddWaypoint(Vector3 point)
    {
        waypoints.Add(point);
    }
}
