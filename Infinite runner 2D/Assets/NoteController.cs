using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    [SerializeField] private float speed = 6;
    [SerializeField] private float lifeDistance;
    private Vector2 initialPos;

    private void Awake()
    {
        initialPos = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < initialPos.x - lifeDistance)
            Destroy(gameObject);
    }
}
