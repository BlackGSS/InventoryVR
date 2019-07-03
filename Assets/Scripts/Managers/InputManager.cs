using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	private KeyCode _touchpadLeftKey, _touchpadRightKey;

	public KeyCode touchpadLeftKey	{ set { _touchpadLeftKey = value;	} }
	public KeyCode touchpadRightKey { set { _touchpadRightKey = value;	} }

	private bool _triggerLeft, _triggerLeftDown, _triggerLeftUp;

	public bool triggerLeft			{ get { return _triggerLeft;		} }
	public bool triggerLeftDown		{ get { return _triggerLeftDown;	} }
	public bool triggerLeftUp		{ get { return _triggerLeftUp;		} }


	private bool _triggerRight, _triggerRightDown, _triggerRightUp;

	public bool triggerRight		{ get { return _triggerRight;		} }
	public bool triggerRightDown	{ get { return _triggerRightDown;	} }
	public bool triggerRightUp		{ get { return _triggerRightUp;		} }

	private bool _touchpadLeft, _touchpadLeftDown, _touchpadLeftUp;

	public bool touchpadLeft		{ get { return _touchpadLeft;		} }
	public bool touchpadLeftDown	{ get { return _touchpadLeftDown;	} }
	public bool touchpadLeftUp		{ get { return _touchpadLeftUp;		} }

	private bool _touchpadRight, _touchpadRightDown, _touchpadRightUp;

	public bool touchpadRight		{ get { return _touchpadRight;		} }
	public bool touchpadRightDown	{ get { return _touchpadRightDown;	} }
	public bool touchpadRightUp		{ get { return _touchpadRightUp;	} }

	//Declaramos el evento
	public event Action OnTriggerRightDown;
	

	// Update is called once per frame
	void Update()
	{
		_triggerLeft		= Input.GetMouseButton(0);
		_triggerLeftDown	= Input.GetMouseButtonDown(0);
		_triggerLeftUp		= Input.GetMouseButtonUp(0);

		_triggerRight		= Input.GetMouseButton(1);
		_triggerRightDown	= Input.GetMouseButtonDown(1);

		//Lanzamos el evento
		if (Input.GetMouseButtonDown(1))
			OnTriggerRightDown();

		_triggerRightUp		= Input.GetMouseButtonUp(1);

		_touchpadLeft		= Input.GetKey(_touchpadLeftKey);
		_touchpadLeftDown	= Input.GetKeyDown(_touchpadLeftKey);
		_touchpadLeftUp		= Input.GetKeyUp(_touchpadLeftKey);

		_touchpadRight		= Input.GetKey(_touchpadRightKey);
		_touchpadRightDown	= Input.GetKeyDown(_touchpadRightKey);
		_touchpadRightUp	= Input.GetKeyUp(_touchpadRightKey);
		
	}
}