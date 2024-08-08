using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeatherInfo
{
    public string name;
    public string timezone;
    public Main main;
    public Weather weather;
    public Wind wind;
}

[System.Serializable]
public class Main
{
    public string temp;
    public string feels_like;
    public string humidity;
}

[System.Serializable]
public class Weather
{
    public string main;
    public string description;
}

[System.Serializable]
public class Wind
{
    public string speed;
    public string deg;
}
