using UnityEngine;

public class EndScreen : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<SceneController>().LoadScene(0);
        }
    }
}
