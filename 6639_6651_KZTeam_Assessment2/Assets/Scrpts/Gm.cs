using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gm : MonoBehaviour
{
    public static Gm instance;
    public Text[] answer;

    public bool isFirstTurretShoot;
    public bool isSecondTurretShoot;
    public bool isThirdTurretShoot;

    public bool isfirstButtonClick;
    public bool isSecondButtonClick;
    public bool isthirdButtonClick;

    public Text hpUI;
    public Text remainUI;
    public GameObject winUI;
    public GameObject loseUI;

    public int hp;
    public int remain;

    void Awake()
    {
        instance = this;
        hp = 3;
        remain = 10;
    }

    void Start()
    {
        EnemySpawner.instance.SummonEnemy();
    }

    void Update()
    {
        hpUI.text = "Chance: " + hp + " / 3";
        remainUI.text = "Remain: " + remain;

        if (hp == 0)
            loseUI.SetActive(true);
        if (remain == 0 && hp != 0)
            winUI.SetActive(true);
    }

    public void MenuScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void onClickFirst()
    {
        isfirstButtonClick = true;
    }
    public void onClickSecond()
    {
        isSecondButtonClick = true;
    }
    public void onClickThrid()
    {
        isthirdButtonClick = true;
    }

    public void Replay()
    {
        SceneManager.LoadScene("Main");
    }
}
