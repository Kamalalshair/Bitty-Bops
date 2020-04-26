//Steering control: seek + arrive + avoid behavior
//Created by James Vanderhyde, 6 November 2019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekArriveAvoid : MonoBehaviour
{
    //The target to seek
    public Transform target;

    //The obstacles to avoid
    public LayerMask obstacleLayer;
    public LayerMask carLayer;

    //We will adjust maxForce and maxSpeed as game feel requires.
    public float maxForce;
    public float maxSpeed;

    //Show the current speed in the inspector
    public float currentSpeed;

    private Rigidbody2D rb;
    private PolygonCollider2D bc;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponentInChildren<PolygonCollider2D>();
    }

    void Update()
    {
        //Get the current speed, just to show it in the inspector
        currentSpeed = rb.velocity.magnitude;

        //Check for future collisions with obstacles
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(obstacleLayer);
        List<RaycastHit2D> results = new List<RaycastHit2D>();
        bc.Cast(rb.velocity, filter, results, rb.velocity.magnitude);

        Vector2 desiredVel;

        //If we are going to collide, we need to steer around it.
        if (results.Count > 0)
        {
            //Find the hit point and the direction of the wall where the hit occurred
            RaycastHit2D hit = results[0];
            Vector2 wall = new Vector2(hit.normal.y, -hit.normal.x);

            //Decide whether to go right or left depending on what point was hit
            Vector2 forward = rb.velocity.normalized;
            Vector2 right = new Vector2(forward.y, -forward.x);
            float side = Vector2.Dot(hit.point - hit.centroid, right);
            if (side < 0) wall = -wall;

            //We need to find the point along the wall
            // that is maxSpeed away from the current position.
            // So we find the intersection of a circle of radius maxSpeed
            // with a ray along the wall.
            //We have to shoot the ray from outside the circle toward
            // the centroid. Otherwise it considers the collision to
            // have occurred immediately.

            //Set up the circle
            CircleCollider2D cc = GetComponent<CircleCollider2D>();
            cc.enabled = true;
            cc.radius = maxSpeed;

            //Cast the ray
            RaycastHit2D intersection =
                Physics2D.Raycast(hit.centroid + maxSpeed * wall, -wall, 2 * maxSpeed, carLayer);
            Debug.DrawRay(hit.centroid + maxSpeed * wall, -wall * 2 * maxSpeed, Color.red);
            cc.enabled = false;

            //The desired velocity is toward that intersection point.
            desiredVel = intersection.point - (Vector2)transform.position;
        }
        else
        {
            //Calculate the seek steering direction
            Vector2 toTarget = target.position - transform.position;
            float distance = toTarget.magnitude;
            float speed;
            if (distance < maxSpeed) //if we will reach in under 1 second
                speed = distance;
            else
                speed = maxSpeed;
            desiredVel = toTarget / toTarget.magnitude * speed;
        }
        Vector2 seekVelocity = desiredVel - rb.velocity;

        //Let's draw the vectors
        Debug.DrawRay(transform.position, rb.velocity, Color.white);
        Debug.DrawRay(transform.position, desiredVel, Color.magenta);
        Debug.DrawRay(transform.position, seekVelocity, Color.green);
        Debug.DrawRay(transform.position + (Vector3)rb.velocity, seekVelocity, Color.green);

        //Apply the force
        Vector2 force = seekVelocity / maxSpeed * maxForce;
        rb.AddForce(force);
    }
}
