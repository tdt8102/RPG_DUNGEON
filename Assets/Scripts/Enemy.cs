using UnityEngine;

public class Enemy : Mover
{
	public int xpValue = 1;

	public float triggerLenght = 1f;

	public float chaseLenght = 5f;

	private bool chasing;

	private bool collidingWithPlayer;

	private Transform playerTransform;

	private Vector3 startingPosition;

	public ContactFilter2D filter;

	private BoxCollider2D hitbox;

	private Collider2D[] hits = new Collider2D[10];

	protected override void Start()
	{
		base.Start();
		playerTransform = GameManager.instance.player.transform;
		startingPosition = base.transform.position;
		hitbox = base.transform.GetChild(0).GetComponent<BoxCollider2D>();
	}

	private void FixedUpdate()
	{
		if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLenght)
		{
			if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
			{
				chasing = true;
			}
			if (chasing)
			{
				if (!collidingWithPlayer)
				{
					UpdateMotor((playerTransform.position - base.transform.position).normalized);
				}
			}
			else
			{
				UpdateMotor(startingPosition - base.transform.position);
			}
		}
		else
		{
			UpdateMotor(startingPosition - base.transform.position);
			chasing = false;
		}
		collidingWithPlayer = false;
		boxCollider.OverlapCollider(filter, hits);
		for (int i = 0; i < hits.Length; i++)
		{
			if (!(hits[i] == null))
			{
				if (hits[i].tag == "Fighter" && hits[i].name == "Player")
				{
					collidingWithPlayer = true;
				}
				hits[i] = null;
			}
		}
	}

	protected override void Death()
	{
		Object.Destroy(base.gameObject);
		GameManager.instance.GrantXp(xpValue);
		GameManager.instance.ShowText("+" + xpValue + "xp", 30, Color.magenta, base.transform.position, Vector3.up * 40f, 1f);
	}
}
