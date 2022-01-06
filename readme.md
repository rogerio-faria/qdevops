qdevops
App to easily automate Qlik Sense operations. 
Import and Export variables between apps, master dimensions, measures and more.

By the way, you can create your own functions.

## Default Commands list
![Commands](images/commands.png)
## Sample commands
#### List apps from Qlik Sense Desktop
```
qdevops -u desktop -l
```
#### Add a Qlik Sense Server reference file
This command will ask individually for each parameter need to connect to Qlik Sense. These parameters will be saved on JSON format, under '.\servers' folder.
```
qdevops -i -a
```
This command create a master.txt file with connection information that can be used later
```
qdevops -a master.txt https://myserver.domain.com/ qproxy login MyDomain\MyUser MyPass
```


#### Export variables from a server
```
qdevops -u master.txt -v export dbc91f77-ec37-4b01-9fe8-9241423aaac8 vars.txt
```



#### Save all scripts, master items and chart objects to a folder
```
qdevops -u master.txt -s c:\qlik-scripts\ gsmdvo
```


#### Check all command options
```
qdevops -h <COMMAND>
```

#### Got a Key, Activated it
```
qdevops -key activate <GET-AT-QDEVOPS.DESQ.COM.BR>
```




## Creating your own functions
Create your file like the the example and put it at 'mycommands' folder, than use 
```
qdevops -compile
```
C# Example 

```csharp
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


```