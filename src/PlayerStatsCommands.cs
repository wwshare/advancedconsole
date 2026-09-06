using System;
using System.Reflection;
using UnityEngine;
using Zorro.Core.CLI;

namespace AdvancedConsole;

public static class PlayerStatsCommands
{
	[ConsoleCommand]
	public static void SetSpeed(float speed = 6f)
	{
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"setspeed: 未找到本地角色!");
			return;
		}
		try
		{
			CharacterMovement component = ((Component)localCharacter).GetComponent<CharacterMovement>();
			if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null)
			{
				component.movementModifier = speed / 5f;
				Debug.Log((object)$"setspeed: 速度修正值已设置为 {component.movementModifier:F2}");
			}
			else
			{
				Debug.LogWarning((object)"setspeed: 未找到 CharacterMovement 组件!");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("setspeed: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void SetJumpPower(float jumpPower = 8f)
	{
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"setjump: 未找到本地角色!");
			return;
		}
		try
		{
			CharacterMovement component = ((Component)localCharacter).GetComponent<CharacterMovement>();
			if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null)
			{
				component.jumpImpulse = jumpPower;
				Debug.Log((object)$"setjump: 跳跃冲量已设置为 {jumpPower}");
			}
			else
			{
				Debug.LogWarning((object)"setjump: 未找到 CharacterMovement 组件!");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("setjump: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void SetClimbSpeed(float climbSpeed = 4f)
	{
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"setclimb: 未找到本地角色!");
			return;
		}
		try
		{
			CharacterMovement component = ((Component)localCharacter).GetComponent<CharacterMovement>();
			if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null)
			{
				FieldInfo field = ((object)component).GetType().GetField("climbSpeed");
				if (field != null)
				{
					field.SetValue(component, climbSpeed);
					Debug.Log((object)$"setclimb: 攀爬速度已设置为 {climbSpeed}");
				}
				else
				{
					Debug.LogWarning((object)"setclimb: 未找到攀爬速度字段!");
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("setclimb: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void SetThrowPower(float throwPower = 10f)
	{
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"setthrow: 未找到本地角色!");
			return;
		}
		try
		{
			Component[] components = ((Component)localCharacter).GetComponents<Component>();
			bool flag = false;
			Component[] array = components;
			foreach (Component val in array)
			{
				FieldInfo fieldInfo = ((object)val).GetType().GetField("throwForce") ?? ((object)val).GetType().GetField("throwPower") ?? ((object)val).GetType().GetField("itemThrowForce");
				if (fieldInfo != null)
				{
					fieldInfo.SetValue(val, throwPower);
					Debug.Log((object)$"setthrow: 投掷力度已设置为 {throwPower} (组件: {((object)val).GetType().Name})");
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				Debug.LogWarning((object)"setthrow: 未找到投掷组件! 此功能可能不可用。");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("setthrow: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void RestoreStamina()
	{
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"restorestamina: 未找到本地角色!");
			return;
		}
		localCharacter.data.currentStamina = 100f;
		Debug.Log((object)"restorestamina: 耐力已完全恢复");
	}

	[ConsoleCommand]
	public static void SetHealth(float health = 100f)
	{
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"sethealth: 未找到本地角色!");
			return;
		}
		health = Mathf.Clamp(health, 0f, 100f);
		Component[] components = ((Component)localCharacter).GetComponents<Component>();
		bool flag = false;
		Component[] array = components;
		foreach (Component val in array)
		{
			FieldInfo fieldInfo = ((object)val).GetType().GetField("currentHealth") ?? ((object)val).GetType().GetField("health") ?? ((object)val).GetType().GetField("hp");
			if (fieldInfo != null)
			{
				try
				{
					fieldInfo.SetValue(val, health);
					Debug.Log((object)$"sethealth: 生命已设置为 {health} (组件: {((object)val).GetType().Name})");
					flag = true;
				}
				catch (Exception ex)
				{
					Debug.LogError((object)("sethealth: 在 " + ((object)val).GetType().Name + " - " + ex.Message));
					continue;
				}
				break;
			}
		}
		if (!flag)
		{
			Debug.LogWarning((object)"sethealth: 未找到生命组件! 此功能可能不可用。");
		}
	}

	[ConsoleCommand]
	public static void SetGodMode(bool enabled = true)
	{
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"godmode: 未找到本地角色!");
			return;
		}
		if (enabled)
		{
			localCharacter.data.currentStamina = 100f;
			try
			{
				FieldInfo fieldInfo = ((object)localCharacter.data).GetType().GetField("invulnerable") ?? ((object)localCharacter).GetType().GetField("invulnerable");
				if (fieldInfo != null)
				{
					fieldInfo.SetValue(localCharacter.data, true);
				}
				FieldInfo field = ((object)localCharacter).GetType().GetField("infiniteStam");
				if (field != null)
				{
					field.SetValue(localCharacter, true);
				}
			}
			catch
			{
			}
			Debug.Log((object)"godmode: 无敌模式已启用 (部分支持)");
			return;
		}
		try
		{
			FieldInfo fieldInfo2 = ((object)localCharacter.data).GetType().GetField("invulnerable") ?? ((object)localCharacter).GetType().GetField("invulnerable");
			if (fieldInfo2 != null)
			{
				fieldInfo2.SetValue(localCharacter.data, false);
			}
			FieldInfo field2 = ((object)localCharacter).GetType().GetField("infiniteStam");
			if (field2 != null)
			{
				field2.SetValue(localCharacter, false);
			}
		}
		catch
		{
		}
		Debug.Log((object)"godmode: 无敌模式已禁用");
	}

	[ConsoleCommand]
	public static void ShowStats()
	{
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"showstats: 未找到本地角色!");
			return;
		}
		Debug.Log((object)"=== 玩家状态 ===");
		Debug.Log((object)$"当前耐力: {localCharacter.data.currentStamina}");
		Debug.Log((object)$"总耐力: {localCharacter.data.TotalStamina}");
		Debug.Log((object)$"死亡: {localCharacter.data.dead}");
		Debug.Log((object)$"晕厥: {localCharacter.data.passedOut}");
		Debug.Log((object)$"完全晕厥: {localCharacter.data.fullyPassedOut}");
		Debug.Log((object)$"是否冲刺: {localCharacter.data.isSprinting}");
		Debug.Log((object)$"是否跳跃: {localCharacter.data.isJumping}");
		Debug.Log((object)$"是否攀爬: {localCharacter.data.isClimbing}");
		Debug.Log((object)$"是否着地: {localCharacter.data.isGrounded}");
		CharacterMovement component = ((Component)localCharacter).GetComponent<CharacterMovement>();
		if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null)
		{
			Debug.Log((object)$"移动修正值: {component.movementModifier}");
			Debug.Log((object)$"跳跃冲量: {component.jumpImpulse}");
		}
		Debug.Log((object)"==================");
	}
}
