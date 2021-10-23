using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    private bool collide;
    [HideInInspector] public GameObject note;

    public bool CheckNote()
    {
        bool hit;

        if (collide)
        {
            Destroy(note.gameObject);

            hit = true;
        }
        else
        {
            hit = false;
        }

        return hit;
    }

    public void DestroyNote()
    {
        Destroy(note.gameObject);
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
