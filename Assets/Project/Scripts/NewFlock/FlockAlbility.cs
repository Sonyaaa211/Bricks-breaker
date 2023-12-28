using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAlbility : MonoBehaviour
{
    [Header("General")]

    public float maxVelocity = 3.5f;

    public float maxAcceleration = 10f;

    public float turnSpeed = 20f;

    [Header("Arrive")]

    /// <summary>
    /// The radius from the target that means we are close enough and have arrived
    /// </summary>
    public float targetRadius = 0.05f;

    /// <summary>
    /// The radius from the target where we start to slow down
    /// </summary>
    public float slowRadius = 1f;

    /// <summary>
    /// The time in which we want to achieve the targetSpeed
    /// </summary>
    /// 
    public float timeToTarget = 0.1f;
    private PlayerMovement playerMovement;
    public Transform parentTransform;
    private Vector3 _acceleration;
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        playerMovement = PlayerMovement.Instance;
        maxVelocity = playerMovement.speed * 2;
        maxAcceleration = playerMovement.speed * 2;
    }

    public void Steer(Vector3 linearAcceleration)
    {
        _rb.velocity += linearAcceleration * Time.deltaTime;

        if (_rb.velocity.magnitude > maxVelocity)
        {
            _rb.velocity = _rb.velocity.normalized * maxVelocity;
        }
    }
    public Vector3 Arrive(Vector3 targetPosition)
    {
        Debug.DrawLine(transform.position, targetPosition, Color.cyan, 0f, false);

        /* Get the right direction for the linear acceleration */
        Vector3 targetVelocity = targetPosition - _rb.position;
        //Debug.Log("Displacement " + targetVelocity.ToString("f4"));

        /* Get the distance to the target */
        float dist = targetVelocity.magnitude;

        /* If we are within the stopping radius then stop */
        if (dist < targetRadius)
        {
            _rb.velocity = Vector3.zero;
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

        /* Give targetVelocity the correct speed */
        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;

        /* Calculate the linear acceleration we want */
        Vector3 acceleration = targetVelocity - _rb.velocity;
        /* Rather than accelerate the character to the correct speed in 1 second, 
         * accelerate so we reach the desired speed in timeToTarget seconds 
         * (if we were to actually accelerate for the full timeToTarget seconds). */
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
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 accel = Vector3.zero;
        accel += Arrive(parentTransform.position);

        Steer(accel);
    }
}
