using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerGame : MonoBehaviour
{
    public static float fuel = 2000;
    [SerializeField] private Text ScoreText;
    [SerializeField] private Text timeText;
    [SerializeField] private Text fuelText;
    [SerializeField] private Text PointText;
    [SerializeField] private GameObject UIVictory;
    [SerializeField] private GameObject UILose;
    [SerializeField] private GameObject LowFuel;
    [SerializeField] private GameObject PressSpaceText;
    public static ManagerGame managerGame;
    private float timer = 0;
    public static int Score;
    public bool StopTimer;
    private bool Space;
    private bool LowfuelActive;


    private void Awake()
    {
        if(!managerGame) managerGame = this;
        ShowUI(false, false, false);
        DisplayScore(0);
        DisplayFuel(0);
    }
    void Update()
    {
        
        if (!StopTimer)
        {
            timer += Time.deltaTime;
            DisplayTime(timer);
        }
        if (Space)
        {
            PressSpace();
        }
        if (fuel < 100 && !LowfuelActive)
        {
            LowfuelActive = true;
            LowFuel.SetActive(true);
        }
        
        
             
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("Timer: {0:00}:{1:00}", minutes, seconds);
    }

    public void DisplayScore(int point)
    {
        Score = Score + point;
        ScoreText.text = string.Format("Score: {0:000}", Score);
    }

    public void DisplayFuel(float removeFuel)
    {
        fuel = fuel - removeFuel;
        fuelText.text = string.Format("Fuel: {0:0000}", fuel);
    }

    public void ShowUI(bool victory, bool lose, bool lowFuel)
    {
        UIVictory.SetActive(victory);
        UILose.SetActive(lose);
        LowFuel.SetActive(lowFuel);
    }

    public void Victory(int victory)
    {
        UIVictory.SetActive(true);
        PointText.text = victory + " Points";
        StartCoroutine(LoadScene(2));
    }
    public void Lose(int lose)
    {
        if (fuel > 0)
        {
            DisplayScore(lose);
            UILose.SetActive(true);
            PressSpaceText.SetActive(false);
            PointText.text = lose + " Points";
            StartCoroutine(LoadScene(2));
        }
        else
        {
            LowFuel.SetActive(false);
            DisplayScore(lose);
            UILose.SetActive(true);
            PressSpaceText.SetActive(true);
            PointText.text = lose + " Points";
            Space = true;
        }
            

        
    }

    void PressSpace()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Score = 0;
            fuel = 2000;
            StartCoroutine(LoadScene(1));
        }
    }


    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("LunarLander");
    }
}
