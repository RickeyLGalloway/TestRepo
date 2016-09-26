using System;
using System.Reflection;
using Agatha.Common;
using Agatha.Common.InversionOfControl;
using MEDSEEK.eHealth.Apps.Providers.Common;
using MEDSEEK.eHealth.ServiceLayer.Common.Administration;

namespace AgathaTest
{
	class Program
	{
		static void Main(string[] args)
		{
			//added comment for git testing
			try
			{
				Console.WriteLine("Initialzing Agatha...");

				InitializeAgatha();

				Console.WriteLine("Getting request dispatcher...");

				var requestDispatcher = IoC.Container.Resolve<IRequestDispatcher>();

				Console.WriteLine("Dispatching request...");



				//var address = "net.tcp://convertdevenv:50555/FulcrumServiceHost/Agatha.Common.WCF.IWcfRequestProcessor.svc";
				var address = "net.tcp://convertdevenv:50555/Agatha.Common.WCF.IWcfRequestProcessor.svc";

				// Warm up services
				SimpleRequestDispatcher.Get<ReadServiceContextInfoResponse>(new ReadServiceContextInfoRequest(), address);



				//var response = requestDispatcher.Get<ListProvidersResponse>(new ListProvidersRequest() { BypassCache = true, RequestedPage = 1 });

				//Console.ForegroundColor = ConsoleColor.Green;

				//Console.WriteLine("\nSuccessfully retrieved first {0} providers of {1} total!", response.Providers.Count(), response.TotalRecords);

				//if(response.Providers.Count() > 0)
				//	Console.WriteLine("\nFirst provider name: {0}", response.Providers.First().FullName);

			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;

				Console.WriteLine("\nError:\n" + ex.Message);

				if (ex.InnerException != null)
					Console.WriteLine("\n\nInner Error:\n" + ex.InnerException.Message);
			}

			Console.ResetColor();

			Console.WriteLine("\nPress any key to exit...");
			Console.ReadKey();
		}

		private static void InitializeAgatha()
		{
			new ClientConfiguration(Assembly.GetAssembly(typeof(ListProvidersRequest)), typeof(Agatha.Unity.Container)).Initialize();
		}
	}
}
