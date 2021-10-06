using System;
using System.IO;
using Qlik.Engine;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using QDevOpsBase.Utils;
using QDevOpsBase.Server;

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
	public string Group => "Custom";
	public List<string> Content { get; set; }
	public string Name => "My list command";
	public int ListOrder { get; set; }
	public bool NeedsServer => true;

	public ExecuteReturn Execute(JObject args, CommandsConfig conf)
	{
		ExecuteReturn eRet = new ExecuteReturn();

		if (conf.QlikHub == null)
		{
			Console.WriteLine("Inaccessible Hub! Exiting...");

			eRet.Result = ExecuteReturnResult.Fail;
			eRet.LastError = new Exception("Inaccessible Hub!");
			return eRet;
		}

		bool localmode = conf.QlikHub.IsDesktopMode();
		Console.WriteLine();
		Console.Write("Last Reload          App Id                                ");
		if (!localmode) Console.WriteLine("App Name"); else Console.WriteLine();
		Console.WriteLine("------------------------------------------------------------------------------");

		eRet.Result = ExecuteReturnResult.Success;
		IEnumerable<IAppIdentifier> apps_info = conf.QlikLocation.GetAppIdentifiers();
		foreach (var item in apps_info)
		{
			string blanks = new string(' ', 20 - (item.LastReloadTime == null ? 0 : item.LastReloadTime.Length));
			string name = "";
			if (!localmode) name = item.AppName;

			eRet.AddReturnedData(item.AppId, item.AppName);
			Console.WriteLine($"{item.LastReloadTime}{blanks} {item.AppId}  {name}");
		}

		return eRet;
	}
}