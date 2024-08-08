using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherAPI : MonoBehaviour
{
    public string cityName;
    private const string API_URL = "https://api.openweathermap.org/data/2.5/weather?units=metric&";
    private const string APP_ID = "appid=ad6c93214c5f16972f6a02d7da2648dd";

    public WeatherInfo retrievedInfo;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FetchInfoByCityName());
    }

    IEnumerator FetchInfoByCityName()
    {
        string apiCall = $"{API_URL}q={cityName}&{APP_ID}";
        //string apiCall = API_URL + "q=" + cityName + "&" + APP_ID;
        UnityWebRequest request = UnityWebRequest.Get(apiCall);

        yield return request.SendWebRequest();
        retrievedInfo = JsonUtility.FromJson<WeatherInfo>(request.downloadHandler.text);
    }
}
