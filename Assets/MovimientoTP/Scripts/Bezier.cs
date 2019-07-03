using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{

	[Range(2,20)]
	public float distanciaMaxima = 5f;

	[Range(1, 5)]
	public float alturaControl = 3f;

	[Range(0, 20)]
	public float descensoMaximo = 10f;

	public Transform room;
	public LayerMask layerTeleport;

	private const int NUMERO_DE_VERTICES = 20;

	private Vector3		 _puntoFinal, _control;
	private RaycastHit   _hitInfo;
	private GameObject   _marcador;

	private LineRenderer _LineR;
	private Vector3[]	 _positions = new Vector3[NUMERO_DE_VERTICES];

	private Gradient	 _gradientLine = new Gradient();
	private GradientColorKey[] _keyColors = new GradientColorKey[5];
	private float		 _gradientTimer;

	private SoundManager _SoundManager;

	// Use this for initialization
	void Start()
	{
		_SoundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
		_LineR = GetComponent<LineRenderer>();

		_marcador = transform.GetChild(0).gameObject;

		_keyColors[0].color = Color.red;
		_keyColors[0].time = 0;

		_keyColors[4].color = Color.red;
		_keyColors[4].time = 1;

		_gradientTimer = 0;
		ChangeGradient(0);
		
	}

	private void ChangeGradient(float t)
	{
		_gradientTimer = (_gradientTimer + t) % (1 - 0.2f);

		_keyColors[1].color = Color.red;
		_keyColors[1].time = _gradientTimer;

		_keyColors[2].color = Color.white;
		_keyColors[2].time = _gradientTimer + 0.1f;

		_keyColors[3].color = Color.red;
		_keyColors[3].time = _gradientTimer + 0.2f;

		_gradientLine.colorKeys = _keyColors;
		_LineR.colorGradient = _gradientLine;

	}

	// Update is called once per frame
	void Update()
	{
		CalculateCurve();
		GetCollision();

		if (GameManager.input.triggerLeftDown && _marcador.activeSelf == true)
		{
			room.position = _marcador.transform.position;
			_SoundManager.CreateSound(0);

		}

		ChangeGradient(Time.deltaTime);
	}

	private void GetCollision()
	{
		_marcador.SetActive(false);
		_marcador.transform.position = transform.position;

		for (int i = 0; i < NUMERO_DE_VERTICES - 1; i++)
		{
			if (Physics.Linecast(_positions[i], _positions[i + 1], out _hitInfo, layerTeleport))
			{
				_marcador.SetActive(true);
				_marcador.transform.position = _hitInfo.point;

				_marcador.transform.rotation = Quaternion.FromToRotation(Vector3.up, _hitInfo.normal);
				return;
			}
		}
	}


	private void CalculateCurve()
	{
		for (int i = 0; i < NUMERO_DE_VERTICES; i++)
		{
			_puntoFinal = transform.position + transform.forward * distanciaMaxima;

			_control = transform.position + (_puntoFinal - transform.position)/2;
			_control.y += alturaControl;

			_puntoFinal.y = transform.position.y - descensoMaximo;

			float t = (float) i / (float)(NUMERO_DE_VERTICES - 1);
			_positions[i] = CalculateBezierPoint(t, transform.position, _control, _puntoFinal);
			

			_LineR.SetPositions(_positions);
		}

	}

	private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
	{

		//B(t) = (1-t)^2 * p0 + 2 * t * (1-t) * p1 + t * t * p2

		// u = 1 - t;

		float u = 1 - t;

		//B(t) = point = u * u * p0 + 2 * t * u * p1 + t * t * p2;
		Vector3 point  = u * u * p0;
				point += 2 * t * u * p1;
				point += t * t * p2;

		return point;

	}
}
