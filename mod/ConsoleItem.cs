using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zorro.Core;

namespace AdvancedConsole;

public class ConsoleItem
{
	public ushort ID { get; set; }

	public string Name { get; set; } = "";

	public Item ItemData { get; set; }

	public override string ToString()
	{
		return Name;
	}

	public static implicit operator ushort(ConsoleItem item)
	{
		return item.ID;
	}

	public static List<ConsoleItem> GetAllItems()
	{
		List<ConsoleItem> list = new List<ConsoleItem>();
		ItemDatabase instance = SingletonAsset<ItemDatabase>.Instance;
		if (instance?.itemLookup != null)
		{
			foreach (KeyValuePair<ushort, Item> item in instance.itemLookup.OrderBy((KeyValuePair<ushort, Item> x) => ((Object)x.Value).name))
			{
				list.Add(new ConsoleItem
				{
					ID = item.Key,
					Name = ((Object)item.Value).name,
					ItemData = item.Value
				});
			}
		}
		return list;
	}
}
