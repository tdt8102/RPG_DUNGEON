using UnityEngine;

public class Crate : FIghter
{
	protected override void Death()
	{
		Object.Destroy(base.gameObject);
	}
}
