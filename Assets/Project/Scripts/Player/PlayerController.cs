using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    playing,
    pause,
    expand,
    die
}
public class PlayerController : Singleton<PlayerController>
{
    public List<GameObject> cubes;
    private ExpandSkill expandSkill;
    private ShootAlbility shootAlbility;

    public PlayerState State = PlayerState.playing;
    // Start is called before the first frame update
    void Start()
    {
        expandSkill = GetComponent<ExpandSkill>();
        shootAlbility = GetComponent<ShootAlbility>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && State == PlayerState.playing)
        {
            expandSkill.Expand();
        }
        if(Input.GetMouseButtonDown(0))
        {
            shootAlbility.Shoot();
        }
    }
}
