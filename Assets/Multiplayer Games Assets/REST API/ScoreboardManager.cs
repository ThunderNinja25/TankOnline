using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;

public class ScoreboardManager : MonoBehaviour
{
    public UserInformationDisplay scoreEntry;
    public UserCollections AllPlayers;
    
    public void DisplayScoreboard()
    {
        StartCoroutine(InitializeScoreboard());
    }

    IEnumerator InitializeScoreboard()
    {
        UnityWebRequest request = UnityWebRequest.Get(LoginRegisterScreen.api_url + ".json");
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
            AllPlayers = JsonConvert.DeserializeObject<UserCollections>(request.downloadHandler.text);
            foreach(UserInformation info in AllPlayers.AllPlayers) 
            {
                UserInformationDisplay display = Instantiate(scoreEntry);
                display.UpdateScoreDisplay(info.highestScore);
                display.UpdateUsername(info.username);
            }
        }
    }
}

[System.Serializable]
public class UserCollections
{
    public List<UserInformation> AllPlayers;

    public UserCollections()
    {
        AllPlayers = new List<UserInformation>();
    }
}
