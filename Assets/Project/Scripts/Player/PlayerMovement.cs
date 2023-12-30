using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    [Header("properties")]
    [SerializeField] private Rigidbody _playerRB;
    [SerializeField] public float speed;
    [SerializeField] public Vector3 direction;

    private void Update()
    {
        direction = new Vector3(Input.GetAxisRaw("Vertical"), 0, -Input.GetAxisRaw("Horizontal")).normalized;
        if (direction != Vector3.zero)
        {
            _playerRB.velocity = direction * speed;
        }
        else
        {
            _playerRB.velocity = Vector3.zero;
        }
    }
}
