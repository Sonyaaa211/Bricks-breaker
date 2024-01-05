using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesModifiler: MonoBehaviour
{
    [SerializeField] private List<Child> defaultNode;
    private int count = 0;
    public void ImportCube(FlockAlbility cube)
    {
        for (int i = 0; i < defaultNode.Count; i++)
        {
            Child newCube = defaultNode[count++ % defaultNode.Count];
            if (newCube.cube == null && cube.transform.parent != null)
            {
                cube.parentTransform = newCube.transform;
                newCube.cube = cube;
                var tmp = newCube;
                while (tmp.preChild != null && tmp.preChild.cube == null)
                {
                    tmp = tmp.preChild;
                    tmp.Rearange();
                }
                cube.parentTransform = tmp.transform;
                tmp.cube = cube;
                newCube.cube = null;
                break;
            }
        }
    }
}
