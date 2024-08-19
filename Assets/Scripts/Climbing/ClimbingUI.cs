using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClimbingUI : MonoBehaviour
{
    private static ClimbingUI instance;
    private static int score;
    [Header("ClimbingUI")]
    [SerializeField] private GameObject climbingUICanvas;
    [SerializeField] private Slider batterySlider;
    [SerializeField] private TMP_Text scoreValText;

    [Header("On Machine End")]
    [SerializeField] private GameObject machineStoppedCanvas;
    [SerializeField] private GameObject moneyTotalBoxObj;
    [SerializeField] private TMP_Text moneyTotalValText;
    [SerializeField] private GameObject continueButtonObj;
    [SerializeField] private Transform moneyHolder;
    [SerializeField] private GameObject moneyPrefab;
    [SerializeField] private GameObject finalScoreHolder;
    [SerializeField] private TMP_Text finalScoreText;
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
        instance.climbingUICanvas.SetActive(true);
        onContinueClick = false;
        machineStoppedCanvas.SetActive(false);
        moneyTotalBoxObj.SetActive(false);
        continueButtonObj.SetActive(false);
        instance.finalScoreHolder.SetActive(false);
    }

    public static void SetScoreVal(int val)
    {
        score = val;
        instance.scoreValText.text = val + "";
    }

    public static void SetMoneyTotalVal(int val)
    {
        instance.moneyTotalValText.text = "$" + val + "";
    }

    public static void SetBatteryVal(float val)
    {
        SetBatteryVal(val, 1);
    }

    public static void SetBatteryVal(float val, float max)
    {
        instance.batterySlider.value = val;
        instance.batterySlider.maxValue = max;
    }

    public static IEnumerator ShowMachineStopped(int moneyEarned)
    {
        instance.climbingUICanvas.SetActive(false);
        instance.machineStoppedCanvas.SetActive(true);
        instance.finalScoreHolder.SetActive(true);
        instance.finalScoreText.text = score + " FT";
        yield return new WaitForSeconds(0.1f);
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
