using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;
    private float idleTimer = 0;
    [SerializeField] private Animator anim;

    // Update is called once per frame
    void Update()
    {
        // if(patrolDestination == 0)
        // {
            // if(Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
            // {
            //     if (idleTimer < 3){
            //         // anim.SetBool("Walk", false);
            //         idleTimer += Time.deltaTime;
            //     } else {
            //         idleTimer = 0;
            //         transform.localScale = new Vector3(1, 1, 1);
            //         patrolDestination = 1;
            //         // anim.SetBool("Walk", true);
            //     }
            // } else {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
                Debug.Log(patrolPoints[0].position);
            // }
        // }

        // if(patrolDestination == 1)
        // {
        //     if(Vector2.Distance(transform.position, patrolPoints[1].position)< 0.2f)
        //     {
        //         if (idleTimer < 3)
        //         {
        //             // anim.SetBool("Walk", false);
        //             idleTimer += Time.deltaTime;
        //         } else {
        //             idleTimer = 0;
        //             transform.localScale = new Vector3(-1, 1, 1);
        //             patrolDestination = 0;
        //             // anim.SetBool("Walk", true);
        //         }
        //     } else {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
            // }
        // }
    }
}
