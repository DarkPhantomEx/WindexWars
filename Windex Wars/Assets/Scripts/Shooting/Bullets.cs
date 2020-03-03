using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float damage = 10f;
    public float speed = 8f;
    public float lifeDuration = 2f;

    [HideInInspector]
    public int playerNum;

    private float lifeTimer;

    
    void Start()
    {
        lifeTimer = lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += transform.forward * speed * Time.deltaTime;

        lifeTimer -= Time.deltaTime;
        if (lifeTimer<=0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Target target = other.transform.GetComponent<Target>();
        if (target != null && target.GetComponent<GunJoystick>().PlayerNumber != playerNum)
        {
            target.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
