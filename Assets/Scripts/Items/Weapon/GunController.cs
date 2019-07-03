using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
	[SerializeField]
	private GameObject _bullet;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (gameObject.activeSelf)
		{
			//
			if (GameManager.input.triggerLeftDown && GameManager.player.teleport.activeSelf == false)
			{
				GameManager.soundManager.CreateSound(3);
				GameObject newBullet = Instantiate(_bullet, new Vector3(transform.position.x - 0.01f, transform.position.y + 0.08f, transform.position.z + 0.10f), Quaternion.Euler(0,0,0));
				newBullet.transform.rotation = Camera.main.transform.rotation;
				Destroy(newBullet, 3);
			}
		}
	}
}
