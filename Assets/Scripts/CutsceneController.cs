using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    public Image[] images;
    int currImage;

    public Image black;
    public Animator anim;


    void Start()
    {
        currImage = 0;
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        yield return new WaitForSeconds(3.5f);
        while (currImage < images.Length - 1)
        {
            anim.SetBool("FadeOut", true);
            yield return new WaitUntil(() => black.color.a == 1);
            anim.SetBool("FadeOut", false);
            images[currImage].gameObject.SetActive(false);
            currImage++;
            images[currImage].gameObject.SetActive(true);
            anim.SetBool("FadeIn", true);
            yield return new WaitUntil(() => black.color.a == 0);
            anim.SetBool("FadeIn", false);
            yield return new WaitForSeconds(3.5f);
        }
        anim.SetBool("FadeOut", true);
        yield return new WaitUntil(() => black.color.a == 1);
        anim.SetBool("FadeOut", false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
