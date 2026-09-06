using System;
using Photon.Pun;
using UnityEngine;

namespace AdvancedConsole;

public static class MasterClientUtils
{
	private static bool? _bypassMasterClient;

	public static bool IsBypassEnabled
	{
		get
		{
			if (!_bypassMasterClient.HasValue)
			{
				_bypassMasterClient = HasBypassArgument();
				if (_bypassMasterClient.Value)
				{
					Plugin.Log.LogWarning((object)"Master Client bypass is ENABLED via launch argument -bypassmasterclient");
					Debug.LogWarning((object)"WARNING: Master Client bypass is ACTIVE! Commands may not work correctly in multiplayer.");
				}
			}
			return _bypassMasterClient.Value;
		}
	}

	private static bool HasBypassArgument()
	{
		try
		{
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			string[] array = commandLineArgs;
			foreach (string text in array)
			{
				if (text.Equals("-bypassmasterclient", StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Error checking command line arguments: " + ex.Message));
		}
		return false;
	}

	public static bool IsMasterClient(string commandName = "command")
	{
		if (PhotonNetwork.IsMasterClient)
		{
			return true;
		}
		if (IsBypassEnabled)
		{
			Debug.LogWarning((object)("WARNING: " + commandName + " executed with Master Client bypass - may not work correctly!"));
			Plugin.Log.LogWarning((object)("Command '" + commandName + "' executed with bypass - not Master Client but continuing"));
			return true;
		}
		Debug.LogWarning((object)(commandName + ": You must be the Master Client to use this command!"));
		Plugin.Log.LogWarning((object)("Command '" + commandName + "' denied - not Master Client"));
		return false;
	}

	public static bool IsMasterClientQuiet()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return IsBypassEnabled;
		}
		return true;
	}
}
