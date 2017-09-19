using System;
using System.Runtime.Loader;
using System.Threading;

/*
	Following article here: https://logankpaschke.com/linux/systemd/dotnet/systemd-dotnet-1/
 
	Publish your app: dotnet publish -o /opt/daemons/myfirstdaemon/

	Now we need to tell systemd about our service. We’re going to use systemd user units. Run systemctl edit --user --full --force myfirstdaemon.service and add the following contents:

	[Unit]
	Description=Test .Net service

	[Service]
	ExecStart=/usr/local/bin/dotnet /opt/daemons/myfirstdaemon/myfirstdaemon.dll #Tell systemd which program to start
	
	Run your service: systemctl start --user myfirstdaemon.service. You should be able to see the output by running systemctl status --user myfirstdaemon.service or journalctl --user -u myfirstdaemon.service
 
 */


namespace systemd_service {
    class Program {
        static void Main(string[] args) {
            AssemblyLoadContext.Default.Unloading += SigTermEventHandler; //register sigterm event handler. Don't forget to import System.Runtime.Loader!
			Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelHandler); //register sigint event handler
			
			while(true) {
				Console.WriteLine("Hello World!");
				Thread.Sleep(2000); //Sleep for 2 seconds. Don't forget to import System.Threading!
			}
        }
		
		private static void SigTermEventHandler(AssemblyLoadContext obj) {
			System.Console.WriteLine("Unloading...");
		}

		private static void CancelHandler(object sender, ConsoleCancelEventArgs e) {	     
			System.Console.WriteLine("Exiting...");
		}
    }
}
