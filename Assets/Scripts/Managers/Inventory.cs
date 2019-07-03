using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
	public GameObject slotsPanel;

	private int _cantidadDeSlots = 24;

	private List<GameObject> slots = new List<GameObject>();
	private List<ItemData> items = new List<ItemData>();

	// Use this for initialization
	void Start()
	{
		for (int i = 0; i < _cantidadDeSlots; i++)
		{
			items.Add(new ItemData());
			slots.Add(Instantiate(GameManager.inventorySlot, slotsPanel.transform));
		}
	}

	public void AddItem(int id)
	{
		ItemData itemToAdd = GameManager.dataBaseItems.FetchItem(id);

		int index = CheckItemInInventory(id);

		if (itemToAdd.stackable && (index > -1))
		{
			slots[index].GetComponentInChildren<InventoryItem>().AddAmount();
		}
		else
		{
			for (int i = 0; i < items.Count; i++)
			{
				if (items[i].id == -1)
				{
					items[i] = itemToAdd;
					GameObject obj = Instantiate(GameManager.inventoryItem, slots[i].transform);
					obj.name = items[i].name;

					switch (itemToAdd.type)
					{
						case ItemData.Type.Garbage:
							obj.AddComponent<ItemAction>();
							break;

						case ItemData.Type.Weapon:
							obj.AddComponent<ItemActionWeapon>();

							break;

						case ItemData.Type.HealthPoti:
							obj.AddComponent<ItemActionHealth>();
							break;

						default:
							break;
					}

					obj.GetComponent<ItemAction>().SetItem(itemToAdd);
					obj.GetComponent<InventoryItem>().SetImage(itemToAdd.GetSprite());
					break;
				}
			}
		}

	}

	public int CheckItemInInventory(int id)
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].id == id)
			{
				return i;
			}
		}

		return -1;
	}
}
