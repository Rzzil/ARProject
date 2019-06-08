using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public GameObject startPoint;

    public static EnemySpawner instance;

    void Awake()
    {
        instance = this;
    }

    public void SummonEnemy()
    {
        Instantiate(enemy, startPoint.transform.position, Quaternion.identity, startPoint.transform);
    }
}
