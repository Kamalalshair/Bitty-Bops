//Updates the heading based on the velocity of the rigid body
//Created by James Vanderhyde, 6 November 2019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignToVelocity : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        //Update the heading, unless the object is not moving.
        //This assumes the sprite is oriented along positive X ("right").
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb.velocity.sqrMagnitude > 0.0001)
            transform.right = rb.velocity;
    }
}
