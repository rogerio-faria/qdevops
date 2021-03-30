using System;
using System.IO;
using QDevOpsBase.Server;
using Qlik.Engine;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using QDevOpsBase.Utils;

public class mylist : ICommand
{
	public CommandsParameters Params
	{
		get
		{
			return new CommandsParameters()
			{
				FullHelp = "TEST = List all Qlik Apps",
				Parameters = new List<CommandsParametersFields>()
			};
		}
	}
	public string CommandId => "mylist";
	public List<string> Content { get; set; }
	public string Name => "My list command";
	public int ListOrder { get; set; }
	public bool NeedsServer => true;

	public void Execute(JObject args, CommandsConfig conf)
	{
		IEnumerable<IAppIdentifier> apps_info = conf.QlikLocation.GetAppIdentifiers();
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