using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gm : MonoBehaviour
{
    public static Gm instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        EnemySpawner.instance.SummonEnemy();
    }

    public void TakeDamage(float damage)
    {

    }

    public void MenuScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
