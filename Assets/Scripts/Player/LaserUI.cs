using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserUI : MonoBehaviour
{
	[SerializeField]
	private Material materialOut, materialIn;

	private LineRenderer _LineR;
	private LayerMask _layerUI;

	private RaycastHit _hitInfo;

	private InventoryItem _currentItem;

	Button exitButton;

	// Use this for initialization
	void Start()
	{
		_LineR = GetComponent<LineRenderer>();
		_layerUI = LayerMask.GetMask("VR_UI");

		//Suscribirse a un evento.
		GameManager.input.OnTriggerRightDown += TriggerRightDowAction;

		exitButton.onClick.AddListener(TriggerRightDowAction);

	}

	//Acción que se realiza al lanzarse el evento. 
	void TriggerRightDowAction()
	{

	}


	// Update is called once per frame
	void Update()
	{
		_LineR.SetPosition(0, transform.position);

		Vector3 endPosition = transform.position + transform.forward * 100f;

		Ray rayUI = new Ray(transform.position, transform.forward);

		_LineR.material = materialOut;

		if (Physics.Raycast(rayUI, out _hitInfo, 100f, _layerUI))
		{
			endPosition = _hitInfo.point;
			_LineR.material = materialIn;

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

		_LineR.SetPosition(1, endPosition);

		if (_currentItem != null && GameManager.input.triggerRightDown)
		{
			_currentItem.GetComponent<ItemAction>().Action();
		}
	}
}
