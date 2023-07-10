using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && this.gameObject.CompareTag("QuestionGate"))
        {
            SceneManager.LoadScene("GuardianGate");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (coll.gameObject.CompareTag("Player") && this.gameObject.CompareTag("NextLevel"))
        {
            // SceneManager.LoadScene("GuardianGate");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
