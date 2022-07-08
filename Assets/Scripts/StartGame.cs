using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void GameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void EndScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void Exit()
    {
        Debug.Log("ExitGame");
        Application.Quit();
    }
}