using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
	[SerializeField]
	private float _speed;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.Translate(Vector3.forward * _speed * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			GameManager.soundManager.CreateSound(5);
			Destroy(other.gameObject);
		}
	}

}
