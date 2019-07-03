using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private const int MAX_HEALTH = 10;

	public GameObject Teleport, laserUI;
	public Transform canvasInventory;
	public HealthUI uiHealth;
	public GameObject[] WeaponRight, WeaponLeft;

	private int _currentWeaponLeft = -1, _currentWeaponRight = -1;
	public int leftHand { get { return _currentWeaponLeft; } }
	public int rightHand { get { return _currentWeaponRight; } }
	private int _playerHealth;
	private Vector3 _canvasInventoryDefaultScale;

	private void Start()
	{
		Invoke("AddItems", 1);
		_canvasInventoryDefaultScale = canvasInventory.localScale;
		SetHealth(6);
		ResetWeapons();
	}

	/// <summary>
	/// Reinicia el estado de todas las armas. 
	/// </summary>
	public void ResetWeapons()
	{
		for (int i = 0; i < WeaponRight.Length; i++)
		{
			WeaponRight[i].SetActive(false);
		}

		for (int i = 0; i < WeaponLeft.Length; i++)
		{
			WeaponLeft[i].SetActive(false);
		}
	}

	/// <summary>
	/// Establece el arma de la mano izquierda.
	/// </summary>
	/// <param name="index"> -1: no tiene arma / 0: si tiene arma</param>
	public void SetWeaponLeft(int index)
	{
		if (_currentWeaponLeft > -1)
		{
			WeaponLeft[_currentWeaponLeft].SetActive(!WeaponLeft[_currentWeaponLeft].activeSelf);
		}

		if (_currentWeaponLeft != index)
		{
			_currentWeaponLeft = index;
			WeaponLeft[_currentWeaponLeft].SetActive(!WeaponLeft[_currentWeaponLeft].activeSelf);
		}

		if (WeaponLeft[_currentWeaponLeft].activeSelf == false)
		{
			_currentWeaponLeft = -1;
		}
	}

	public void SetWeaponRight(int index)
	{
		if (_currentWeaponRight > -1)
		{
			WeaponRight[_currentWeaponRight].SetActive(!WeaponRight[_currentWeaponRight].activeSelf);
		}

		if (_currentWeaponRight != index)
		{
			_currentWeaponRight = index;
			WeaponRight[_currentWeaponRight].SetActive(!WeaponRight[_currentWeaponRight].activeSelf);
		}

		if (WeaponRight[_currentWeaponRight].activeSelf == false)
		{
			_currentWeaponRight = -1;
		}

	}

	private void AddItems()
	{
		GameManager.inventory.AddItem(1);
		GameManager.inventory.AddItem(1);
		GameManager.inventory.AddItem(0);
		GameManager.inventory.AddItem(0);

	}

	// Update is called once per frame
	void Update()
	{

		Teleport.SetActive(GameManager.input.touchpadLeft);

		laserUI.SetActive(GameManager.input.touchpadRight);

		if (GameManager.input.touchpadRight)
		{
			canvasInventory.localScale = _canvasInventoryDefaultScale;
		}
		else
		{
			canvasInventory.localScale = Vector3.zero;
		}
	}

	public void SetHealth(int health)
	{
		_playerHealth = health;
		uiHealth.SetValue((float)_playerHealth / MAX_HEALTH);
	}

	public void Heal(int health)
	{
		if (_playerHealth < MAX_HEALTH)
		{
			_playerHealth += health;
		}

		uiHealth.SetValue((float)_playerHealth / MAX_HEALTH);
	}

	private void OnTriggerEnter(Collider Col)
	{
		if (Col.tag == "Gun" && Input.GetKeyDown(KeyCode.E))
		{
			Destroy(Col.gameObject);
			GameManager.soundManager.CreateSound(2);
			GameManager.inventory.AddItem(2);
		}

		if (Col.tag == "HealPotion" && Input.GetKeyDown(KeyCode.E))
		{
			Destroy(Col.gameObject);
			GameManager.soundManager.CreateSound(2);
			GameManager.inventory.AddItem(1);
		}
	}

	private void OnTriggerStay(Collider Col)
	{
		if (Col.tag == "Gun" && Input.GetKeyDown(KeyCode.E))
		{
			Destroy(Col.gameObject);
			GameManager.soundManager.CreateSound(2);
			GameManager.inventory.AddItem(0);
		}

		if (Col.tag == "HealPotion" && Input.GetKeyDown(KeyCode.E))
		{
			Destroy(Col.gameObject);
			GameManager.soundManager.CreateSound(2);
			GameManager.inventory.AddItem(1);
		}
	}
}
