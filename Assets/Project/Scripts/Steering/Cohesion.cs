using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohesion : MonoBehaviour
{
    public float facingCosine = 120f;

    float facingCosineVal;

    SteeringBasics steeringBasics;

    void Awake()
    {
        facingCosineVal = Mathf.Cos(facingCosine * Mathf.Deg2Rad);
        steeringBasics = GetComponent<SteeringBasics>();
    }

    public Vector3 GetSteering(ICollection<Rigidbody> targets)
    {
        Vector3 centerOfMass = Vector3.zero;
        int count = 0;

        /* Sums up everyone's position who is close enough and in front of the character */
        foreach (Rigidbody r in targets)
        {
            if (steeringBasics.IsFacing(r.position, facingCosineVal))
            {
                centerOfMass += r.position;
                count++;
            }
        }

        if (count == 0)
        {
            return Vector3.zero;
        }
        else
        {
            centerOfMass = centerOfMass / count;

            return steeringBasics.Arrive(centerOfMass);
        }
    }
}
