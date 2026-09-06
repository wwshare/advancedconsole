using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using Zorro.Core.CLI;

using PhotonPlayer = Photon.Realtime.Player;
namespace AdvancedConsole;

public class PlayerCLIParser : CLITypeParser
{
	public override object? Parse(string raw)
	{
		string name = raw.Replace("_", " ");
		return PhotonNetwork.PlayerList.FirstOrDefault((PhotonPlayer p) => p.NickName.Equals(name, StringComparison.OrdinalIgnoreCase));
	}

	public override List<ParameterAutocomplete> FindAutocomplete(string textSoFar)
	{
		return (from p in PhotonNetwork.PlayerList
			select p.NickName.Replace(" ", "_") into n
			where n.StartsWith(textSoFar, StringComparison.OrdinalIgnoreCase)
			select n).Select((Func<string, ParameterAutocomplete>)delegate(string n)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Expected O, but got Unknown
			return new ParameterAutocomplete(n);
		}).ToList();
	}
}
