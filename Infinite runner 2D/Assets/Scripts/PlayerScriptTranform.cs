using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScriptTranform : MonoBehaviour
{
    private HitBoxController[] hitBoxController = new HitBoxController[2];

    [SerializeField] private Rigidbody2D rb;

    private float timeLastInputJump = 0f;
    private float timeBeforeGoingDown = 0f;
    private bool jumped;
    private float timeLastInputGround = 0f;
    private bool grounded;
    private float timeLastInputMiddle = 0f;
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
            if (Time.time - 0.06f <= timeLastInputGround && timeLastInputMiddle <= Time.time - 0.12f)
            {
                grounded = false;

                Middle();

                timeLastInputMiddle = Time.time; // stores the time you middled
            }
            else if (timeLastInputJump <= Time.time - 0.122f)
            {
                jumped = true;

                timeLastInputJump = Time.time;// stores the time you jumped
            }
        }
    }

    public void Ground(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Time.time - 0.06f <= timeLastInputJump && timeLastInputMiddle <= Time.time - 0.12f)
            {
                jumped = false;

                Middle();

                timeLastInputMiddle = Time.time; // stores the time you middled
            }
            else if (timeLastInputGround <= Time.time - 0.122f)
            {
                grounded = true;

                timeLastInputGround = Time.time; // stores the time you grounded
            }
        }
    }

    void Middle()
    {
        bool hitNoteAir;
        hitNoteAir = hitBoxController[1].CheckNote();
        bool hitNoteGround;
        hitNoteGround = hitBoxController[0].CheckNote();

        air = 1;

        timeBeforeGoingDown = Time.time;
    }

    private void Update()
    {
        // air waiter
        if (timeBeforeGoingDown <= Time.time - 0.6f && air >= 1)
            air = 0;

        // action jump
        if (timeLastInputJump <= Time.time - 0.061f && jumped)
        {
            bool hitNoteAir;
            hitNoteAir = hitBoxController[1].CheckNote();

            if (hitNoteAir || air == -1)
            {
                air = 2;

                if (hitBoxController[0].note != null)
                    hitBoxController[0].note.transform.localScale = new Vector2(hitBoxController[0].note.transform.localScale.x / 2, hitBoxController[0].note.transform.localScale.y / 2);

                timeBeforeGoingDown = Time.time;
            }

            jumped = false;
        }
        // action ground
        if (timeLastInputGround <= Time.time - 0.061f && grounded)
        {
            bool hitNoteGround;
            hitNoteGround = hitBoxController[0].CheckNote();

            air = -1;

            if (hitBoxController[1].note != null)
                hitBoxController[1].note.transform.localScale = new Vector2(hitBoxController[1].note.transform.localScale.x / 2, hitBoxController[1].note.transform.localScale.y / 2);

            grounded = false;
        }

        // actualy switch the gravity scale based on the air number
        switch (air)
        {
            case 2:
                rb.velocity = new Vector2(0, 0);
                transform.position = new Vector2(transform.position.x, 4f);
                break;
            case 1:
                rb.velocity = new Vector2(0, 0);
                transform.position = new Vector2(transform.position.x, 2.5f);
                break;
            case 0:
                rb.isKinematic = false;
                rb.velocity = new Vector2(0, -9);
                break;
            case -1:
                rb.velocity = new Vector2(0, 0);
                transform.position = new Vector2(transform.position.x, 1);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            air = -1;
        }
    }
}
