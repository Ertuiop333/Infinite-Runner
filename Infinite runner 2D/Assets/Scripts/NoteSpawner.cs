
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private GameObject note;
    private GameObject noteinstanse;

    private float time = 0;
    private float interval = 0.7f;

    private void Update()
    {
        time += Time.deltaTime;

        if (time > interval)
        {
            time = 0;

            int pos = Random.Range(1, 4);

            switch (pos)
            {
                case 1:
                    noteinstanse = Instantiate(note, new Vector3(15, 1), Quaternion.identity);
                    noteinstanse.GetComponent<NoteController>().speed = Random.Range(12, 15);
                    break;

                case 2:
                    float speed = Random.Range(12, 15);

                    noteinstanse = Instantiate(note, new Vector3(15, 1), Quaternion.identity);
                    noteinstanse.GetComponent<NoteController>().speed = speed;

                    noteinstanse = Instantiate(note, new Vector3(15, 4), Quaternion.identity);
                    noteinstanse.GetComponent<NoteController>().speed = speed;
                    break;

                case 3:
                    noteinstanse = Instantiate(note, new Vector3(15, 4), Quaternion.identity);
                    noteinstanse.GetComponent<NoteController>().speed = Random.Range(12, 15);
                    break;
            }
        }

        
    }
}
