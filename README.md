# HCI.AspNetCore.Extensions

HCI.AspNetCore.Extensions is a .net standard 2.0 library for dealing with AspNetCore projects.

## Installation

Use the package manager [nuget](https://nuget.org) to install HCI.AspNetCore.Extensions.

```bash
nuget install HCI.AspNetCore.Extensions
```

## Usage
ServiceCollectionExtensions are used in the StartUp class.

```csharp
using HCI.AspNetCore.Extensions;

public void ConfigureServices(IServiceCollection services)
{
    services.AddCustomApiVersioning();
}
```

WebHostExtensions are used in the Program.cs class.
```csharp
using HCI.AspNetCore.Extensions;

public static void Main(string[] args)
{
    var host = CreateWebHostBuilder(args)
        .Build()
        .ApplyPendingDatabaseMigrations<DbContext>();
}
```
## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[GNU](https://choosealicense.com/licenses/gpl-3.0/)
