using System;
using System.Collections.Generic;
using System.Linq;
using Zorro.Core.CLI;

namespace AdvancedConsole;

public class AchievementTypeCLIParser : CLITypeParser
{
	public override object? Parse(string raw)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (Enum.TryParse<ACHIEVEMENTTYPE>(raw, true, out ACHIEVEMENTTYPE result))
		{
			return result;
		}
		return null;
	}

	public override List<ParameterAutocomplete> FindAutocomplete(string textSoFar)
	{
		return (from name in Enum.GetNames(typeof(ACHIEVEMENTTYPE))
			where name != "NONE"
			where name.StartsWith(textSoFar, StringComparison.OrdinalIgnoreCase)
			select name).Select((Func<string, ParameterAutocomplete>)delegate(string name)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Expected O, but got Unknown
			return new ParameterAutocomplete(name);
		}).ToList();
	}
}
