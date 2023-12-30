using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 diff;
    // Start is called before the first frame update
    void Start()
    {
        diff = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = diff + playerTransform.position;
    }
}
