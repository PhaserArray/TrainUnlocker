﻿using Rocket.API;

namespace PhaserArray.TrainUnlocker
{
	public class TrainUnlockerConfiguration : IRocketPluginConfiguration
	{
		public float UnlockInterval;
		public bool SendUnlockMessage;

		public void LoadDefaults()
		{
			UnlockInterval = 300f;
			SendUnlockMessage = true;
		}
	}
}
