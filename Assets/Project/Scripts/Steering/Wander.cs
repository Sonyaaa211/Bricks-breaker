using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteeringBasics))]
public class Wander2 : MonoBehaviour
{
    public float wanderRadius = 1.2f;

    public float wanderDistance = 2f;

    /// <summary>
    /// Maximum amount of random displacement a second
    /// </summary>
    public float wanderJitter = 40f;

    Vector3 wanderTarget;

    SteeringBasics steeringBasics;

    Rigidbody rb;

    void Awake()
    {
        steeringBasics = GetComponent<SteeringBasics>();

        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        float theta = Random.value * 2 * Mathf.PI;
        wanderTarget = new Vector3(wanderRadius * Mathf.Cos(theta), 0f, wanderRadius * Mathf.Sin(theta));
    }

    public Vector3 GetSteering()
    {
        /* Get the jitter for this time frame */
        float jitter = wanderJitter * Time.deltaTime;

        wanderTarget += new Vector3(Random.Range(-1f, 1f) * jitter, 0f, Random.Range(-1f, 1f) * jitter);


        /* Make the wanderTarget fit on the wander circle again */
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        /* Move the target in front of the character */
        Vector3 targetPosition = transform.position + transform.right * wanderDistance + wanderTarget;

        //Debug.DrawLine(transform.position, targetPosition);

        return steeringBasics.Seek(targetPosition);
    }
}
