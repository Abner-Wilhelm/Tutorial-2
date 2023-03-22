using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
// Transforms to act as start and end markers for the journey.
{ 
public GameObject point1;
    public GameObject point2;
    public float movementSpeed;
    private bool touchedFirstPoint;

    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        touchedFirstPoint = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!touchedFirstPoint)
        {
          transform.Rotate(0,0,-rotationSpeed);
            transform.position = Vector2.MoveTowards(transform.position, point1.transform.position, movementSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, point1.transform.position) == 0)
            {
                touchedFirstPoint = true;
            }
        } 
        else
        {
        transform.Rotate(0,0,rotationSpeed);
            transform.position = Vector2.MoveTowards(transform.position, point2.transform.position, movementSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, point2.transform.position) == 0)
            {
                touchedFirstPoint = false;
            }
        }

    }
}
