using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SavedGame", menuName = "SavedGame")]
public class SavedGame : ScriptableObject
{

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore");
    }

    public void SetHighScore(int newHighScore)
    {
        PlayerPrefs.SetInt("HighScore", newHighScore);
    }
}

