using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private Vector2 movingToPos;
    private Vector2 groundPos;
    private Vector2 airPos;
    private float time = 0f;
    private bool inAir;

    private void Awake()
    {
        groundPos = transform.position;
        airPos = new Vector2(transform.position.x, 4);
        movingToPos = groundPos;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("jump! " + context);

            movingToPos = airPos;

            inAir = true;

            time = Time.time;
        }
    }

    public void Ground(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Ground! " + context);

            movingToPos = groundPos;

            inAir = false;
        }
    }

    private void Update()
    {
        if (time <= Time.time - 0.8 && inAir)
        {
            movingToPos = groundPos;
            inAir = false;
        }

        transform.position = movingToPos;
    }
}
