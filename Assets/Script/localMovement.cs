using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class localMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    public float Speed = 5f;
    public float RotateSpeed = 1f;
    public Vector3 Velocity = Vector3.zero;
    public Transform GroundCheck;
    public float CheckRadius = 0.2f;
    private bool IsGround;
    public LayerMask layerMask;


    // Start is called before the first frame update
    void Start()
    {
        controller = transform.GetComponent<CharacterController>();
        animator = transform.GetComponent<Animator>();
        // set collision model to foot
        controller.center = new Vector3(0, 1, 0);
    }


    // Update is called once per frame
    void Update()
    {
        MoveLikeWoW();

    }

    private void GravityDrivenMove()
    {
        // add gravity
        // print GroundCheck.position, CheckRadius, layerMask to console
        print("GroundCheck.position: " + GroundCheck.position + " CheckRadius: " + CheckRadius + " layerMask: " + layerMask);
        IsGround = Physics.CheckSphere(GroundCheck.position, CheckRadius, layerMask);
        if (IsGround && Velocity.y < 0)
        {
            Velocity.y = 0;
        }
        Velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(Velocity * Time.deltaTime);



    }


    private void MoveLikeUnity()
    {

        GravityDrivenMove();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        var move = new Vector3(horizontal, 0, vertical).normalized;
        controller.Move(move * Speed * Time.deltaTime);
        // if move is not zero, play run animation
        if (move != Vector3.zero)
        {
            animator.SetBool("IsRun", true);
        }
        else
        {
            animator.SetBool("IsRun", false);
        }

        // orient to the direction of mouse
        var playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        var mousePos = Input.mousePosition;
        var angle = Mathf.Atan2(mousePos.x - playerScreenPos.x, mousePos.y - playerScreenPos.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, angle, 0);



        // print IsGround to console
        print("IsGround: " + IsGround);
        // press space to jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGround)
        {
            Velocity.y = 5f;
        }

        // click left mouse button to attack
        if (Input.GetMouseButtonDown(0))
        {
            // play attack animation
            animator.SetTrigger("TriggerAttack");
        }

    }




    private void MoveLikeWoW()
    {
        GravityDrivenMove();

        // print Vector3.forward, Vector3.right, Vector3.up to console
        print("Vector3.forward: " + Vector3.forward + " Vector3.right: " + Vector3.right + " Vector3.up: " + Vector3.up);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // print height and radius of controller to console
        print("controller.height: " + controller.height + " controller.radius: " + controller.radius);
        // print position of the character to console
        print("character position: " + transform.position);
        var move = transform.forward * Speed * vertical * Time.deltaTime;
        // print move to console
        print("move: " + move);
        controller.Move(move);

        IsGround = Physics.CheckSphere(GroundCheck.position, CheckRadius, layerMask);
        if (IsGround && Velocity.y < 0)
        {
            Velocity.y = 0;
        }
        // add gravity
        Velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(Velocity * Time.deltaTime);
        transform.Rotate(Vector3.up, horizontal * RotateSpeed);
    }

}