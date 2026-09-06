using System.Collections.Generic;

namespace AdvancedConsole;

public class LuggageSize
{
	public string Size { get; set; } = "";

	public string Description { get; set; } = "";

	public override string ToString()
	{
		return Size;
	}

	public static implicit operator string(LuggageSize size)
	{
		return size.Size;
	}

	public static List<LuggageSize> GetAllSizes()
	{
		return new List<LuggageSize>
		{
			new LuggageSize
			{
				Size = "small",
				Description = "Small luggage - обычный маленький чемодан"
			},
			new LuggageSize
			{
				Size = "big",
				Description = "Big luggage - большой чемодан"
			},
			new LuggageSize
			{
				Size = "epic",
				Description = "Epic luggage - эпический чемодан"
			},
			new LuggageSize
			{
				Size = "ancient",
				Description = "Ancient luggage - древний чемодан"
			}
		};
	}
}
