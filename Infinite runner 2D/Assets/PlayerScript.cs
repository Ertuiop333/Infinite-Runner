using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private float time = 0f;
    private bool air;

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("jump! " + context);

            air = true;

            // to wait before going down
            time = Time.time;
        }
    }

    public void Ground(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Ground! " + context);

            air = false;
        }
    }

    private void Update()
    {
        if (time <= Time.time - 0.45 && air)
        {
            air = false;
        }

        switch (air)
        {
            case true:
                rb.gravityScale = -300;
                break;
            case false:
                rb.gravityScale = 50;
                break;
        }

    }
}
