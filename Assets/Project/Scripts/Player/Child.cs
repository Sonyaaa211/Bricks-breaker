using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Child : MonoBehaviour
{
    public float level;
    public FlockAlbility cube;
    public Child preChild;
    public List<Child> postChild;

    public void Rearange()
    {
        
        if (preChild != null)
        {
            preChild.postChild.Remove(this);
            foreach(Child child in postChild)
            {
                preChild.postChild.Add(child);
            }
        }
        if(postChild != null)
        {
            foreach (Child child in postChild)
            {
                if(child != null && child.cube != null)
                {
                    child.cube.parentTransform = transform;
                    child.Rearange();
                    break;
                }
            }
        }
    }
}
