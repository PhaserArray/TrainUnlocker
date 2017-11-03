using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;

namespace PhaserArray.TrainUnlocker
{
	public class TrainUnlockerConfiguration : IRocketPluginConfiguration
	{
		public bool UnlockOccupied;

		public void LoadDefaults()
		{
			UnlockOccupied = true;
		}
	}
}
