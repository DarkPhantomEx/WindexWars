using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunJoystick : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public GameObject player;
    public float offset;

    public GameObject bulletPerfab;

    public int PlayerNumber;
    private bool reload=false;



    // Update is called once per frame
    void Update()
    {
        bool b = Input.GetButton("JoystickBPlayer" + PlayerNumber);
        bool u = Input.GetButtonUp("JoystickBPlayer" + PlayerNumber);
        if (b & !reload)
        {
            //Shoot();
            GameObject bulletObject = Instantiate(bulletPerfab);
            bulletObject.transform.position = this.transform.position+ transform.forward;
            bulletObject.transform.forward = this.transform.forward;
            reload = true;
        }
        if (u)
        {
            reload = false;
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
