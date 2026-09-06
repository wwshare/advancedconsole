using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Zorro.Core;
using Zorro.Core.CLI;

using PhotonPlayer = Photon.Realtime.Player;
namespace AdvancedConsole;

public class ServerInfoPage : DebugPage
{
	public class PlayerCheatInfo
	{
		public PhotonPlayer? PhotonPlayer { get; set; }

		public int ActorNumber { get; set; }

		public string NickName { get; set; } = "";

		public bool HasAtlas { get; set; }

		public bool IsAtlasOwner { get; set; }

		public bool HasCherry { get; set; }

		public bool IsCherryOwner { get; set; }

		public bool HasAdvancedConsole { get; set; }

		public bool IsCheater
		{
			get
			{
				if (!HasAtlas && !IsAtlasOwner && !HasCherry && !IsCherryOwner)
				{
					return CheatFunctions.Count > 0;
				}
				return true;
			}
		}

		public string ModLoader { get; set; } = "";

		public List<string> ModList { get; set; } = new List<string>();

		public string ModHash { get; set; } = "";

		public List<string> CheatFunctions { get; set; } = new List<string>();
	}

	private VisualElement _serverInfoContainer;

	private VisualElement _playersContainer;

	private Button _refreshButton;

	private Label _lastUpdateLabel;

	private float _lastUpdateTime;

	private Dictionary<int, VisualElement> _playerCards = new Dictionary<int, VisualElement>();

	public ServerInfoPage()
	{
		((VisualElement)this).name = "ServerInfo";
		SetupPage();
		RefreshInfo();
		_lastUpdateTime = Time.time;
		((VisualElement)this).schedule.Execute((Action)AutoRefresh).Every(1000L);
	}

	private void AutoRefresh()
	{
		if (PluginConfig.AutoRefreshServerInfo)
		{
			float serverInfoRefreshInterval = PluginConfig.ServerInfoRefreshInterval;
			if (Time.time - _lastUpdateTime > serverInfoRefreshInterval)
			{
				RefreshInfo();
				_lastUpdateTime = Time.time;
			}
		}
	}

	private void SetupPage()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Expected O, but got Unknown
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Expected O, but got Unknown
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Expected O, but got Unknown
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Expected O, but got Unknown
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Expected O, but got Unknown
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Expected O, but got Unknown
		Label val = new Label("服务器信息");
		((VisualElement)val).style.fontSize = new StyleLength(20f);
		((VisualElement)val).style.unityFontStyleAndWeight = new StyleEnum<FontStyle>((FontStyle)1);
		((VisualElement)val).style.color = new StyleColor(new Color(0.2f, 0.6f, 0.9f));
		((VisualElement)val).style.marginBottom = new StyleLength(8f);
		((VisualElement)val).style.marginTop = new StyleLength(3f);
		Label val2 = val;
		((VisualElement)this).Add((VisualElement)(object)val2);
		VisualElement val3 = new VisualElement();
		val3.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)2);
		val3.style.marginBottom = new StyleLength(8f);
		val3.style.backgroundColor = new StyleColor(new Color(0.15f, 0.15f, 0.15f, 0.8f));
		val3.style.paddingTop = new StyleLength(4f);
		val3.style.paddingBottom = new StyleLength(4f);
		val3.style.paddingLeft = new StyleLength(6f);
		val3.style.paddingRight = new StyleLength(6f);
		val3.style.borderTopLeftRadius = new StyleLength(4f);
		val3.style.borderTopRightRadius = new StyleLength(4f);
		val3.style.borderBottomLeftRadius = new StyleLength(4f);
		val3.style.borderBottomRightRadius = new StyleLength(4f);
		val3.style.justifyContent = new StyleEnum<Justify>((Justify)3);
		val3.style.alignItems = new StyleEnum<Align>((Align)2);
		VisualElement val4 = val3;
		Button val5 = new Button((Action)RefreshInfo)
		{
			text = "刷新"
		};
		((VisualElement)val5).style.backgroundColor = new StyleColor(new Color(0.2f, 0.5f, 0.8f));
		((VisualElement)val5).style.color = new StyleColor(Color.white);
		((VisualElement)val5).style.paddingTop = new StyleLength(4f);
		((VisualElement)val5).style.paddingBottom = new StyleLength(4f);
		((VisualElement)val5).style.paddingLeft = new StyleLength(8f);
		((VisualElement)val5).style.paddingRight = new StyleLength(8f);
		_refreshButton = val5;
		val4.Add((VisualElement)(object)_refreshButton);
		Label val6 = new Label("从未更新");
		((VisualElement)val6).style.fontSize = new StyleLength(11f);
		((VisualElement)val6).style.color = new StyleColor(new Color(0.7f, 0.7f, 0.7f));
		((VisualElement)val6).style.alignSelf = new StyleEnum<Align>((Align)3);
		_lastUpdateLabel = val6;
		val4.Add((VisualElement)(object)_lastUpdateLabel);
		((VisualElement)this).Add(val4);
		ScrollView val7 = new ScrollView();
		((VisualElement)val7).style.flexGrow = new StyleFloat(1f);
		ScrollView val8 = val7;
		VisualElement val9 = new VisualElement();
		val9.style.marginBottom = new StyleLength(12f);
		_serverInfoContainer = val9;
		((VisualElement)val8).Add(_serverInfoContainer);
		_playersContainer = new VisualElement();
		((VisualElement)val8).Add(_playersContainer);
		((VisualElement)this).Add((VisualElement)(object)val8);
	}

	private void RefreshInfo()
	{
		try
		{
			if (_serverInfoContainer != null)
			{
				_serverInfoContainer.Clear();
				CreateServerInfoSection();
			}
			if (_playersContainer != null)
			{
				UpdatePlayersSection();
			}
			if (_lastUpdateLabel != null)
			{
				((TextElement)_lastUpdateLabel).text = $"上次更新：{DateTime.Now:HH:mm:ss}";
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("服务器信息页刷新信息出错：" + ex.Message));
			if (_lastUpdateLabel != null)
			{
				((TextElement)_lastUpdateLabel).text = $"更新失败：{DateTime.Now:HH:mm:ss}";
			}
		}
	}

	private void CreateServerInfoSection()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Expected O, but got Unknown
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		Label val = new Label("服务器详情");
		((VisualElement)val).style.fontSize = new StyleLength(16f);
		((VisualElement)val).style.unityFontStyleAndWeight = new StyleEnum<FontStyle>((FontStyle)1);
		((VisualElement)val).style.color = new StyleColor(new Color(0.9f, 0.9f, 0.2f));
		((VisualElement)val).style.marginBottom = new StyleLength(6f);
		Label val2 = val;
		_serverInfoContainer.Add((VisualElement)(object)val2);
		VisualElement val3 = new VisualElement();
		val3.style.backgroundColor = new StyleColor(new Color(0.1f, 0.1f, 0.1f, 0.8f));
		val3.style.paddingTop = new StyleLength(8f);
		val3.style.paddingBottom = new StyleLength(8f);
		val3.style.paddingLeft = new StyleLength(10f);
		val3.style.paddingRight = new StyleLength(10f);
		val3.style.borderTopLeftRadius = new StyleLength(4f);
		val3.style.borderTopRightRadius = new StyleLength(4f);
		val3.style.borderBottomLeftRadius = new StyleLength(4f);
		val3.style.borderBottomRightRadius = new StyleLength(4f);
		val3.style.marginBottom = new StyleLength(8f);
		VisualElement val4 = val3;
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		Room currentRoom = PhotonNetwork.CurrentRoom;
		dictionary.Add("房间名称", ((currentRoom != null) ? currentRoom.Name : null) ?? "无");
		Scene activeScene = SceneManager.GetActiveScene();
		dictionary.Add("当前地图", activeScene.name);
		Room currentRoom2 = PhotonNetwork.CurrentRoom;
		object arg = ((currentRoom2 != null) ? currentRoom2.PlayerCount : 0);
		Room currentRoom3 = PhotonNetwork.CurrentRoom;
		dictionary.Add("玩家人数", $"{arg} / {((currentRoom3 != null) ? currentRoom3.MaxPlayers : 0)}");
		PhotonPlayer masterClient = PhotonNetwork.MasterClient;
		dictionary.Add("房主", ((masterClient != null) ? masterClient.NickName : null) ?? "无");
		PhotonPlayer localPlayer = PhotonNetwork.LocalPlayer;
		dictionary.Add("本机玩家", ((localPlayer != null) ? localPlayer.NickName : null) ?? "无");
		dictionary.Add("连接状态", PhotonNetwork.IsConnected ? "已连接" : "未连接");
		dictionary.Add("是否为房主", PhotonNetwork.IsMasterClient ? "是" : "否");
		dictionary.Add("网络时间", $"{PhotonNetwork.Time:F2}s");
		dictionary.Add("延迟", $"{PhotonNetwork.GetPing()} ms");
		dictionary.Add("区域", PhotonNetwork.CloudRegion ?? "无");
		dictionary.Add("应用版本", PhotonNetwork.AppVersion ?? "无");
		Dictionary<string, string> dictionary2 = dictionary;
		foreach (KeyValuePair<string, string> item in dictionary2)
		{
			VisualElement val5 = CreateInfoRow(item.Key, item.Value);
			val4.Add(val5);
		}
		_serverInfoContainer.Add(val4);
	}

	private void CreatePlayersSection()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Expected O, but got Unknown
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Expected O, but got Unknown
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Expected O, but got Unknown
		Room currentRoom = PhotonNetwork.CurrentRoom;
		Label val = new Label($"玩家 ({((currentRoom != null) ? currentRoom.PlayerCount : 0)})");
		((VisualElement)val).style.fontSize = new StyleLength(16f);
		((VisualElement)val).style.unityFontStyleAndWeight = new StyleEnum<FontStyle>((FontStyle)1);
		((VisualElement)val).style.color = new StyleColor(new Color(0.2f, 0.9f, 0.2f));
		((VisualElement)val).style.marginBottom = new StyleLength(6f);
		Label val2 = val;
		_playersContainer.Add((VisualElement)(object)val2);
		VisualElement val3 = new VisualElement();
		val3.style.backgroundColor = new StyleColor(new Color(0.1f, 0.1f, 0.1f, 0.8f));
		val3.style.paddingTop = new StyleLength(8f);
		val3.style.paddingBottom = new StyleLength(8f);
		val3.style.paddingLeft = new StyleLength(10f);
		val3.style.paddingRight = new StyleLength(10f);
		val3.style.borderTopLeftRadius = new StyleLength(4f);
		val3.style.borderTopRightRadius = new StyleLength(4f);
		val3.style.borderBottomLeftRadius = new StyleLength(4f);
		val3.style.borderBottomRightRadius = new StyleLength(4f);
		VisualElement val4 = val3;
		PhotonPlayer[] playerList = PhotonNetwork.PlayerList;
		if (playerList != null && playerList.Length != 0)
		{
			IOrderedEnumerable<PhotonPlayer> orderedEnumerable = from p in PhotonNetwork.PlayerList
				orderby (!p.IsLocal) ? 1 : 0, (!p.IsMasterClient) ? 1 : 0, p.ActorNumber
				select p;
			foreach (PhotonPlayer item in orderedEnumerable)
			{
				try
				{
					VisualElement val5 = CreatePlayerCard(item);
					val4.Add(val5);
					_playerCards[item.ActorNumber] = val5;
				}
				catch (Exception ex)
				{
					Debug.LogError((object)("创建玩家卡片出错 " + ((item != null) ? item.NickName : null) + "：" + ex.Message));
					Label val6 = new Label("加载玩家出错：" + (((item != null) ? item.NickName : null) ?? "未知"));
					((VisualElement)val6).style.color = new StyleColor(new Color(1f, 0.5f, 0.5f));
					((VisualElement)val6).style.fontSize = new StyleLength(12f);
					((VisualElement)val6).style.marginTop = new StyleLength(4f);
					((VisualElement)val6).style.marginBottom = new StyleLength(4f);
					Label val7 = val6;
					val4.Add((VisualElement)(object)val7);
				}
			}
		}
		else
		{
			Label val8 = new Label("未找到玩家");
			((VisualElement)val8).style.color = new StyleColor(new Color(0.7f, 0.7f, 0.7f));
			((VisualElement)val8).style.fontSize = new StyleLength(12f);
			((VisualElement)val8).style.unityTextAlign = new StyleEnum<TextAnchor>((TextAnchor)4);
			((VisualElement)val8).style.marginTop = new StyleLength(20f);
			((VisualElement)val8).style.marginBottom = new StyleLength(20f);
			Label val9 = val8;
			val4.Add((VisualElement)(object)val9);
		}
		_playersContainer.Add(val4);
	}

	private void UpdatePlayersSection()
	{
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Expected O, but got Unknown
		try
		{
			Dictionary<int, PhotonPlayer> dictionary = PhotonNetwork.PlayerList?.ToDictionary((PhotonPlayer p) => p.ActorNumber, (PhotonPlayer p) => p) ?? new Dictionary<int, PhotonPlayer>();
			List<int> list = new List<int>();
			foreach (KeyValuePair<int, VisualElement> item in _playerCards.ToList())
			{
				int key = item.Key;
				VisualElement value = item.Value;
				if (!dictionary.ContainsKey(key))
				{
					value.RemoveFromHierarchy();
					_playerCards.Remove(key);
					Debug.Log((object)$"已移除玩家卡片，编号 {key}");
				}
			}
			Label val = UQueryExtensions.Q<Label>(_playersContainer, (string)null, (string)null);
			if (val != null)
			{
				Room currentRoom = PhotonNetwork.CurrentRoom;
				((TextElement)val).text = $"玩家 ({((currentRoom != null) ? currentRoom.PlayerCount : 0)})";
			}
			VisualElement val2 = _playersContainer.Children().Skip(1).FirstOrDefault();
			if (val2 == null)
			{
				_playersContainer.Clear();
				_playerCards.Clear();
				CreatePlayersSection();
				return;
			}
			IOrderedEnumerable<PhotonPlayer> orderedEnumerable = from p in dictionary.Values
				orderby (!p.IsLocal) ? 1 : 0, (!p.IsMasterClient) ? 1 : 0, p.ActorNumber
				select p;
			foreach (PhotonPlayer item2 in orderedEnumerable)
			{
				if (!_playerCards.ContainsKey(item2.ActorNumber))
				{
					try
					{
						VisualElement val3 = CreatePlayerCard(item2);
						val2.Add(val3);
						_playerCards[item2.ActorNumber] = val3;
						Debug.Log((object)$"已添加新玩家卡片：{item2.NickName}（编号：{item2.ActorNumber}）");
					}
					catch (Exception ex)
					{
						Debug.LogError((object)("创建玩家卡片出错 " + item2.NickName + "：" + ex.Message));
					}
				}
				else
				{
					try
					{
						UpdatePlayerCardData(_playerCards[item2.ActorNumber], item2);
					}
					catch (Exception ex2)
					{
						Debug.LogError((object)("更新玩家卡片出错 " + item2.NickName + "：" + ex2.Message));
					}
				}
			}
			if (dictionary.Count > 0)
			{
				Label val4 = UQueryExtensions.Q<Label>(val2, "NoPlayersMessage", (string)null);
				if (val4 != null)
				{
					((VisualElement)val4).RemoveFromHierarchy();
				}
			}
			else if (dictionary.Count == 0 && val2.childCount == 0)
			{
				Label val5 = new Label("未找到玩家")
				{
					name = "NoPlayersMessage"
				};
				((VisualElement)val5).style.color = new StyleColor(new Color(0.7f, 0.7f, 0.7f));
				((VisualElement)val5).style.fontSize = new StyleLength(12f);
				((VisualElement)val5).style.unityTextAlign = new StyleEnum<TextAnchor>((TextAnchor)4);
				((VisualElement)val5).style.marginTop = new StyleLength(20f);
				((VisualElement)val5).style.marginBottom = new StyleLength(20f);
				Label val6 = val5;
				val2.Add((VisualElement)(object)val6);
			}
		}
		catch (Exception ex3)
		{
			Debug.LogError((object)("更新玩家区域出错：" + ex3.Message));
			_playersContainer.Clear();
			_playerCards.Clear();
			CreatePlayersSection();
		}
	}

	private void UpdatePlayerCardData(VisualElement playerCard, PhotonPlayer player)
	{
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Character val = Character.AllCharacters.FirstOrDefault(delegate(Character c)
			{
				PhotonView photonView = ((MonoBehaviourPun)c).photonView;
				int? obj;
				if (photonView == null)
				{
					obj = null;
				}
				else
				{
					PhotonPlayer owner = photonView.Owner;
					obj = ((owner != null) ? new int?(owner.ActorNumber) : ((int?)null));
				}
				return obj == player.ActorNumber;
			});
			if (!((UnityEngine.Object)(object)val != (UnityEngine.Object)null))
			{
				return;
			}
			VisualElement val2 = UQueryExtensions.Q(playerCard, "StaminaContainer", (string)null);
			if (val2 != null)
			{
				VisualElement val3 = UQueryExtensions.Q(val2, "StaminaBar", (string)null);
				Label val4 = UQueryExtensions.Q<Label>(val2, "StaminaText", (string)null);
				if (val3 != null && val4 != null)
				{
					float num = ((val.data.TotalStamina > 0f) ? (val.data.currentStamina / val.data.TotalStamina) : 0f);
					num = Mathf.Clamp01(num);
					val3.style.width = new StyleLength(new Length(num * 100f, (LengthUnit)1));
					val3.style.backgroundColor = new StyleColor(GetStaminaColor(val.data.currentStamina, val.data.TotalStamina));
					((TextElement)val4).text = $"{val.data.currentStamina:F1}/{val.data.TotalStamina:F1}";
				}
			}
			if (PhotonNetwork.IsMasterClient || MasterClientUtils.IsBypassEnabled)
			{
				VisualElement val5 = UQueryExtensions.Q(playerCard, "InventoryContainer", (string)null);
				if (val5 != null)
				{
					UpdateInventoryDropdowns(val5, player, val);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("更新玩家卡片数据出错 " + player.NickName + "：" + ex.Message));
		}
	}

	private void UpdateInventoryDropdowns(VisualElement inventoryContainer, PhotonPlayer photonPlayer, Character character)
	{
		try
		{
			Player player = PlayerHandler.GetPlayer(photonPlayer);
			if ((UnityEngine.Object)(object)player == (UnityEngine.Object)null)
			{
				return;
			}
			for (byte b = 0; b <= 3; b++)
			{
				DropdownField val = UQueryExtensions.Q<DropdownField>(inventoryContainer, $"Slot_{b}", (string)null);
				if (val != null)
				{
					try
					{
						ItemSlot itemSlot = player.GetItemSlot(b);
						string valueWithoutNotify = "EMPTY";
						if (itemSlot != null && !itemSlot.IsEmpty() && (UnityEngine.Object)(object)itemSlot.prefab != (UnityEngine.Object)null)
						{
							valueWithoutNotify = ((UnityEngine.Object)itemSlot.prefab).name;
						}
						((BaseField<string>)(object)val).SetValueWithoutNotify(valueWithoutNotify);
					}
					catch (Exception ex)
					{
						Debug.LogWarning((object)$"更新栏位 {b} 的下拉框出错：{ex.Message}");
					}
				}
			}
		}
		catch (Exception ex2)
		{
			Debug.LogError((object)("更新物品栏下拉框出错：" + ex2.Message));
		}
	}

	private VisualElement CreatePlayerCard(PhotonPlayer player)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Expected O, but got Unknown
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Expected O, but got Unknown
		//IL_22cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_22d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_22e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_22eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_22f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2300: Unknown result type (might be due to invalid IL or missing references)
		//IL_230c: Expected O, but got Unknown
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Expected O, but got Unknown
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Expected O, but got Unknown
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Expected O, but got Unknown
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Expected O, but got Unknown
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Expected O, but got Unknown
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_0666: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0680: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0695: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_0713: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0728: Unknown result type (might be due to invalid IL or missing references)
		//IL_0732: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		//IL_0752: Unknown result type (might be due to invalid IL or missing references)
		//IL_075e: Expected O, but got Unknown
		//IL_077d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0782: Unknown result type (might be due to invalid IL or missing references)
		//IL_0797: Unknown result type (might be due to invalid IL or missing references)
		//IL_079c: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07db: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0805: Unknown result type (might be due to invalid IL or missing references)
		//IL_080f: Unknown result type (might be due to invalid IL or missing references)
		//IL_081a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0824: Unknown result type (might be due to invalid IL or missing references)
		//IL_082f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0839: Unknown result type (might be due to invalid IL or missing references)
		//IL_0844: Unknown result type (might be due to invalid IL or missing references)
		//IL_084e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_0863: Unknown result type (might be due to invalid IL or missing references)
		//IL_086e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0878: Unknown result type (might be due to invalid IL or missing references)
		//IL_0883: Unknown result type (might be due to invalid IL or missing references)
		//IL_088f: Expected O, but got Unknown
		//IL_08a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0907: Unknown result type (might be due to invalid IL or missing references)
		//IL_0911: Unknown result type (might be due to invalid IL or missing references)
		//IL_091c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0926: Unknown result type (might be due to invalid IL or missing references)
		//IL_0931: Unknown result type (might be due to invalid IL or missing references)
		//IL_093b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0946: Unknown result type (might be due to invalid IL or missing references)
		//IL_0950: Unknown result type (might be due to invalid IL or missing references)
		//IL_095b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0965: Unknown result type (might be due to invalid IL or missing references)
		//IL_0970: Unknown result type (might be due to invalid IL or missing references)
		//IL_097a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0985: Unknown result type (might be due to invalid IL or missing references)
		//IL_098f: Unknown result type (might be due to invalid IL or missing references)
		//IL_099a: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09af: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bb: Expected O, but got Unknown
		//IL_09da: Unknown result type (might be due to invalid IL or missing references)
		//IL_09df: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aec: Expected O, but got Unknown
		//IL_0b06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c18: Expected O, but got Unknown
		//IL_0d7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9b: Expected O, but got Unknown
		//IL_0c4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ceb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d60: Expected O, but got Unknown
		//IL_0e6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e90: Unknown result type (might be due to invalid IL or missing references)
		//IL_1045: Unknown result type (might be due to invalid IL or missing references)
		//IL_104a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1051: Unknown result type (might be due to invalid IL or missing references)
		//IL_105b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1066: Unknown result type (might be due to invalid IL or missing references)
		//IL_1070: Unknown result type (might be due to invalid IL or missing references)
		//IL_1077: Unknown result type (might be due to invalid IL or missing references)
		//IL_1083: Expected O, but got Unknown
		//IL_1083: Unknown result type (might be due to invalid IL or missing references)
		//IL_1088: Unknown result type (might be due to invalid IL or missing references)
		//IL_108f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1099: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_10aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c1: Expected O, but got Unknown
		//IL_10cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_10dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1101: Unknown result type (might be due to invalid IL or missing references)
		//IL_1107: Unknown result type (might be due to invalid IL or missing references)
		//IL_110c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1116: Unknown result type (might be due to invalid IL or missing references)
		//IL_1121: Unknown result type (might be due to invalid IL or missing references)
		//IL_112b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1136: Unknown result type (might be due to invalid IL or missing references)
		//IL_1140: Unknown result type (might be due to invalid IL or missing references)
		//IL_114b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1155: Unknown result type (might be due to invalid IL or missing references)
		//IL_1160: Unknown result type (might be due to invalid IL or missing references)
		//IL_116a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1175: Unknown result type (might be due to invalid IL or missing references)
		//IL_117f: Unknown result type (might be due to invalid IL or missing references)
		//IL_118a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1194: Unknown result type (might be due to invalid IL or missing references)
		//IL_119f: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11be: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11de: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1208: Unknown result type (might be due to invalid IL or missing references)
		//IL_1214: Expected O, but got Unknown
		//IL_1220: Unknown result type (might be due to invalid IL or missing references)
		//IL_1225: Unknown result type (might be due to invalid IL or missing references)
		//IL_1230: Unknown result type (might be due to invalid IL or missing references)
		//IL_1245: Unknown result type (might be due to invalid IL or missing references)
		//IL_124a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1254: Unknown result type (might be due to invalid IL or missing references)
		//IL_125a: Unknown result type (might be due to invalid IL or missing references)
		//IL_125f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1269: Unknown result type (might be due to invalid IL or missing references)
		//IL_1274: Unknown result type (might be due to invalid IL or missing references)
		//IL_127e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1289: Unknown result type (might be due to invalid IL or missing references)
		//IL_1293: Unknown result type (might be due to invalid IL or missing references)
		//IL_129e: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_12dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_12fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1307: Unknown result type (might be due to invalid IL or missing references)
		//IL_1311: Unknown result type (might be due to invalid IL or missing references)
		//IL_131c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1326: Unknown result type (might be due to invalid IL or missing references)
		//IL_1331: Unknown result type (might be due to invalid IL or missing references)
		//IL_133b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1346: Unknown result type (might be due to invalid IL or missing references)
		//IL_1350: Unknown result type (might be due to invalid IL or missing references)
		//IL_135b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1367: Expected O, but got Unknown
		//IL_1373: Unknown result type (might be due to invalid IL or missing references)
		//IL_1378: Unknown result type (might be due to invalid IL or missing references)
		//IL_1383: Unknown result type (might be due to invalid IL or missing references)
		//IL_1398: Unknown result type (might be due to invalid IL or missing references)
		//IL_139d: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1406: Unknown result type (might be due to invalid IL or missing references)
		//IL_1410: Unknown result type (might be due to invalid IL or missing references)
		//IL_141b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1425: Unknown result type (might be due to invalid IL or missing references)
		//IL_1430: Unknown result type (might be due to invalid IL or missing references)
		//IL_143a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1445: Unknown result type (might be due to invalid IL or missing references)
		//IL_144f: Unknown result type (might be due to invalid IL or missing references)
		//IL_145a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1464: Unknown result type (might be due to invalid IL or missing references)
		//IL_146f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1479: Unknown result type (might be due to invalid IL or missing references)
		//IL_1484: Unknown result type (might be due to invalid IL or missing references)
		//IL_148e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1499: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ba: Expected O, but got Unknown
		//IL_14c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_14cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_14eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_14fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1500: Unknown result type (might be due to invalid IL or missing references)
		//IL_1505: Unknown result type (might be due to invalid IL or missing references)
		//IL_150f: Unknown result type (might be due to invalid IL or missing references)
		//IL_151a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1524: Unknown result type (might be due to invalid IL or missing references)
		//IL_152f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1539: Unknown result type (might be due to invalid IL or missing references)
		//IL_1544: Unknown result type (might be due to invalid IL or missing references)
		//IL_154e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1559: Unknown result type (might be due to invalid IL or missing references)
		//IL_1563: Unknown result type (might be due to invalid IL or missing references)
		//IL_156e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1578: Unknown result type (might be due to invalid IL or missing references)
		//IL_1583: Unknown result type (might be due to invalid IL or missing references)
		//IL_158d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1598: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_15cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1601: Unknown result type (might be due to invalid IL or missing references)
		//IL_160d: Expected O, but got Unknown
		//IL_1619: Unknown result type (might be due to invalid IL or missing references)
		//IL_161e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1629: Unknown result type (might be due to invalid IL or missing references)
		//IL_163e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1643: Unknown result type (might be due to invalid IL or missing references)
		//IL_164d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1653: Unknown result type (might be due to invalid IL or missing references)
		//IL_1658: Unknown result type (might be due to invalid IL or missing references)
		//IL_1662: Unknown result type (might be due to invalid IL or missing references)
		//IL_166d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1677: Unknown result type (might be due to invalid IL or missing references)
		//IL_1682: Unknown result type (might be due to invalid IL or missing references)
		//IL_168c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1697: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_16cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_16e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1700: Unknown result type (might be due to invalid IL or missing references)
		//IL_170a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1715: Unknown result type (might be due to invalid IL or missing references)
		//IL_171f: Unknown result type (might be due to invalid IL or missing references)
		//IL_172a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1734: Unknown result type (might be due to invalid IL or missing references)
		//IL_173f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1749: Unknown result type (might be due to invalid IL or missing references)
		//IL_1754: Unknown result type (might be due to invalid IL or missing references)
		//IL_1760: Expected O, but got Unknown
		//IL_176c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1771: Unknown result type (might be due to invalid IL or missing references)
		//IL_177c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1791: Unknown result type (might be due to invalid IL or missing references)
		//IL_1796: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_17df: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_17f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1809: Unknown result type (might be due to invalid IL or missing references)
		//IL_1814: Unknown result type (might be due to invalid IL or missing references)
		//IL_181e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1829: Unknown result type (might be due to invalid IL or missing references)
		//IL_1833: Unknown result type (might be due to invalid IL or missing references)
		//IL_183e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1848: Unknown result type (might be due to invalid IL or missing references)
		//IL_1853: Unknown result type (might be due to invalid IL or missing references)
		//IL_185d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1868: Unknown result type (might be due to invalid IL or missing references)
		//IL_1872: Unknown result type (might be due to invalid IL or missing references)
		//IL_187d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1887: Unknown result type (might be due to invalid IL or missing references)
		//IL_1892: Unknown result type (might be due to invalid IL or missing references)
		//IL_189c: Unknown result type (might be due to invalid IL or missing references)
		//IL_18a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b3: Expected O, but got Unknown
		//IL_18bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_18cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_18fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1908: Unknown result type (might be due to invalid IL or missing references)
		//IL_1913: Unknown result type (might be due to invalid IL or missing references)
		//IL_191d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1928: Unknown result type (might be due to invalid IL or missing references)
		//IL_1932: Unknown result type (might be due to invalid IL or missing references)
		//IL_193d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1947: Unknown result type (might be due to invalid IL or missing references)
		//IL_1952: Unknown result type (might be due to invalid IL or missing references)
		//IL_195c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1967: Unknown result type (might be due to invalid IL or missing references)
		//IL_1971: Unknown result type (might be due to invalid IL or missing references)
		//IL_197c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1986: Unknown result type (might be due to invalid IL or missing references)
		//IL_1991: Unknown result type (might be due to invalid IL or missing references)
		//IL_199b: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_19b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_19bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_19c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_19d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_19da: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_19fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a06: Expected O, but got Unknown
		//IL_1a12: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a17: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a22: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a46: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a51: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a66: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a70: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a85: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a90: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aaf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aba: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1acf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ad9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ae4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1af9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b18: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b23: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b38: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b42: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b59: Expected O, but got Unknown
		//IL_1b65: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b75: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b99: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ba4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bae: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bce: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1be3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bed: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c02: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c17: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c22: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c37: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c41: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c56: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c61: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c76: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c80: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c95: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ca0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cac: Expected O, but got Unknown
		//IL_1cb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cec: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cf2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d01: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d16: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d21: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d36: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d40: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d55: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d60: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d75: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d94: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1da9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dde: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1df3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dff: Expected O, but got Unknown
		//IL_1e0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e10: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e30: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e35: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e45: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e54: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e69: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e74: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e89: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e93: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ebd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ed2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1edd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ee7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1efc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f07: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f11: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f26: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f31: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f46: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f52: Expected O, but got Unknown
		//IL_1f5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f63: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f83: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f88: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f92: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f98: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fa7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fe6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ff1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ffb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2006: Unknown result type (might be due to invalid IL or missing references)
		//IL_2010: Unknown result type (might be due to invalid IL or missing references)
		//IL_201b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2025: Unknown result type (might be due to invalid IL or missing references)
		//IL_2030: Unknown result type (might be due to invalid IL or missing references)
		//IL_203a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2045: Unknown result type (might be due to invalid IL or missing references)
		//IL_204f: Unknown result type (might be due to invalid IL or missing references)
		//IL_205a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2064: Unknown result type (might be due to invalid IL or missing references)
		//IL_206f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2079: Unknown result type (might be due to invalid IL or missing references)
		//IL_2084: Unknown result type (might be due to invalid IL or missing references)
		//IL_208e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2099: Unknown result type (might be due to invalid IL or missing references)
		//IL_20a5: Expected O, but got Unknown
		//IL_20b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_20b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_20c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_20d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_20db: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_20eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_20f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_20fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_2105: Unknown result type (might be due to invalid IL or missing references)
		//IL_210f: Unknown result type (might be due to invalid IL or missing references)
		//IL_211a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2124: Unknown result type (might be due to invalid IL or missing references)
		//IL_212f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2139: Unknown result type (might be due to invalid IL or missing references)
		//IL_2144: Unknown result type (might be due to invalid IL or missing references)
		//IL_214e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2159: Unknown result type (might be due to invalid IL or missing references)
		//IL_2163: Unknown result type (might be due to invalid IL or missing references)
		//IL_216e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2178: Unknown result type (might be due to invalid IL or missing references)
		//IL_2183: Unknown result type (might be due to invalid IL or missing references)
		//IL_218d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2198: Unknown result type (might be due to invalid IL or missing references)
		//IL_21a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_21c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_21cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_21d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_21e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_21f8: Expected O, but got Unknown
		VisualElement val = new VisualElement();
		val.style.backgroundColor = new StyleColor(new Color(0.15f, 0.15f, 0.15f, 0.9f));
		val.style.marginBottom = new StyleLength(6f);
		val.style.paddingTop = new StyleLength(8f);
		val.style.paddingBottom = new StyleLength(8f);
		val.style.paddingLeft = new StyleLength(10f);
		val.style.paddingRight = new StyleLength(10f);
		val.style.borderTopLeftRadius = new StyleLength(3f);
		val.style.borderTopRightRadius = new StyleLength(3f);
		val.style.borderBottomLeftRadius = new StyleLength(3f);
		val.style.borderBottomRightRadius = new StyleLength(3f);
		val.style.borderBottomWidth = new StyleFloat(1f);
		val.style.borderTopColor = new StyleColor(new Color(0.3f, 0.3f, 0.3f));
		val.style.borderBottomColor = new StyleColor(new Color(0.3f, 0.3f, 0.3f));
		val.style.borderLeftColor = new StyleColor(new Color(0.3f, 0.3f, 0.3f));
		val.style.borderRightColor = new StyleColor(new Color(0.3f, 0.3f, 0.3f));
		VisualElement val2 = val;
		try
		{
			VisualElement val3 = new VisualElement();
			val3.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)2);
			val3.style.justifyContent = new StyleEnum<Justify>((Justify)3);
			val3.style.alignItems = new StyleEnum<Align>((Align)2);
			VisualElement val4 = val3;
			PhotonPlayer obj = player;
			Label val5 = new Label(((obj != null) ? obj.NickName : null) ?? "未知玩家");
			((VisualElement)val5).style.fontSize = new StyleLength(14f);
			((VisualElement)val5).style.unityFontStyleAndWeight = new StyleEnum<FontStyle>((FontStyle)1);
			IStyle style = ((VisualElement)val5).style;
			PhotonPlayer obj2 = player;
			style.color = new StyleColor((Color)((obj2 != null && obj2.IsMasterClient) ? new Color(1f, 0.8f, 0.2f) : Color.white));
			Label val6 = val5;
			VisualElement val7 = new VisualElement();
			val7.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)2);
			VisualElement val8 = val7;
			PlayerCheatInfo playerCheatInfo = ((player != null) ? GetPlayerCheatInfo(player) : new PlayerCheatInfo());
			if (player != null && player.IsMasterClient)
			{
				Label val9 = new Label("房主");
				((VisualElement)val9).style.backgroundColor = new StyleColor(new Color(0.8f, 0.6f, 0.2f));
				((VisualElement)val9).style.color = new StyleColor(Color.black);
				((VisualElement)val9).style.fontSize = new StyleLength(10f);
				((VisualElement)val9).style.paddingTop = new StyleLength(2f);
				((VisualElement)val9).style.paddingBottom = new StyleLength(2f);
				((VisualElement)val9).style.paddingLeft = new StyleLength(4f);
				((VisualElement)val9).style.paddingRight = new StyleLength(4f);
				((VisualElement)val9).style.borderTopLeftRadius = new StyleLength(2f);
				((VisualElement)val9).style.borderTopRightRadius = new StyleLength(2f);
				((VisualElement)val9).style.borderBottomLeftRadius = new StyleLength(2f);
				((VisualElement)val9).style.borderBottomRightRadius = new StyleLength(2f);
				((VisualElement)val9).style.marginRight = new StyleLength(4f);
				Label val10 = val9;
				val8.Add((VisualElement)(object)val10);
			}
			if (player != null && player.IsLocal)
			{
				Label val11 = new Label("你");
				((VisualElement)val11).style.backgroundColor = new StyleColor(new Color(0.2f, 0.8f, 0.2f));
				((VisualElement)val11).style.color = new StyleColor(Color.black);
				((VisualElement)val11).style.fontSize = new StyleLength(10f);
				((VisualElement)val11).style.paddingTop = new StyleLength(2f);
				((VisualElement)val11).style.paddingBottom = new StyleLength(2f);
				((VisualElement)val11).style.paddingLeft = new StyleLength(4f);
				((VisualElement)val11).style.paddingRight = new StyleLength(4f);
				((VisualElement)val11).style.borderTopLeftRadius = new StyleLength(2f);
				((VisualElement)val11).style.borderTopRightRadius = new StyleLength(2f);
				((VisualElement)val11).style.borderBottomLeftRadius = new StyleLength(2f);
				((VisualElement)val11).style.borderBottomRightRadius = new StyleLength(2f);
				((VisualElement)val11).style.marginRight = new StyleLength(4f);
				Label val12 = val11;
				val8.Add((VisualElement)(object)val12);
			}
			if (playerCheatInfo.IsCheater)
			{
				Label val13 = new Label("作弊者");
				((VisualElement)val13).style.backgroundColor = new StyleColor(new Color(1f, 0.1f, 0.1f));
				((VisualElement)val13).style.color = new StyleColor(Color.white);
				((VisualElement)val13).style.fontSize = new StyleLength(10f);
				((VisualElement)val13).style.paddingTop = new StyleLength(2f);
				((VisualElement)val13).style.paddingBottom = new StyleLength(2f);
				((VisualElement)val13).style.paddingLeft = new StyleLength(4f);
				((VisualElement)val13).style.paddingRight = new StyleLength(4f);
				((VisualElement)val13).style.borderTopLeftRadius = new StyleLength(2f);
				((VisualElement)val13).style.borderTopRightRadius = new StyleLength(2f);
				((VisualElement)val13).style.borderBottomLeftRadius = new StyleLength(2f);
				((VisualElement)val13).style.borderBottomRightRadius = new StyleLength(2f);
				((VisualElement)val13).style.marginRight = new StyleLength(4f);
				Label val14 = val13;
				val8.Add((VisualElement)(object)val14);
			}
			if (playerCheatInfo.IsAtlasOwner)
			{
				Label val15 = new Label("ATLAS 拥有者");
				((VisualElement)val15).style.backgroundColor = new StyleColor(new Color(0.8f, 0.1f, 0.1f));
				((VisualElement)val15).style.color = new StyleColor(Color.white);
				((VisualElement)val15).style.fontSize = new StyleLength(10f);
				((VisualElement)val15).style.paddingTop = new StyleLength(2f);
				((VisualElement)val15).style.paddingBottom = new StyleLength(2f);
				((VisualElement)val15).style.paddingLeft = new StyleLength(4f);
				((VisualElement)val15).style.paddingRight = new StyleLength(4f);
				((VisualElement)val15).style.borderTopLeftRadius = new StyleLength(2f);
				((VisualElement)val15).style.borderTopRightRadius = new StyleLength(2f);
				((VisualElement)val15).style.borderBottomLeftRadius = new StyleLength(2f);
				((VisualElement)val15).style.borderBottomRightRadius = new StyleLength(2f);
				((VisualElement)val15).style.marginRight = new StyleLength(4f);
				Label val16 = val15;
				val8.Add((VisualElement)(object)val16);
			}
			else if (playerCheatInfo.HasAtlas)
			{
				Label val17 = new Label("ATLAS");
				((VisualElement)val17).style.backgroundColor = new StyleColor(new Color(1f, 0.3f, 0.3f));
				((VisualElement)val17).style.color = new StyleColor(Color.white);
				((VisualElement)val17).style.fontSize = new StyleLength(10f);
				((VisualElement)val17).style.paddingTop = new StyleLength(2f);
				((VisualElement)val17).style.paddingBottom = new StyleLength(2f);
				((VisualElement)val17).style.paddingLeft = new StyleLength(4f);
				((VisualElement)val17).style.paddingRight = new StyleLength(4f);
				((VisualElement)val17).style.borderTopLeftRadius = new StyleLength(2f);
				((VisualElement)val17).style.borderTopRightRadius = new StyleLength(2f);
				((VisualElement)val17).style.borderBottomLeftRadius = new StyleLength(2f);
				((VisualElement)val17).style.borderBottomRightRadius = new StyleLength(2f);
				((VisualElement)val17).style.marginRight = new StyleLength(4f);
				Label val18 = val17;
				val8.Add((VisualElement)(object)val18);
			}
			if (playerCheatInfo.IsCherryOwner)
			{
				Label val19 = new Label("CHERRY 拥有者");
				((VisualElement)val19).style.backgroundColor = new StyleColor(new Color(0.8f, 0.1f, 0.1f));
				((VisualElement)val19).style.color = new StyleColor(Color.white);
				((VisualElement)val19).style.fontSize = new StyleLength(10f);
				((VisualElement)val19).style.paddingTop = new StyleLength(2f);
				((VisualElement)val19).style.paddingBottom = new StyleLength(2f);
				((VisualElement)val19).style.paddingLeft = new StyleLength(4f);
				((VisualElement)val19).style.paddingRight = new StyleLength(4f);
				((VisualElement)val19).style.borderTopLeftRadius = new StyleLength(2f);
				((VisualElement)val19).style.borderTopRightRadius = new StyleLength(2f);
				((VisualElement)val19).style.borderBottomLeftRadius = new StyleLength(2f);
				((VisualElement)val19).style.borderBottomRightRadius = new StyleLength(2f);
				((VisualElement)val19).style.marginRight = new StyleLength(4f);
				Label val20 = val19;
				val8.Add((VisualElement)(object)val20);
			}
			else if (playerCheatInfo.HasCherry)
			{
				Label val21 = new Label("CHERRY");
				((VisualElement)val21).style.backgroundColor = new StyleColor(new Color(1f, 0.3f, 0.3f));
				((VisualElement)val21).style.color = new StyleColor(Color.white);
				((VisualElement)val21).style.fontSize = new StyleLength(10f);
				((VisualElement)val21).style.paddingTop = new StyleLength(2f);
				((VisualElement)val21).style.paddingBottom = new StyleLength(2f);
				((VisualElement)val21).style.paddingLeft = new StyleLength(4f);
				((VisualElement)val21).style.paddingRight = new StyleLength(4f);
				((VisualElement)val21).style.borderTopLeftRadius = new StyleLength(2f);
				((VisualElement)val21).style.borderTopRightRadius = new StyleLength(2f);
				((VisualElement)val21).style.borderBottomLeftRadius = new StyleLength(2f);
				((VisualElement)val21).style.borderBottomRightRadius = new StyleLength(2f);
				((VisualElement)val21).style.marginRight = new StyleLength(4f);
				Label val22 = val21;
				val8.Add((VisualElement)(object)val22);
			}
			if (playerCheatInfo.HasAdvancedConsole)
			{
				Label val23 = new Label("控制台");
				((VisualElement)val23).style.backgroundColor = new StyleColor(new Color(0.2f, 0.8f, 0.2f));
				((VisualElement)val23).style.color = new StyleColor(Color.white);
				((VisualElement)val23).style.fontSize = new StyleLength(10f);
				((VisualElement)val23).style.paddingTop = new StyleLength(2f);
				((VisualElement)val23).style.paddingBottom = new StyleLength(2f);
				((VisualElement)val23).style.paddingLeft = new StyleLength(4f);
				((VisualElement)val23).style.paddingRight = new StyleLength(4f);
				((VisualElement)val23).style.borderTopLeftRadius = new StyleLength(2f);
				((VisualElement)val23).style.borderTopRightRadius = new StyleLength(2f);
				((VisualElement)val23).style.borderBottomLeftRadius = new StyleLength(2f);
				((VisualElement)val23).style.borderBottomRightRadius = new StyleLength(2f);
				((VisualElement)val23).style.marginRight = new StyleLength(4f);
				Label val24 = val23;
				val8.Add((VisualElement)(object)val24);
			}
			if (playerCheatInfo.CheatFunctions.Count > 0)
			{
				Label val25 = new Label("作弊功能：" + string.Join(", ", playerCheatInfo.CheatFunctions));
				((VisualElement)val25).style.backgroundColor = new StyleColor(new Color(1f, 0.5f, 0f));
				((VisualElement)val25).style.color = new StyleColor(Color.white);
				((VisualElement)val25).style.fontSize = new StyleLength(9f);
				((VisualElement)val25).style.paddingTop = new StyleLength(1f);
				((VisualElement)val25).style.paddingBottom = new StyleLength(1f);
				((VisualElement)val25).style.paddingLeft = new StyleLength(3f);
				((VisualElement)val25).style.paddingRight = new StyleLength(3f);
				((VisualElement)val25).style.borderTopLeftRadius = new StyleLength(2f);
				((VisualElement)val25).style.borderTopRightRadius = new StyleLength(2f);
				((VisualElement)val25).style.borderBottomLeftRadius = new StyleLength(2f);
				((VisualElement)val25).style.borderBottomRightRadius = new StyleLength(2f);
				((VisualElement)val25).style.marginRight = new StyleLength(4f);
				Label val26 = val25;
				val8.Add((VisualElement)(object)val26);
			}
			val4.Add((VisualElement)(object)val6);
			val4.Add(val8);
			val2.Add(val4);
			VisualElement val27 = new VisualElement();
			val27.style.marginTop = new StyleLength(4f);
			VisualElement val28 = val27;
			PhotonPlayer obj3 = player;
			VisualElement val29 = CreateInfoRow("玩家编号", ((obj3 != null) ? obj3.ActorNumber.ToString() : null) ?? "未知", small: true);
			val28.Add(val29);
			string text = ((player != null) ? GetPlayerSteamId(player) : null);
			if (!string.IsNullOrEmpty(text))
			{
				VisualElement val30 = CreateSteamIdRow(text);
				val28.Add(val30);
			}
			Character val31 = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)player) ?? false);
			if ((UnityEngine.Object)(object)val31 != (UnityEngine.Object)null)
			{
				VisualElement val32 = CreateInfoRow("状态", val31.data.dead ? "死亡" : "存活", small: true);
				val28.Add(val32);
				VisualElement val33 = CreateInfoRow("位置", $"({val31.Center.x:F1}, {val31.Center.y:F1}, {val31.Center.z:F1})", small: true);
				val28.Add(val33);
				VisualElement val34 = CreateStaminaDisplay(val31);
				val28.Add(val34);
				if (player != null)
				{
					VisualElement val35 = CreateInventoryDisplay(player, val31);
					val28.Add(val35);
				}
			}
			else
			{
				VisualElement val36 = CreateInfoRow("角色", "未加载", small: true);
				val28.Add(val36);
			}
			if (playerCheatInfo != null)
			{
				if (!string.IsNullOrEmpty(playerCheatInfo.ModLoader))
				{
					VisualElement val37 = CreateInfoRow("模组加载器", playerCheatInfo.ModLoader, small: true);
					val28.Add(val37);
				}
				if (playerCheatInfo.ModList.Count > 0)
				{
					VisualElement val38 = CreateInfoRow("模组", $"{playerCheatInfo.ModList.Count} 个已安装", small: true);
					val28.Add(val38);
					VisualElement val39 = CreateModListContainer(playerCheatInfo.ModList);
					val28.Add(val39);
				}
				if (!string.IsNullOrEmpty(playerCheatInfo.ModHash))
				{
					VisualElement val40 = CreateInfoRow("模组哈希", playerCheatInfo.ModHash.Substring(0, Math.Min(12, playerCheatInfo.ModHash.Length)) + "...", small: true);
					val28.Add(val40);
				}
				if (playerCheatInfo.CheatFunctions.Count > 0)
				{
					VisualElement val41 = CreateInfoRow("作弊功能", string.Join(", ", playerCheatInfo.CheatFunctions), small: true);
					val28.Add(val41);
				}
			}
			if ((PhotonNetwork.IsMasterClient || MasterClientUtils.IsBypassEnabled) && player != null && !player.IsLocal)
			{
				VisualElement val42 = new VisualElement();
				val42.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)2);
				val42.style.marginTop = new StyleLength(8f);
				val42.style.justifyContent = new StyleEnum<Justify>((Justify)3);
				VisualElement val43 = val42;
				VisualElement val44 = new VisualElement();
				val44.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)2);
				val44.style.flexWrap = new StyleEnum<Wrap>((Wrap)1);
				val44.style.marginTop = new StyleLength(4f);
				VisualElement val45 = val44;
				Button val46 = new Button((Action)delegate
				{
					SoftLockPlayer(player, GetKickReason(player));
				})
				{
					text = "软锁定"
				};
				((VisualElement)val46).style.backgroundColor = new StyleColor(new Color(0.8f, 0.2f, 0.2f));
				((VisualElement)val46).style.color = new StyleColor(Color.white);
				((VisualElement)val46).style.fontSize = new StyleLength(10f);
				((VisualElement)val46).style.paddingTop = new StyleLength(3f);
				((VisualElement)val46).style.paddingBottom = new StyleLength(3f);
				((VisualElement)val46).style.paddingLeft = new StyleLength(6f);
				((VisualElement)val46).style.paddingRight = new StyleLength(6f);
				((VisualElement)val46).style.borderTopLeftRadius = new StyleLength(3f);
				((VisualElement)val46).style.borderTopRightRadius = new StyleLength(3f);
				((VisualElement)val46).style.borderBottomLeftRadius = new StyleLength(3f);
				((VisualElement)val46).style.borderBottomRightRadius = new StyleLength(3f);
				((VisualElement)val46).style.marginRight = new StyleLength(2f);
				((VisualElement)val46).style.marginBottom = new StyleLength(2f);
				((VisualElement)val46).style.minWidth = new StyleLength(60f);
				Button val47 = val46;
				Button val48 = new Button((Action)delegate
				{
					ForceKickPlayer(player);
				})
				{
					text = "强制踢出"
				};
				((VisualElement)val48).style.backgroundColor = new StyleColor(new Color(0.6f, 0.1f, 0.1f));
				((VisualElement)val48).style.color = new StyleColor(Color.white);
				((VisualElement)val48).style.fontSize = new StyleLength(10f);
				((VisualElement)val48).style.paddingTop = new StyleLength(3f);
				((VisualElement)val48).style.paddingBottom = new StyleLength(3f);
				((VisualElement)val48).style.paddingLeft = new StyleLength(6f);
				((VisualElement)val48).style.paddingRight = new StyleLength(6f);
				((VisualElement)val48).style.borderTopLeftRadius = new StyleLength(3f);
				((VisualElement)val48).style.borderTopRightRadius = new StyleLength(3f);
				((VisualElement)val48).style.borderBottomLeftRadius = new StyleLength(3f);
				((VisualElement)val48).style.borderBottomRightRadius = new StyleLength(3f);
				((VisualElement)val48).style.marginRight = new StyleLength(2f);
				((VisualElement)val48).style.marginBottom = new StyleLength(2f);
				((VisualElement)val48).style.minWidth = new StyleLength(50f);
				Button val49 = val48;
				Button val50 = new Button((Action)delegate
				{
					BlackScreenKick(player);
				})
				{
					text = "黑屏"
				};
				((VisualElement)val50).style.backgroundColor = new StyleColor(new Color(0.1f, 0.1f, 0.1f));
				((VisualElement)val50).style.color = new StyleColor(Color.white);
				((VisualElement)val50).style.fontSize = new StyleLength(10f);
				((VisualElement)val50).style.paddingTop = new StyleLength(3f);
				((VisualElement)val50).style.paddingBottom = new StyleLength(3f);
				((VisualElement)val50).style.paddingLeft = new StyleLength(6f);
				((VisualElement)val50).style.paddingRight = new StyleLength(6f);
				((VisualElement)val50).style.borderTopLeftRadius = new StyleLength(3f);
				((VisualElement)val50).style.borderTopRightRadius = new StyleLength(3f);
				((VisualElement)val50).style.borderBottomLeftRadius = new StyleLength(3f);
				((VisualElement)val50).style.borderBottomRightRadius = new StyleLength(3f);
				((VisualElement)val50).style.marginRight = new StyleLength(2f);
				((VisualElement)val50).style.marginBottom = new StyleLength(2f);
				((VisualElement)val50).style.minWidth = new StyleLength(50f);
				Button val51 = val50;
				Button val52 = new Button((Action)delegate
				{
					SlowPlayer(player);
				})
				{
					text = "减速"
				};
				((VisualElement)val52).style.backgroundColor = new StyleColor(new Color(0.2f, 0.4f, 0.8f));
				((VisualElement)val52).style.color = new StyleColor(Color.white);
				((VisualElement)val52).style.fontSize = new StyleLength(10f);
				((VisualElement)val52).style.paddingTop = new StyleLength(3f);
				((VisualElement)val52).style.paddingBottom = new StyleLength(3f);
				((VisualElement)val52).style.paddingLeft = new StyleLength(6f);
				((VisualElement)val52).style.paddingRight = new StyleLength(6f);
				((VisualElement)val52).style.borderTopLeftRadius = new StyleLength(3f);
				((VisualElement)val52).style.borderTopRightRadius = new StyleLength(3f);
				((VisualElement)val52).style.borderBottomLeftRadius = new StyleLength(3f);
				((VisualElement)val52).style.borderBottomRightRadius = new StyleLength(3f);
				((VisualElement)val52).style.marginRight = new StyleLength(2f);
				((VisualElement)val52).style.marginBottom = new StyleLength(2f);
				((VisualElement)val52).style.minWidth = new StyleLength(50f);
				Button val53 = val52;
				Button val54 = new Button((Action)delegate
				{
					SpeedPlayer(player);
				})
				{
					text = "加速"
				};
				((VisualElement)val54).style.backgroundColor = new StyleColor(new Color(0.2f, 0.8f, 0.2f));
				((VisualElement)val54).style.color = new StyleColor(Color.white);
				((VisualElement)val54).style.fontSize = new StyleLength(10f);
				((VisualElement)val54).style.paddingTop = new StyleLength(3f);
				((VisualElement)val54).style.paddingBottom = new StyleLength(3f);
				((VisualElement)val54).style.paddingLeft = new StyleLength(6f);
				((VisualElement)val54).style.paddingRight = new StyleLength(6f);
				((VisualElement)val54).style.borderTopLeftRadius = new StyleLength(3f);
				((VisualElement)val54).style.borderTopRightRadius = new StyleLength(3f);
				((VisualElement)val54).style.borderBottomLeftRadius = new StyleLength(3f);
				((VisualElement)val54).style.borderBottomRightRadius = new StyleLength(3f);
				((VisualElement)val54).style.marginRight = new StyleLength(2f);
				((VisualElement)val54).style.marginBottom = new StyleLength(2f);
				((VisualElement)val54).style.minWidth = new StyleLength(50f);
				Button val55 = val54;
				Button val56 = new Button((Action)delegate
				{
					DropCoconutOnPlayer(player);
				})
				{
					text = "椰子"
				};
				((VisualElement)val56).style.backgroundColor = new StyleColor(new Color(0.6f, 0.4f, 0.2f));
				((VisualElement)val56).style.color = new StyleColor(Color.white);
				((VisualElement)val56).style.fontSize = new StyleLength(10f);
				((VisualElement)val56).style.paddingTop = new StyleLength(3f);
				((VisualElement)val56).style.paddingBottom = new StyleLength(3f);
				((VisualElement)val56).style.paddingLeft = new StyleLength(6f);
				((VisualElement)val56).style.paddingRight = new StyleLength(6f);
				((VisualElement)val56).style.borderTopLeftRadius = new StyleLength(3f);
				((VisualElement)val56).style.borderTopRightRadius = new StyleLength(3f);
				((VisualElement)val56).style.borderBottomLeftRadius = new StyleLength(3f);
				((VisualElement)val56).style.borderBottomRightRadius = new StyleLength(3f);
				((VisualElement)val56).style.marginRight = new StyleLength(2f);
				((VisualElement)val56).style.marginBottom = new StyleLength(2f);
				((VisualElement)val56).style.minWidth = new StyleLength(50f);
				Button val57 = val56;
				Button val58 = new Button((Action)delegate
				{
					SpawnBeesOnPlayer(player);
				})
				{
					text = "蜜蜂"
				};
				((VisualElement)val58).style.backgroundColor = new StyleColor(new Color(0.8f, 0.8f, 0.2f));
				((VisualElement)val58).style.color = new StyleColor(Color.black);
				((VisualElement)val58).style.fontSize = new StyleLength(10f);
				((VisualElement)val58).style.paddingTop = new StyleLength(3f);
				((VisualElement)val58).style.paddingBottom = new StyleLength(3f);
				((VisualElement)val58).style.paddingLeft = new StyleLength(6f);
				((VisualElement)val58).style.paddingRight = new StyleLength(6f);
				((VisualElement)val58).style.borderTopLeftRadius = new StyleLength(3f);
				((VisualElement)val58).style.borderTopRightRadius = new StyleLength(3f);
				((VisualElement)val58).style.borderBottomLeftRadius = new StyleLength(3f);
				((VisualElement)val58).style.borderBottomRightRadius = new StyleLength(3f);
				((VisualElement)val58).style.marginRight = new StyleLength(2f);
				((VisualElement)val58).style.marginBottom = new StyleLength(2f);
				((VisualElement)val58).style.minWidth = new StyleLength(50f);
				Button val59 = val58;
				Button val60 = new Button((Action)delegate
				{
					GiveBackpackToPlayer(player);
				})
				{
					text = "给背包"
				};
				((VisualElement)val60).style.backgroundColor = new StyleColor(new Color(0.4f, 0.6f, 0.4f));
				((VisualElement)val60).style.color = new StyleColor(Color.white);
				((VisualElement)val60).style.fontSize = new StyleLength(10f);
				((VisualElement)val60).style.paddingTop = new StyleLength(3f);
				((VisualElement)val60).style.paddingBottom = new StyleLength(3f);
				((VisualElement)val60).style.paddingLeft = new StyleLength(6f);
				((VisualElement)val60).style.paddingRight = new StyleLength(6f);
				((VisualElement)val60).style.borderTopLeftRadius = new StyleLength(3f);
				((VisualElement)val60).style.borderTopRightRadius = new StyleLength(3f);
				((VisualElement)val60).style.borderBottomLeftRadius = new StyleLength(3f);
				((VisualElement)val60).style.borderBottomRightRadius = new StyleLength(3f);
				((VisualElement)val60).style.marginRight = new StyleLength(2f);
				((VisualElement)val60).style.marginBottom = new StyleLength(2f);
				((VisualElement)val60).style.minWidth = new StyleLength(50f);
				Button val61 = val60;
				Button val62 = new Button((Action)delegate
				{
					ClearPlayerBackpack(player);
				})
				{
					text = "清空背包"
				};
				((VisualElement)val62).style.backgroundColor = new StyleColor(new Color(0.6f, 0.4f, 0.4f));
				((VisualElement)val62).style.color = new StyleColor(Color.white);
				((VisualElement)val62).style.fontSize = new StyleLength(10f);
				((VisualElement)val62).style.paddingTop = new StyleLength(3f);
				((VisualElement)val62).style.paddingBottom = new StyleLength(3f);
				((VisualElement)val62).style.paddingLeft = new StyleLength(6f);
				((VisualElement)val62).style.paddingRight = new StyleLength(6f);
				((VisualElement)val62).style.borderTopLeftRadius = new StyleLength(3f);
				((VisualElement)val62).style.borderTopRightRadius = new StyleLength(3f);
				((VisualElement)val62).style.borderBottomLeftRadius = new StyleLength(3f);
				((VisualElement)val62).style.borderBottomRightRadius = new StyleLength(3f);
				((VisualElement)val62).style.marginRight = new StyleLength(2f);
				((VisualElement)val62).style.marginBottom = new StyleLength(2f);
				((VisualElement)val62).style.minWidth = new StyleLength(50f);
				Button val63 = val62;
				Button val64 = new Button((Action)delegate
				{
					KillPlayerFromUI(player);
				})
				{
					text = "击杀"
				};
				((VisualElement)val64).style.backgroundColor = new StyleColor(new Color(0.8f, 0.1f, 0.1f));
				((VisualElement)val64).style.color = new StyleColor(Color.white);
				((VisualElement)val64).style.fontSize = new StyleLength(10f);
				((VisualElement)val64).style.paddingTop = new StyleLength(3f);
				((VisualElement)val64).style.paddingBottom = new StyleLength(3f);
				((VisualElement)val64).style.paddingLeft = new StyleLength(6f);
				((VisualElement)val64).style.paddingRight = new StyleLength(6f);
				((VisualElement)val64).style.borderTopLeftRadius = new StyleLength(3f);
				((VisualElement)val64).style.borderTopRightRadius = new StyleLength(3f);
				((VisualElement)val64).style.borderBottomLeftRadius = new StyleLength(3f);
				((VisualElement)val64).style.borderBottomRightRadius = new StyleLength(3f);
				((VisualElement)val64).style.marginRight = new StyleLength(2f);
				((VisualElement)val64).style.marginBottom = new StyleLength(2f);
				((VisualElement)val64).style.minWidth = new StyleLength(50f);
				Button val65 = val64;
				Button val66 = new Button((Action)delegate
				{
					RevivePlayerFromUI(player);
				})
				{
					text = "复活"
				};
				((VisualElement)val66).style.backgroundColor = new StyleColor(new Color(0.1f, 0.8f, 0.1f));
				((VisualElement)val66).style.color = new StyleColor(Color.white);
				((VisualElement)val66).style.fontSize = new StyleLength(10f);
				((VisualElement)val66).style.paddingTop = new StyleLength(3f);
				((VisualElement)val66).style.paddingBottom = new StyleLength(3f);
				((VisualElement)val66).style.paddingLeft = new StyleLength(6f);
				((VisualElement)val66).style.paddingRight = new StyleLength(6f);
				((VisualElement)val66).style.borderTopLeftRadius = new StyleLength(3f);
				((VisualElement)val66).style.borderTopRightRadius = new StyleLength(3f);
				((VisualElement)val66).style.borderBottomLeftRadius = new StyleLength(3f);
				((VisualElement)val66).style.borderBottomRightRadius = new StyleLength(3f);
				((VisualElement)val66).style.marginRight = new StyleLength(2f);
				((VisualElement)val66).style.marginBottom = new StyleLength(2f);
				((VisualElement)val66).style.minWidth = new StyleLength(50f);
				Button val67 = val66;
				Button val68 = new Button((Action)delegate
				{
					HealPlayerFromUI(player);
				})
				{
					text = "治疗"
				};
				((VisualElement)val68).style.backgroundColor = new StyleColor(new Color(0.1f, 0.6f, 0.8f));
				((VisualElement)val68).style.color = new StyleColor(Color.white);
				((VisualElement)val68).style.fontSize = new StyleLength(10f);
				((VisualElement)val68).style.paddingTop = new StyleLength(3f);
				((VisualElement)val68).style.paddingBottom = new StyleLength(3f);
				((VisualElement)val68).style.paddingLeft = new StyleLength(6f);
				((VisualElement)val68).style.paddingRight = new StyleLength(6f);
				((VisualElement)val68).style.borderTopLeftRadius = new StyleLength(3f);
				((VisualElement)val68).style.borderTopRightRadius = new StyleLength(3f);
				((VisualElement)val68).style.borderBottomLeftRadius = new StyleLength(3f);
				((VisualElement)val68).style.borderBottomRightRadius = new StyleLength(3f);
				((VisualElement)val68).style.marginRight = new StyleLength(2f);
				((VisualElement)val68).style.marginBottom = new StyleLength(2f);
				((VisualElement)val68).style.minWidth = new StyleLength(50f);
				Button val69 = val68;
				Button val70 = new Button((Action)delegate
				{
					PassOutPlayerFromUI(player);
				})
				{
					text = "晕倒"
				};
				((VisualElement)val70).style.backgroundColor = new StyleColor(new Color(0.5f, 0.2f, 0.8f));
				((VisualElement)val70).style.color = new StyleColor(Color.white);
				((VisualElement)val70).style.fontSize = new StyleLength(10f);
				((VisualElement)val70).style.paddingTop = new StyleLength(3f);
				((VisualElement)val70).style.paddingBottom = new StyleLength(3f);
				((VisualElement)val70).style.paddingLeft = new StyleLength(6f);
				((VisualElement)val70).style.paddingRight = new StyleLength(6f);
				((VisualElement)val70).style.borderTopLeftRadius = new StyleLength(3f);
				((VisualElement)val70).style.borderTopRightRadius = new StyleLength(3f);
				((VisualElement)val70).style.borderBottomLeftRadius = new StyleLength(3f);
				((VisualElement)val70).style.borderBottomRightRadius = new StyleLength(3f);
				((VisualElement)val70).style.marginRight = new StyleLength(2f);
				((VisualElement)val70).style.marginBottom = new StyleLength(2f);
				((VisualElement)val70).style.minWidth = new StyleLength(50f);
				Button val71 = val70;
				val45.Add((VisualElement)(object)val47);
				val45.Add((VisualElement)(object)val49);
				val45.Add((VisualElement)(object)val51);
				val45.Add((VisualElement)(object)val53);
				val45.Add((VisualElement)(object)val55);
				val45.Add((VisualElement)(object)val57);
				val45.Add((VisualElement)(object)val59);
				val45.Add((VisualElement)(object)val61);
				val45.Add((VisualElement)(object)val63);
				val45.Add((VisualElement)(object)val65);
				val45.Add((VisualElement)(object)val67);
				val45.Add((VisualElement)(object)val69);
				val45.Add((VisualElement)(object)val71);
				val43.Add(val45);
				val28.Add(val43);
			}
			val2.Add(val28);
			return val2;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("创建玩家卡片详情出错：" + ex.Message));
			PhotonPlayer obj4 = player;
			Label val72 = new Label("加载玩家出错：" + (((obj4 != null) ? obj4.NickName : null) ?? "未知"));
			((VisualElement)val72).style.color = new StyleColor(new Color(1f, 0.5f, 0.5f));
			((VisualElement)val72).style.fontSize = new StyleLength(12f);
			Label val73 = val72;
			val2.Add((VisualElement)(object)val73);
			return val2;
		}
	}

	private VisualElement CreateSteamIdRow(string steamId)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Expected O, but got Unknown
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Expected O, but got Unknown
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Expected O, but got Unknown
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Expected O, but got Unknown
		VisualElement val = new VisualElement();
		val.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)2);
		val.style.justifyContent = new StyleEnum<Justify>((Justify)3);
		val.style.alignItems = new StyleEnum<Align>((Align)2);
		val.style.marginBottom = new StyleLength(2f);
		VisualElement val2 = val;
		VisualElement val3 = new VisualElement();
		val3.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)2);
		val3.style.alignItems = new StyleEnum<Align>((Align)2);
		VisualElement val4 = val3;
		string text = (steamId.StartsWith("UUID:") ? "用户 ID：" : "Steam ID：");
		bool flag = IsValidSteamId64(steamId);
		Label val5 = new Label(text);
		((VisualElement)val5).style.fontSize = new StyleLength(11f);
		((VisualElement)val5).style.color = new StyleColor(new Color(0.8f, 0.8f, 0.8f));
		((VisualElement)val5).style.minWidth = new StyleLength(80f);
		((VisualElement)val5).style.maxWidth = new StyleLength(80f);
		Label val6 = val5;
		Label val7 = new Label(steamId);
		((VisualElement)val7).style.fontSize = new StyleLength(11f);
		((VisualElement)val7).style.color = new StyleColor(flag ? new Color(0.95f, 0.95f, 0.95f) : new Color(0.8f, 0.8f, 0.6f));
		((VisualElement)val7).style.marginLeft = new StyleLength(8f);
		Label val8 = val7;
		val4.Add((VisualElement)(object)val6);
		val4.Add((VisualElement)(object)val8);
		val2.Add(val4);
		if (flag)
		{
			Button val9 = new Button((Action)delegate
			{
				OpenSteamProfile(steamId);
			})
			{
				text = "个人资料"
			};
			((VisualElement)val9).style.fontSize = new StyleLength(10f);
			((VisualElement)val9).style.backgroundColor = new StyleColor(new Color(0.1f, 0.4f, 0.7f));
			((VisualElement)val9).style.color = new StyleColor(Color.white);
			((VisualElement)val9).style.paddingTop = new StyleLength(2f);
			((VisualElement)val9).style.paddingBottom = new StyleLength(2f);
			((VisualElement)val9).style.paddingLeft = new StyleLength(6f);
			((VisualElement)val9).style.paddingRight = new StyleLength(6f);
			((VisualElement)val9).style.borderTopLeftRadius = new StyleLength(2f);
			((VisualElement)val9).style.borderTopRightRadius = new StyleLength(2f);
			((VisualElement)val9).style.borderBottomLeftRadius = new StyleLength(2f);
			((VisualElement)val9).style.borderBottomRightRadius = new StyleLength(2f);
			Button val10 = val9;
			val2.Add((VisualElement)(object)val10);
		}
		else
		{
			Label val11 = new Label("(非 Steam ID)");
			((VisualElement)val11).style.fontSize = new StyleLength(9f);
			((VisualElement)val11).style.color = new StyleColor(new Color(0.6f, 0.6f, 0.6f));
			((VisualElement)val11).style.unityFontStyleAndWeight = new StyleEnum<FontStyle>((FontStyle)2);
			Label val12 = val11;
			val2.Add((VisualElement)(object)val12);
		}
		return val2;
	}

	private VisualElement CreateInfoRow(string key, string value, bool small = false)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Expected O, but got Unknown
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Expected O, but got Unknown
		VisualElement val = new VisualElement();
		val.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)2);
		val.style.marginBottom = new StyleLength((float)(small ? 2 : 4));
		VisualElement val2 = val;
		Label val3 = new Label(key + ":");
		((VisualElement)val3).style.fontSize = new StyleLength((float)(small ? 11 : 12));
		((VisualElement)val3).style.color = new StyleColor(new Color(0.8f, 0.8f, 0.8f));
		((VisualElement)val3).style.minWidth = new StyleLength((float)(small ? 80 : 120));
		((VisualElement)val3).style.maxWidth = new StyleLength((float)(small ? 80 : 120));
		Label val4 = val3;
		Label val5 = new Label(value);
		((VisualElement)val5).style.fontSize = new StyleLength((float)(small ? 11 : 12));
		((VisualElement)val5).style.color = new StyleColor(new Color(0.95f, 0.95f, 0.95f));
		((VisualElement)val5).style.marginLeft = new StyleLength(8f);
		((VisualElement)val5).style.flexGrow = new StyleFloat(1f);
		Label val6 = val5;
		val2.Add((VisualElement)(object)val4);
		val2.Add((VisualElement)(object)val6);
		return val2;
	}

	private string? GetPlayerSteamId(PhotonPlayer player)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (player.IsLocal)
			{
				try
				{
					if (SteamManager.Initialized)
					{
						CSteamID steamID = SteamUser.GetSteamID();
						if (steamID.IsValid())
						{
							return steamID.m_SteamID.ToString();
						}
					}
				}
				catch (Exception ex)
				{
					Debug.LogWarning((object)("通过 Steamworks 获取本地 Steam ID 失败：" + ex.Message));
				}
			}
			string playerSteamIdFromLobby = GetPlayerSteamIdFromLobby(player);
			if (!string.IsNullOrEmpty(playerSteamIdFromLobby) && IsValidSteamId64(playerSteamIdFromLobby))
			{
				return playerSteamIdFromLobby;
			}
			if (player.CustomProperties != null)
			{
				string[] array = new string[6] { "steamid", "SteamID", "steam_id", "STEAMID", "steam64", "SteamID64" };
				string[] array2 = array;
				foreach (string key in array2)
				{
					if (((Dictionary<object, object>)(object)player.CustomProperties).TryGetValue((object)key, out object value))
					{
						string text = value.ToString();
						if (IsValidSteamId64(text))
						{
							return text;
						}
					}
				}
			}
			if (!string.IsNullOrEmpty(player.UserId))
			{
				string userId = player.UserId;
				if (IsValidSteamId64(userId))
				{
					return userId;
				}
				if (userId.StartsWith("Steam:", StringComparison.OrdinalIgnoreCase))
				{
					string text2 = userId.Substring(6);
					if (IsValidSteamId64(text2))
					{
						return text2;
					}
				}
			}
			if (!string.IsNullOrEmpty(player.UserId))
			{
				return "UUID: " + player.UserId.Substring(0, Math.Min(16, player.UserId.Length)) + "...";
			}
			return "未知";
		}
		catch (Exception ex2)
		{
			Debug.LogError((object)("获取玩家 Steam ID 出错 " + ((player != null) ? player.NickName : null) + "：" + ex2.Message));
			return "错误";
		}
	}

	private string? GetPlayerSteamIdFromLobby(PhotonPlayer player)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			SteamLobbyHandler service = GameHandler.GetService<SteamLobbyHandler>();
			if (service == null)
			{
				Debug.LogWarning((object)"[GetPlayerSteamIdFromLobby] SteamLobbyHandler not found.");
				return null;
			}
			Type type = ((object)service).GetType();
			FieldInfo field = type.GetField("m_currentLobby", BindingFlags.Instance | BindingFlags.NonPublic);
			if (field == null)
			{
				Debug.LogError((object)"[GetPlayerSteamIdFromLobby] Could not find m_currentLobby field via reflection.");
				return null;
			}
			CSteamID val = (CSteamID)field.GetValue(service);
			if (val == CSteamID.Nil)
			{
				Debug.LogWarning((object)"[GetPlayerSteamIdFromLobby] Current Steam lobby is NIL.");
				return null;
			}
			int numLobbyMembers = SteamMatchmaking.GetNumLobbyMembers(val);
			Debug.Log((object)$"[GetPlayerSteamIdFromLobby] Scanning {numLobbyMembers} Steam lobby members for match with Photon name '{player.NickName}'.");
			Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
			for (int i = 0; i < numLobbyMembers; i++)
			{
				CSteamID lobbyMemberByIndex = SteamMatchmaking.GetLobbyMemberByIndex(val, i);
				string friendPersonaName = SteamFriends.GetFriendPersonaName(lobbyMemberByIndex);
				string text = lobbyMemberByIndex.m_SteamID.ToString();
				Debug.Log((object)("[GetPlayerSteamIdFromLobby] Found Steam user: Name='" + friendPersonaName + "', SteamID=" + text));
				if (!dictionary.ContainsKey(friendPersonaName))
				{
					dictionary[friendPersonaName] = new List<string>();
				}
				dictionary[friendPersonaName].Add(text);
			}
			if (!dictionary.ContainsKey(player.NickName))
			{
				Debug.LogWarning((object)("[GetPlayerSteamIdFromLobby] No Steam player found with the name '" + player.NickName + "'. Returning null."));
				return null;
			}
			List<string> list = dictionary[player.NickName];
			Debug.Log((object)string.Format("[GetPlayerSteamIdFromLobby] Found {0} Steam players with name '{1}': {2}", list.Count, player.NickName, string.Join(", ", list)));
			List<PhotonPlayer> list2 = new List<PhotonPlayer>();
			PhotonPlayer[] playerList = PhotonNetwork.PlayerList;
			foreach (PhotonPlayer val2 in playerList)
			{
				if (val2.NickName == player.NickName)
				{
					list2.Add(val2);
				}
			}
			Debug.Log((object)$"[GetPlayerSteamIdFromLobby] Found {list2.Count} Photon players with name '{player.NickName}'.");
			if (list2.Count > list.Count)
			{
				Debug.LogWarning((object)$"[GetPlayerSteamIdFromLobby] More Photon players ({list2.Count}) than Steam players ({list.Count}) for name '{player.NickName}'. Possible name spoofing.");
				return null;
			}
			list2.Sort((PhotonPlayer a, PhotonPlayer b) => a.ActorNumber.CompareTo(b.ActorNumber));
			int num = list2.FindIndex((PhotonPlayer p) => p.ActorNumber == player.ActorNumber);
			if (num >= 0 && num < list.Count)
			{
				Debug.Log((object)$"[GetPlayerSteamIdFromLobby] Matched ActorNumber {player.ActorNumber} to SteamID {list[num]} (index {num}).");
				return list[num];
			}
			Debug.LogWarning((object)("[GetPlayerSteamIdFromLobby] Could not find matching ActorNumber for " + player.NickName + ". Returning first available SteamID: " + list[0]));
			return list[0];
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("[GetPlayerSteamIdFromLobby] Exception while resolving Steam ID for " + player.NickName + ": " + ex.Message));
			return null;
		}
	}

	private bool IsValidSteamId64(string steamId)
	{
		if (string.IsNullOrEmpty(steamId))
		{
			return false;
		}
		if (steamId.Length == 17 && steamId.StartsWith("7656119") && long.TryParse(steamId, out var _))
		{
			return true;
		}
		return false;
	}

	private void OpenSteamProfile(string steamId)
	{
		try
		{
			if (steamId.StartsWith("UUID:"))
			{
				Debug.LogWarning((object)("Cannot open Steam profile for UUID: " + steamId));
				Plugin.Log.LogWarning((object)("Cannot open Steam profile - this is a UUID, not a Steam ID: " + steamId));
				return;
			}
			if (!IsValidSteamId64(steamId))
			{
				Debug.LogWarning((object)("Invalid Steam ID: " + steamId));
				Plugin.Log.LogWarning((object)("Cannot open Steam profile - invalid Steam ID: " + steamId));
				return;
			}
			string text = "https://steamcommunity.com/profiles/" + steamId;
			Application.OpenURL(text);
			Debug.Log((object)("Opened Steam profile in browser: " + text));
			Plugin.Log.LogInfo((object)("Opened Steam profile in browser for ID: " + steamId));
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("Failed to open Steam profile: " + ex.Message));
			Plugin.Log.LogError((object)("Failed to open Steam profile for ID " + steamId + ": " + ex.Message));
		}
	}

	private VisualElement CreateModListContainer(List<string> mods)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Expected O, but got Unknown
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Expected O, but got Unknown
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Expected O, but got Unknown
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Expected O, but got Unknown
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Expected O, but got Unknown
		VisualElement val = new VisualElement();
		val.style.marginTop = new StyleLength(4f);
		val.style.marginBottom = new StyleLength(4f);
		VisualElement val2 = val;
		Button val3 = new Button
		{
			text = $"▶ 显示模组 ({mods.Count})"
		};
		((VisualElement)val3).style.fontSize = new StyleLength(10f);
		((VisualElement)val3).style.backgroundColor = new StyleColor(new Color(0.3f, 0.3f, 0.3f));
		((VisualElement)val3).style.color = new StyleColor(new Color(0.9f, 0.9f, 0.9f));
		((VisualElement)val3).style.paddingTop = new StyleLength(2f);
		((VisualElement)val3).style.paddingBottom = new StyleLength(2f);
		((VisualElement)val3).style.paddingLeft = new StyleLength(6f);
		((VisualElement)val3).style.paddingRight = new StyleLength(6f);
		((VisualElement)val3).style.borderTopLeftRadius = new StyleLength(2f);
		((VisualElement)val3).style.borderTopRightRadius = new StyleLength(2f);
		((VisualElement)val3).style.borderBottomLeftRadius = new StyleLength(2f);
		((VisualElement)val3).style.borderBottomRightRadius = new StyleLength(2f);
		((VisualElement)val3).style.marginBottom = new StyleLength(2f);
		Button toggleButton = val3;
		VisualElement val4 = new VisualElement();
		val4.style.display = new StyleEnum<DisplayStyle>((DisplayStyle)1);
		val4.style.backgroundColor = new StyleColor(new Color(0.1f, 0.1f, 0.1f, 0.9f));
		val4.style.paddingTop = new StyleLength(4f);
		val4.style.paddingBottom = new StyleLength(4f);
		val4.style.paddingLeft = new StyleLength(6f);
		val4.style.paddingRight = new StyleLength(6f);
		val4.style.borderTopLeftRadius = new StyleLength(2f);
		val4.style.borderTopRightRadius = new StyleLength(2f);
		val4.style.borderBottomLeftRadius = new StyleLength(2f);
		val4.style.borderBottomRightRadius = new StyleLength(2f);
		val4.style.maxHeight = new StyleLength(150f);
		VisualElement modListElement = val4;
		ScrollView val5 = new ScrollView();
		((VisualElement)val5).style.maxHeight = new StyleLength(140f);
		ScrollView val6 = val5;
		foreach (string item in mods.Take(20))
		{
			Label val7 = new Label(item);
			((VisualElement)val7).style.fontSize = new StyleLength(9f);
			((VisualElement)val7).style.color = new StyleColor(new Color(0.85f, 0.85f, 0.85f));
			((VisualElement)val7).style.marginBottom = new StyleLength(1f);
			((VisualElement)val7).style.whiteSpace = new StyleEnum<WhiteSpace>((WhiteSpace)0);
			Label val8 = val7;
			((VisualElement)val6).Add((VisualElement)(object)val8);
		}
		if (mods.Count > 20)
		{
			Label val9 = new Label($"... 还有 {mods.Count - 20} 个模组");
			((VisualElement)val9).style.fontSize = new StyleLength(9f);
			((VisualElement)val9).style.color = new StyleColor(new Color(0.6f, 0.6f, 0.6f));
			((VisualElement)val9).style.unityFontStyleAndWeight = new StyleEnum<FontStyle>((FontStyle)2);
			Label val10 = val9;
			((VisualElement)val6).Add((VisualElement)(object)val10);
		}
		modListElement.Add((VisualElement)(object)val6);
		bool isExpanded = false;
		toggleButton.clicked += delegate
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			isExpanded = !isExpanded;
			modListElement.style.display = (isExpanded ? DisplayStyle.Flex : DisplayStyle.None);
			((TextElement)toggleButton).text = (isExpanded ? $"▼ 隐藏模组 ({mods.Count})" : $"▶ 显示模组 ({mods.Count})");
		};
		val2.Add((VisualElement)(object)toggleButton);
		val2.Add(modListElement);
		return val2;
	}

	private void SoftLockPlayer(PhotonPlayer player, string reason)
	{
		try
		{
			if (!PhotonNetwork.IsMasterClient)
			{
				Debug.LogWarning((object)"SoftLock: You are not the MasterClient!");
				return;
			}
			if (player == null || player.IsLocal)
			{
				Debug.LogWarning((object)"SoftLock: Cannot soft-lock yourself or invalid player!");
				return;
			}
			SoftLockPlayerInternal(player, reason);
			Debug.Log((object)("SoftLock: Soft-locked player '" + player.NickName + "' from UI - Reason: " + reason));
			Plugin.Log.LogInfo((object)("Soft-locked player from UI: " + player.NickName + " - Reason: " + reason));
			RefreshInfo();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("SoftLock: Error - " + ex.Message));
			Plugin.Log.LogError((object)("Failed to soft-lock player from UI: " + ex.Message));
		}
	}

	private void ForceKickPlayer(PhotonPlayer player)
	{
		try
		{
			if (!PhotonNetwork.IsMasterClient)
			{
				Debug.LogWarning((object)"forcekick: You are not the MasterClient!");
				return;
			}
			if (player == null || player.IsLocal)
			{
				Debug.LogWarning((object)"forcekick: Cannot kick yourself or invalid player!");
				return;
			}
			PhotonNetwork.CloseConnection(player);
			Debug.Log((object)("forcekick: Force disconnected player '" + player.NickName + "'"));
			Plugin.Log.LogInfo((object)("Player force kicked from UI: " + player.NickName));
			RefreshInfo();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("forcekick: Error - " + ex.Message));
			Plugin.Log.LogError((object)("Failed to force kick player from UI: " + ex.Message));
		}
	}

	private void BlackScreenKick(PhotonPlayer player)
	{
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!PhotonNetwork.IsMasterClient)
			{
				Debug.LogWarning((object)"blackscreen: You are not the MasterClient!");
				return;
			}
			if (player == null || player.IsLocal)
			{
				Debug.LogWarning((object)"blackscreen: Cannot blackscreen yourself or invalid player!");
				return;
			}
			Character[] array = UnityEngine.Object.FindObjectsByType<Character>((FindObjectsInactive)1, (FindObjectsSortMode)0);
			Character val = null;
			Character[] array2 = array;
			foreach (Character val2 in array2)
			{
				PhotonView component = ((Component)val2).GetComponent<PhotonView>();
				if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null && component.Owner != null && component.Owner.ActorNumber == player.ActorNumber)
				{
					val = val2;
					break;
				}
			}
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				PhotonView component2 = ((Component)val).GetComponent<PhotonView>();
				if ((UnityEngine.Object)(object)component2 != (UnityEngine.Object)null)
				{
					Vector3 val3 = default(Vector3);
					val3 = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
					try
					{
						component2.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { val3, true });
					}
					catch
					{
						((Component)val).transform.position = val3;
					}
					Debug.Log((object)("blackscreen: Created black screen for player '" + player.NickName + "'"));
					Plugin.Log.LogInfo((object)("Player blackscreened from UI: " + player.NickName));
				}
				else
				{
					Debug.LogWarning((object)("blackscreen: No PhotonView on character for " + player.NickName));
					ForceKickPlayer(player);
				}
			}
			else
			{
				Debug.LogWarning((object)("blackscreen: Character not found for " + player.NickName + ", using force kick"));
				ForceKickPlayer(player);
			}
			RefreshInfo();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("blackscreen: Error - " + ex.Message));
			Plugin.Log.LogError((object)("Failed to blackscreen player from UI: " + ex.Message));
		}
	}

	private void SlowPlayer(PhotonPlayer player)
	{
		try
		{
			if (!PhotonNetwork.IsMasterClient)
			{
				Debug.LogWarning((object)"slowplayer: You are not the MasterClient!");
				return;
			}
			if (player == null || player.IsLocal)
			{
				Debug.LogWarning((object)"slowplayer: Cannot slow yourself or invalid player!");
				return;
			}
			Character val = Character.AllCharacters.FirstOrDefault(delegate(Character c)
			{
				PhotonPlayer owner = ((MonoBehaviourPun)c).photonView.Owner;
				return ((owner != null) ? new int?(owner.ActorNumber) : ((int?)null)) == player.ActorNumber;
			});
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				foreach (Bodypart part in val.refs.ragdoll.partList)
				{
					Rigidbody rig = part.rig;
					rig.maxLinearVelocity *= 0.1f;
				}
				Debug.Log((object)("slowplayer: Slowed down player '" + player.NickName + "' from UI"));
				Plugin.Log.LogInfo((object)("Player slowed down from UI: " + player.NickName));
			}
			else
			{
				Debug.LogWarning((object)("slowplayer: Character not found for " + player.NickName));
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("slowplayer: Error - " + ex.Message));
			Plugin.Log.LogError((object)("Failed to slow player from UI: " + ex.Message));
		}
	}

	private void SpeedPlayer(PhotonPlayer player)
	{
		try
		{
			if (!PhotonNetwork.IsMasterClient)
			{
				Debug.LogWarning((object)"speedplayer: You are not the MasterClient!");
				return;
			}
			if (player == null || player.IsLocal)
			{
				Debug.LogWarning((object)"speedplayer: Cannot speed yourself or invalid player!");
				return;
			}
			Character val = Character.AllCharacters.FirstOrDefault(delegate(Character c)
			{
				PhotonPlayer owner = ((MonoBehaviourPun)c).photonView.Owner;
				return ((owner != null) ? new int?(owner.ActorNumber) : ((int?)null)) == player.ActorNumber;
			});
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				foreach (Bodypart part in val.refs.ragdoll.partList)
				{
					Rigidbody rig = part.rig;
					rig.maxLinearVelocity *= 10f;
				}
				Debug.Log((object)("speedplayer: Sped up player '" + player.NickName + "' from UI"));
				Plugin.Log.LogInfo((object)("Player sped up from UI: " + player.NickName));
			}
			else
			{
				Debug.LogWarning((object)("speedplayer: Character not found for " + player.NickName));
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("speedplayer: Error - " + ex.Message));
			Plugin.Log.LogError((object)("Failed to speed up player from UI: " + ex.Message));
		}
	}

	private string GetKickReason(PhotonPlayer player)
	{
		try
		{
			Hashtable customProperties = player.CustomProperties;
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"AtlOwner"))
			{
				return "ATLAS cheat mod owner";
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"AtlUser"))
			{
				return "ATLAS cheat mod user";
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"CherryOwner"))
			{
				return "Cherry cheat mod owner";
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"CherryUser"))
			{
				return "Cherry cheat mod user";
			}
			return "Kicked by host";
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("Error determining kick reason for " + player.NickName + ": " + ex.Message));
			return "Kicked by host";
		}
	}

	private void SoftLockPlayerInternal(PhotonPlayer player, string reason)
	{
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			AirportCheckInKiosk val = UnityEngine.Object.FindFirstObjectByType<AirportCheckInKiosk>();
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				PhotonView component = ((Component)val).GetComponent<PhotonView>();
				if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null)
				{
					try
					{
						component.RPC("BeginIslandLoadRPC", (RpcTarget)0, new object[1] { 0 });
						Debug.Log((object)("SoftLock: Used kiosk method for " + player.NickName));
						return;
					}
					catch (Exception ex)
					{
						Debug.LogWarning((object)("Failed to use kiosk soft-lock: " + ex.Message));
					}
				}
			}
			Character[] array = UnityEngine.Object.FindObjectsByType<Character>((FindObjectsInactive)1, (FindObjectsSortMode)0);
			Character val2 = null;
			Character[] array2 = array;
			foreach (Character val3 in array2)
			{
				PhotonView component2 = ((Component)val3).GetComponent<PhotonView>();
				if ((UnityEngine.Object)(object)component2 != (UnityEngine.Object)null && component2.Owner != null && component2.Owner.ActorNumber == player.ActorNumber)
				{
					val2 = val3;
					break;
				}
			}
			if ((UnityEngine.Object)(object)val2 != (UnityEngine.Object)null)
			{
				PhotonView component3 = ((Component)val2).GetComponent<PhotonView>();
				if ((UnityEngine.Object)(object)component3 != (UnityEngine.Object)null)
				{
					try
					{
						Vector3 val4 = default(Vector3);
						val4 = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
						component3.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { val4, true });
						Debug.Log((object)("SoftLock: Used character warp for " + player.NickName));
						return;
					}
					catch (Exception ex2)
					{
						Debug.LogWarning((object)("Failed to use character warp: " + ex2.Message));
					}
				}
			}
			PhotonNetwork.CloseConnection(player);
			Debug.Log((object)("SoftLock: Used fallback kick for " + player.NickName));
		}
		catch (Exception ex3)
		{
			Debug.LogError((object)("SoftLockPlayerInternal: Critical error - " + ex3.Message));
			PhotonNetwork.CloseConnection(player);
		}
	}

	private PlayerCheatInfo GetPlayerCheatInfo(PhotonPlayer player)
	{
		PlayerCheatInfo playerCheatInfo = new PlayerCheatInfo
		{
			PhotonPlayer = player,
			ActorNumber = player.ActorNumber,
			NickName = (player.NickName ?? "Unknown")
		};
		if (((player != null) ? player.CustomProperties : null) == null)
		{
			return playerCheatInfo;
		}
		try
		{
			Hashtable customProperties = player.CustomProperties;
			playerCheatInfo.HasAtlas = ((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"AtlUser");
			playerCheatInfo.IsAtlasOwner = ((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"AtlOwner");
			playerCheatInfo.HasCherry = ((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"CherryUser");
			playerCheatInfo.IsCherryOwner = ((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"CherryOwner");
			playerCheatInfo.HasAdvancedConsole = ((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"AdvancedConsole") || ((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"AdvancedConsoleUser");
			if (((Dictionary<object, object>)(object)customProperties).TryGetValue((object)"ModLoader", out object value))
			{
				playerCheatInfo.ModLoader = value.ToString();
			}
			if (((Dictionary<object, object>)(object)customProperties).TryGetValue((object)"ModList", out object value2))
			{
				if (value2 is string[] collection)
				{
					playerCheatInfo.ModList.AddRange(collection);
				}
				else if (value2 is string text)
				{
					playerCheatInfo.ModList.AddRange(from s in text.Split(',')
						select s.Trim());
				}
			}
			if (((Dictionary<object, object>)(object)customProperties).TryGetValue((object)"ModHash", out object value3))
			{
				playerCheatInfo.ModHash = value3.ToString();
			}
			playerCheatInfo.CheatFunctions = DetectCheatFunctions(player);
			Debug.Log((object)string.Format("[CheatInfo] Player {0}: Atlas={1}, AtlasOwner={2}, Cherry={3}, CherryOwner={4}, Console={5}, Cheats={6}", new object[7]
			{
				player.NickName,
				playerCheatInfo.HasAtlas,
				playerCheatInfo.IsAtlasOwner,
				playerCheatInfo.HasCherry,
				playerCheatInfo.IsCherryOwner,
				playerCheatInfo.HasAdvancedConsole,
				playerCheatInfo.CheatFunctions.Count
			}));
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("Error getting cheat info for player " + player.NickName + ": " + ex.Message));
		}
		return playerCheatInfo;
	}

	private List<string> DetectCheatFunctions(PhotonPlayer player)
	{
		List<string> list = new List<string>();
		try
		{
			Hashtable customProperties = player.CustomProperties;
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"SpeedHack"))
			{
				list.Add("Speed Hack");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"FlyHack"))
			{
				list.Add("Fly Hack");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"NoClip"))
			{
				list.Add("No Clip");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"Teleport"))
			{
				list.Add("Teleport");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"GodMode"))
			{
				list.Add("God Mode");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"InfiniteStamina"))
			{
				list.Add("Infinite Stamina");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"ESPHack"))
			{
				list.Add("ESP/Wallhack");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"ItemSpawn"))
			{
				list.Add("Item Spawning");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"WeatherControl"))
			{
				list.Add("Weather Control");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"TimeControl"))
			{
				list.Add("Time Control");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"UnlimitedItems"))
			{
				list.Add("Unlimited Items");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"InstantWin"))
			{
				list.Add("Instant Win");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"KillAllPlayers"))
			{
				list.Add("Kill All Players");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"ReviveAllPlayers"))
			{
				list.Add("Revive All Players");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"ForceStart"))
			{
				list.Add("Force Start Game");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"TeleportAllPlayers"))
			{
				list.Add("Teleport All Players");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"ModMenu"))
			{
				list.Add("Mod Menu");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"NoClipHack"))
			{
				list.Add("NoClip");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"GodModeHack"))
			{
				list.Add("God Mode");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"InfiniteHealth"))
			{
				list.Add("Infinite Health");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"TeleportHack"))
			{
				list.Add("Teleport");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"ItemSpawner"))
			{
				list.Add("Item Spawner");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"WallHack"))
			{
				list.Add("Wall Hack");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"AimbotHack"))
			{
				list.Add("Aimbot");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"AntiKickHack"))
			{
				list.Add("Anti-Kick");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"CharacterTheft"))
			{
				list.Add("Character Theft");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"OwnershipTheft"))
			{
				list.Add("Ownership Theft");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"MassDestroy"))
			{
				list.Add("Mass Destroy");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"RPCSpam"))
			{
				list.Add("RPC Spam");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"NetworkExploit"))
			{
				list.Add("Network Exploit");
			}
			if ((player.NickName.ToLower().Contains("atlas") || player.NickName.ToLower().Contains("cherry")) && !list.Contains("Name Spoofing"))
			{
				list.Add("Name Spoofing");
			}
			string.IsNullOrEmpty(player.UserId);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("Error detecting cheat functions for " + player.NickName + ": " + ex.Message));
		}
		return list;
	}

	private VisualElement CreateStaminaDisplay(Character character)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Expected O, but got Unknown
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Expected O, but got Unknown
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Expected O, but got Unknown
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Expected O, but got Unknown
		VisualElement val = new VisualElement
		{
			name = "StaminaContainer"
		};
		val.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)0);
		val.style.marginTop = new StyleLength(4f);
		val.style.marginBottom = new StyleLength(4f);
		VisualElement val2 = val;
		Label val3 = new Label("Stamina");
		((VisualElement)val3).style.fontSize = new StyleLength(11f);
		((VisualElement)val3).style.color = new StyleColor(new Color(0.8f, 0.8f, 0.8f));
		((VisualElement)val3).style.marginBottom = new StyleLength(2f);
		Label val4 = val3;
		val2.Add((VisualElement)(object)val4);
		VisualElement val5 = new VisualElement();
		val5.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)2);
		val5.style.alignItems = new StyleEnum<Align>((Align)2);
		val5.style.height = new StyleLength(12f);
		val5.style.backgroundColor = new StyleColor(new Color(0.2f, 0.2f, 0.2f));
		val5.style.borderTopLeftRadius = new StyleLength(2f);
		val5.style.borderTopRightRadius = new StyleLength(2f);
		val5.style.borderBottomLeftRadius = new StyleLength(2f);
		val5.style.borderBottomRightRadius = new StyleLength(2f);
		val5.style.paddingTop = new StyleLength(1f);
		val5.style.paddingBottom = new StyleLength(1f);
		val5.style.paddingLeft = new StyleLength(1f);
		val5.style.paddingRight = new StyleLength(1f);
		VisualElement val6 = val5;
		VisualElement val7 = new VisualElement
		{
			name = "StaminaBar"
		};
		val7.style.height = new StyleLength(8f);
		val7.style.backgroundColor = new StyleColor(GetStaminaColor(character.data.currentStamina, character.data.TotalStamina));
		val7.style.borderTopLeftRadius = new StyleLength(1f);
		val7.style.borderTopRightRadius = new StyleLength(1f);
		val7.style.borderBottomLeftRadius = new StyleLength(1f);
		val7.style.borderBottomRightRadius = new StyleLength(1f);
		VisualElement val8 = val7;
		float num = ((character.data.TotalStamina > 0f) ? (character.data.currentStamina / character.data.TotalStamina) : 0f);
		num = Mathf.Clamp01(num);
		val8.style.width = new StyleLength(new Length(num * 100f, (LengthUnit)1));
		val6.Add(val8);
		Label val9 = new Label($"{character.data.currentStamina:F1}/{character.data.TotalStamina:F1}")
		{
			name = "StaminaText"
		};
		((VisualElement)val9).style.fontSize = new StyleLength(9f);
		((VisualElement)val9).style.color = new StyleColor(new Color(0.9f, 0.9f, 0.9f));
		((VisualElement)val9).style.marginLeft = new StyleLength(4f);
		Label val10 = val9;
		VisualElement val11 = new VisualElement();
		val11.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)2);
		val11.style.alignItems = new StyleEnum<Align>((Align)2);
		VisualElement val12 = val11;
		val12.Add(val6);
		val12.Add((VisualElement)(object)val10);
		val2.Add(val12);
		return val2;
	}

	private Color GetStaminaColor(float currentStamina, float totalStamina)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		float num = ((totalStamina > 0f) ? (currentStamina / totalStamina) : 0f);
		if (num > 0.7f)
		{
			return new Color(0.2f, 0.8f, 0.2f);
		}
		if (num > 0.3f)
		{
			return new Color(0.8f, 0.8f, 0.2f);
		}
		return new Color(0.8f, 0.2f, 0.2f);
	}

	private VisualElement CreateInventoryDisplay(PhotonPlayer photonPlayer, Character character)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Expected O, but got Unknown
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Expected O, but got Unknown
		VisualElement val = new VisualElement
		{
			name = "InventoryContainer"
		};
		val.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)0);
		val.style.marginTop = new StyleLength(4f);
		val.style.marginBottom = new StyleLength(4f);
		VisualElement val2 = val;
		Label val3 = new Label("Inventory");
		((VisualElement)val3).style.fontSize = new StyleLength(11f);
		((VisualElement)val3).style.color = new StyleColor(new Color(0.8f, 0.8f, 0.8f));
		((VisualElement)val3).style.marginBottom = new StyleLength(2f);
		Label val4 = val3;
		val2.Add((VisualElement)(object)val4);
		try
		{
			Player player = PlayerHandler.GetPlayer(photonPlayer);
			if ((UnityEngine.Object)(object)player != (UnityEngine.Object)null)
			{
				VisualElement val5 = CreateInventorySlotsDisplay(player, photonPlayer, "Main Slots", new byte[4] { 0, 1, 2, 250 }, new string[4] { "Slot 1", "Slot 2", "Slot 3", "Temp" });
				val2.Add(val5);
				VisualElement val6 = CreateBackpackDisplay(player, photonPlayer);
				val2.Add(val6);
			}
			else
			{
				Label val7 = new Label("Player component not found");
				((VisualElement)val7).style.fontSize = new StyleLength(9f);
				((VisualElement)val7).style.color = new StyleColor(new Color(0.7f, 0.7f, 0.7f));
				Label val8 = val7;
				val2.Add((VisualElement)(object)val8);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("Error creating inventory display for " + ((photonPlayer != null) ? photonPlayer.NickName : null) + ": " + ex.Message));
			Debug.LogError((object)("Stack trace: " + ex.StackTrace));
			Label val9 = new Label("Inventory Error: " + ex.Message);
			((VisualElement)val9).style.fontSize = new StyleLength(9f);
			((VisualElement)val9).style.color = new StyleColor(new Color(1f, 0.5f, 0.5f));
			((VisualElement)val9).style.whiteSpace = new StyleEnum<WhiteSpace>((WhiteSpace)0);
			Label val10 = val9;
			val2.Add((VisualElement)(object)val10);
		}
		return val2;
	}

	private VisualElement CreateInventorySlotsDisplay(Player player, PhotonPlayer photonPlayer, string sectionName, byte[] slotIds, string[] slotNames)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		VisualElement val = new VisualElement();
		val.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)0);
		val.style.marginBottom = new StyleLength(4f);
		VisualElement val2 = val;
		Label val3 = new Label(sectionName);
		((VisualElement)val3).style.fontSize = new StyleLength(10f);
		((VisualElement)val3).style.color = new StyleColor(new Color(0.7f, 0.7f, 0.7f));
		((VisualElement)val3).style.marginBottom = new StyleLength(2f);
		Label val4 = val3;
		val2.Add((VisualElement)(object)val4);
		VisualElement val5 = new VisualElement();
		val5.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)2);
		val5.style.flexWrap = new StyleEnum<Wrap>((Wrap)1);
		val5.style.justifyContent = new StyleEnum<Justify>((Justify)0);
		VisualElement val6 = val5;
		for (int i = 0; i < slotIds.Length; i++)
		{
			byte slotId = slotIds[i];
			string slotName = slotNames[i];
			VisualElement val7 = CreateSingleSlotDisplay(player, photonPlayer, slotId, slotName);
			val7.style.marginRight = new StyleLength(8f);
			val7.style.marginBottom = new StyleLength(2f);
			val6.Add(val7);
		}
		val2.Add(val6);
		return val2;
	}

	private VisualElement CreateSingleSlotDisplay(Player player, PhotonPlayer photonPlayer, byte slotId, string slotName)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Expected O, but got Unknown
		VisualElement val = new VisualElement();
		val.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)0);
		val.style.alignItems = new StyleEnum<Align>((Align)2);
		val.style.minWidth = new StyleLength(100f);
		val.style.maxWidth = new StyleLength(100f);
		val.style.backgroundColor = new StyleColor(new Color(0.15f, 0.15f, 0.15f));
		val.style.borderTopLeftRadius = new StyleLength(3f);
		val.style.borderTopRightRadius = new StyleLength(3f);
		val.style.borderBottomLeftRadius = new StyleLength(3f);
		val.style.borderBottomRightRadius = new StyleLength(3f);
		val.style.paddingTop = new StyleLength(4f);
		val.style.paddingBottom = new StyleLength(4f);
		val.style.paddingLeft = new StyleLength(4f);
		val.style.paddingRight = new StyleLength(4f);
		VisualElement val2 = val;
		Label val3 = new Label(slotName);
		((VisualElement)val3).style.fontSize = new StyleLength(8f);
		((VisualElement)val3).style.color = new StyleColor(new Color(0.8f, 0.8f, 0.8f));
		((VisualElement)val3).style.marginBottom = new StyleLength(2f);
		((VisualElement)val3).style.unityTextAlign = new StyleEnum<TextAnchor>((TextAnchor)4);
		Label val4 = val3;
		val2.Add((VisualElement)(object)val4);
		string text = "EMPTY";
		try
		{
			ItemSlot itemSlot = player.GetItemSlot(slotId);
			if (itemSlot != null && !itemSlot.IsEmpty() && (UnityEngine.Object)(object)itemSlot.prefab != (UnityEngine.Object)null)
			{
				text = ((UnityEngine.Object)itemSlot.prefab).name;
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)$"Error getting item slot {slotId} for {photonPlayer.NickName}: {ex.Message}");
			text = "ERROR";
		}
		if (PhotonNetwork.IsMasterClient || MasterClientUtils.IsBypassEnabled)
		{
			DropdownField val5 = CreateItemDropdownForSlot(photonPlayer, slotId, text);
			((VisualElement)val5).style.width = new StyleLength(90f);
			val2.Add((VisualElement)(object)val5);
		}
		else
		{
			Label val6 = new Label(text);
			((VisualElement)val6).style.fontSize = new StyleLength(8f);
			((VisualElement)val6).style.color = new StyleColor((text == "EMPTY") ? new Color(0.6f, 0.6f, 0.6f) : new Color(0.9f, 0.9f, 0.9f));
			((VisualElement)val6).style.unityTextAlign = new StyleEnum<TextAnchor>((TextAnchor)4);
			((VisualElement)val6).style.whiteSpace = new StyleEnum<WhiteSpace>((WhiteSpace)0);
			((VisualElement)val6).style.maxWidth = new StyleLength(90f);
			Label val7 = val6;
			val2.Add((VisualElement)(object)val7);
		}
		return val2;
	}

	private DropdownField CreateItemDropdownForSlot(PhotonPlayer photonPlayer, byte slotId, string currentItem)
	{
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Expected O, but got Unknown
		try
		{
			List<string> list = new List<string> { "EMPTY" };
			ItemDatabase instance = SingletonAsset<ItemDatabase>.Instance;
			if (instance?.itemLookup != null)
			{
				List<string> collection = (from kvp in instance.itemLookup
					orderby ((UnityEngine.Object)kvp.Value).name
					select ((UnityEngine.Object)kvp.Value).name).ToList();
				list.AddRange(collection);
			}
			int num = list.FindIndex((string name) => name.Equals(currentItem, StringComparison.OrdinalIgnoreCase));
			if (num == -1)
			{
				num = 0;
			}
			DropdownField val = new DropdownField(list, num, (Func<string, string>)null, (Func<string, string>)null)
			{
				name = $"Slot_{slotId}"
			};
			((VisualElement)val).style.fontSize = new StyleLength(8f);
			((VisualElement)val).style.backgroundColor = new StyleColor(new Color(0.2f, 0.2f, 0.2f));
			((VisualElement)val).style.color = new StyleColor(new Color(0.95f, 0.95f, 0.95f));
			((VisualElement)val).style.minWidth = new StyleLength(120f);
			((VisualElement)val).style.maxWidth = new StyleLength(120f);
			DropdownField val2 = val;
			INotifyValueChangedExtensions.RegisterValueChangedCallback<string>((INotifyValueChanged<string>)(object)val2, (EventCallback<ChangeEvent<string>>)delegate(ChangeEvent<string> evt)
			{
				try
				{
					string newValue = evt.newValue;
					ChangePlayerInventorySlot(photonPlayer, slotId, newValue);
				}
				catch (Exception ex2)
				{
					Debug.LogError((object)("Error changing inventory slot: " + ex2.Message));
				}
			});
			return val2;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("Error creating item dropdown: " + ex.Message));
			DropdownField val3 = new DropdownField(new List<string> { "Error" }, 0, (Func<string, string>)null, (Func<string, string>)null);
			((VisualElement)val3).style.fontSize = new StyleLength(8f);
			((VisualElement)val3).style.backgroundColor = new StyleColor(new Color(0.4f, 0.2f, 0.2f));
			((VisualElement)val3).style.color = new StyleColor(Color.white);
			return val3;
		}
	}

	private VisualElement CreateBackpackDisplay(Player player, PhotonPlayer photonPlayer)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Expected O, but got Unknown
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Expected O, but got Unknown
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Expected O, but got Unknown
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Expected O, but got Unknown
		VisualElement val = new VisualElement();
		val.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)0);
		val.style.marginTop = new StyleLength(4f);
		VisualElement val2 = val;
		Label val3 = new Label("Backpack:");
		((VisualElement)val3).style.fontSize = new StyleLength(10f);
		((VisualElement)val3).style.color = new StyleColor(new Color(0.7f, 0.7f, 0.7f));
		((VisualElement)val3).style.marginBottom = new StyleLength(2f);
		Label val4 = val3;
		val2.Add((VisualElement)(object)val4);
		VisualElement val5 = CreateSingleSlotDisplay(player, photonPlayer, 3, "Worn");
		val2.Add(val5);
		if (!((ItemSlot)player.backpackSlot).IsEmpty())
		{
			Label val6 = new Label("Contents:");
			((VisualElement)val6).style.fontSize = new StyleLength(9f);
			((VisualElement)val6).style.color = new StyleColor(new Color(0.6f, 0.6f, 0.6f));
			((VisualElement)val6).style.marginTop = new StyleLength(2f);
			((VisualElement)val6).style.marginBottom = new StyleLength(2f);
			Label val7 = val6;
			val2.Add((VisualElement)(object)val7);
			try
			{
				BackpackData val8 = default(BackpackData);
				if (((ItemSlot)player.backpackSlot).data.TryGetDataEntry<BackpackData>((DataEntryKey)7, out val8) && val8?.itemSlots != null && val8.itemSlots.Length != 0)
				{
					VisualElement val9 = new VisualElement();
					val9.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)2);
					val9.style.flexWrap = new StyleEnum<Wrap>((Wrap)1);
					val9.style.justifyContent = new StyleEnum<Justify>((Justify)0);
					val9.style.marginLeft = new StyleLength(10f);
					VisualElement val10 = val9;
					for (int i = 0; i < 4; i++)
					{
						string currentItem = "EMPTY";
						try
						{
							if (i < val8.itemSlots.Length)
							{
								ItemSlot val11 = val8.itemSlots[i];
								if (val11 != null && !val11.IsEmpty() && (UnityEngine.Object)(object)val11.prefab != (UnityEngine.Object)null)
								{
									currentItem = ((UnityEngine.Object)val11.prefab).name;
								}
							}
						}
						catch (Exception ex)
						{
							Debug.LogWarning((object)$"Error getting backpack slot {i} for {photonPlayer.NickName}: {ex.Message}");
							currentItem = "ERROR";
						}
						VisualElement val12 = CreateBackpackContentSlot(photonPlayer, i, currentItem);
						val12.style.marginRight = new StyleLength(8f);
						val12.style.marginBottom = new StyleLength(2f);
						val10.Add(val12);
					}
					val2.Add(val10);
				}
				else
				{
					Label val13 = new Label("Empty backpack");
					((VisualElement)val13).style.fontSize = new StyleLength(9f);
					((VisualElement)val13).style.color = new StyleColor(new Color(0.5f, 0.5f, 0.5f));
					((VisualElement)val13).style.marginLeft = new StyleLength(10f);
					Label val14 = val13;
					val2.Add((VisualElement)(object)val14);
				}
			}
			catch (Exception ex2)
			{
				Debug.LogError((object)("Error getting backpack contents: " + ex2.Message));
				Label val15 = new Label("Error reading backpack");
				((VisualElement)val15).style.fontSize = new StyleLength(9f);
				((VisualElement)val15).style.color = new StyleColor(new Color(1f, 0.5f, 0.5f));
				((VisualElement)val15).style.marginLeft = new StyleLength(10f);
				Label val16 = val15;
				val2.Add((VisualElement)(object)val16);
			}
		}
		else
		{
			Label val17 = new Label("No backpack equipped");
			((VisualElement)val17).style.fontSize = new StyleLength(9f);
			((VisualElement)val17).style.color = new StyleColor(new Color(0.5f, 0.5f, 0.5f));
			((VisualElement)val17).style.marginLeft = new StyleLength(10f);
			Label val18 = val17;
			val2.Add((VisualElement)(object)val18);
		}
		return val2;
	}

	private VisualElement CreateBackpackContentSlot(PhotonPlayer photonPlayer, int backpackSlotIndex, string currentItem)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Expected O, but got Unknown
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Expected O, but got Unknown
		VisualElement val = new VisualElement();
		val.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)0);
		val.style.alignItems = new StyleEnum<Align>((Align)2);
		val.style.minWidth = new StyleLength(80f);
		val.style.maxWidth = new StyleLength(80f);
		val.style.backgroundColor = new StyleColor(new Color(0.12f, 0.12f, 0.12f));
		val.style.borderTopLeftRadius = new StyleLength(3f);
		val.style.borderTopRightRadius = new StyleLength(3f);
		val.style.borderBottomLeftRadius = new StyleLength(3f);
		val.style.borderBottomRightRadius = new StyleLength(3f);
		val.style.paddingTop = new StyleLength(3f);
		val.style.paddingBottom = new StyleLength(3f);
		val.style.paddingLeft = new StyleLength(3f);
		val.style.paddingRight = new StyleLength(3f);
		VisualElement val2 = val;
		Label val3 = new Label($"BP{backpackSlotIndex + 1}");
		((VisualElement)val3).style.fontSize = new StyleLength(7f);
		((VisualElement)val3).style.color = new StyleColor(new Color(0.7f, 0.7f, 0.7f));
		((VisualElement)val3).style.marginBottom = new StyleLength(2f);
		((VisualElement)val3).style.unityTextAlign = new StyleEnum<TextAnchor>((TextAnchor)4);
		Label val4 = val3;
		val2.Add((VisualElement)(object)val4);
		if (PhotonNetwork.IsMasterClient || MasterClientUtils.IsBypassEnabled)
		{
			DropdownField val5 = CreateItemDropdownForBackpackSlot(photonPlayer, backpackSlotIndex, currentItem);
			((VisualElement)val5).style.width = new StyleLength(70f);
			val2.Add((VisualElement)(object)val5);
		}
		else
		{
			Label val6 = new Label(currentItem);
			((VisualElement)val6).style.fontSize = new StyleLength(7f);
			((VisualElement)val6).style.color = new StyleColor((currentItem == "EMPTY") ? new Color(0.6f, 0.6f, 0.6f) : new Color(0.9f, 0.9f, 0.9f));
			((VisualElement)val6).style.unityTextAlign = new StyleEnum<TextAnchor>((TextAnchor)4);
			((VisualElement)val6).style.whiteSpace = new StyleEnum<WhiteSpace>((WhiteSpace)0);
			((VisualElement)val6).style.maxWidth = new StyleLength(70f);
			Label val7 = val6;
			val2.Add((VisualElement)(object)val7);
		}
		return val2;
	}

	private DropdownField CreateItemDropdownForBackpackSlot(PhotonPlayer photonPlayer, int backpackSlotIndex, string currentItem)
	{
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Expected O, but got Unknown
		try
		{
			List<string> list = new List<string> { "EMPTY" };
			ItemDatabase instance = SingletonAsset<ItemDatabase>.Instance;
			if (instance?.itemLookup != null)
			{
				List<string> collection = (from kvp in instance.itemLookup
					where !(kvp.Value is Backpack)
					orderby ((UnityEngine.Object)kvp.Value).name
					select ((UnityEngine.Object)kvp.Value).name).ToList();
				list.AddRange(collection);
			}
			int num = list.FindIndex((string name) => name.Equals(currentItem, StringComparison.OrdinalIgnoreCase));
			if (num == -1)
			{
				num = 0;
			}
			DropdownField val = new DropdownField(list, num, (Func<string, string>)null, (Func<string, string>)null);
			((VisualElement)val).style.fontSize = new StyleLength(8f);
			((VisualElement)val).style.backgroundColor = new StyleColor(new Color(0.2f, 0.2f, 0.2f));
			((VisualElement)val).style.color = new StyleColor(new Color(0.95f, 0.95f, 0.95f));
			((VisualElement)val).style.minWidth = new StyleLength(100f);
			((VisualElement)val).style.maxWidth = new StyleLength(100f);
			DropdownField val2 = val;
			INotifyValueChangedExtensions.RegisterValueChangedCallback<string>((INotifyValueChanged<string>)(object)val2, (EventCallback<ChangeEvent<string>>)delegate(ChangeEvent<string> evt)
			{
				try
				{
					string newValue = evt.newValue;
					ChangeBackpackSlotContent(photonPlayer, backpackSlotIndex, newValue);
				}
				catch (Exception ex2)
				{
					Debug.LogError((object)("Error changing backpack slot: " + ex2.Message));
				}
			});
			return val2;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("Error creating backpack dropdown: " + ex.Message));
			return new DropdownField(new List<string> { "Error" }, 0, (Func<string, string>)null, (Func<string, string>)null);
		}
	}

	private void ChangePlayerInventorySlot(PhotonPlayer photonPlayer, byte slotId, string itemName)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		try
		{
			if (!PhotonNetwork.IsMasterClient && !MasterClientUtils.IsBypassEnabled)
			{
				Debug.LogWarning((object)"changeinventory: Only Master Client can change inventory!");
				return;
			}
			Player player = PlayerHandler.GetPlayer(photonPlayer);
			if ((UnityEngine.Object)(object)player == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("changeinventory: Player component not found for " + photonPlayer.NickName));
				return;
			}
			try
			{
				player.EmptySlot(Optionable<byte>.Some(slotId));
				Debug.Log((object)$"changeinventory: Emptied slot {slotId} for {photonPlayer.NickName}");
				if (itemName != "EMPTY")
				{
					SpawnCommands.GiveItemByName(photonPlayer, itemName);
					Debug.Log((object)("changeinventory: Added '" + itemName + "' to " + photonPlayer.NickName + " using SpawnCommands"));
				}
				Plugin.Log.LogInfo((object)$"Changed inventory slot {slotId} to '{itemName}' for {photonPlayer.NickName}");
			}
			catch (Exception ex)
			{
				Debug.LogError((object)("changeinventory: Operation failed: " + ex.Message));
				if (itemName != "EMPTY")
				{
					try
					{
						ItemDatabase instance = SingletonAsset<ItemDatabase>.Instance;
						if (instance?.itemLookup != null)
						{
							KeyValuePair<ushort, Item> keyValuePair = instance.itemLookup.FirstOrDefault((KeyValuePair<ushort, Item> kvp) => ((UnityEngine.Object)kvp.Value).name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
							if ((UnityEngine.Object)(object)keyValuePair.Value != (UnityEngine.Object)null)
							{
								ItemInstanceData val = new ItemInstanceData(Guid.NewGuid());
								ItemInstanceDataHandler.AddInstanceData(val);
								ItemSlot val2 = default(ItemSlot);
								if (player.AddItem(keyValuePair.Key, val, out val2))
								{
									Debug.Log((object)("changeinventory: Added '" + itemName + "' using fallback method"));
								}
								else
								{
									Debug.LogWarning((object)("changeinventory: Failed to add '" + itemName + "' using fallback method"));
								}
							}
						}
					}
					catch (Exception ex2)
					{
						Debug.LogError((object)("changeinventory: Fallback method also failed: " + ex2.Message));
					}
				}
			}
			((VisualElement)this).schedule.Execute((Action)delegate
			{
				RefreshInfo();
			}).ExecuteLater(100L);
		}
		catch (Exception ex3)
		{
			Debug.LogError((object)("changeinventory: Error - " + ex3.Message));
		}
	}

	private void TryUseConsoleCommandForInventory(PhotonPlayer photonPlayer, byte slotId, string itemName)
	{
		try
		{
			if (itemName == "EMPTY")
			{
				Debug.Log((object)$"changeinventory: Using console fallback to empty slot {slotId} for {photonPlayer.NickName}");
				return;
			}
			Debug.Log((object)$"changeinventory: Using console fallback to add '{itemName}' to slot {slotId} for {photonPlayer.NickName}");
			SpawnCommands.GiveItemByName(photonPlayer, itemName);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("changeinventory: Console fallback failed: " + ex.Message));
		}
	}

	private void ChangeBackpackSlotContent(PhotonPlayer photonPlayer, int backpackSlotIndex, string itemName)
	{
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Expected O, but got Unknown
		try
		{
			Debug.Log((object)$"changebackpack: Changing backpack slot {backpackSlotIndex} to '{itemName}' for {photonPlayer.NickName}");
			Character val = Character.AllCharacters.FirstOrDefault(delegate(Character c)
			{
				PhotonPlayer owner = ((MonoBehaviourPun)c).photonView.Owner;
				return ((owner != null) ? new int?(owner.ActorNumber) : ((int?)null)) == photonPlayer.ActorNumber;
			});
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("changebackpack: Character not found for " + photonPlayer.NickName));
				return;
			}
			Player player = val.player;
			if ((UnityEngine.Object)(object)player == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("changebackpack: Player component not found for " + photonPlayer.NickName));
				return;
			}
			if (((ItemSlot)player.backpackSlot).IsEmpty())
			{
				Debug.LogWarning((object)("changebackpack: " + photonPlayer.NickName + " has no backpack equipped"));
				return;
			}
			if (backpackSlotIndex < 0 || backpackSlotIndex >= 4)
			{
				Debug.LogWarning((object)$"changebackpack: Invalid backpack slot index {backpackSlotIndex} (must be 0-3)");
				return;
			}
			CharacterBackpackHandler component = ((Component)val).GetComponent<CharacterBackpackHandler>();
			if ((UnityEngine.Object)(object)component == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("changebackpack: CharacterBackpackHandler not found for " + photonPlayer.NickName));
				return;
			}
			if (itemName.Equals("EMPTY", StringComparison.OrdinalIgnoreCase))
			{
				Debug.Log((object)$"changebackpack: Removing item from slot {backpackSlotIndex} not yet implemented via RPC");
				return;
			}
			ItemDatabase instance = SingletonAsset<ItemDatabase>.Instance;
			if (instance?.itemLookup == null)
			{
				Debug.LogWarning((object)"changebackpack: ItemDatabase not found");
				return;
			}
			KeyValuePair<ushort, Item> keyValuePair = instance.itemLookup.Where((KeyValuePair<ushort, Item> kvp) => ((UnityEngine.Object)kvp.Value).name.Equals(itemName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
			if ((UnityEngine.Object)(object)keyValuePair.Value == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("changebackpack: Item '" + itemName + "' not found in database"));
				return;
			}
			ItemInstanceData val2 = new ItemInstanceData(Guid.NewGuid());
			ItemInstanceDataHandler.AddInstanceData(val2);
			if (PhotonNetwork.IsMasterClient)
			{
				component.photonView.RPC("RPCAddItemToCharacterBackpack", (RpcTarget)0, new object[3]
				{
					keyValuePair.Key,
					val2,
					(byte)backpackSlotIndex
				});
				Debug.Log((object)string.Format("changebackpack: Sent RPC to add '{0}' (ID: {1}) to slot {2} for {3}", new object[4] { itemName, keyValuePair.Key, backpackSlotIndex, photonPlayer.NickName }));
				Plugin.Log.LogInfo((object)$"Requested backpack slot {backpackSlotIndex} change to '{itemName}' for {photonPlayer.NickName}");
			}
			else
			{
				Debug.LogWarning((object)"changebackpack: Only master client can modify backpacks");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("changebackpack: Error - " + ex.Message));
			Debug.LogError((object)("Stack trace: " + ex.StackTrace));
		}
	}

	private void DropCoconutOnPlayer(PhotonPlayer player)
	{
		try
		{
			if (!PhotonNetwork.IsMasterClient && !MasterClientUtils.IsBypassEnabled)
			{
				Debug.LogWarning((object)"dropcoconut: Only Master Client can drop coconuts!");
				return;
			}
			FunCommands.DropCoconutOnPlayer(player);
			Debug.Log((object)("dropcoconut: Dropped coconut on '" + player.NickName + "' from UI"));
			Plugin.Log.LogInfo((object)("Dropped coconut on player from UI: " + player.NickName));
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("dropcoconut: Error - " + ex.Message));
		}
	}

	private void SpawnBeesOnPlayer(PhotonPlayer player)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!PhotonNetwork.IsMasterClient && !MasterClientUtils.IsBypassEnabled)
			{
				Debug.LogWarning((object)"spawnbees: Only Master Client can spawn bees!");
				return;
			}
			Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)player) ?? false);
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("spawnbees: no Character for '" + player.NickName + "'"));
				return;
			}
			Vector3 val2 = val.Center + Vector3.up * 1f;
			try
			{
				GameObject val3 = PhotonNetwork.Instantiate("BeeSwarm", val2, Quaternion.identity, (byte)0, (object[])null);
				if ((UnityEngine.Object)(object)val3 != (UnityEngine.Object)null)
				{
					Debug.Log((object)("spawnbees: Successfully spawned BeeSwarm on '" + player.NickName + "' from UI"));
					Plugin.Log.LogInfo((object)("Spawned BeeSwarm on player from UI: " + player.NickName));
				}
				else
				{
					Debug.LogError((object)("spawnbees: Failed to spawn BeeSwarm on '" + player.NickName + "'"));
				}
			}
			catch (Exception ex)
			{
				Debug.LogError((object)("spawnbees: Spawn error - " + ex.Message));
			}
		}
		catch (Exception ex2)
		{
			Debug.LogError((object)("spawnbees: Error - " + ex2.Message));
		}
	}

	private void GiveBackpackToPlayer(PhotonPlayer player)
	{
		try
		{
			if (!PhotonNetwork.IsMasterClient && !MasterClientUtils.IsBypassEnabled)
			{
				Debug.LogWarning((object)"givebackpack: Only Master Client can give backpacks!");
				return;
			}
			SpawnCommands.GiveItemByName(player, "Backpack");
			Debug.Log((object)("givebackpack: Gave backpack to '" + player.NickName + "' from UI"));
			Plugin.Log.LogInfo((object)("Gave backpack to player from UI: " + player.NickName));
			RefreshInfo();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("givebackpack: Error - " + ex.Message));
			Plugin.Log.LogError((object)("Failed to give backpack to player from UI: " + ex.Message));
		}
	}

	private void ClearPlayerBackpack(PhotonPlayer player)
	{
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		try
		{
			if (!PhotonNetwork.IsMasterClient && !MasterClientUtils.IsBypassEnabled)
			{
				Debug.LogWarning((object)"clearbackpack: Only Master Client can clear backpacks!");
				return;
			}
			Player player2 = PlayerHandler.GetPlayer(player);
			BackpackData val = default(BackpackData);
			if ((UnityEngine.Object)(object)player2 == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("clearbackpack: Player component not found for " + player.NickName));
			}
			else if (((ItemSlot)player2.backpackSlot).IsEmpty())
			{
				Debug.LogWarning((object)("clearbackpack: " + player.NickName + " has no backpack equipped"));
			}
			else if (((ItemSlot)player2.backpackSlot).data.TryGetDataEntry<BackpackData>((DataEntryKey)7, out val) && val?.itemSlots != null)
			{
				for (int i = 0; i < val.itemSlots.Length; i++)
				{
					if (val.itemSlots[i] != null)
					{
						val.itemSlots[i].prefab = null;
						val.itemSlots[i].data = new ItemInstanceData(Guid.NewGuid());
					}
				}
				Debug.Log((object)("clearbackpack: Cleared backpack for '" + player.NickName + "' from UI"));
				Plugin.Log.LogInfo((object)("Cleared backpack for player from UI: " + player.NickName));
				try
				{
					Character val2 = Character.AllCharacters.FirstOrDefault(delegate(Character c)
					{
						PhotonPlayer owner = ((MonoBehaviourPun)c).photonView.Owner;
						return ((owner != null) ? new int?(owner.ActorNumber) : ((int?)null)) == player.ActorNumber;
					});
					if ((UnityEngine.Object)(object)val2 != (UnityEngine.Object)null)
					{
						CharacterBackpackHandler component = ((Component)val2).GetComponent<CharacterBackpackHandler>();
						if ((UnityEngine.Object)(object)component?.backpackVisuals != (UnityEngine.Object)null && PhotonNetwork.IsMasterClient)
						{
							((BackpackVisuals)component.backpackVisuals).RefreshVisuals();
						}
						if (val2.IsLocal)
						{
							val2.refs.afflictions.UpdateWeight();
						}
					}
				}
				catch (Exception ex)
				{
					Debug.LogWarning((object)("clearbackpack: Error refreshing visuals - " + ex.Message));
				}
				RefreshInfo();
			}
			else
			{
				Debug.LogWarning((object)("clearbackpack: No BackpackData found for " + player.NickName));
			}
		}
		catch (Exception ex2)
		{
			Debug.LogError((object)("clearbackpack: Error - " + ex2.Message));
			Plugin.Log.LogError((object)("Failed to clear backpack for player from UI: " + ex2.Message));
		}
	}

	private void KillPlayerFromUI(PhotonPlayer player)
	{
		try
		{
			if (!PhotonNetwork.IsMasterClient && !MasterClientUtils.IsBypassEnabled)
			{
				Debug.LogWarning((object)"killplayer: Only Master Client can kill players!");
				return;
			}
			if (player == null || player.IsLocal)
			{
				Debug.LogWarning((object)"killplayer: Cannot kill yourself or invalid player!");
				return;
			}
			PlayerEffectCommands.KillPlayer(player);
			Debug.Log((object)("killplayer: Killed '" + player.NickName + "' from UI"));
			RefreshInfo();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("killplayer: Error - " + ex.Message));
			Plugin.Log.LogError((object)("Failed to kill player from UI: " + ex.Message));
		}
	}

	private void RevivePlayerFromUI(PhotonPlayer player)
	{
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!PhotonNetwork.IsMasterClient && !MasterClientUtils.IsBypassEnabled)
			{
				Debug.LogWarning((object)"reviveplayer: Only Master Client can revive players!");
				return;
			}
			if (player == null)
			{
				Debug.LogWarning((object)"reviveplayer: Invalid player!");
				return;
			}
			Character targetCharacter = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)player) ?? false);
			if ((UnityEngine.Object)(object)targetCharacter == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("reviveplayer: character not found for player '" + player.NickName + "'"));
				return;
			}
			if (!targetCharacter.data.dead)
			{
				Debug.LogWarning((object)("reviveplayer: player '" + player.NickName + "' is already alive!"));
				return;
			}
			((MonoBehaviourPun)targetCharacter).photonView.RPC("RPCA_Revive", (RpcTarget)0, new object[1] { false });
			List<Character> list = Character.AllCharacters.Where((Character c) => (UnityEngine.Object)(object)c != (UnityEngine.Object)(object)targetCharacter && !c.data.dead && ((MonoBehaviourPun)c).photonView.Owner != null).ToList();
			Vector3 val2 = default(Vector3);
			if (list.Count > 0)
			{
				Character val = list[UnityEngine.Random.Range(0, list.Count)];
				val2 = val.Center + Vector3.up * 2f;
				Debug.Log((object)("reviveplayer: Reviving '" + player.NickName + "' near alive player '" + ((MonoBehaviourPun)val).photonView.Owner.NickName + "'"));
			}
			else
			{
				SpawnPoint[] array = UnityEngine.Object.FindObjectsByType<SpawnPoint>((FindObjectsInactive)1, (FindObjectsSortMode)0);
				if (array.Length != 0)
				{
					SpawnPoint val3 = array[UnityEngine.Random.Range(0, array.Length)];
					val2 = ((Component)val3).transform.position + Vector3.up * 1f;
					Debug.Log((object)("reviveplayer: Reviving '" + player.NickName + "' at spawn point"));
				}
				else
				{
					val2 = new Vector3(0f, 10f, 0f);
					Debug.Log((object)("reviveplayer: Reviving '" + player.NickName + "' at default position (no spawn points found)"));
				}
			}
			((MonoBehaviourPun)targetCharacter).photonView.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { val2, true });
			Debug.Log((object)("reviveplayer: Revived and teleported '" + player.NickName + "' from UI"));
			Plugin.Log.LogInfo((object)("Revived and teleported player: " + player.NickName));
			RefreshInfo();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("reviveplayer: Error - " + ex.Message));
			Plugin.Log.LogError((object)("Failed to revive player from UI: " + ex.Message));
		}
	}

	private void HealPlayerFromUI(PhotonPlayer player)
	{
		try
		{
			if (!PhotonNetwork.IsMasterClient && !MasterClientUtils.IsBypassEnabled)
			{
				Debug.LogWarning((object)"healplayer: Only Master Client can heal players!");
				return;
			}
			if (player == null)
			{
				Debug.LogWarning((object)"healplayer: Invalid player!");
				return;
			}
			PlayerEffectCommands.HealPlayer(player);
			Debug.Log((object)("healplayer: Healed '" + player.NickName + "' from UI"));
			RefreshInfo();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("healplayer: Error - " + ex.Message));
			Plugin.Log.LogError((object)("Failed to heal player from UI: " + ex.Message));
		}
	}

	private void PassOutPlayerFromUI(PhotonPlayer player)
	{
		try
		{
			if (!PhotonNetwork.IsMasterClient && !MasterClientUtils.IsBypassEnabled)
			{
				Debug.LogWarning((object)"passoutplayer: Only Master Client can make players pass out!");
				return;
			}
			if (player == null || player.IsLocal)
			{
				Debug.LogWarning((object)"passoutplayer: Cannot make yourself pass out or invalid player!");
				return;
			}
			RPCCommands.PassOutPlayer(player);
			Debug.Log((object)("passoutplayer: Made '" + player.NickName + "' pass out from UI"));
			RefreshInfo();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("passoutplayer: Error - " + ex.Message));
			Plugin.Log.LogError((object)("Failed to make player pass out from UI: " + ex.Message));
		}
	}
}
