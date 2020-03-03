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

    Rigidbody m_Rigidbody;
    Animator m_Animator;
    bool m_IsGrounded;
    float m_OrigGroundCheckDistance;
    [SerializeField] float m_GroundCheckDistance = 0.1f;
    [Range(1f, 4f)] [SerializeField] float m_GravityMultiplier = 2f;
    float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
    const float k_Half = 0.5f;
    Vector3 m_GroundNormal;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
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
        //float hor1 = Input.GetAxis("JoystickRightStickHorizontalPlayer" + PlayerNumber);
        //float ver1 = Input.GetAxis("JoystickRightStickVerticalPlayer" + PlayerNumber);
        //float hor2 = Input.GetAxis("JoystickDPadHorizontalPlayer" + PlayerNumber);
        //float ver2 = Input.GetAxis("JoystickDPadVerticalPlayer" + PlayerNumber);
        float lt = Input.GetAxis("JoystickLTPlayer" + PlayerNumber);
        float rt = Input.GetAxis("JoystickRTPlayer" + PlayerNumber);
        bool a = Input.GetButton("JoystickAPlayer"+PlayerNumber);
        //bool b = Input.GetButton("JoystickBPlayer" + PlayerNumber);
        //bool x = Input.GetButton("JoystickXPlayer" + PlayerNumber);
        //bool y = Input.GetButton("JoystickYPlayer" + PlayerNumber);
        //bool lb = Input.GetButton("JoystickLBPlayer" + PlayerNumber);
        //bool rb = Input.GetButton("JoystickRBPlayer" + PlayerNumber);
        //bool select = Input.GetButton("JoystickSelectPlayer" + PlayerNumber);
        bool start = Input.GetButton("JoystickStartPlayer" + PlayerNumber);
        /*if (a)
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
        Debug.Log("LT - " + lt + " RT - " + rt + " Player" + PlayerNumber);*/
        Vector3 playerMovement = new Vector3(hor, 0f, ver) * Speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);

        CheckGroundStatus();

        // Animations
        m_Animator.SetFloat("Forward", ver, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("Turn", hor, 0.1f, Time.deltaTime);

        m_IsGrounded = Physics.Raycast(transform.position, Vector3.down, m_GroundCheckDistance);

        m_Animator.SetBool("OnGround", m_IsGrounded);
        if (!m_IsGrounded)
            // Animations
            m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);

        /*if (a && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(0, Jump, 0);

            // Animations
            m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
        }*/

        // control and velocity handling is different when grounded and airborne:
        if (m_IsGrounded)
        {
            HandleGroundedMovement(a);
        }
        else
        {
            HandleAirborneMovement();
        }

        // calculate which leg is behind, so as to leave that leg trailing in the jump animation
        // (This code is reliant on the specific run cycle offset in our animations,
        // and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
        float runCycle =
            Mathf.Repeat(
                m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
        float jumpLeg = (runCycle < k_Half ? 1 : -1) * ver;
        if (m_IsGrounded)
        {
            m_Animator.SetFloat("JumpLeg", jumpLeg);
        }
    }

    /*void UpdateAnimator(Vector3 move)
    {
        // update the animator parameters
        m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        m_Animator.SetBool("Crouch", m_Crouching);
        m_Animator.SetBool("OnGround", m_IsGrounded);
        if (!m_IsGrounded)
        {
            m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
        }

        // calculate which leg is behind, so as to leave that leg trailing in the jump animation
        // (This code is reliant on the specific run cycle offset in our animations,
        // and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
        float runCycle =
            Mathf.Repeat(
                m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
        float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
        if (m_IsGrounded)
        {
            m_Animator.SetFloat("JumpLeg", jumpLeg);
        }

        // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
        // which affects the movement speed because of the root motion.
        if (m_IsGrounded && move.magnitude > 0)
        {
            m_Animator.speed = m_AnimSpeedMultiplier;
        }
        else
        {
            // don't use that while airborne
            m_Animator.speed = 1;
        }
    }*/


    void HandleAirborneMovement()
    {
        // apply extra gravity from multiplier:
        Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
        m_Rigidbody.AddForce(extraGravityForce);

        m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.2f;
    }


    void HandleGroundedMovement(bool jump)
    {
        // check whether conditions are right to allow a jump:
        if (jump && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {
            // jump!
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, Jump, m_Rigidbody.velocity.z);
            m_IsGrounded = false;
            m_Animator.applyRootMotion = false;
            m_GroundCheckDistance = 0.2f;
        }
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.2f), transform.position + (Vector3.up * 0.2f) + (Vector3.down * m_GroundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            m_GroundNormal = hitInfo.normal;
            m_IsGrounded = true;
            m_Animator.applyRootMotion = true;
        }
        else
        {
            m_IsGrounded = false;
            m_GroundNormal = Vector3.up;
            m_Animator.applyRootMotion = false;
        }
    }
}
