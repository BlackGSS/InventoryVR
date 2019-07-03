using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameManager
{
	public static GameObject inventorySlot;
	public static GameObject inventoryItem;

	public static DataBaseItems dataBaseItems;

	public static InputManager input;

	public static GameManagerMonoBehaviour manager;
	public static Inventory inventory;

	public static Player player;

	public static SoundManager soundManager;
}

public class GameManagerMonoBehaviour : MonoBehaviour
{

	[Header ("Prefabs Inventario")]
	public GameObject inventorySlot;
	public GameObject inventoryItem;

	[Header ("Referencia de escena del panel de slots")]
	public GameObject slotsPanel;

	[Header("Teclas para emular los inputs de VR")]
	public KeyCode touchpadLeftKey;
	public KeyCode touchpadRightKey;


	// Use this for initialization
	void Start()
	{
		GameManager.player = GameObject.FindObjectOfType<Player>();

		GameManager.inventorySlot = inventorySlot;
		GameManager.inventoryItem = inventoryItem;

		GameObject go = new GameObject("Data Base Items");
		go.transform.parent = transform;
		
		GameManager.dataBaseItems = go.AddComponent<DataBaseItems>();
		GameManager.dataBaseItems.LoadData();

		go = new GameObject("Input System");
		go.transform.parent = transform;

		GameManager.input = go.AddComponent<InputManager>();
		GameManager.manager = this;

		GameManager.input.touchpadLeftKey = touchpadLeftKey;
		GameManager.input.touchpadRightKey = touchpadRightKey;

		go = new GameObject("Inventory");
		go.transform.parent = transform;

		GameManager.inventory = go.AddComponent<Inventory>();

		GameManager.inventory.slotsPanel = slotsPanel;

		GameManager.soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
		GameManager.soundManager.CreateSoundtrack(4);

	}

	public void ShowLog(string text)
	{
		Debug.Log(text);
	}

}
