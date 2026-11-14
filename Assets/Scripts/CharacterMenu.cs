using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
	public Text levelText;

	public Text hitpointText;

	public Text pesosText;

	public Text upgradeCostText;

	public Text xpText;

	private int currentCharacterSelection;

	public Image characterSelectionSprite;

	public Image weaponSprite;

	public RectTransform xpBar;

	public void OnArrowClick(bool right)
	{
		if (right)
		{
			currentCharacterSelection++;
			if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
			{
				currentCharacterSelection = 0;
			}
			OnSelectionChanged();
		}
		else
		{
			currentCharacterSelection++;
			if (currentCharacterSelection < 0)
			{
				currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
			}
			OnSelectionChanged();
		}
	}

	private void OnSelectionChanged()
	{
		characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
		GameManager.instance.player.SwapSprite(currentCharacterSelection);
	}

	public void OnUpgradeClick()
	{
		if (GameManager.instance.TryUpgradeWeapon())
		{
			UpdateMenu();
		}
	}

	public void UpdateMenu()
	{
		weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
		if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
		{
			upgradeCostText.text = "Max";
		}
		else
		{
			upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
		}
		levelText.text = GameManager.instance.GetCurrentLevel().ToString();
		hitpointText.text = GameManager.instance.player.hitpoint.ToString();
		pesosText.text = GameManager.instance.pesos.ToString();
		int currentLevel = GameManager.instance.GetCurrentLevel();
		if (currentLevel == GameManager.instance.xpTable.Count)
		{
			xpText.text = GameManager.instance.experience + "tổng điểm kinh nghiệm";
			xpBar.localScale = Vector3.one;
			return;
		}
		int xpToLevel = GameManager.instance.GetXpToLevel(currentLevel - 1);
		int num = GameManager.instance.GetXpToLevel(currentLevel) - xpToLevel;
		int num2 = GameManager.instance.experience - xpToLevel;
		float x = (float)num2 / (float)num;
		xpBar.localScale = new Vector3(x, 1f, 1f);
		xpText.text = num2 + " / " + num;
	}
}
