using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zorro.Core.CLI;

using PhotonPlayer = Photon.Realtime.Player;
namespace AdvancedConsole;

public static class ExampleMasterClientCommands
{
	[ConsoleCommand]
	[MasterClientOnly("TestMasterCommand")]
	public static void TestMasterCommand()
	{
		Debug.Log((object)"此命令由房主执行!");
		Plugin.Log.LogInfo((object)"TestMasterCommand 执行成功");
	}

	[ConsoleCommand]
	[MasterClientOnly(null, WarnOnly = true)]
	public static void TestWarnCommand()
	{
		Debug.Log((object)"此命令会向非房主客户端显示警告, 但仍会执行");
		Plugin.Log.LogInfo((object)"TestWarnCommand 已执行");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void KickAllPlayers()
	{
		Debug.Log((object)"正在踢出所有玩家 (仅房主)");
		PhotonPlayer[] playerList = PhotonNetwork.PlayerList;
		foreach (PhotonPlayer val in playerList)
		{
			if (!val.IsLocal && val != PhotonNetwork.MasterClient)
			{
				Debug.Log((object)("将踢出玩家: " + val.NickName));
			}
		}
	}

	[ConsoleCommand]
	public static void PublicCommand()
	{
		Debug.Log((object)"此命令任何人都可执行");
	}
}
