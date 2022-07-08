using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public Text scoreDisplay;
    public GameObject player;
    public Text timeDisplay;
    static int timer = 30; //length of game
    float elapsed = 0f;
    public bool paused = false;
    private GameObject pauseScreen;

    void Start()
    {
        StartCoroutine(GameOver());
        player = GameObject.Find("Player");
        pauseScreen = GameObject.Find("PauseScreen");
        pauseScreen.SetActive(false);
    }

    public void Update()
    {
        //on screen count down timer
        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
            timeDisplay.text = Mathf.Round(timer - elapsed).ToString();

        //pause toggle system
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paused == false)
            {
                PauseGame();
                paused = true;
            }
            else if (paused == true)
            {
                ResumeGame();
                paused = false;
            }
        }

        //stopping the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("EndScene");
        }

        //score display
        score = player.transform.childCount;
        scoreDisplay.text = score.ToString();

        PlayerPrefs.SetInt("Score", score);
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        //I found that for some reason I could still move the player after pause
        //so I had to set a playerpref to store a bool to pause everything for the session
        PlayerPrefs.SetInt("Paused", 1);
        GameObject.Find("Pause").transform.GetComponent<Text>().text = "Press P to unpause";
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        PlayerPrefs.SetInt("Paused", 0);
        GameObject.Find("Pause").transform.GetComponent<Text>().text = "Press P to pause";
        Time.timeScale = 1;
    }

    //time is up
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(timer);

        SceneManager.LoadScene("EndScene");
    }
}