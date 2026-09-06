using System;

namespace AdvancedConsole;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class MasterClientOnlyAttribute : Attribute
{
	public string CommandName { get; }

	public bool WarnOnly { get; set; }

	public MasterClientOnlyAttribute(string? commandName = null)
	{
		CommandName = commandName ?? string.Empty;
	}
}
