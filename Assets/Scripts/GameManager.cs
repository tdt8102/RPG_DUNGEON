using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public List<Sprite> playerSprites;

	public List<Sprite> weaponSprites;

	public List<int> weaponPrices;

	public List<int> xpTable;

	public Player player;

	public Weapon weapon;

	public FloatingTextManager FloatingTextManager;

	public RectTransform hitpointBar;

	public Animator deathMenuAnim;

	public GameObject hud;

	public GameObject menu;

	public int pesos;

	public int experience;

	private void Awake()
	{
		if (instance != null)
		{
			Object.Destroy(base.gameObject);
			Object.Destroy(player.gameObject);
			Object.Destroy(FloatingTextManager.gameObject);
			Object.Destroy(hud);
			Object.Destroy(menu);
		}
		else
		{
			instance = this;
			SceneManager.sceneLoaded += LoadState;
			SceneManager.sceneLoaded += OnSceneLoaded;
		}
	}

	public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
	{
		FloatingTextManager.Show(msg, fontSize, color, position, motion, duration);
	}

	public bool TryUpgradeWeapon()
	{
		if (weaponPrices.Count <= weapon.weaponLevel)
		{
			return false;
		}
		if (pesos >= weaponPrices[weapon.weaponLevel])
		{
			pesos -= weaponPrices[weapon.weaponLevel];
			weapon.UpgradeWeapon();
			return true;
		}
		return false;
	}

	public void OnHitpointChange()
	{
		float y = (float)player.hitpoint / (float)player.maxHitpoint;
		hitpointBar.localScale = new Vector3(1f, y, 1f);
	}

	public int GetCurrentLevel()
	{
		int num = 0;
		int num2 = 0;
		while (experience >= num2)
		{
			num2 += xpTable[num];
			num++;
			if (num == xpTable.Count)
			{
				return num;
			}
		}
		return num;
	}

	public int GetXpToLevel(int level)
	{
		int i = 0;
		int num = 0;
		for (; i < level; i++)
		{
			num += xpTable[i];
		}
		return num;
	}

	public void GrantXp(int xp)
	{
		int currentLevel = GetCurrentLevel();
		experience += xp;
		if (currentLevel < GetCurrentLevel())
		{
			OnLevelUp();
		}
	}

	public void OnLevelUp()
	{
		Debug.Log("Level Up!");
		player.OnLevelUp();
		OnHitpointChange();
	}

	public void OnSceneLoaded(Scene s, LoadSceneMode mode)
	{
		player.transform.position = GameObject.Find("hoisinh").transform.position;
	}

	public void Respawn()
	{
		deathMenuAnim.SetTrigger("Hide");
		SceneManager.LoadScene("Main");
		player.Respawn();
	}

	public void SaveState()
	{
		string text = " ";
		text += "0 | ";
		text = text + pesos + " | ";
		text = text + experience + " | ";
		text += weapon.weaponLevel;
		PlayerPrefs.SetString("SaveState", text);
	}

	public void LoadState(Scene s, LoadSceneMode mode)
	{
		SceneManager.sceneLoaded -= LoadState;
		if (PlayerPrefs.HasKey("SaveState"))
		{
			string[] array = PlayerPrefs.GetString("SaveState").Split('|');
			pesos = int.Parse(array[1]);
			experience = int.Parse(array[2]);
			if (GetCurrentLevel() != 1)
			{
				player.SetLevel(GetCurrentLevel());
			}
			weapon.SetWeaponLevel(int.Parse(array[3]));
		}
	}
}
