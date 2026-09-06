using System;
using UnityEngine;
using Zorro.Core.CLI;

namespace AdvancedConsole;

public static class ConsoleIntegration
{
	public static void RegisterAdvancedConsolePage()
	{
		try
		{
			DebugUIHandler val = UnityEngine.Object.FindFirstObjectByType<DebugUIHandler>();
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				val.RegisterPage("Advanced Console", (Func<DebugPage>)(() => (DebugPage)(object)new AdvancedConsolePage()));
				val.RegisterPage("Server Info", (Func<DebugPage>)(() => (DebugPage)(object)new ServerInfoPage()));
				Plugin.Log.LogInfo((object)"Advanced Console and Server Info pages registered successfully");
			}
			else
			{
				Plugin.Log.LogWarning((object)"DebugUIHandler not found - cannot register Advanced Console pages");
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Failed to register Advanced Console pages: " + ex.Message));
		}
	}
}
