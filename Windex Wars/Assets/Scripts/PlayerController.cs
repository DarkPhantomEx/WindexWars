using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Source: https://www.youtube.com/watch?v=7nxpDwnU0uU
/// </summary>
public class PlayerController : MonoBehaviour
{
    // Inspector Variables
    public float Speed;
    public float Jump;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(hor, 0f, ver) * Speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);

        if (Input.GetButtonDown("Jump") && Physics.Raycast(transform.position, Vector3.down, 1.1f))
        {
            GetComponent<Rigidbody>().AddForce(0, Jump, 0);
        }
    }
}
