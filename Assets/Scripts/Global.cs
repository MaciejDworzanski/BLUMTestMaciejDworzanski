using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void ReloadScene()
    {
        ui.playerDead = true;
        StartCoroutine(ReloadSceneAfterWhile());
    }

    private IEnumerator ReloadSceneAfterWhile()
    {

        yield return new WaitForSeconds(2);
        hp = 3;
        SceneManager.LoadScene(0);
        StopCoroutine(ReloadSceneAfterWhile());
    }
}
