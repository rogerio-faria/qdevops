using System;
using System.IO;
using QDevOpsBase.Server;
using Qlik.Engine;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class mylist : IQlikCommand
{
	public qlikcommandparameters Params
	{
		get
		{
			return new qlikcommandparameters()
			{
				FullHelp = "TEST = List all Qlik Apps",
				Parameters = new Dictionary<string, string>()
			};
		}
	}
	public string CommandId => "mylist";
	public List<string> Content { get; set; }
	public string Name => "My list command";
	public string Group => "Application";
	public int ListOrder { get; set; }

	public void Execute(JObject args, qlikcommandconfig conf)
	{

		IEnumerable<IAppIdentifier> apps_info = conf.loc.GetAppIdentifiers();
		if (apps_info == null)
		{
			Console.WriteLine("Apps List returned null!");
			return;
		}
		foreach (var item in apps_info)
		{
			Console.WriteLine($"TEST - {item.AppId}");
		}
	}
}

