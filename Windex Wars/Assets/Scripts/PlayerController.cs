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
        float hor2 = Input.GetAxis("JoystickDPadHorizontalPlayer" + PlayerNumber);
        float ver2 = Input.GetAxis("JoystickDPadVerticalPlayer" + PlayerNumber);
        float lt = Input.GetAxis("JoystickLTPlayer" + PlayerNumber);
        float rt = Input.GetAxis("JoystickRTPlayer" + PlayerNumber);
        bool a = Input.GetButton("JoystickAPlayer"+PlayerNumber);
        bool b = Input.GetButton("JoystickBPlayer" + PlayerNumber);
        bool x = Input.GetButton("JoystickXPlayer" + PlayerNumber);
        bool y = Input.GetButton("JoystickYPlayer" + PlayerNumber);
        bool lb = Input.GetButton("JoystickLBPlayer" + PlayerNumber);
        bool rb = Input.GetButton("JoystickRBPlayer" + PlayerNumber);
        bool select = Input.GetButton("JoystickSelectPlayer" + PlayerNumber);
        bool start = Input.GetButton("JoystickStartPlayer" + PlayerNumber);
        if (a)
        {
            Debug.Log("A - Player"+PlayerNumber);
        }
        if (b)
        {
            Debug.Log("B - Player" + PlayerNumber);
        }
        if (x)
        {
            Debug.Log("X - Player" + PlayerNumber);
        }
        if (y)
        {
            Debug.Log("Y - Player" + PlayerNumber);
        }
        if (start)
        {
            Debug.Log("Start - Player" + PlayerNumber);
        }
        if (select)
        {
            Debug.Log("Select - Player" + PlayerNumber);
        }
        if (lb)
        {
            Debug.Log("LB - Player" + PlayerNumber);
        }
        if (rb)
        {
            Debug.Log("RB - Player" + PlayerNumber);
        }
        Debug.Log("Left Stick Hor - "+hor+" Ver - "+ver +" Player" + PlayerNumber);
        Debug.Log("Right Stick Hor - " + hor1 + " Ver - " + ver1 + " Player" + PlayerNumber);
        Debug.Log("Dpad hor - " + hor2 + " Ver - " + ver2 + " Player" + PlayerNumber);
        Debug.Log("LT - " + lt + " RT - " + rt + " Player" + PlayerNumber);
        Vector3 playerMovement = new Vector3(hor, 0f, ver) * Speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);

        if (Input.GetButtonDown("Jump") && Physics.Raycast(transform.position, Vector3.down, 1.1f))
        {
            GetComponent<Rigidbody>().AddForce(0, Jump, 0);
        }
    }
}
