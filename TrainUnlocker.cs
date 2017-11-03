using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;

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
	    }

	    protected override void Unload()
	    {
	    }
	}
}
