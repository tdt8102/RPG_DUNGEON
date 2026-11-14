using UnityEngine;

public class Weapon : Collidable
{
	public int[] damagePoint = new int[7] { 1, 2, 3, 4, 5, 6, 7 };

	public float[] pushForce = new float[7] { 2f, 2.2f, 2.5f, 3f, 3.2f, 3.5f, 4f };

	public int weaponLevel;

	public SpriteRenderer spriteRenderer;

	private Animator anim;

	private float cooldown = 0.5f;

	private float lastSwing;

	protected override void Start()
	{
		base.Start();
		anim = GetComponent<Animator>();
	}

	protected override void Update()
	{
		base.Update();
		if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastSwing > cooldown)
		{
			lastSwing = Time.time;
			Swing();
		}
	}

	protected override void OnCollide(Collider2D coll)
	{
		if (coll.tag == "Fighter" && !(coll.name == "Player"))
		{
			Damage damage = new Damage
			{
				damageAmount = damagePoint[weaponLevel],
				origin = base.transform.position,
				pushForce = pushForce[weaponLevel]
			};
			coll.SendMessage("ReceiveDamage", damage);
		}
	}

	private void Swing()
	{
		anim.SetTrigger("Swing");
	}

	public void UpgradeWeapon()
	{
		weaponLevel++;
		spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
	}

	public void SetWeaponLevel(int level)
	{
		weaponLevel = level;
		spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
	}
}
