using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsUI;
    int cointAmount;

    private void Start()
    {
        UpdateCoinUI();
    }

    public void AddCoin()
    {
        cointAmount++;
        UpdateCoinUI();
    }

    private void UpdateCoinUI()
    {
        coinsUI.text = "x" + cointAmount.ToString();
    }
}
