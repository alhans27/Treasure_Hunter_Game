using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private bool once;
    private GameManager gm;
    private CheckpointMaster cm;
    public static bool _notLoad = true;
    public static List<string> goName = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        once = false;
        cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckpointMaster>();
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if (cm.collect.Count != 0)
        {
            gm.CoinCollected = cm.collect[0];
        }


        if (cm.obj.Count > 0)
        {
            goName.Clear();
            goName = new List<string>(cm.obj);
            foreach (string i in goName)
            {
                GameObject.Find(i).SetActive(false);
            }
        }
        Debug.Log(CheckpointMaster.isLoaded);
        StartCoroutine(Load());
    }

    private void OnTriggerEnter2D(Collider2D Collide)
    {
        if (Collide.CompareTag("Player") && once == false)
        {
            anim.SetBool("expand", true);
            anim.SetTrigger("fluttering");
            once = true;
            cm.lastCheckpointPos = transform.position;
            cm.SaveCollect(gm.CoinCollected);
            cm.SaveObj();
        }
    }

    private IEnumerator Load()
    {
        for (var j = 0; j < 2; j++)
        {
            if (CheckpointMaster.isLoaded == true)
            {
                // Debug.Log(CheckpointMaster.isLoaded);
                if (cm.collect_load.Count != 0)
                {
                    gm.CoinCollected = cm.collect_load[1];
                }

                if (cm.obj_load.Count != 0)
                {
                    goName.Clear();
                    goName = new List<string>(cm.obj_load);
                    foreach (string i in goName)
                    {
                        print(i);
                        if (GameObject.Find(i))
                        {
                            GameObject.Find(i).SetActive(false);
                        }
                    }
                }
            }
            yield return null;
        }
        CheckpointMaster.isLoaded = false;
    }
}
