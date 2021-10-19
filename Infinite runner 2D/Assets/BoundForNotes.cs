using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundForNotes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Note"))
            Destroy(collision.gameObject);
    }
}
