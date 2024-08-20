using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class TutorialManager : MonoBehaviour
{
    private static bool hasBeenSeen = false;
    private bool cont;
    private GameInput input;
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private GameObject[] tutorialParts;

    // Start is called before the first frame update
    void Start()
    {
        cont = false;
        if (!hasBeenSeen)
        {
            input = new GameInput();
            input.Player.RocketFly.Enable();
            input.Player.RocketFly.performed += (e) => { Continue(); };
            StartCoroutine(TutorialRoutine());
        }
        else
        {
            tutorialCanvas.SetActive(false);
        }
    }

    private IEnumerator TutorialRoutine()
    {
        tutorialCanvas.SetActive(true);
        for (int i = 0; i < tutorialParts.Length; i++)
        {
            tutorialParts[i].SetActive(true);
            yield return new WaitUntil(() => cont);
            cont = false;
            tutorialParts[i].SetActive(false);
        }
        tutorialCanvas.SetActive(false);
        hasBeenSeen = true;
    }

    private void Continue()
    {
        cont = true;
    }
}
