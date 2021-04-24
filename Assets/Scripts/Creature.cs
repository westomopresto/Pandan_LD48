using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{

    private Transform thisTransform;
    private Vector3 spawnLocation;
    private int health;
    public Vector2 input;

    // physics
    public LayerMask floorMasks;
    public Transform camPivot;
    Vector3 intent;
    Vector3 velocity;
    Vector3 velocityXZ;
    float turnSpeed = 10;

    public float moveSpeed = 4;
    public float airSpeed = 2.5f;
    public float turnSpeedLow = 30;
    public float turnSpeedHigh = 50;
    public float accel = 25;
    public float gravity = 10;
    public float jumpPwr = 1;
    public Animator animator;
    public bool grounded = false;

    public AudioSource audioData; 
    CharacterController mover;
    public AudioClip[] sfx;

    // Start is called before the first frame update
    public virtual void Start()
    {
        mover = GetComponent<CharacterController>();
        audioData = GetComponent<AudioSource>();
        gameObject.layer = 2;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        CalculateGround();
        DoMove();
        DoJump();
        mover.Move(velocity * Time.deltaTime);
        CalculateCamera();

        //animator.SetBool("grounded", grounded);
        var vMag = velocityXZ.magnitude;
        vMag = Mathf.Clamp(vMag, 0.0f, 10.0f);
        //animator.SetFloat("walkSpeed", vMag);
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }

    private Vector3 camF;
    private Vector3 camR;
    void CalculateCamera()
    {
        camF = camPivot.forward;
        camR = camPivot.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
    }

    void CalculateGround()
    {
        RaycastHit hit;
        //if (Physics.Raycast(transform.position + Vector3.up * 0.1f, -Vector3.up, out hit, 0.4f))
        DebugExtension.DebugWireSphere(transform.position + Vector3.up - new Vector3(0, 1.0f, 0), new Color(0.0f, 1.0f, 0.0f), 0.34f);

        if (Physics.SphereCast(transform.position + Vector3.up, 0.34f, -Vector3.up, out hit, 1.0f, floorMasks, QueryTriggerInteraction.UseGlobal))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    void DoMove()
    {
        intent = camF * input.y + camR * input.x;

        float tS = velocity.magnitude/moveSpeed;
        turnSpeed = Mathf.Lerp(turnSpeedHigh, turnSpeedLow, tS);
        if(input.magnitude >= 0.1f)
        {
            Quaternion rot = Quaternion.LookRotation(intent);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }

        float speed;

        if (!grounded)
            speed = airSpeed;
        else
            speed = moveSpeed;

            velocityXZ = velocity; // planar move direction
            velocityXZ.y = 0;

        input = Vector2.ClampMagnitude(input, 1);
        if (input.magnitude > 0.1 || grounded)
        {
            velocityXZ = Vector3.MoveTowards(velocityXZ, transform.forward * input.magnitude * speed, accel * Time.deltaTime);
            velocity.x = velocityXZ.x;
            velocity.z = velocityXZ.z;
        }
            velocity.y -= gravity * Time.deltaTime;
        if (grounded)
        {
            velocity.y = Mathf.Clamp(velocity.y, -4, 999);
        }
    }

    private float CalcJumpSpeed(float jumpHeight, float gravity)
    {
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    void Jump()
    {
        velocity.y = CalcJumpSpeed(jumpPwr, gravity);
        //animator.SetTrigger("jump");
    }

    void DoJump()
    {
        if(grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
    }
}
