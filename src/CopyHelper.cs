using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace AdvancedConsole;

/// <summary>
/// 右键自动复制: 在页面根元素上注册右键事件, 复制光标下 Label/TextElement 的文本。
/// </summary>
public static class CopyHelper
{
	public static void AttachCopyOnRightClick(VisualElement root)
	{
		try
		{
			root.RegisterCallback<ContextClickEvent>(delegate(ContextClickEvent evt)
			{
				try
				{
					string text = FindText(evt.target as VisualElement);
					if (!string.IsNullOrEmpty(text))
					{
						GUIUtility.systemCopyBuffer = text;
						Debug.Log((object)("[AdvancedConsole] 已复制: " + Truncate(text)));
					}
				}
				catch (Exception ex)
				{
					Debug.LogWarning((object)("右键复制失败: " + ex.Message));
				}
			}, (TrickleDown)0);
		}
		catch (Exception ex)
		{
			Plugin.Log.LogDebug((object)("AttachCopyOnRightClick 失败: " + ex.Message));
		}
	}

	private static string FindText(VisualElement element)
	{
		VisualElement val = element;
		while (val != null)
		{
			if (val is Label label && !string.IsNullOrEmpty(label.text))
			{
				return label.text;
			}
			if (val is TextElement textElement && !string.IsNullOrEmpty(textElement.text))
			{
				return textElement.text;
			}
			val = val.parent;
		}
		return null;
	}

	private static string Truncate(string s)
	{
		if (s.Length <= 60)
		{
			return s;
		}
		return s.Substring(0, 60) + "...";
	}
}
