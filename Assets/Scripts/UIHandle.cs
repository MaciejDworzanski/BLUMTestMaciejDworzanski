using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandle : MonoBehaviour
{
    public TextMeshProUGUI coinsNumber;
    public Image[] hearths;
    public Image endImage;
    private float color;
    public bool playerDead;
    void Start()
    {
        Global.Instance.ui = this;
        SetCoins(Global.Instance.money);
        SetHP(Global.Instance.hp);
        color = 0;
    }

    private void Update()
    {
        if(playerDead) SetEndImage();
    }
    public void SetCoins(int number)
    {
        coinsNumber.text = number.ToString();
    }

    public void SetHP(int hp)
    {
        for (int i = 0; i < hearths.Length; i++)
        {
            hearths[i].gameObject.SetActive(i < hp);
        }
    }

    public void SetEndImage()
    {
        color += Time.deltaTime/2f;
        endImage.color = new(0, 0, 0, color);
    }
}
