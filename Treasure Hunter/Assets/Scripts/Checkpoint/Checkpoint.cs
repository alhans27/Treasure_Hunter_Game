using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private bool once;
    private CheckpointMaster cm;
    public static bool _notLoad = true;
    public static List<string> goName = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckpointMaster>();

        once = false;
        if (cm.checkpos.Contains(gameObject.name))
        {
            once = true;
            anim.SetBool("expand", true);
            anim.SetTrigger("fluttering");
        }

        if (cm.coin > 0)
        {
            GameManager.Instance.CoinCollected = cm.coin;
        }


        if (cm.obj.Count > 0)
        {
            goName.Clear();
            goName = new List<string>(cm.obj);
            foreach (string i in goName)
            {
                if (GameObject.Find(i))
                {
                    GameObject.Find(i).SetActive(false);
                }
            }
        }
        StartCoroutine(Load());
    }

    private void OnTriggerEnter2D(Collider2D Collide)
    {
        if (Collide.CompareTag("Player") && once == false)
        {
            anim.SetBool("expand", true);
            anim.SetTrigger("fluttering");
            cm.checkpos.Add(gameObject.name);
            cm.lastCheckpointPos = transform.position;
            cm.SaveCoin(GameManager.Instance.CoinCollected);
            cm.SaveObj();
        }
    }

    private IEnumerator Load()
    {
        for (var j = 0; j < 2; j++)
        {
            if (CheckpointMaster.isLoaded == true)
            {
                if (cm.coin_load != 0)
                {
                    GameManager.Instance.CoinCollected = cm.coin_load;
                }

                if (cm.obj_load.Count != 0)
                {
                    goName.Clear();
                    goName = new List<string>(cm.obj_load);
                    foreach (string i in goName)
                    {
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
