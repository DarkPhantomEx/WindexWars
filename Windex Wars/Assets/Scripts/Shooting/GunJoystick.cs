using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunJoystick : MonoBehaviour
{
    public float damage = 10f;
    public GameObject bulletPerfab;
    public int PlayerNumber;

    private bool held;

    private void Start()
    {
        held = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool b = Input.GetButtonDown("JoystickBPlayer" + PlayerNumber);
        float lt = Input.GetAxis("JoystickLTPlayer" + PlayerNumber);
        float rt = Input.GetAxis("JoystickRTPlayer" + PlayerNumber);

        if (b)
            Shoot();
        else if (!held && (lt != 0 || rt != 0))
        {
            held = true;

            Shoot();
        }
        else if (held && (lt == 0 && rt == 0))
            held = false;
    }

    void Shoot()
    {
        for (int i = 0; i < 3; i++)
            SpawnBullet();
    }

    void SpawnBullet()
    {
        GameObject bulletObject = Instantiate(bulletPerfab);
        bulletObject.GetComponent<Bullets>().playerNum = PlayerNumber;
        bulletObject.transform.position = transform.position + transform.forward + (Vector3.left * Random.Range(-.25f, .25f)) + Vector3.up + (Vector3.up * Random.Range(0.25f, 0.5f));
        bulletObject.transform.forward = transform.forward;
    }
}
