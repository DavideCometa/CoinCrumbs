using UnityEngine;

public class GraphLoader : MonoBehaviour
{
    public WebClient webClient;
    UserData user;
    bool started = false;

    void Start()
    {
        user = UserData.GetInstance;
        started = true;
        webClient.RetrieveCoinHistory(user.lastCoinId, user.historyRange);
    }

    void OnEnable() {
        if(started)
            webClient.RetrieveCoinHistory(user.lastCoinId, user.historyRange);
    }
}
