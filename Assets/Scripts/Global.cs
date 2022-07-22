using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global Instance { get; private set; }
    public int hp;
    public int money;
    public UIHandle ui;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            hp = 3;
            money = 0;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCoinsOnUI()
    {
        ui.SetCoins(money);
    }

    public void SetHPOnUI()
    {
        ui.SetHP(hp);
    }
}
