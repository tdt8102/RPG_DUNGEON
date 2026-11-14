using UnityEngine;

public class Chest : Collectable
{
	public Sprite emptyChest;

	public int pesosAmount = 10;

	protected override void OnCollect()
	{
		if (!collected)
		{
			collected = true;
			GetComponent<SpriteRenderer>().sprite = emptyChest;
			GameManager.instance.pesos += pesosAmount;
			GameManager.instance.ShowText("+" + pesosAmount + " pesos!", 25, Color.yellow, base.transform.position, Vector3.up * 25f, 0.5f);
		}
	}
}
