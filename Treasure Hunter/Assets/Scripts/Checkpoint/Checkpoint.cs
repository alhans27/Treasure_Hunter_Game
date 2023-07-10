using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private bool once;
    private GameManager gm;
    private CheckpointMaster cm;
    public static List<string> goName = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        once = false;
        cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckpointMaster>();
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();

        goName.Clear();
        if (cm.collect.Count != 0)
        {
            gm.DiamondCollected = cm.collect[0];
            gm.CoinCollected = cm.collect[1];

        }

        if (cm.obj.Count > 0)
        {
            goName = new List<string>(cm.obj);
            foreach (var i in goName)
            {
                GameObject.Find(i).SetActive(false);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D Collide)
    {
        if (Collide.CompareTag("Player") && once == false)
        {
            anim.SetBool("expand", true);
            anim.SetTrigger("fluttering");
            once = true;
            cm.lastCheckpointPos = transform.position;
            cm.SaveCollect(gm.DiamondCollected, gm.CoinCollected);
            cm.SaveObj();
        }
    }
}
