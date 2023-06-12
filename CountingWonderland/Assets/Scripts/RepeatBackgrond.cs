using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackgrond : MonoBehaviour
{

    private Vector3 startPosition;
    private float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.z/2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < startPosition.z - repeatWidth) {
            transform.position = startPosition;
        }
    }
}
