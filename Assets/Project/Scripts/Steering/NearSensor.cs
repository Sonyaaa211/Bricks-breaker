using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearSensor : MonoBehaviour
{
    public HashSet<Rigidbody> _targets = new HashSet<Rigidbody>();
    public Rigidbody playerRb;
    private void Start()
    {
        _targets.Add(playerRb);
    }
    public HashSet<Rigidbody> targets
    {
        get
        {
            /* Remove any MovementAIRigidbodies that have been destroyed */
            _targets.RemoveWhere(IsNull);
            return _targets;
        }
    }

    static bool IsNull(Rigidbody r)
    {
        return (r == null || r.Equals(null));
    }

    void TryToAdd(Component other)
    {
        Debug.Log("Added");
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {   
            
            _targets.Add(rb);
        }
    }

    void TryToRemove(Component other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            _targets.Remove(rb);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        TryToAdd(other);
    }
    void OnTriggerExit(Collider other)
    {
        TryToRemove(other);
    }
}
