using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootAlbility : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] private Camera mainCamera;
    private Vector3 targetPos;
    public GameObject shootCube;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if(targetPos != null && shootCube != null)
        {
            Debug.DrawLine(shootCube.transform.position, targetPos, Color.cyan, 0f, false);
        }
    }
    public void Shoot()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            targetPos = raycastHit.point;
        }
        float min = 99;
        
        for(int i = 0; i < playerController.cubes.Count; i++)
        {
            float distance = (targetPos - playerController.cubes[i].transform.position).magnitude;
            if (min > distance)
            {
                min = distance;
                shootCube = playerController.cubes[i];
            }
        }
        FlockAlbility flockAlbility = shootCube.GetComponent<FlockAlbility>();
        flockAlbility.parentTransform = null;
        playerController.cubes.Remove(shootCube);
        shootCube.GetComponent<Rigidbody>().AddForce((targetPos - shootCube.transform.position).normalized * 30, ForceMode.Impulse);
    }
}
