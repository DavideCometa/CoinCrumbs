using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Unity.Services.Core.Environments;
using SimpleJSON;
using MiniJSON;
using System.Collections.Generic;
using UnityEngine.UI;
using AwesomeCharts;

/**
    Client that retrieves latest news from my Crypto-related news web scraper
*/
public class NewsDataClient : MonoBehaviour {

    public MainPageManager mainPageManager;
    private UserData user;
    private long latestUpdate = 0;

    private const string NEWSDATACRAWLER_URL = System.Environment.GetEnvironmentVariable("NEWSDATACRAWLER_URL");

    private const long UPDATE_OFFSET = 60;

    void Start() {
        user = UserData.GetInstance;
    }

    public void GetLatestNews() {
        StartCoroutine(RetrieveLatestNews(NEWSDATACRAWLER_URL));
        Debug.Log("Retrieving latest news...");
    }

    void OnEnable() {
        var currTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        if (latestUpdate < currTime - UPDATE_OFFSET) {
            latestUpdate = currTime;
            GetLatestNews();
        }
    }

    IEnumerator RetrieveLatestNews(string uri) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError) {
                Debug.Log("NEWS LOADER: Error: " + webRequest.error);
            } else {
                Debug.Log("NEWS LOADER:\nReceived: " + webRequest.downloadHandler.text);
                var N = JSON.Parse(webRequest.downloadHandler.text);
                var news = JsonHelper.GetArray<DataStructures.News>(webRequest.downloadHandler.text);
                mainPageManager.UpdateNewsData(news);
            }
        }
    }


}

