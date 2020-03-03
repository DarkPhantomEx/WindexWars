using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Source: https://www.youtube.com/watch?v=7nxpDwnU0uU
/// </summary>
public class CameraController : MonoBehaviour
{
    // Inspector Variables
    public float RotationSpeed = 1;
    public Transform Target, Player;
    public int PlayerNumber;

    private float mouseX, mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        CamControl();
    }

    void CamControl()
    {
        float hor = Input.GetAxis("JoystickRightStickHorizontalPlayer" + PlayerNumber);
        float ver = Input.GetAxis("JoystickRightStickVerticalPlayer" + PlayerNumber);

        mouseX += hor * RotationSpeed;
        mouseY -= ver * RotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Player.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
