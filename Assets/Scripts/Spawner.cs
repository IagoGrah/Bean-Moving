using System.Collections;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;
    public float[] spawnIntervals;

    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            yield return new WaitForSeconds(spawnIntervals[i]);
            enemies[i].SetActive(true);
        }
        yield return new WaitUntil(() => enemies.All(x => x == null));
        FindObjectOfType<WinBean>().Win();
    }
}
