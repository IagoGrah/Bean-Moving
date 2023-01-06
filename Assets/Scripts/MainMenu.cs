using UnityEngine;

public class MainMenu : MonoBehaviour
{
    SceneController sceneController;

    void Start()
    {
        sceneController = GameObject.FindObjectOfType<SceneController>();
    }

    public void StartGame()
    {
        sceneController.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }
}
