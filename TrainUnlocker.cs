using System.Collections;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using Steamworks;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace PhaserArray.TrainUnlocker
{
    public class TrainUnlocker : RocketPlugin<TrainUnlockerConfiguration>
	{
	    public const string Version = "v1.1";

	    private TrainUnlockerConfiguration _config;

	    protected override void Load()
	    {
		    _config = Configuration.Instance;

		    if (_config.Enabled)
			{
				SteamChannel.onTriggerSend += OnTriggerSend;
				Logger.Log("Loaded TrainUnlocker " + Version + "!");
			}
		    else
			{
				Logger.Log("TrainUnlocker " + Version + " is installed but not enabled!");
			}
	    }

	    protected override void Unload()
		{
			if (!_config.Enabled)
			{
				return;
			}
			SteamChannel.onTriggerSend -= OnTriggerSend;
			Logger.Log("Unloaded TrainUnlocker " + Version + "!");
		}

		public void OnTriggerSend(SteamPlayer player, string name, ESteamCall mode, ESteamPacket type, params object[] arguments)
		{
			if (name != "tellVehicleLock")
			{
				return;
			}
			var vehicle = VehicleManager.getVehicle((uint) arguments[0]);
			if (vehicle.asset.engine != EEngine.TRAIN || (bool) arguments[3] == false)
			{
				return;
			}
			UnturnedChat.Say((CSteamID) arguments[1], Translate("trainunlocker_undid_lock"), Color.red);
			StartCoroutine(DelayedUnlockVehicle(vehicle, 1f));
		}

		public IEnumerator DelayedUnlockVehicle(InteractableVehicle vehicle, float delay)
		{
			yield return new WaitForSecondsRealtime(delay);
			VehicleManager.instance.channel.send(
				"tellVehicleLock",
				ESteamCall.ALL,
				ESteamPacket.UPDATE_RELIABLE_BUFFER,
				vehicle.instanceID,
				CSteamID.Nil,
				CSteamID.Nil,
				false);
		}

		public override TranslationList DefaultTranslations => new TranslationList()
		{
			{"trainunlocker_undid_lock", "Do not lock the train!"}
		};
	}
}
