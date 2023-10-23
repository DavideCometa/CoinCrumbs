using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenManager : MonoBehaviour
{
    public GameObject logoPrefab;
    public Transform background;

    private const int NUM_LOGOS = 15;

    public List<Texture> logosList = new List<Texture>(); //Pre-loaded via inspector

    void Start()
    {
        for (int i = 0; i < NUM_LOGOS; i++) {
            float spawnY = Random.Range(0, Screen.height);
            float spawnX = Random.Range(0, Screen.width);

            float randomScale = Random.Range(0.5f, 2.0f);

            int randomLogo = Random.Range(0, logosList.Count - 1);

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            GameObject go = Instantiate(logoPrefab, spawnPosition, Quaternion.identity, background);
            go.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            go.GetComponent<RawImage>().texture = logosList[randomLogo];
            logosList.RemoveAt(randomLogo);
        }
    }
}
