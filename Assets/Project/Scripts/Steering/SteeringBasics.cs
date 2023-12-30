using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBasics : MonoBehaviour
{
    [Header("General")]

    public float maxVelocity = 3.5f;

    public float maxAcceleration = 10f;

    public float turnSpeed = 20f;

    [Header("Arrive")]

    public float targetRadius = 0.005f;
    public float slowRadius = 1f;
    public float timeToTarget = 0.1f;


    [Header("Look Direction Smoothing")]

    [Header("Look Direction Smoothing")]
    public bool smoothing = true;
    public int numSamplesForSmoothing = 5;
    Queue<Vector3> velocitySamples = new Queue<Vector3>();
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();    
    }

    public void Steer(Vector3 linearAcceleration)
    {
        rb.velocity += linearAcceleration * Time.deltaTime;

        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }

    public Vector3 Seek(Vector3 targetPosition, float maxSeekAccel)
    {
        /* Get the direction */
        Vector3 acceleration = targetPosition - transform.position;

        acceleration.Normalize();

        /* Accelerate to the target */
        acceleration *= maxSeekAccel;

        return acceleration;
    }

    public Vector3 Seek(Vector3 targetPosition)
    {
        return Seek(targetPosition, maxAcceleration);
    }
    public Vector3 Arrive(Vector3 targetPosition)
    {
        Debug.DrawLine(transform.position, targetPosition, Color.cyan, 0f, false);

        /* Get the right direction for the linear acceleration */
        Vector3 targetVelocity = targetPosition - rb.position;
        //Debug.Log("Displacement " + targetVelocity.ToString("f4"));

        /* Get the distance to the target */
        float dist = targetVelocity.magnitude;

        /* If we are within the stopping radius then stop */
        if (dist < targetRadius)
        {
            rb.velocity = Vector3.zero;
            return Vector3.zero;
        }

        /* Calculate the target speed, full speed at slowRadius distance and 0 speed at 0 distance */
        float targetSpeed;
        if (dist > slowRadius)
        {
            targetSpeed = maxVelocity;
        }
        else
        {
            targetSpeed = maxVelocity * (dist / slowRadius);
        }
        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;
        Vector3 acceleration = targetVelocity - rb.velocity;
        acceleration *= 1 / timeToTarget;

        /* Make sure we are accelerating at max acceleration */
        if (acceleration.magnitude > maxAcceleration)
        {
            acceleration.Normalize();
            acceleration *= maxAcceleration;
        }
        //Debug.Log("Accel " + acceleration.ToString("f4"));
        return acceleration;
    }
    public void LookWhereYoureGoing()
    {
        Vector3 direction = rb.velocity;

        if (smoothing)
        {
            if (velocitySamples.Count == numSamplesForSmoothing)
            {
                velocitySamples.Dequeue();
            }

            velocitySamples.Enqueue(rb.velocity);

            direction = Vector3.zero;

            foreach (Vector3 v in velocitySamples)
            {
                direction += v;
            }

            direction /= velocitySamples.Count;
        }

        LookAtDirection(direction);
    }

    public void LookAtDirection(Vector3 direction)
    {
        direction.Normalize();

        /* If we have a non-zero direction then look towards that direciton otherwise do nothing */
        if (direction.sqrMagnitude > 0.001f)
        {
            /* Mulitply by -1 because counter clockwise on the y-axis is in the negative direction */
            float toRotation = -1 * (Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg);
            float rotation = Mathf.LerpAngle(rb.rotation.eulerAngles.y, toRotation, Time.deltaTime * turnSpeed);

            rb.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }

    public bool IsFacing(Vector3 target, float cosineValue)
    {
        Vector3 facing = transform.right.normalized;

        Vector3 directionToTarget = (target - transform.position);
        directionToTarget.Normalize();

        return Vector3.Dot(facing, directionToTarget) >= cosineValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
