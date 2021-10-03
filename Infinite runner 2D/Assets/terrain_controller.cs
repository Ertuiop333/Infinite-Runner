using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrain_controller : MonoBehaviour
{
    private Vector2 initialPos;

    private void Awake()
    {
        initialPos = transform.position;
    }

    void Update()
    {
        if (transform.position.x <= initialPos.x - transform.localScale.x / 2)
            transform.position = initialPos;
        else
            transform.Translate(Vector2.left * Time.deltaTime * 10f);

        
    }
}
