using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserInformationDisplay : MonoBehaviour
{
    public TextMeshProUGUI usernameText;

    public TextMeshProUGUI scoreText;
    
    public void UpdateUsername(string username)
    {
        usernameText.text = username;
    }

    public void UpdateScoreDisplay(int score)
    {
        scoreText.text = score.ToString();
    }
}
