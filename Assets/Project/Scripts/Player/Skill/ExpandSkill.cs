using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandSkill : MonoBehaviour
{
    public List<Transform> childPlaces1;
    public List<Transform> childPlaces2;
    public List<Transform> childPlaces3;
    [SerializeField] private float _expandRate = 2;
    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void Expand()
    {
        playerController.State = PlayerState.expand;
        for (int i = 0; i < childPlaces1.Count; i++)
        {
            childPlaces1[i].position = (childPlaces1[i].position - transform.position) * _expandRate + transform.position;
        }
        for (int i = 0; i < childPlaces2.Count; i++)
        {
            childPlaces2[i].position = (childPlaces2[i].position - transform.position) * _expandRate + transform.position;
        }
        Invoke("DeExpand", 3);
    }

    public void DeExpand()
    {
        playerController.State = PlayerState.playing;
        for (int i = 0; i < childPlaces1.Count; i++)
        {
            childPlaces1[i].position = (childPlaces1[i].position - transform.position) / _expandRate + transform.position;
        }
        for (int i = 0; i < childPlaces2.Count; i++)
        {
            childPlaces2[i].position = (childPlaces2[i].position - transform.position) / _expandRate + transform.position;
        }
    }
}
