using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActionHealth : ItemAction
{
	public override void Action()
	{
		_itemUI.SubstractAmount();

		GameManager.player.Heal(_data.damage);

		base.Action();
	}
}
