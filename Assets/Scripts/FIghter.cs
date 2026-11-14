using UnityEngine;

public class FIghter : MonoBehaviour
{
	public int hitpoint = 10;

	public int maxHitpoint = 10;

	public float pushRecoverySpeed = 0.2f;

	protected float immuneTime = 1f;

	protected float lastImmune;

	protected Vector3 pushDirection;

	protected virtual void ReceiveDamage(Damage dmg)
	{
		if (Time.time - lastImmune > immuneTime)
		{
			lastImmune = Time.time;
			hitpoint -= dmg.damageAmount;
			pushDirection = (base.transform.position - dmg.origin).normalized * dmg.pushForce;
			GameManager.instance.ShowText(dmg.damageAmount.ToString(), 15, Color.red, base.transform.position, Vector3.zero, 0.5f);
			if (hitpoint <= 0)
			{
				hitpoint = 0;
				Death();
			}
		}
	}

	protected virtual void Death()
	{
	}
}
