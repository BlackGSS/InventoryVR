using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryItem : MonoBehaviour
{
	[SerializeField]
	private Color _selectedColor;

	private Color _unselectedColor;
	private Image _img;
	private Text _text;
	private Image _parentImage;

	private int _amount = 0;
	public int amount { get { return _amount;  } }

	// Use this for initialization
	void Awake()
	{
		_img = GetComponent<Image>();
		_text = GetComponentInChildren<Text>();
		_parentImage = transform.parent.GetComponent<Image>();
		_unselectedColor = _parentImage.color;

		ResetAmount();
	}

	public void AddAmount(int amount = 1)
	{
		if (_amount + amount > 99)
		{
			return;
		}
		else
		{
			_amount += amount;
			_text.text = _amount.ToString("00");
			_text.gameObject.SetActive(_amount > 1);
		}
	}

	public void SubstractAmount(int amount = 1)
	{
		GameManager.soundManager.CreateSound(1);
		if (_amount - amount < 0)
		{
			return;
		}
		else
		{
			_amount -= amount;
			_text.text = _amount.ToString("00");
			_text.gameObject.SetActive(_amount > 1);
		}
	}

	public void ResetAmount()
	{
		_amount = 1;
		_text.gameObject.SetActive(false);
	}

	public void SetImage(Sprite sprite)
	{
		_img.sprite = sprite;
	}

	public void SetSelected()
	{
		_parentImage.color = _selectedColor;
	}

	public void SetUnselected()
	{
		_parentImage.color = _unselectedColor;
	}

}
