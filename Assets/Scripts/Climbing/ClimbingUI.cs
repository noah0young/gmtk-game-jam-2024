using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClimbingUI : MonoBehaviour
{
    private static ClimbingUI instance;
    [SerializeField] private TMP_Text scoreValText;

    [Header("On Machine End")]
    [SerializeField] private GameObject machineStoppedCanvas;
    [SerializeField] private GameObject moneyTotalBoxObj;
    [SerializeField] private TMP_Text moneyTotalValText;
    [SerializeField] private GameObject continueButtonObj;
    [SerializeField] private Transform moneyHolder;
    [SerializeField] private GameObject moneyPrefab;
    private bool onContinueClick;

    private void Start()
    {
        if (instance != null)
        {
            throw new System.Exception("Only one UI can exist");
        }
        else
        {
            instance = this;
        }
        onContinueClick = false;
        machineStoppedCanvas.SetActive(false);
        moneyTotalBoxObj.SetActive(false);
        continueButtonObj.SetActive(false);
    }

    public static void SetScoreVal(int val)
    {
        instance.scoreValText.text = val + "";
    }

    public static void SetMoneyTotalVal(int val)
    {
        instance.moneyTotalValText.text = "$" + val + "";
    }

    public static IEnumerator ShowMachineStopped(int moneyEarned)
    {
        instance.machineStoppedCanvas.SetActive(true);
        yield return instance.ShowMoneyEarned(moneyEarned);
        yield return new WaitForSeconds(0.1f);
        instance.moneyTotalBoxObj.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        instance.continueButtonObj.SetActive(true);
        yield return new WaitUntil(() => instance.onContinueClick);
    }

    private IEnumerator ShowMoneyEarned(int moneyEarned)
    {
        for (int i = 0; i < moneyEarned; i++)
        {
            GameObject moneyIcon = Instantiate(moneyPrefab);
            moneyIcon.transform.SetParent(moneyHolder);
            yield return new WaitForSeconds(.5f); // Time between money show
        }
    }

    public void ContinueClick()
    {
        onContinueClick = true;
    }
}
