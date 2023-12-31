using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class NomalEnemyAttack : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private float attackCooldown;
    private bool shootable = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - player.transform.position).magnitude < 15)
        {
            transform.LookAt(player.transform);
            if (shootable)
            {
                shootable = false;
                StartCoroutine(Shoot());
            }
        }
    }

    IEnumerator Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefabs, attackPosition);
        bullet.transform.SetParent(null);
        bullet.GetComponent<Rigidbody>().AddForce((attackPosition.position - transform.position).normalized * 30, ForceMode.Impulse);
        yield return new WaitForSeconds(attackCooldown);
        shootable = true;
    }
}
