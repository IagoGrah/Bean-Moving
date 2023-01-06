using UnityEngine;

public class GameOver : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<SceneController>().Continue();
        }
    }
}
