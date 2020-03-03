using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun3 : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public GameObject player;
    public float offset;

    public GameObject bulletPerfab;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //Shoot();
            GameObject bulletObject = Instantiate(bulletPerfab);
            bulletObject.transform.position = this.transform.position + transform.forward;
            bulletObject.transform.forward = this.transform.forward;
        }
    }

    /*void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }*/





}
