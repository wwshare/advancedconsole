using System;
using Photon.Pun;
using UnityEngine;

namespace AdvancedConsole;

public class CharacterAfflictionsRPC : MonoBehaviourPunCallbacks
{
	private CharacterAfflictions CA => ((Component)this).GetComponent<CharacterAfflictions>();

	[PunRPC]
	public void RPC_ClearStatus(int statusType)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		CharacterAfflictions.STATUSTYPE val = (CharacterAfflictions.STATUSTYPE)statusType;
		CA.SetStatus(val, 0f);
	}

	[PunRPC]
	public void RPC_AddStatus(int statusType, float amount)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		CharacterAfflictions.STATUSTYPE val = (CharacterAfflictions.STATUSTYPE)statusType;
		CA.AddStatus(val, amount, false);
	}

	[PunRPC]
	public void RPC_ClearCold()
	{
		CA.SetStatus((CharacterAfflictions.STATUSTYPE)2, 0f);
	}

	[PunRPC]
	public void RPC_AddCold(float amt)
	{
		CA.AddStatus((CharacterAfflictions.STATUSTYPE)2, amt, false);
	}

	[PunRPC]
	public void RPC_ClearHunger()
	{
		CA.SetStatus((CharacterAfflictions.STATUSTYPE)1, 0f);
	}

	[PunRPC]
	public void RPC_AddHunger(float amt)
	{
		CA.AddStatus((CharacterAfflictions.STATUSTYPE)1, amt, false);
	}

	[PunRPC]
	public void RPC_ClearPoison()
	{
		CA.SetStatus((CharacterAfflictions.STATUSTYPE)3, 0f);
	}

	[PunRPC]
	public void RPC_AddPoison(float amt)
	{
		CA.AddStatus((CharacterAfflictions.STATUSTYPE)3, amt, false);
	}

	[PunRPC]
	public void RPC_ClearCurse()
	{
		CA.SetStatus((CharacterAfflictions.STATUSTYPE)5, 0f);
	}

	[PunRPC]
	public void RPC_AddCurse(float amt)
	{
		CA.AddStatus((CharacterAfflictions.STATUSTYPE)5, amt, false);
	}

	[PunRPC]
	public void RPC_ClearDrowsy()
	{
		CA.SetStatus((CharacterAfflictions.STATUSTYPE)6, 0f);
	}

	[PunRPC]
	public void RPC_AddDrowsy(float amt)
	{
		CA.AddStatus((CharacterAfflictions.STATUSTYPE)6, amt, false);
	}

	[PunRPC]
	public void RPC_ClearInjury()
	{
		CA.SetStatus((CharacterAfflictions.STATUSTYPE)0, 0f);
	}

	[PunRPC]
	public void RPC_AddInjury(float amt)
	{
		CA.AddStatus((CharacterAfflictions.STATUSTYPE)0, amt, false);
	}

	[PunRPC]
	public void RPC_ClearHot()
	{
		CA.SetStatus((CharacterAfflictions.STATUSTYPE)8, 0f);
	}

	[PunRPC]
	public void RPC_AddHot(float amt)
	{
		CA.AddStatus((CharacterAfflictions.STATUSTYPE)8, amt, false);
	}

	[PunRPC]
	public void RPC_ClearAllStatuses()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		foreach (CharacterAfflictions.STATUSTYPE value in Enum.GetValues(typeof(CharacterAfflictions.STATUSTYPE)))
		{
			CA.SetStatus(value, 0f);
		}
	}
}
