using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private bool once;

    // Start is called before the first frame update
    void Start()
    {
        once = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D Collide)
    {
        if(Collide.name == "Player")
        {
            if (once == false)
            {
                anim.SetBool("expand", true);
                anim.SetTrigger("fluttering");
                once = true;
            }
        }

    }
}
