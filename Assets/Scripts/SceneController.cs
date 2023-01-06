using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Image black;
    public Animator anim;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(int index)
    {
        StartCoroutine(Fading(index));
    }

    IEnumerator Fading(int index)
    {
        anim.SetBool("FadeOut", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(index);
    }

    int currLevel;
    public void GameOver()
    {
        currLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("GameOver");
    }

    public void Continue()
    {
        SceneManager.LoadScene(currLevel);
    }
}
