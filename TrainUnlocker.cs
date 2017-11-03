using Rocket.API.Collections;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using UnityEngine;

namespace PhaserArray.TrainUnlocker
{
    public class TrainUnlocker : RocketPlugin<TrainUnlockerConfiguration>
	{
	    public const string Version = "v1.0";

	    private TrainUnlockerConfiguration _config;

	    protected override void Load()
	    {
		    _config = Configuration.Instance;
			
		    Logger.Log("Starting TrainUnlocker " + Version + "!");
			InvokeRepeating(nameof(RunTrainUnlocker), _config.UnlockInterval, _config.UnlockInterval);
	    }

	    protected override void Unload()
	    {
			CancelInvoke(nameof(RunTrainUnlocker));
	    }

		public void RunTrainUnlocker()
		{
			Logger.Log("Unlocking trains!");
			var unlockedTrains = UnlockTrains();

			if (!_config.SendUnlockMessage) return;
			if (unlockedTrains == 1)
			{
				UnturnedChat.Say(Translate("trainunlocker_unlocked_singular"), Color.yellow);
			}
			else if (unlockedTrains > 1)
			{
				UnturnedChat.Say(Translate("trainunlocker_unlocked_plural", unlockedTrains), Color.yellow);
			}
		}

		public static int UnlockTrains()
		{
			var unlockCount = 0;
			var vehicles = VehicleManager.vehicles;
			foreach (var vehicle in vehicles)
			{
				if (vehicle.asset.engine != EEngine.TRAIN || !vehicle.isLocked) continue;
				VehicleManager.unlockVehicle(vehicle);
				unlockCount++;
			}
			return unlockCount;
		}

		public override TranslationList DefaultTranslations => new TranslationList()
		{
			{"trainunlocker_unlocked_singular", "Unlocked the train!"},
			{"trainunlocker_unlocked_plural", "Unlocked {0} trains!"}
		};
	}
}
