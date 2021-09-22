using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorController : MonoBehaviour
{
    public static CursorController instance;
    [SerializeField] Texture2D basicCursor, crosshair;
    private void Awake()
    {
        if (instance == null) instance = this;
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            ActivateBasicCursor();
        }
        if (SceneManager.GetActiveScene().name == "Game")
        {
            ActivateCrosshairCursor();
        }
        if (SceneManager.GetActiveScene().name == "Training")
        {
            ActivateCrosshairCursor();
        }
    }

    public void ActivateBasicCursor()
    {
        Cursor.SetCursor(basicCursor, Vector2.zero, CursorMode.Auto);
    }
    public void ActivateCrosshairCursor()
    {
        Cursor.SetCursor(crosshair, new Vector2(crosshair.width / 2, crosshair.height / 2), CursorMode.Auto);
    }

}
