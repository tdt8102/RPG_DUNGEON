using UnityEngine;

public class Player : Mover
{
	private SpriteRenderer spriteRenderer;

	private bool isAlive = true;

	protected override void Start()
	{
		base.Start();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	protected override void ReceiveDamage(Damage dmg)
	{
		if (isAlive)
		{
			base.ReceiveDamage(dmg);
			GameManager.instance.OnHitpointChange();
		}
	}

	protected override void Death()
	{
		isAlive = false;
		GameManager.instance.deathMenuAnim.SetTrigger("Show");
	}

	private void FixedUpdate()
	{
		float axisRaw = Input.GetAxisRaw("Horizontal");
		float axisRaw2 = Input.GetAxisRaw("Vertical");
		if (isAlive)
		{
			UpdateMotor(new Vector3(axisRaw, axisRaw2, 0f));
		}
	}

	public void SwapSprite(int skinId)
	{
		GetComponent<SpriteRenderer>().sprite = GameManager.instance.playerSprites[skinId];
	}

	public void OnLevelUp()
	{
		maxHitpoint++;
		hitpoint = maxHitpoint;
	}

	public void SetLevel(int level)
	{
		for (int i = 0; i < level; i++)
		{
			OnLevelUp();
		}
	}

	public void Heal(int healingAmount)
	{
		if (hitpoint != maxHitpoint)
		{
			hitpoint += healingAmount;
			if (hitpoint > maxHitpoint)
			{
				hitpoint = maxHitpoint;
			}
			GameManager.instance.ShowText("+" + healingAmount + "hp", 25, Color.green, base.transform.position, Vector3.up * 30f, 1f);
			GameManager.instance.OnHitpointChange();
		}
	}

	public void Respawn()
	{
		Heal(maxHitpoint);
		isAlive = true;
		lastImmune = Time.time;
	}
}
