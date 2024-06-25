using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    private int gold = 0;
    public GameObject shopPanel;

    void Start()
    {
        if (goldText == null)
        {
            Debug.LogError("goldText is not assigned in the inspector!");
        }
        LoadGold();
        UpdateGoldText();
    }

    void LoadGold()
    {
        if (PlayerPrefs.HasKey("PlayerGold"))
        {
            gold = PlayerPrefs.GetInt("PlayerGold");
        }
        UpdateGoldText();
    }

    void UpdateGoldText()
    {
        if (goldText != null)
        {
            goldText.text = "Gold: " + gold;
        }
        else
        {
            Debug.LogError("goldText is null in UpdateGoldText");
        }
    }

    public void SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            SaveGold();
            UpdateGoldText();
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }

    void SaveGold()
    {
        PlayerPrefs.SetInt("PlayerGold", gold);
        PlayerPrefs.Save();
        Debug.Log("Gold Saved in Main Menu: " + gold);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Demo");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenShopPanel()
    {
        shopPanel.SetActive(true);
    }

    public void CloseShopPanel()
    {
        shopPanel.SetActive(false);
    }

    public void BuyPowerUp(int cost)
    {
        if (gold >= cost)
        {
            gold -= cost;
            SaveGold();
            UpdateGoldText();
            Debug.Log("Power-up purchased!");
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }
}
