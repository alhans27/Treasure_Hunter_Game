using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;
    

    // Update is called once per frame
    void Update()
    {
        if(patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
            {
                patrolDestination = 1;
            }
        }

        if(patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position, patrolPoints[1].position) < 0.2f)
            {
                patrolDestination = 0;
            }
        }
    }
}
