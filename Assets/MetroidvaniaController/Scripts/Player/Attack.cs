using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	public Transform attackCheck;
	private Rigidbody2D m_Rigidbody2D;
	public Animator animator;
	public bool canAttack = true;
	public bool isTimeToCheck = false;
	private GameObject attackArea = default;

	public GameObject cam;
	private bool attacking = false;
	private float timeToAttack = 0.25f;
    private float timer = 0f;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	// Start is called before the first frame update
	void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.X) && canAttack)
		{
			Attacks();
			SoundManager.instance.PlaySFX("Attack");
			StartCoroutine(AttackCooldown());
			if(attacking)
        {
            timer += Time.deltaTime;

            if(timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }

        }
		}
	}

	IEnumerator AttackCooldown()
	{
		yield return new WaitForSeconds(0.25f);
		canAttack = true;
	}
	private void Attacks()
    {
 	        attacking = true;
        attackArea.SetActive(attacking);
    }
}
