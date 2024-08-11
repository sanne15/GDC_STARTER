using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Moneymanager : MonoBehaviour
{
    private int money;
    private DayManager dayManager;

    public TextMeshProUGUI moneyText;

    void Start()
    {
        dayManager = FindObjectOfType<DayManager>();
        
        if (dayManager == null)
        {
            Debug.LogError("DayManager not existing in this scene.");
            return;
        }

        money = 0;
        UpdateMoneyText();
    }

    public int GetMoney()
    {
        return money;
    }

    public void SetMoney(int amount)
    {
        money = amount;
        UpdateMoneyText();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        int currentDay = dayManager.currentDay;
        int fine = currentDay * (currentDay + 1) / 2 * 1000;
        if (moneyText != null)
        {
            moneyText.text = $"Money : {money}₩ / Fine : {fine}₩";
        }
    }
}
