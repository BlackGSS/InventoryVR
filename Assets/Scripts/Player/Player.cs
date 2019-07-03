using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private const int MAX_HEALTH = 10;

	public GameObject teleport;

	[SerializeField]
	private GameObject _laserUI;

	[SerializeField]
	private Transform _canvasInventory;

	[SerializeField]
	private HealthUI _uiHealth;

	[SerializeField]
	private GameObject[] _weaponRight, _weaponLeft;

	private int _currentWeaponLeft = -1, _currentWeaponRight = -1;
	public int leftHand { get { return _currentWeaponLeft; } }
	public int rightHand { get { return _currentWeaponRight; } }
	private int _playerHealth;
	private Vector3 _canvasInventoryDefaultScale;

	private void Start()
	{
		Invoke("AddItems", 1);
		_canvasInventoryDefaultScale = _canvasInventory.localScale;
		SetHealth(6);
		ResetWeapons();
	}

	/// <summary>
	/// Reinicia el estado de todas las armas. 
	/// </summary>
	public void ResetWeapons()
	{
		for (int i = 0; i < _weaponRight.Length; i++)
		{
			_weaponRight[i].SetActive(false);
		}

		for (int i = 0; i < _weaponLeft.Length; i++)
		{
			_weaponLeft[i].SetActive(false);
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
			_weaponLeft[_currentWeaponLeft].SetActive(!_weaponLeft[_currentWeaponLeft].activeSelf);
		}

		if (_currentWeaponLeft != index)
		{
			_currentWeaponLeft = index;
			_weaponLeft[_currentWeaponLeft].SetActive(!_weaponLeft[_currentWeaponLeft].activeSelf);
		}

		if (_weaponLeft[_currentWeaponLeft].activeSelf == false)
		{
			_currentWeaponLeft = -1;
		}
	}

	public void SetWeaponRight(int index)
	{
		if (_currentWeaponRight > -1)
		{
			_weaponRight[_currentWeaponRight].SetActive(!_weaponRight[_currentWeaponRight].activeSelf);
		}

		if (_currentWeaponRight != index)
		{
			_currentWeaponRight = index;
			_weaponRight[_currentWeaponRight].SetActive(!_weaponRight[_currentWeaponRight].activeSelf);
		}

		if (_weaponRight[_currentWeaponRight].activeSelf == false)
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

		teleport.SetActive(GameManager.input.touchpadLeft);

		_laserUI.SetActive(GameManager.input.touchpadRight);

		if (GameManager.input.touchpadRight)
		{
			_canvasInventory.localScale = _canvasInventoryDefaultScale;
		}
		else
		{
			_canvasInventory.localScale = Vector3.zero;
		}
	}

	public void SetHealth(int health)
	{
		_playerHealth = health;
		_uiHealth.SetValue((float)_playerHealth / MAX_HEALTH);
	}

	public void Heal(int health)
	{
		if (_playerHealth < MAX_HEALTH)
		{
			_playerHealth += health;
		}

		_uiHealth.SetValue((float)_playerHealth / MAX_HEALTH);
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
