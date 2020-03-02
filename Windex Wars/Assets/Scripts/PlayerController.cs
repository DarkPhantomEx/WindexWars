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
    public int PlayerNumber;

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
        float hor = Input.GetAxis("JoystickLeftStickHorizontalPlayer"+PlayerNumber);
        float ver = Input.GetAxis("JoystickLeftStickVerticalPlayer"+PlayerNumber);
        float hor1 = Input.GetAxis("JoystickRightStickHorizontalPlayer" + PlayerNumber);
        float ver1 = Input.GetAxis("JoystickRightStickVerticalPlayer" + PlayerNumber);
        bool a = Input.GetButton("JoystickAPlayer"+PlayerNumber);
        bool b = Input.GetButton("JoystickBPlayer" + PlayerNumber);
        if (a)
        {
            Debug.Log("A - Player"+PlayerNumber);
        }
        if (b)
        {
            Debug.Log("B - Player" + PlayerNumber);
        }
        Debug.Log("Left Stick Hor - "+hor+" Ver"+ver +" Player" + PlayerNumber);
        Debug.Log("Right Stick Hor - " + hor1 + " Ver" + ver1 + " Player" + PlayerNumber);
        Vector3 playerMovement = new Vector3(hor, 0f, ver) * Speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);

        if (Input.GetButtonDown("Jump") && Physics.Raycast(transform.position, Vector3.down, 1.1f))
        {
            GetComponent<Rigidbody>().AddForce(0, Jump, 0);
        }
    }
}
