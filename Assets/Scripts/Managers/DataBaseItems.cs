using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseItems : MonoBehaviour
{
	[SerializeField]
	private Data _data;

	public void LoadData()
	{
		_data = JsonUtility.FromJson<Data>(Resources.Load<TextAsset>("Files/Items").text);
	}

	public ItemData FetchItem(int id)
	{
		for (int i = 0; i < _data.itemData.Length; i++)
		{
			if (id == _data.itemData[i].id)
			{
				return _data.itemData[i];
			}
		}
		return null;
	}
}

[System.Serializable]
public class Data
{
	public ItemData[] itemData;
}


[System.Serializable]
public class ItemData
{

	public enum Type { Garbage = 0, Weapon, HealthPoti }

	public int id = -1;
	public string name = null;
	public Type type = Type.Garbage;
	public string description = null;
	public int damage = 0;
	public string spritePath = null;
	public bool stackable = false;

	public ItemData(int id = -1)
	{
		this.id = id;
	}

	public Sprite GetSprite()
	{
		return Resources.Load<Sprite>(spritePath);
	}
}
