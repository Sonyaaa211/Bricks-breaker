using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : MonoBehaviour
{
    /// <summary>
    /// The maximum acceleration for separation
    /// </summary>
    public float sepMaxAcceleration = 25;

    /// <summary>
    /// This should be the maximum separation distance possible between a
    /// separation target and the character. So it should be: separation
    /// sensor radius + max target radius
    /// </summary>
    public float maxSepDist = 1f;

    public SphereCollider colider;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public Vector3 GetSteering(ICollection<Rigidbody> targets)
    {
        Vector3 acceleration = Vector3.zero;

        foreach (Rigidbody r in targets)
        {
            /* Get the direction and distance from the target */
            Vector3 direction = rb.position - r.position;
            float dist = direction.magnitude;

            if (dist < maxSepDist)
            {
                /* Calculate the separation strength (can be changed to use inverse square law rather than linear) */
                var strength = sepMaxAcceleration * (maxSepDist - dist) / (maxSepDist);

                /* Added separation acceleration to the existing steering */
                direction.Normalize();
                acceleration += direction * strength;
            }
        }

        return acceleration;
    }
}
