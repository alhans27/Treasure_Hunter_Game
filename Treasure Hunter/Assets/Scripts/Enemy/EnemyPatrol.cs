using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolBehaviour;
    private int patrolBehaviourBefore;
    private float idleTimer = 0;
    private string action;
    [SerializeField] private Animator anim;

    private void Start() {
        if (!gameObject.CompareTag("Enemy Bats")){
            anim.SetBool("Walk", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Enemy Bats"))
        {
            if(patrolBehaviour == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
                if(Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    patrolBehaviour = 1;
                }
            }

            else if(patrolBehaviour == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
                if(Vector2.Distance(transform.position, patrolPoints[1].position)< 0.2f)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    patrolBehaviour = 0;
                }
            }
        }

        else 
        {           
            if(patrolBehaviour == 0)
            {
                if(Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
                {
                    if (idleTimer < 3){
                        anim.SetBool("Walk", false);
                        idleTimer += Time.deltaTime;
                    } else {
                        idleTimer = 0;
                        transform.localScale = new Vector3(1, 1, 1);
                        patrolBehaviour = 1;
                        anim.SetBool("Walk", true);
                    }
                } else {
                    transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
                }
            }

            else if(patrolBehaviour == 1)
            {
                if(Vector2.Distance(transform.position, patrolPoints[1].position)< 0.2f)
                {
                    if (idleTimer < 3)
                    {
                        anim.SetBool("Walk", false);
                        idleTimer += Time.deltaTime;
                    } else {
                        idleTimer = 0;
                        transform.localScale = new Vector3(-1, 1, 1);
                        patrolBehaviour = 0;
                        anim.SetBool("Walk", true);
                    }
                } else {
                    transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
                }
            }

            else if (patrolBehaviour == 2)
            {
                idleTimer += Time.deltaTime;
                if (idleTimer < 0.6f)
                {
                    anim.SetTrigger(action);
                } else if (idleTimer < 1.3f)
                {
                    anim.SetBool("Walk", false);
                } else if (idleTimer > 1.5f){
                    anim.SetBool("Walk", true);
                    patrolBehaviour = patrolBehaviourBefore;
                }
            }
        }
    }

    public void Behaviour (string act)
    {
        patrolBehaviourBefore = patrolBehaviour;
        patrolBehaviour = 2;
        idleTimer = 0;
        action = act;
    }
}
