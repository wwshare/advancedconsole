using System;
using System.Linq;
using System.Reflection;
using Photon.Pun;
using UnityEngine;
using Zorro.Core.CLI;

namespace AdvancedConsole;

public static class WeatherCommands
{
	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void ToggleSnowStorm()
	{
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		Type type = AppDomain.CurrentDomain.GetAssemblies().SelectMany(delegate(Assembly a)
		{
			try
			{
				return a.GetTypes();
			}
			catch
			{
				return Array.Empty<Type>();
			}
		}).FirstOrDefault((Type t) => t.Name == "WindChillZone");
		if (type == null)
		{
			Debug.LogWarning((object)"WindChillZone type not found.");
			return;
		}
		object obj = type.GetProperty("Instance", BindingFlags.Static | BindingFlags.Public)?.GetValue(null);
		if (obj == null)
		{
			Debug.LogWarning((object)"WindChillZone.Instance is null.");
			return;
		}
		FieldInfo field = type.GetField("windActive", BindingFlags.Instance | BindingFlags.Public);
		if (field == null)
		{
			Debug.LogWarning((object)"windActive field not found.");
			return;
		}
		bool flag = (bool)field.GetValue(obj);
		bool flag2 = !flag;
		MethodInfo method = type.GetMethod("RandomWindDirection", BindingFlags.Instance | BindingFlags.NonPublic);
		if (method == null)
		{
			Debug.LogWarning((object)"RandomWindDirection method not found.");
			return;
		}
		Vector3 val = (Vector3)method.Invoke(obj, null);
		FieldInfo field2 = type.GetField("view", BindingFlags.Instance | BindingFlags.NonPublic);
		if (field2 == null)
		{
			Debug.LogWarning((object)"view field not found.");
			return;
		}
		object value = field2.GetValue(obj);
		if (value == null)
		{
			Debug.LogWarning((object)"PhotonView is null.");
			return;
		}
		MethodInfo method2 = value.GetType().GetMethod("RPC", new Type[3]
		{
			typeof(string),
			typeof(RpcTarget),
			typeof(object[])
		});
		if (method2 == null)
		{
			Debug.LogWarning((object)"RPC method not found.");
			return;
		}
		method2.Invoke(value, new object[3]
		{
			"RPCA_ToggleWind",
			(object)(RpcTarget)0,
			new object[2] { flag2, val }
		});
		Debug.Log((object)("wind is now " + (flag2 ? "ON" : "OFF")));
		Plugin.Log.LogInfo((object)("Wind toggled: " + (flag2 ? "ON" : "OFF")));
	}
}
