using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Zorro.Core.CLI;

namespace AdvancedConsole;

public class MapSelectionPage : DebugPage
{
	private DropdownField _mapDropdown;

	private Button _changeMapButton;

	private Button _forceChangeMapButton;

	private Label _statusLabel;

	private Label _currentMapLabel;

	private readonly Dictionary<string, string> _availableScenes = LobbyCommands.GetAvailableScenes();

	public MapSelectionPage()
	{
		((VisualElement)this).name = "Map Selection";
		SetupPage();
		UpdateUI();
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
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Expected O, but got Unknown
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Expected O, but got Unknown
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Expected O, but got Unknown
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Expected O, but got Unknown
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Expected O, but got Unknown
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Expected O, but got Unknown
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_067d: Expected O, but got Unknown
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0701: Unknown result type (might be due to invalid IL or missing references)
		//IL_070b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_072b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		//IL_0740: Unknown result type (might be due to invalid IL or missing references)
		//IL_074a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_076a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0776: Expected O, but got Unknown
		//IL_077b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0780: Unknown result type (might be due to invalid IL or missing references)
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0795: Unknown result type (might be due to invalid IL or missing references)
		//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07af: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cc: Expected O, but got Unknown
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0809: Unknown result type (might be due to invalid IL or missing references)
		//IL_080e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0818: Unknown result type (might be due to invalid IL or missing references)
		//IL_081f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0829: Unknown result type (might be due to invalid IL or missing references)
		//IL_0834: Unknown result type (might be due to invalid IL or missing references)
		//IL_0840: Expected O, but got Unknown
		Label val = new Label("地图选择");
		((VisualElement)val).style.fontSize = new StyleLength(20f);
		((VisualElement)val).style.unityFontStyleAndWeight = new StyleEnum<FontStyle>((FontStyle)1);
		((VisualElement)val).style.color = new StyleColor(new Color(0.2f, 0.8f, 1f));
		((VisualElement)val).style.marginBottom = new StyleLength(8f);
		((VisualElement)val).style.marginTop = new StyleLength(3f);
		Label val2 = val;
		((VisualElement)this).Add((VisualElement)(object)val2);
		Label val3 = new Label("当前地图：加载中...");
		((VisualElement)val3).style.fontSize = new StyleLength(14f);
		((VisualElement)val3).style.color = new StyleColor(new Color(0.9f, 0.9f, 0.9f));
		((VisualElement)val3).style.marginBottom = new StyleLength(10f);
		((VisualElement)val3).style.backgroundColor = new StyleColor(new Color(0.1f, 0.1f, 0.1f, 0.8f));
		((VisualElement)val3).style.paddingTop = new StyleLength(6f);
		((VisualElement)val3).style.paddingBottom = new StyleLength(6f);
		((VisualElement)val3).style.paddingLeft = new StyleLength(8f);
		((VisualElement)val3).style.paddingRight = new StyleLength(8f);
		((VisualElement)val3).style.borderTopLeftRadius = new StyleLength(4f);
		((VisualElement)val3).style.borderTopRightRadius = new StyleLength(4f);
		((VisualElement)val3).style.borderBottomLeftRadius = new StyleLength(4f);
		((VisualElement)val3).style.borderBottomRightRadius = new StyleLength(4f);
		_currentMapLabel = val3;
		((VisualElement)this).Add((VisualElement)(object)_currentMapLabel);
		VisualElement val4 = new VisualElement();
		val4.style.backgroundColor = new StyleColor(new Color(0.15f, 0.15f, 0.15f, 0.8f));
		val4.style.paddingTop = new StyleLength(12f);
		val4.style.paddingBottom = new StyleLength(12f);
		val4.style.paddingLeft = new StyleLength(10f);
		val4.style.paddingRight = new StyleLength(10f);
		val4.style.borderTopLeftRadius = new StyleLength(6f);
		val4.style.borderTopRightRadius = new StyleLength(6f);
		val4.style.borderBottomLeftRadius = new StyleLength(6f);
		val4.style.borderBottomRightRadius = new StyleLength(6f);
		val4.style.marginBottom = new StyleLength(12f);
		VisualElement val5 = val4;
		List<string> list = _availableScenes.Select<KeyValuePair<string, string>, string>((KeyValuePair<string, string> kvp) => kvp.Value + " (" + kvp.Key + ")").ToList();
		DropdownField val6 = new DropdownField("选择地图：", list, 0, (Func<string, string>)null, (Func<string, string>)null);
		((VisualElement)val6).style.marginBottom = new StyleLength(10f);
		((VisualElement)val6).style.color = new StyleColor(new Color(0.95f, 0.95f, 0.95f));
		((VisualElement)val6).style.backgroundColor = new StyleColor(new Color(0.1f, 0.1f, 0.1f));
		_mapDropdown = val6;
		val5.Add((VisualElement)(object)_mapDropdown);
		VisualElement val7 = new VisualElement();
		val7.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)2);
		val7.style.justifyContent = new StyleEnum<Justify>((Justify)3);
		val7.style.marginBottom = new StyleLength(10f);
		VisualElement val8 = val7;
		Button val9 = new Button((Action)OnChangeMapClicked)
		{
			text = "切换地图"
		};
		((VisualElement)val9).style.height = new StyleLength(30f);
		((VisualElement)val9).style.flexGrow = new StyleFloat(1f);
		((VisualElement)val9).style.marginRight = new StyleLength(5f);
		((VisualElement)val9).style.backgroundColor = new StyleColor(new Color(0.2f, 0.7f, 0.2f));
		((VisualElement)val9).style.color = new StyleColor(Color.white);
		((VisualElement)val9).style.unityFontStyleAndWeight = new StyleEnum<FontStyle>((FontStyle)1);
		((VisualElement)val9).style.fontSize = new StyleLength(14f);
		_changeMapButton = val9;
		val8.Add((VisualElement)(object)_changeMapButton);
		Button val10 = new Button((Action)OnForceChangeMapClicked)
		{
			text = "强制切换"
		};
		((VisualElement)val10).style.height = new StyleLength(30f);
		((VisualElement)val10).style.flexGrow = new StyleFloat(1f);
		((VisualElement)val10).style.marginLeft = new StyleLength(5f);
		((VisualElement)val10).style.backgroundColor = new StyleColor(new Color(0.8f, 0.4f, 0.2f));
		((VisualElement)val10).style.color = new StyleColor(Color.white);
		((VisualElement)val10).style.unityFontStyleAndWeight = new StyleEnum<FontStyle>((FontStyle)1);
		((VisualElement)val10).style.fontSize = new StyleLength(14f);
		_forceChangeMapButton = val10;
		val8.Add((VisualElement)(object)_forceChangeMapButton);
		val5.Add(val8);
		((VisualElement)this).Add(val5);
		Label val11 = new Label("");
		((VisualElement)val11).style.fontSize = new StyleLength(12f);
		((VisualElement)val11).style.color = new StyleColor(new Color(0.8f, 0.8f, 0.8f));
		((VisualElement)val11).style.marginTop = new StyleLength(8f);
		((VisualElement)val11).style.whiteSpace = new StyleEnum<WhiteSpace>((WhiteSpace)0);
		((VisualElement)val11).style.backgroundColor = new StyleColor(new Color(0.1f, 0.1f, 0.1f, 0.6f));
		((VisualElement)val11).style.paddingTop = new StyleLength(6f);
		((VisualElement)val11).style.paddingBottom = new StyleLength(6f);
		((VisualElement)val11).style.paddingLeft = new StyleLength(8f);
		((VisualElement)val11).style.paddingRight = new StyleLength(8f);
		((VisualElement)val11).style.borderTopLeftRadius = new StyleLength(3f);
		((VisualElement)val11).style.borderTopRightRadius = new StyleLength(3f);
		((VisualElement)val11).style.borderBottomLeftRadius = new StyleLength(3f);
		((VisualElement)val11).style.borderBottomRightRadius = new StyleLength(3f);
		_statusLabel = val11;
		((VisualElement)this).Add((VisualElement)(object)_statusLabel);
		VisualElement val12 = new VisualElement();
		val12.style.marginTop = new StyleLength(16f);
		val12.style.backgroundColor = new StyleColor(new Color(0.2f, 0.2f, 0.3f, 0.8f));
		val12.style.paddingTop = new StyleLength(8f);
		val12.style.paddingBottom = new StyleLength(8f);
		val12.style.paddingLeft = new StyleLength(10f);
		val12.style.paddingRight = new StyleLength(10f);
		val12.style.borderTopLeftRadius = new StyleLength(4f);
		val12.style.borderTopRightRadius = new StyleLength(4f);
		val12.style.borderBottomLeftRadius = new StyleLength(4f);
		val12.style.borderBottomRightRadius = new StyleLength(4f);
		VisualElement val13 = val12;
		Label val14 = new Label("(i) 只有房主(Master Client)在大厅或房间中才能切换地图。");
		((VisualElement)val14).style.fontSize = new StyleLength(11f);
		((VisualElement)val14).style.color = new StyleColor(new Color(0.7f, 0.7f, 0.9f));
		((VisualElement)val14).style.whiteSpace = new StyleEnum<WhiteSpace>((WhiteSpace)0);
		Label val15 = val14;
		val13.Add((VisualElement)(object)val15);
		Label val16 = new Label("! 地图切换使用 Steam 大厅数据，并以 Photon 作为后备。");
		((VisualElement)val16).style.fontSize = new StyleLength(11f);
		((VisualElement)val16).style.color = new StyleColor(new Color(0.7f, 0.9f, 0.7f));
		((VisualElement)val16).style.whiteSpace = new StyleEnum<WhiteSpace>((WhiteSpace)0);
		((VisualElement)val16).style.marginTop = new StyleLength(4f);
		Label val17 = val16;
		val13.Add((VisualElement)(object)val17);
		((VisualElement)this).Add(val13);
	}

	private void UpdateUI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		string text = "未知";
		try
		{
			SceneManager.GetActiveScene();
			Scene activeScene = SceneManager.GetActiveScene();
			text = activeScene.name;
		}
		catch
		{
			text = "无法检测";
		}
		((TextElement)_currentMapLabel).text = "当前地图：" + text;
		bool enabled = MasterClientUtils.IsMasterClient() && (PhotonNetwork.InLobby || PhotonNetwork.InRoom);
		((VisualElement)_changeMapButton).SetEnabled(enabled);
		((VisualElement)_forceChangeMapButton).SetEnabled(enabled);
		if (!PhotonNetwork.IsConnected)
		{
			((TextElement)_statusLabel).text = "[X] 未连接到网络";
			((VisualElement)_statusLabel).style.color = new StyleColor(new Color(1f, 0.4f, 0.4f));
		}
		else if (!PhotonNetwork.InLobby && !PhotonNetwork.InRoom)
		{
			((TextElement)_statusLabel).text = "[X] 不在大厅或房间中";
			((VisualElement)_statusLabel).style.color = new StyleColor(new Color(1f, 0.6f, 0.2f));
		}
		else if (!MasterClientUtils.IsMasterClient())
		{
			((TextElement)_statusLabel).text = "[X] 只有房主才能切换地图";
			((VisualElement)_statusLabel).style.color = new StyleColor(new Color(1f, 0.6f, 0.2f));
		}
		else
		{
			((TextElement)_statusLabel).text = "[+] 已准备好切换地图";
			((VisualElement)_statusLabel).style.color = new StyleColor(new Color(0.2f, 0.8f, 0.2f));
		}
	}

	private void OnChangeMapClicked()
	{
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!MasterClientUtils.IsMasterClient())
			{
				UpdateStatus("[X] 只有房主才能切换地图！", new Color(1f, 0.4f, 0.4f));
				return;
			}
			if (!PhotonNetwork.InLobby && !PhotonNetwork.InRoom)
			{
				UpdateStatus("[X] 你必须在大厅或房间中才能切换地图！", new Color(1f, 0.4f, 0.4f));
				return;
			}
			string value = ((BaseField<string>)(object)_mapDropdown).value;
			if (string.IsNullOrEmpty(value))
			{
				UpdateStatus("[X] 请先选择一个地图！", new Color(1f, 0.4f, 0.4f));
				return;
			}
			string text = ExtractMapKeyFromOption(value);
			if (string.IsNullOrEmpty(text) || !_availableScenes.ContainsKey(text))
			{
				UpdateStatus("[X] 无效的地图选择！", new Color(1f, 0.4f, 0.4f));
				return;
			}
			string text2 = _availableScenes[text];
			UpdateStatus("[R] 正在切换到地图 " + text2 + "...", new Color(0.2f, 0.8f, 1f));
			LobbyCommands.SetMap(text);
			Plugin.Log.LogInfo((object)("地图选择界面：已切换到地图 " + text2));
		}
		catch (Exception ex)
		{
			UpdateStatus("[X] 切换地图出错：" + ex.Message, new Color(1f, 0.4f, 0.4f));
			Plugin.Log.LogError((object)("地图选择界面：切换地图出错 - " + ex.Message));
		}
	}

	private string ExtractMapKeyFromOption(string option)
	{
		int num = option.LastIndexOf('(');
		int num2 = option.LastIndexOf(')');
		if (num >= 0 && num2 > num)
		{
			return option.Substring(num + 1, num2 - num - 1).Trim();
		}
		return string.Empty;
	}

	private void OnForceChangeMapClicked()
	{
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!MasterClientUtils.IsMasterClient())
			{
				UpdateStatus("[X] 只有房主才能强制切换地图！", new Color(1f, 0.4f, 0.4f));
				return;
			}
			string value = ((BaseField<string>)(object)_mapDropdown).value;
			if (string.IsNullOrEmpty(value))
			{
				UpdateStatus("[X] 请先选择一个地图！", new Color(1f, 0.4f, 0.4f));
				return;
			}
			string text = ExtractMapKeyFromOption(value);
			if (string.IsNullOrEmpty(text) || !_availableScenes.ContainsKey(text))
			{
				UpdateStatus("[X] 无效的地图选择！", new Color(1f, 0.4f, 0.4f));
				return;
			}
			string text2 = _availableScenes[text];
			UpdateStatus("[!] 正在强制切换到地图 " + text2 + "...", new Color(1f, 0.6f, 0.2f));
			if (LobbyCommands.ForceChangeMap(text))
			{
				UpdateStatus("[✓] 已发起强制切换地图：" + text2, new Color(0.2f, 1f, 0.2f));
				Plugin.Log.LogInfo((object)("地图选择界面：已强制切换到地图 " + text2));
			}
			else
			{
				UpdateStatus("[X] 强制切换地图失败！", new Color(1f, 0.4f, 0.4f));
			}
		}
		catch (Exception ex)
		{
			UpdateStatus("[X] 强制切换地图出错：" + ex.Message, new Color(1f, 0.4f, 0.4f));
			Plugin.Log.LogError((object)("地图选择界面：强制切换地图出错 - " + ex.Message));
		}
	}

	private void UpdateStatus(string message, Color color)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		((TextElement)_statusLabel).text = message;
		((VisualElement)_statusLabel).style.color = new StyleColor(color);
	}

	public void OnShowPage()
	{
		UpdateUI();
	}

	public void RefreshUI()
	{
		UpdateUI();
	}
}
