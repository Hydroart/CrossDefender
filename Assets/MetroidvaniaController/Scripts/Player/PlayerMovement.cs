using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool dash = false;
	private static int killCount = 0;
	private static int gold = 0;
	[SerializeField] TextMeshProUGUI goldText;
	[SerializeField] TextMeshProUGUI killCountText;

    void Start()
    {
		LoadGold();
		Debug.Log("Loaded Gold: " + gold); // Debug log
		goldText.text = "Gold: " + gold;
		killCount = 0;
		killCountText.text = "Enemies defeated: " + killCount;
    }

	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetKeyDown(KeyCode.Z))
		{
			jump = true;
			SoundManager.instance.PlaySFX("Jump");
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			dash = true;
			SoundManager.instance.PlaySFX("Dash");
		}
	}

	public void OnFall()
	{
		animator.SetBool("IsJumping", true);
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
		SoundManager.instance.PlaySFX("Land");
	}

	void FixedUpdate ()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);
		jump = false;
		dash = false;
	}

	public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("Gold Added: " + amount + ", Total Gold: " + gold); // Debug log
		goldText.text = "Gold: " + gold;
		SaveGold();
    }

	void UpdateKillCountText()
    {
        killCountText.text = "Enemies defeated: " + killCount;
    }

	public void AddKill()
    {
        killCount++;
        UpdateKillCountText();
        Debug.Log("Enemies defeated: " + killCount);
    }

    void SaveGold()
    {
        PlayerPrefs.SetInt("PlayerGold", gold);
        PlayerPrefs.Save();
		Debug.Log("Gold Saved: " + gold); // Debug log
    }

    void LoadGold()
    {
        if (PlayerPrefs.HasKey("PlayerGold"))
        {
            gold = PlayerPrefs.GetInt("PlayerGold");
        }
    }

    void OnApplicationQuit()
    {
        SaveGold();
    }
}
