namespace Clients;

using NLog;

using Services;
using System.ComponentModel;


/// <summary>
/// Client example.
/// </summary>
class Client
{

	/// <summary>
	/// Logger for this class.
	/// </summary>
	Logger mLog = LogManager.GetCurrentClassLogger();

	/// <summary>
	/// Configures logging subsystem.
	/// </summary>
	private void ConfigureLogging()
	{
		var config = new NLog.Config.LoggingConfiguration();

		var console =
			new NLog.Targets.ConsoleTarget("console")
			{
				Layout = @"${date:format=HH\:mm\:ss}|${level}| ${message} ${exception}"
			};
		config.AddTarget(console);
		config.AddRuleForAllLevels(console);

		LogManager.Configuration = config;
	}

	/// <summary>
	/// Program body.
	/// </summary>
	private void Run() {
		//configure logging
		ConfigureLogging();

		//run everythin in a loop to recover from connection errors
		while( true )
		{
			try {
				//connect to the server, get service client proxy
				var container = new ContainerClient();

                mLog.Info($"Output client actives");
                while (true)
                {
                    if (container.ActiveClient() == 2)
                    {
                        var random = new Random();
                        double rd = random.NextDouble() * 5;
                        container.SetMass(-rd);
                        mLog.Info($"Output client working.\tPressure was {container.ContainerInfo().Pressure:F2}");
                        mLog.Info($"Mass reduction: {-rd:F2}");
                        Thread.Sleep(2000);
                    }
                }
            }
			catch( Exception e )
			{
				//log whatever exception to console
				mLog.Warn(e, "Unhandled exception caught. Will restart main loop.");

				//prevent console spamming
				Thread.Sleep(2000);
			}
		}
	}

	/// <summary>
	/// Program entry point.
	/// </summary>
	/// <param name="args">Command line arguments.</param>
	static void Main(string[] args)
	{
		var self = new Client();
		self.Run();
	}
}
