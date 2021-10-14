using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    private bool collide;
    private GameObject note;

    public bool CheckNote()
    {
        bool hit;

        if (collide)
        {
            Debug.Log("hit note!");

            Destroy(note);

            hit = true;
        }
        else
        {
            Debug.Log("didn't hit...");

            hit = false;
        }

        return hit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Note"))
        {
            note = collision.gameObject;
            collide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Note"))
        {
            note = null;
            collide = false;
        }
    }
}
