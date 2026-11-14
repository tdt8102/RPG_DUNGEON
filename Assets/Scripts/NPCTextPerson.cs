using UnityEngine;

public class NPCTextPerson : Collidable
{
	public string message;

	private float cooldown = 4f;

	private float lastShout;

	protected override void Start()
	{
		base.Start();
		lastShout = 0f - cooldown;
	}

	protected override void OnCollide(Collider2D coll)
	{
		if (Time.time - lastShout > cooldown)
		{
			lastShout = Time.time;
			GameManager.instance.ShowText(message, 20, Color.white, base.transform.position + new Vector3(0f, 0.16f, 0f), Vector3.zero, cooldown);
		}
	}
}
