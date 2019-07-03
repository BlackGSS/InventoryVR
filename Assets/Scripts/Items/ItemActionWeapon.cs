using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActionWeapon : ItemAction
{
	public override void Action()
	{
		switch (_data.id)
		{
			case 0:
				if (GameManager.player.rightHand != 0)
				{
					GameManager.soundManager.CreateSound(1);
					GameManager.player.SetWeaponRight(0);
				}
				else if (GameManager.player.leftHand != 0)
				{
					GameManager.soundManager.CreateSound(1);
					GameManager.player.SetWeaponLeft(0);
				}
				else
				{
					GameManager.soundManager.CreateSound(1);
					GameManager.player.SetWeaponRight(0);
				}

				break;

			default:
				break;
		}
		
		base.Action();
	}
}
