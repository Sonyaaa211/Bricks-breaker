using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceChild : MonoBehaviour
{
    PlayerController controller;
    public GameObject child;
    void Awake()
    {
        controller = PlayerController.Instance;    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
