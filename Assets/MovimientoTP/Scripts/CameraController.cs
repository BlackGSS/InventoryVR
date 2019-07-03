using UnityEngine;

public class CameraController : MonoBehaviour
{

	public float angularSpeed;
	private Transform _camera;

	private Vector2 _oldMousePosition;

	// Use this for initialization
	void Start()
	{
		_camera = Camera.main.transform;

		_oldMousePosition = Input.mousePosition;
	}

	// Update is called once per frame
	void Update()
	{

		Vector3 difference = new Vector3(_oldMousePosition.y - Input.mousePosition.y, Input.mousePosition.x - _oldMousePosition.x, 0);

		difference *= angularSpeed;

		_camera.rotation = Quaternion.Euler(_camera.rotation.eulerAngles + difference);

		_oldMousePosition = Input.mousePosition;

	}
}
