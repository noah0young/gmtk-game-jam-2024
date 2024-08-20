using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneManager : MonoBehaviour
{
    [SerializeField] private string firstSceneName;

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(firstSceneName);
    }
}
