using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinCube : MonoBehaviour
{
    private FlockAlbility flockAlbility;
    private void Start()
    {
        flockAlbility = GetComponent<FlockAlbility>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Child" && flockAlbility.parentTransform == null)
        {
            Debug.Log("collided");
            PlayerController.Instance.ImportNewCube(flockAlbility);
        }
        else if(other.tag == "Bullet" && flockAlbility.parentTransform != null)
        {
            Destroy(other.gameObject);
            flockAlbility.parentTransform.GetComponent<Child>().Rearange();
            flockAlbility.parentTransform = null;
            PlayerController.Instance.cubes.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
