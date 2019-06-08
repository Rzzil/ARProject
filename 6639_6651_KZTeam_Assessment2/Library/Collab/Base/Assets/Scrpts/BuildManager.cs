using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    [Header("Turret build")]
    public GameObject BuildOperationUI;
    public Animator BuildPanelAnimator;
    public Text firstNumber;
    public Text secondNumber;
    public Text firstAnswer;
    public Text SecondAnswer;
    public Text ThirdAnswer;
    public Text Symbol;
    private int trueAnswer;
    public Text BuildEqualSymbol;

    [Header("Turret upgrade")]
    public GameObject upgradeOperationUI;
    public Animator UpgradePanelAnimator;
    public Text UpgradefirstNumber;
    public Text UpgradesecondNumber;
    public Text UpgradefirstAnswer;
    public Text UpgradeSecondAnswer;
    public Text UpgradeThirdAnswer;
    public Text UpgradeSymbol;
    private int UpgradetrueAnswer;
    public Text UpgradeEqualSymbol;

    public Text QuestionType;

    public Text UpgradeTowerType;

    public Text UpgradeCurretLvl;

    [Header("Turret Destroy")]
    public GameObject DestroyOperationUI;

    MapCube mapCube;

    //the lock of mouse when ui shows
    private bool isShowBuildUI;
    private bool isShowUpgradeUI;
    private bool isShowDestroyUI;

    void Update()
    {
        //if (Input.GetMouseButtonDown(0) && isShowBuildUI == false && isShowUpgradeUI == false && isShowDestroyUI == false)
        if (Input.GetMouseButtonDown(0) && isShowBuildUI == false && isShowUpgradeUI == false && isShowDestroyUI == false)
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000.0f, LayerMask.GetMask("MapCube")))
                {
                    mapCube = hit.collider.GetComponent<MapCube>();
                    if (mapCube.currentTurret == null)
                    {
                        //can build turret
                        TDSoundManager.instance.playButtonSound();
                        showBuildOperationUI();
                        hideUpgradeOperationUI();
                        hideDestroyOperationUI();
                    }
                    else if(mapCube.currentTurret != null && mapCube.TurrentLevel != 5 && mapCube.TurrentLevel != 10 && mapCube.TurrentLevel != 15 && mapCube.TurrentLevel != 20)
                    {
                        //need upgrade turret
                        TDSoundManager.instance.playButtonSound();
                        showUpgradeOperationUI();
                        HideBuildOperationUI();
                        hideDestroyOperationUI();
                    }
                    else
                    {
                        //the max upgrade situation, need to show shut destroy turret ui
                        TDSoundManager.instance.playButtonSound();
                        showDestroyOperationUI();
                        HideBuildOperationUI();
                        hideUpgradeOperationUI();
                    }
                }
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            HideBuildOperationUI();
            hideUpgradeOperationUI();
            isShowBuildUI = false;
            isShowUpgradeUI = false;
            isEqualOpen = false;
            trueCompare = -1;
            updateTrueCompare = -1;
            isUpdateEqualson = false;

            hideDestroyOperationUI();
            isShowDestroyUI = false;
}
    }

    //-------destroy parts---------
    public void showDestroyOperationUI()
    {
        DestroyOperationUI.SetActive(true);
        isShowDestroyUI = true;
    }

    public void hideDestroyOperationUI()
    {
        DestroyOperationUI.SetActive(false);
        isShowDestroyUI = false;
    }

    //destroy turret
    public void destroyTurret()
    {
        mapCube.DestroyCurrentTurret();
        hideUpgradeOperationUI();
        hideDestroyOperationUI();
        HideBuildOperationUI();
    }

    //-------build parts---------

    public void showBuildOperationUI()
    {
        BuildOperationUI.SetActive(true);
        isShowBuildUI = true;
    }

    public void HideBuildOperationUI()
    {
        BuildOperationUI.SetActive(false);
        isShowBuildUI = false;
    }

    //for determing the different calculating type
    private int pubSignSymbol = -1;

    //for the equals turret comparing
    private int trueCompare = -1;

    //for the euals turret switch
    private bool isEqualOpen = false;

    //-------showquestionui paremeter 0 = plus, 1 = minus, 2 = mult, 3 = euqual
    public void showQuestionUI(int signSymbol)
    {

        TDSoundManager.instance.playButtonSound();

        int firstNumberTemp = 0;
        int secondNumberTemp = 0;

        List<Text> answers = new List<Text>();

        int TrueAnswerIndex;
        BuildPanelAnimator.SetTrigger("showQuestion");

        //paremeter 0 = plus, 1 = minus, 2 = mult, 3 = euqual
        switch (signSymbol)
        {
            case 0:
                firstNumberTemp = (int)Random.Range(5, 10);
                secondNumberTemp = (int)Random.Range(1, 5);
                BuildEqualSymbol.text = "=";
                break;
            case 1:
                firstNumberTemp = (int)Random.Range(5, 10);
                secondNumberTemp = (int)Random.Range(1, 5);
                BuildEqualSymbol.text = "=";
                break;
            case 2:
                firstNumberTemp = (int)Random.Range(0, 3);
                secondNumberTemp = (int)Random.Range(0, 3);
                BuildEqualSymbol.text = "=";
                break;
            case 3:
                firstNumberTemp = (int)Random.Range(1, 10);
                secondNumberTemp = (int)Random.Range(1, 10);
                BuildEqualSymbol.text = "";
                isEqualOpen = true;
                break;
            default:
                break;
        }
        firstNumber.text = "" + firstNumberTemp;
        secondNumber.text = "" + secondNumberTemp;

        TrueAnswerIndex = (int)Random.Range(0,3);

        answers.Add(firstAnswer);
        answers.Add(SecondAnswer);
        answers.Add(ThirdAnswer);

        //paremeter 0 = plus, 1 = minus, 2 = mult, 3 = euqual
        switch (signSymbol)
        {
            case 0:
                Symbol.text = "+";
                QuestionType.text = "Plus";
                pubSignSymbol = signSymbol;
                break;
            case 1:
                Symbol.text = "-";
                QuestionType.text = "Minus";
                pubSignSymbol = signSymbol;
                break;
            case 2:
                Symbol.text = "×";
                QuestionType.text = "Times";
                pubSignSymbol = signSymbol;
                break;
            case 3:
                Symbol.text = "?";
                QuestionType.text = "Compare";
                pubSignSymbol = signSymbol;
                break;
            default:
                break;
        }
        //the situation of equal situation
        if(signSymbol == 3)
        {
            firstAnswer.text = ">";
            SecondAnswer.text = "<";
            ThirdAnswer.text = "=";
            if (firstNumberTemp > secondNumberTemp)
                trueCompare = 1;
            else if (firstNumberTemp < secondNumberTemp)
                trueCompare = 2;
            else
                trueCompare = 3;
        }
        else
        {
            int preTemp = 0;
            foreach (Text answer in answers)
            {
                if (answer != answers[TrueAnswerIndex])
                {
                    int temp;
                    do
                    {
                        temp = Random.Range(-2, 3);
                    } while (temp == 0 || temp == preTemp);

                    //-------showquestionui paremeter 0 = plus, 1 = minus, 2 = mult, 3 = euqual
                    switch (signSymbol)
                    {
                        case 0:
                            answer.text = "" + (secondNumberTemp + firstNumberTemp + temp);
                            preTemp = temp;
                            break;
                        case 1:
                            answer.text = "" + (firstNumberTemp - secondNumberTemp + temp);
                            preTemp = temp;
                            break;
                        case 2:
                            answer.text = "" + (firstNumberTemp * secondNumberTemp + temp);
                            preTemp = temp;
                            break;
                        default:
                            break;
                    }
                }
            }
            switch (signSymbol)
            {
                case 0:
                    answers[TrueAnswerIndex].text = "" + (secondNumberTemp + firstNumberTemp);
                    trueAnswer = secondNumberTemp + firstNumberTemp;
                    break;
                case 1:
                    answers[TrueAnswerIndex].text = "" + (firstNumberTemp - secondNumberTemp);
                    trueAnswer = firstNumberTemp - secondNumberTemp;
                    break;
                case 2:
                    answers[TrueAnswerIndex].text = "" + (firstNumberTemp * secondNumberTemp);
                    trueAnswer = firstNumberTemp * secondNumberTemp;
                    break;
                default:
                    break;
            }
        }
    }

    //-------paremeter 0 = plus, 1 = minus, 2 = mult, 3 = euqual
    public void OnSubmitFirstAnswer()
    {
        if(isEqualOpen)
        {
            if (trueCompare == 1)
            {
                //answer correct, can build turret
                mapCube.BuildTurret(pubSignSymbol);
                HideBuildOperationUI();
                isEqualOpen = false;
            }
            else
            {
                //answer wrong, reset all numbers
                BuildPanelAnimator.Play("BuildingWrongAnswerAnnim", -1, 0);
                firstAnswer.GetComponentInParent<Animator>().Play("AnswerShake", -1, 0);

                TDSoundManager.instance.playWrongSound();
                //Invoke("showQuestionUI",0.9f);
                showQuestionUI(pubSignSymbol);
            }
        }
        else
        {
            if (int.Parse(firstAnswer.text) == trueAnswer)
            {
                //answer correct, can build turret
                mapCube.BuildTurret(pubSignSymbol);
                HideBuildOperationUI();
            }
            else
            {
                //answer wrong, reset all numbers
                BuildPanelAnimator.Play("BuildingWrongAnswerAnnim", -1, 0);
                firstAnswer.GetComponentInParent<Animator>().Play("AnswerShake", -1, 0);

                TDSoundManager.instance.playWrongSound();
                //Invoke("showQuestionUI",0.9f);
                showQuestionUI(pubSignSymbol);
            }
        }
    }
    public void OnSubmitSecondAnswer()
    {
        if (isEqualOpen)
        {
            if (trueCompare == 2)
            {
                //answer correct, can build turret
                mapCube.BuildTurret(pubSignSymbol);
                HideBuildOperationUI();
                isEqualOpen = false;
            }
            else
            {
                //answer wrong, reset all numbers
                BuildPanelAnimator.Play("BuildingWrongAnswerAnnim", -1, 0);
                SecondAnswer.GetComponentInParent<Animator>().Play("AnswerShake", -1, 0);

                TDSoundManager.instance.playWrongSound();
                //Invoke("showQuestionUI",0.9f);
                showQuestionUI(pubSignSymbol);
            }
        }
        else
        {
            if (int.Parse(SecondAnswer.text) == trueAnswer)
            {
                //can build turret
                mapCube.BuildTurret(pubSignSymbol);
                HideBuildOperationUI();
            }
            else
            {
                //reset all numbers
                //showQuestionUI();
                BuildPanelAnimator.Play("BuildingWrongAnswerAnnim", -1, 0);
                SecondAnswer.GetComponentInParent<Animator>().Play("AnswerShake", -1, 0);

                TDSoundManager.instance.playWrongSound();
                //Invoke("showQuestionUI", 0.9f);
                showQuestionUI(pubSignSymbol);
            }
        }
    }
    public void OnSubmitThirdAnswer()
    {
        if (isEqualOpen)
        {
            if (trueCompare == 3)
            {
                //answer correct, can build turret
                mapCube.BuildTurret(pubSignSymbol);
                HideBuildOperationUI();
                isEqualOpen = false;
            }
            else
            {
                //answer wrong, reset all numbers
                BuildPanelAnimator.Play("BuildingWrongAnswerAnnim", -1, 0);
                ThirdAnswer.GetComponentInParent<Animator>().Play("AnswerShake", -1, 0);

                TDSoundManager.instance.playWrongSound();
                //Invoke("showQuestionUI",0.9f);
                showQuestionUI(pubSignSymbol);
            }
        }
        else
        {
            if (int.Parse(ThirdAnswer.text) == trueAnswer)
            {
                //can build turret
                mapCube.BuildTurret(pubSignSymbol);
                HideBuildOperationUI();
            }
            else
            {
                //reset all numbers
                //showQuestionUI();
                BuildPanelAnimator.Play("BuildingWrongAnswerAnnim", -1, 0);
                ThirdAnswer.GetComponentInParent<Animator>().Play("AnswerShake", -1, 0);

                TDSoundManager.instance.playWrongSound();
                //Invoke("showQuestionUI", 0.9f);
                showQuestionUI(pubSignSymbol);
            }
        }
    }



    //--------upgrade parts-------------

    public void showUpgradeOperationUI()
    {
        //determine the sign of questions
        if (mapCube.TurrentLevel >= 1 && mapCube.TurrentLevel <= 4)
        {
            UpgradeSymbol.text = "+";
            UpgradeEqualSymbol.text = "=";
            UpgradeTowerType.text = "Plus";
            switch (mapCube.TurrentLevel)
            {
                case 1:
                    UpgradeCurretLvl.text = "Level 1 out of 5";
                    break;
                case 2:
                    UpgradeCurretLvl.text = "Level 2 out of 5";
                    break;
                case 3:
                    UpgradeCurretLvl.text = "Level 3 out of 5";
                    break;
                case 4:
                    UpgradeCurretLvl.text = "Level 4 out of 5";
                    break;
            }

        }
        else if (mapCube.TurrentLevel >= 6 && mapCube.TurrentLevel <= 9)
        {
            UpgradeSymbol.text = "-";
            UpgradeEqualSymbol.text = "=";
            UpgradeTowerType.text = "Minus";
            switch (mapCube.TurrentLevel)
            {
                case 6:
                    UpgradeCurretLvl.text = "Level 1 out of 5";
                    break;
                case 7:
                    UpgradeCurretLvl.text = "Level 2 out of 5";
                    break;
                case 8:
                    UpgradeCurretLvl.text = "Level 3 out of 5";
                    break;
                case 9:
                    UpgradeCurretLvl.text = "Level 4 out of 5";
                    break;
            }
        }
        else if (mapCube.TurrentLevel >= 11 && mapCube.TurrentLevel <= 14)
        {
            UpgradeSymbol.text = "×";
            UpgradeEqualSymbol.text = "=";
            UpgradeTowerType.text = "Times";
            switch (mapCube.TurrentLevel)
            {
                case 11:
                    UpgradeCurretLvl.text = "Level 1 out of 5";
                    break;
                case 12:
                    UpgradeCurretLvl.text = "Level 2 out of 5";
                    break;
                case 13:
                    UpgradeCurretLvl.text = "Level 3 out of 5";
                    break;
                case 14:
                    UpgradeCurretLvl.text = "Level 4 out of 5";
                    break;
            }
        }
        else if (mapCube.TurrentLevel >= 16 && mapCube.TurrentLevel <= 19)
        {
            UpgradeSymbol.text = "?";
            UpgradeEqualSymbol.text = "";
            UpgradeTowerType.text = "Equals";
            switch (mapCube.TurrentLevel)
            {
                case 1:
                    UpgradeCurretLvl.text = "Level 1 out of 5";
                    break;
                case 2:
                    UpgradeCurretLvl.text = "Level 2 out of 5";
                    break;
                case 3:
                    UpgradeCurretLvl.text = "Level 3 out of 5";
                    break;
                case 4:
                    UpgradeCurretLvl.text = "Level 4 out of 5";
                    break;
            }
        }
        TDSoundManager.instance.playButtonSound();
        upgradeOperationUI.SetActive(true);
        isShowUpgradeUI = true;
    }

    public void hideUpgradeOperationUI()
    {
        upgradeOperationUI.SetActive(false);
        isShowUpgradeUI = false;
    }
    //for the equals compare and switch
    private int updateTrueCompare = -1;
    private bool isUpdateEqualson = false;

    //show the questions
    public void showUpgradeQuestionUI()
    {
        TDSoundManager.instance.playButtonSound();

        int firstNumberTemp = 0;
        int secondNumberTemp = 0;

        List<Text> answers = new List<Text>();

        int TrueAnswerIndex;

        UpgradePanelAnimator.SetTrigger("showQuestion");

        if (mapCube.TurrentLevel == 1 || mapCube.TurrentLevel == 6)
        {
            firstNumberTemp = (int)Random.Range(5, 10);
            UpgradefirstNumber.text = "" + firstNumberTemp;
            secondNumberTemp = (int)Random.Range(1, 5);
            UpgradesecondNumber.text = "" + secondNumberTemp;
        }
        else if (mapCube.TurrentLevel == 2 || mapCube.TurrentLevel == 7)
        {
            firstNumberTemp = (int)Random.Range(30, 35);
            UpgradefirstNumber.text = "" + firstNumberTemp;
            secondNumberTemp = (int)Random.Range(25, 30);
            UpgradesecondNumber.text = "" + secondNumberTemp;
        }
        else if (mapCube.TurrentLevel == 3 || mapCube.TurrentLevel == 8)
        {
            firstNumberTemp = (int)Random.Range(60, 65);
            UpgradefirstNumber.text = "" + firstNumberTemp;
            secondNumberTemp = (int)Random.Range(55, 60);
            UpgradesecondNumber.text = "" + secondNumberTemp;
        }
        else if (mapCube.TurrentLevel == 4 || mapCube.TurrentLevel == 9)
        {
            firstNumberTemp = (int)Random.Range(100, 105);
            UpgradefirstNumber.text = "" + firstNumberTemp;
            secondNumberTemp = (int)Random.Range(95, 100);
            UpgradesecondNumber.text = "" + secondNumberTemp;
        }
        else if (mapCube.TurrentLevel == 11)
        {
            firstNumberTemp = (int)Random.Range(2, 4);
            UpgradefirstNumber.text = "" + firstNumberTemp;
            secondNumberTemp = (int)Random.Range(2, 4);
            UpgradesecondNumber.text = "" + secondNumberTemp;
        }
        else if (mapCube.TurrentLevel == 12)
        {
            firstNumberTemp = (int)Random.Range(4, 7);
            UpgradefirstNumber.text = "" + firstNumberTemp;
            secondNumberTemp = (int)Random.Range(4, 7);
            UpgradesecondNumber.text = "" + secondNumberTemp;
        }
        else if (mapCube.TurrentLevel == 13)
        {
            firstNumberTemp = (int)Random.Range(6, 8);
            UpgradefirstNumber.text = "" + firstNumberTemp;
            secondNumberTemp = (int)Random.Range(6, 8);
            UpgradesecondNumber.text = "" + secondNumberTemp;
        }
        else if (mapCube.TurrentLevel == 14)
        {
            firstNumberTemp = (int)Random.Range(8, 10);
            UpgradefirstNumber.text = "" + firstNumberTemp;
            secondNumberTemp = (int)Random.Range(8, 10);
            UpgradesecondNumber.text = "" + secondNumberTemp;
        }
        else if (mapCube.TurrentLevel == 16)
        {
            firstNumberTemp = (int)Random.Range(20, 100);
            UpgradefirstNumber.text = "" + firstNumberTemp;
            secondNumberTemp = (int)Random.Range(20, 100);
            UpgradesecondNumber.text = "" + secondNumberTemp;
        }
        else if (mapCube.TurrentLevel == 17)
        {
            firstNumberTemp = (int)Random.Range(100, 300);
            UpgradefirstNumber.text = "" + firstNumberTemp;
            secondNumberTemp = (int)Random.Range(100, 300);
            UpgradesecondNumber.text = "" + secondNumberTemp;
        }
        else if (mapCube.TurrentLevel == 18)
        {
            firstNumberTemp = (int)Random.Range(300, 600);
            UpgradefirstNumber.text = "" + firstNumberTemp;
            secondNumberTemp = (int)Random.Range(300, 600);
            UpgradesecondNumber.text = "" + secondNumberTemp;
        }
        else if (mapCube.TurrentLevel == 19)
        {
            firstNumberTemp = (int)Random.Range(600, 1000);
            UpgradefirstNumber.text = "" + firstNumberTemp;
            secondNumberTemp = (int)Random.Range(600, 1000);
            UpgradesecondNumber.text = "" + secondNumberTemp;
        }

        TrueAnswerIndex = (int)Random.Range(0, 3);

        answers.Add(UpgradefirstAnswer);
        answers.Add(UpgradeSecondAnswer);
        answers.Add(UpgradeThirdAnswer);

        //for the euquals answers part
        if(mapCube.TurrentLevel >= 16 && mapCube.TurrentLevel <= 19)
        {
            isUpdateEqualson = true;
            UpgradefirstAnswer.text = ">";
            UpgradeSecondAnswer.text = "<";
            UpgradeThirdAnswer.text = "=";
            if (firstNumberTemp > secondNumberTemp)
            {
                updateTrueCompare = 1;
            }
            else if (firstNumberTemp < secondNumberTemp)
            {
                updateTrueCompare = 2;
            }
            else
                updateTrueCompare = 3;
        }
        else
        {
            int preTemp = 0;
            foreach (Text answer in answers)
            {
                if (answer != answers[TrueAnswerIndex])
                {
                    int temp;
                    do
                    {
                        temp = Random.Range(-2, 3);
                    } while (temp == 0 || temp == preTemp);

                    //the place has different calculation method, 
                    if (mapCube.TurrentLevel >= 1 && mapCube.TurrentLevel <= 4)
                        answer.text = "" + (secondNumberTemp + firstNumberTemp + temp);
                    else if (mapCube.TurrentLevel >= 6 && mapCube.TurrentLevel <= 9)
                        answer.text = "" + (firstNumberTemp - secondNumberTemp + temp);
                    else if (mapCube.TurrentLevel >= 11 && mapCube.TurrentLevel <= 14)
                        answer.text = "" + (firstNumberTemp * secondNumberTemp + temp);
                    preTemp = temp;
                }
            }
            if (mapCube.TurrentLevel >= 1 && mapCube.TurrentLevel <= 4)
            {
                answers[TrueAnswerIndex].text = "" + (secondNumberTemp + firstNumberTemp);
                UpgradetrueAnswer = secondNumberTemp + firstNumberTemp;
            }
            else if (mapCube.TurrentLevel >= 6 && mapCube.TurrentLevel <= 9)
            {
                answers[TrueAnswerIndex].text = "" + (firstNumberTemp - secondNumberTemp);
                UpgradetrueAnswer = firstNumberTemp - secondNumberTemp;
            }
            else if (mapCube.TurrentLevel >= 11 && mapCube.TurrentLevel <= 14)
            {
                answers[TrueAnswerIndex].text = "" + (firstNumberTemp * secondNumberTemp);
                UpgradetrueAnswer = firstNumberTemp * secondNumberTemp;
            }
        }
    }

    public void OnSubmitUpgradeFirstAnswer()
    {
        if(isUpdateEqualson)
        {
            if(updateTrueCompare == 1)
            {
                //can upgrade turret
                mapCube.UpgradeTurrent();
                hideUpgradeOperationUI();
                isUpdateEqualson = false;
            }
            else
            {
                //reset all numbers
                //showUpgradeQuestionUI();
                UpgradePanelAnimator.Play("BuildWrongAnswerAnim", -1, 0);
                UpgradefirstAnswer.GetComponentInParent<Animator>().Play("AnswerShake", -1, 0);

                TDSoundManager.instance.playWrongSound();
                Invoke("showUpgradeQuestionUI", 0.9f);
            }
        }
        else
        {
            if (int.Parse(UpgradefirstAnswer.text) == UpgradetrueAnswer)
            {
                //can upgrade turret
                mapCube.UpgradeTurrent();
                hideUpgradeOperationUI();
            }
            else
            {
                //reset all numbers
                //showUpgradeQuestionUI();
                UpgradePanelAnimator.Play("BuildWrongAnswerAnim", -1, 0);
                UpgradefirstAnswer.GetComponentInParent<Animator>().Play("AnswerShake", -1, 0);

                TDSoundManager.instance.playWrongSound();
                Invoke("showUpgradeQuestionUI", 0.9f);
            }
        }
    }
    public void OnSubmitUpgradeSecondAnswer()
    {
        if (isUpdateEqualson)
        {
            if (updateTrueCompare == 2)
            {
                //can upgrade turret
                mapCube.UpgradeTurrent();
                hideUpgradeOperationUI();
                isUpdateEqualson = false;
            }
            else
            {
                //reset all numbers
                //showUpgradeQuestionUI();
                UpgradePanelAnimator.Play("BuildWrongAnswerAnim", -1, 0);
                UpgradeSecondAnswer.GetComponentInParent<Animator>().Play("AnswerShake", -1, 0);

                TDSoundManager.instance.playWrongSound();
                Invoke("showUpgradeQuestionUI", 0.9f);
            }
        }
        else
        {
            if (int.Parse(UpgradeSecondAnswer.text) == UpgradetrueAnswer)
            {
                //can upgrade turret
                mapCube.UpgradeTurrent();
                hideUpgradeOperationUI();
            }
            else
            {
                //reset all numbers
                //showUpgradeQuestionUI();
                UpgradePanelAnimator.Play("BuildWrongAnswerAnim", -1, 0);
                UpgradeSecondAnswer.GetComponentInParent<Animator>().Play("AnswerShake", -1, 0);

                TDSoundManager.instance.playWrongSound();
                Invoke("showUpgradeQuestionUI", 0.9f);
            }
        }
    }
    public void OnSubmitUpgradeThirdAnswer()
    {
        if (isUpdateEqualson)
        {
            if (updateTrueCompare == 3)
            {
                //can upgrade turret
                mapCube.UpgradeTurrent();
                hideUpgradeOperationUI();
                isUpdateEqualson = false;
            }
            else
            {
                //reset all numbers
                //showUpgradeQuestionUI();
                UpgradePanelAnimator.Play("BuildWrongAnswerAnim", -1, 0);
                UpgradeThirdAnswer.GetComponentInParent<Animator>().Play("AnswerShake", -1, 0);

                TDSoundManager.instance.playWrongSound();
                Invoke("showUpgradeQuestionUI", 0.9f);
            }
        }
        else
        {
            if (int.Parse(UpgradeThirdAnswer.text) == UpgradetrueAnswer)
            {
                //can upgrade turret
                mapCube.UpgradeTurrent();
                hideUpgradeOperationUI();
            }
            else
            {
                //reset all numbers
                //showUpgradeQuestionUI();
                UpgradePanelAnimator.Play("BuildWrongAnswerAnim", -1, 0);
                UpgradeThirdAnswer.GetComponentInParent<Animator>().Play("AnswerShake", -1, 0);

                TDSoundManager.instance.playWrongSound();
                Invoke("showUpgradeQuestionUI", 0.9f);
            }
        }
    }
}
