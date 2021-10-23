using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private HitBoxController[] hitBoxController = new HitBoxController[2];
    
    [SerializeField] private Rigidbody2D rb;

    private float timeLastJumped = 0f;
    private bool jumped;
    private float timeLastGrounded = 0f;
    private bool grounded;
    private float timeLastMiddled = 0f;
    private bool middled;
    private int air;

    private void Awake()
    {
        hitBoxController[0] = GameObject.FindGameObjectWithTag("HitBoxG").GetComponent<HitBoxController>();
        hitBoxController[1] = GameObject.FindGameObjectWithTag("HitBoxA").GetComponent<HitBoxController>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Time.time - 0.08f <= timeLastGrounded) // pressed grounded 0.08 sec ago
            {
                middled = true;
                
                Middle();

                timeLastMiddled = Time.time;
            }
            else
            {
                jumped = true;

                timeLastJumped = Time.time;// stores the time you jumped
            }
        }
    }

    public void Ground(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Time.time - 0.08f <= timeLastJumped) // pressed jumped 0.08 sec ago
            {
                middled = true;

                Middle();

                timeLastMiddled = Time.time;
            }
            else
            {
                grounded = true;

                timeLastGrounded = Time.time; // stores the time you grounded
            }
        }
    }

    void Middle()
    {
        Debug.Log("middled!");

        bool hitNoteAir;
        hitNoteAir = hitBoxController[1].CheckNote();
        bool hitNoteGround;
        hitNoteGround = hitBoxController[0].CheckNote();

        air = 1;

        transform.position = new Vector2(transform.position.x, 2.5f);
    }

    private void Update()
    {
        // air waiters
        if (timeLastJumped <= Time.time - 0.53f && air == 2)
            air = 0;
        
        if (timeLastMiddled <= Time.time - 0.53f && air == 1)
            air = 0;

        // middle wait
        if (timeLastMiddled <= Time.time - 0.20f)
            middled = false;

        // actions
        if (timeLastJumped <= Time.time - 0.08f && jumped)
        {
            bool hitNoteAir;
            hitNoteAir = hitBoxController[1].CheckNote();

            air = 2;

            jumped = false;
        }

        if (timeLastGrounded <= Time.time - 0.08f && grounded)
        {
            bool hitNoteGround;
            hitNoteGround = hitBoxController[0].CheckNote();

            air = -1;

            grounded = false;
        }

        // shut them up if middle (contain the on that triggered first before middle trigered)
        if (middled)
        {
            grounded = false;
            jumped = false;
        }

        // actualy switch the gravity scale based on the air number
        switch (air)
        {
            case 2:
                rb.gravityScale = -100;
                break;
            case 1:
                rb.gravityScale = 0;
                break;
            case 0:
                rb.gravityScale = 20;
                break;
            case -1:
                rb.gravityScale = 70;
                break;
        }
    }
}
