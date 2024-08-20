using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void MoveToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }

    public void MoveToBuild()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BuildScene");
    }
}
