using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //through this field we can assign the cursor sprite in Unity
    [SerializeField] private Texture2D cursorTexture = null;

    private void Start()
    {
        SetCursorIcon();
    }

    private void SetCursorIcon()
    {
        Cursor.SetCursor(cursorTexture,new Vector2((cursorTexture.width/2f), (cursorTexture.height/2f)), CursorMode.Auto);
    }

    public void RestartLevel()
    {
        //DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
