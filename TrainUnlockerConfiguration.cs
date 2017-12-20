using Rocket.API;

namespace PhaserArray.TrainUnlocker
{
	public class TrainUnlockerConfiguration : IRocketPluginConfiguration
	{
		public bool Enabled;

		public void LoadDefaults()
		{
			Enabled = true;
		}
	}
}
