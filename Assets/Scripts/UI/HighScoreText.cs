using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreText : MonoBehaviour
{
    [SerializeField] SavedGame savedGame;

    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = savedGame.GetHighScore().ToString();
    }

}
