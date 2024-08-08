using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApiTesting : MonoBehaviour
{
    public ChuckNorrisJoke jokeReceived;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MakeRequestToWebsite());
    }

    IEnumerator MakeRequestToWebsite()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://api.chucknorris.io/jokes/random");
        yield return request.SendWebRequest();

        jokeReceived = JsonUtility.FromJson<ChuckNorrisJoke>(request.downloadHandler.text);
        Debug.Log(jokeReceived.value);
    }
}
