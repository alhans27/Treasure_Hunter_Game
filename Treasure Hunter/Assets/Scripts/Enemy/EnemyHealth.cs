using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float totalHealth;

    public void damaged(float damagePoint)
    {
        totalHealth -= damagePoint;
        return;
    }
}
