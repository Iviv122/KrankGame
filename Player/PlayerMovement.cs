using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private InputManager input;
    [SerializeField] private Transform orientation;
    [SerializeField] public Rigidbody rb;
    [Header("Walking")]
    [SerializeField] private float Speed;
    [SerializeField] private float MaxGroundSpeed = 25;
    [SerializeField] private float MaxAirSpeed = 40;
    [SerializeField] private float MoveSpeed = 15;
    [SerializeField] private float stopSpeed = 5; // static?
    [SerializeField] private float friction = 0.8f; // kinetic
    [SerializeField] private float accel = 3;
    [Header("Ground Check")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool canJump;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float height;
    [Header("Input")]
    [SerializeField] private Vector3 wishvel;
    [SerializeField] public Vector3 wishdir;
    [Header("Jump")]
    [SerializeField] private float jumpForce;

    [SerializeField] private float jumpBufferTime = 0.2f; // Buffer window duration
    private float jumpBufferCounter = 0f;
    [SerializeField] RaycastHit slopeHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        input.Jump += JumpInput;

        input.Move += GetDir;
    }
    private Vector2 dir;
    private void GetDir(Vector2 d)
    {
        dir = d;
    }
    private void Accelerate(float wishspeed)
    {
        float currentSpeed = Vector3.Dot(rb.linearVelocity, wishdir);
        float addSpeed = wishspeed - currentSpeed;

        if (addSpeed <= 0)
        {
            return;
        }
        float accelSpeed = accel * Time.deltaTime * wishspeed;
        if (accelSpeed > addSpeed)
        {
            accelSpeed = addSpeed;
        }
        rb.linearVelocity += accelSpeed * wishdir;
    }
    private void AirAccelerate(float wishspeed)
    {
        if (wishspeed > MaxAirSpeed)
        {
            wishspeed = MaxAirSpeed;
        }
        float currSpeed = Vector3.Dot(rb.linearVelocity, wishdir);
        float addSpeed = wishspeed - currSpeed;
        if (addSpeed <= 0)
        {
            return;
        }
        float accelSpeed = accel * wishspeed * Time.deltaTime;
        if (accelSpeed > addSpeed)
        {
            accelSpeed = addSpeed;
        }
        rb.linearVelocity += accelSpeed * wishdir;
    }
    private void Move()
    {

        wishvel = (orientation.forward * dir.y + orientation.right * dir.x).normalized;
        wishdir = new Vector3(wishvel.x, 0, wishvel.z);
        wishdir = Vector3.ProjectOnPlane(wishdir, slopeHit.normal) * MoveSpeed;

        wishdir = Vector3.ProjectOnPlane(wishdir, slopeHit.normal);

        float wishspeed = wishdir.magnitude;
        wishdir.Normalize();

        if (wishspeed > MaxGroundSpeed)
        {
            wishvel *= MaxGroundSpeed / wishspeed;
            wishspeed = MaxGroundSpeed;
        }

        if (isGrounded)
        {
            Accelerate(wishspeed);
        }
        else
        {
            AirAccelerate(wishspeed);
        }

    }

    private void Friction()
    {
        if (canJump && jumpBufferCounter >= 0f)
        {
            // when jumping no friction
            return;
        }
        if (isGrounded)
        {
            float speed = Mathf.Sqrt(rb.linearVelocity.z * rb.linearVelocity.z + rb.linearVelocity.x * rb.linearVelocity.x);

            if (speed < 1)
            {
                rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
                return;
            }
            float control = (speed < stopSpeed) ? stopSpeed : speed;
            float newspeed = control * friction * Time.deltaTime;

            float drop = 0;
            drop += control * friction * Time.deltaTime;
            newspeed = speed - drop;
            newspeed /= speed;

            //rb.linearVelocity *= newspeed;

            rb.linearVelocity = new Vector3(rb.linearVelocity.x * newspeed, rb.linearVelocity.y, rb.linearVelocity.z * newspeed);

        }
    }
    private void IsGrounded()
    {
        canJump = isGrounded = Physics.Raycast(transform.position, Vector3.down, height / 2 + 0.2f, ground);
    }
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, height / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    private void JumpInput()
    {
        jumpBufferCounter = jumpBufferTime;
    }
    private void IsJump()
    {
        jumpBufferCounter -= Time.deltaTime; // Decrease buffer over time
        if (canJump && jumpBufferCounter >= 0f)
        {

            jumpBufferCounter = jumpBufferTime;
            rb.linearVelocity += Vector3.up * jumpForce;
            jumpBufferCounter = 0f;
        }
    }
    private void FixedUpdate()
    {
        Move();
        Friction();
    }
    private void Update()
    {
        OnSlope();
        IsGrounded();
        IsJump();


        //Debug.Log($"{input.x} : {input.y}");
        Speed = rb.linearVelocity.magnitude;
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
