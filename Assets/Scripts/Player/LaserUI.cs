using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserUI : MonoBehaviour
{
	[SerializeField]
	private Material _materialOut, _materialIn;

	private LineRenderer _lineR;
	private LayerMask _layerUI;

	private RaycastHit _hitInfo;

	private InventoryItem _currentItem;

	//Button exitButton;

	// Use this for initialization
	void Start()
	{
		_lineR = GetComponent<LineRenderer>();
		_layerUI = LayerMask.GetMask("VR_UI");

		//Suscribirse a un evento.
		GameManager.input.OnTriggerRightDown += TriggerRightDownAction;

		//exitButton.onClick.AddListener(TriggerRightDowAction);

	}

	//Acción que se realiza al lanzarse el evento. 
	void TriggerRightDownAction()
	{
		print("me usan");
	}


	// Update is called once per frame
	void Update()
	{
		_lineR.SetPosition(0, transform.position);

		Vector3 endPosition = transform.position + transform.forward * 100f;

		Ray rayUI = new Ray(transform.position, transform.forward);

		_lineR.material = _materialOut;

		if (Physics.Raycast(rayUI, out _hitInfo, 100f, _layerUI))
		{
			endPosition = _hitInfo.point;
			_lineR.material = _materialIn;

			if (_currentItem != null)
			{
				_currentItem.SetUnselected();
			}

			_currentItem = _hitInfo.collider.GetComponent<InventoryItem>();
			_currentItem.SetSelected();
		}
		else
		{
			if (_currentItem != null)
			{
				_currentItem.SetUnselected();
				_currentItem = null;
			}
		}

		_lineR.SetPosition(1, endPosition);

		if (_currentItem != null && GameManager.input.triggerRightDown)
		{
			_currentItem.GetComponent<ItemAction>().Action();
		}
	}
}
