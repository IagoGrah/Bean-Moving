using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinBean : MonoBehaviour
{
    bool winning;

    void Update()
    {
        if (winning)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, -4), 0.035f);
        }
    }

    public void Win()
    {
        StartCoroutine(WinCoroutine());
    }

    IEnumerator WinCoroutine()
    {
        winning = true;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
