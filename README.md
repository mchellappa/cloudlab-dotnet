# cloudlab-dotnet
# Test Driven Development

> The best time to plant a tree was 20 years ago. The second best time is now.

## Setup

Run the following command to create an XUnit project

```powershell
dotnet new xunit -o Kitchen.Test
dotnet new mvc -o Kitchen.Api

cd Kitchen.Test
dotnet add reference ../Kitchen.Api

dotnet dev-certs https --trust
```

Create a `nuget.config` file in both the `Kitchen.Test` and `Kitchen.Api` directories and add the following configuration:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
 <packageSources>
    <clear/>
    <add key="artifactory" value="https://artifactory.platform.manulife.io/artifactory/api/nuget/nuget" />

  </packageSources>
</configuration>
```

This will force nuget to target the Manulife artifactory environment when restoring packages.

## First Test

Replace the contents of `UnitTest1.cs` in the `Kitchen.Test` project with the following:

```csharp
using System;
using Xunit;

namespace Kitchen.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(false);
        }
    }
}
```

Now `cd` into the `Kitchen.Test` directory and run the `dotnet test` command.

It should fail. Congrats!

### Lets Build an MVC Application
Open Startup.cs and add MVC

Setup the VS Code to run dotnet Core projects. Launch.json and tasks.json will have to be created.


Create InventoryController.cs (Routing)


### Cupcake

Currently, this will not build because `Cupcake` is not defined. To resolve this, create a folder named `Models` in the `Kitchen.Api` project and add a file named `Cupcake.cs` to it. Copy the following code:

```csharp
namespace Kitchen.Api.Models
{
    public class Cupcake
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public decimal Price { get; set; }

        public string Image { get; set; }
    }
}
```

Add `using Kitchen.Api.Models` to the top of `IdentityService.cs` to resolve the Cupcake reference.



## Building Web API

### MVC

Delete the `app.Run` statement `Kitchen.Api/Startup.cs`. Now, register MVC as a service and using it during app configuration as shown below.

## Old dotnet6 way
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Kitchen.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
```

The app is now using MVC; however, it does not have any routes mapped. In the folder named `Controllers` in `Kitchen.Api` and create a file named `InventoryController.cs`.

```csharp
using System.Collections.Generic;
using System.Threading.Tasks;
using Kitchen.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Api.Controllers
{
    [Route("api/[controller]")]
    public class InventoryController : Controller
    {
        [HttpGet]
        public Task<List<Cupcake>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
```

Now, navigating to [https://localhost:5001/api/inventory](https://localhost:5001/api/inventory) will result in a thrown exception. Well done!





### Data Persistence

Now, when the project is run, we are once again getting our favorite `NotImplementedException`. This is by design since the `CupcakeRepository` has not actually be implemented yet. You're witnessing the power of [Inversion of Control](https://en.wikipedia.org/wiki/Inversion_of_control)!

Create a file named `ApplicationDbContext.cs` under the `Kitchen.Api/Data` folder with the following content:

```csharp
public class InventoryContext : DbContext    
    {
        public DbSet<Cupcake> Cupcakes {get; set;}

        public InventoryContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cupcake>().HasData(Seed.Chocolate, Seed.Vanilla, Seed.RedVelvet, Seed.PumpkinSpice, Seed.Bubblegum, Seed.Unicorn);
        }

        
    }
```

Let's register `InventoryContext` in our `Startup.cs` file. Add the following code to the `ConfigureServices` method:

```csharp
services.AddDbContext<InventoryContext>(options =>
{
    options.UseInMemoryDatabase(nameof(Cupcake));
});
```

This currently will not build. We must add a references to `Microsft.EntityFrameworkCore` and `Microsft.EntityFrameworkCore.InMemory` using the following commands:

```powershell
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.InMemory
```

Lastly, add the following `using` statments at the top of `Startup.cs` to import this newly installed method.

```csharp
using Microsoft.EntityFrameworkCore;
```



### Seed Data

Now for the moment of truth. Run `dotnet run` in the `Kitchen.Api` direcory and then navigate to [https://localhost:5001/api/inventory](https://localhost:5001/api/inventory).
You should only see a blank page with a `[]` in the top left corner (in Chrome). This signifies (in our case) an empty array of cupcakes. However, this is clearly unnacceptable for a Cupcake Shop, so let's add some seed data!

Create a file named `Seed.cs` inside the `Kitchen.Api/Data` folder and paste the following code:

```csharp

    public static class Seed
    {
        public static Cupcake Chocolate = new Cupcake
        {
            Id = 1,
            Name = "Chocolate",
            Description = "Chocolatey and delicious",
            Price = 4.95m,
            Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRgsnbhtyxwu83esiCshNY7-YCypRKUHikL58EuaX-DUq-A698-FA"
        };

        public static Cupcake Vanilla = new Cupcake
        {
            Id = 2,
            Name = "Vanilla",
            Description = "Plain, boring vanilla cupcake",
            Price = 4.95m,
            Image = "https://www.lifeloveandsugar.com/wp-content/uploads/2017/01/Moist-Vanilla-Cupcakes2.jpg"
        };

        public static Cupcake RedVelvet = new Cupcake
        {
            Id = 3,
            Name = "Red Velvet",
            Description = "Is it chocolate? I don't know! It looks red and tastes delicious",
            Price = 5.5m,
            Image = "https://therecipecritic.com/wp-content/uploads/2017/01/RedVelvetCupcakes2-667x1000.jpg"

        };

        public static Cupcake PumpkinSpice = new Cupcake
        {
            Id = 4,
            Name = "Pumpkin Spice",
            Description = "Seasonal classic that tastes like everyone's favorite squash",
            Price = 6.99m,
            Image = "https://cdn.crownmediadev.com/4a/1e/9808ba9b4487a12d0ccccc84f61c/home-family-pumpkin-spice-latte-cupcakes.jpg"

        };

        public static Cupcake Bubblegum = new Cupcake
        {
            Id = 5,
            Name = "Bubblegum",
            Description = "You asked, we delivered! Bubblegum cupcake that defies every culinary and natural rule",
            Price = 20,
            Image = "https://www.sbs.com.au/food/sites/sbs.com.au.food/files/bubblepop-electric-cupcakes_lc.jpg"

        };

        public static Cupcake Unicorn = new Cupcake
        {
            Id = 6,
            Name = "Unicorn",
            Description = "Magical and delicious! Limited edition treat made with real unicorn!",
            Price = 999.99m,
            Image = "https://media2.s-nbcnews.com/j/newscms/2018_16/1332898/unicorn-cupcakes-today-041918-tease_607876a763a32491c1bf4bb7c8eab53e.today-inline-large.jpg"
        };
    }

```

Next, modify `InventoryContext.cs` to add a `protected override` method called `OnModelCreating`.

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Cupcake>().HasData(Seed.Chocolate, Seed.Vanilla, Seed.RedVelvet, Seed.PumpkinSpice, Seed.Bubblegum, Seed.Unicorn);
}
```

## Update InventoryController with this to get the Inventory
```csharp
using System.Collections.Generic;
using System.Threading.Tasks;
using Kitchen.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.Api.Controllers
{
    [Route("api/[controller]")]
    public class InventoryController : Controller
    {
        private readonly InventoryContext _context;

        public InventoryController(InventoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Cupcake>> GetAllAsync()
        {
            return await _context.Cupcakes.ToListAsync();
        }
    }
}
```

## When we were using PCF

### Manifest

Create a file named `manifest.yml` inside the `src` folder. Add the following markup with your name instead of `{NAME}`:

```yml
---
applications:
- name: KitchenApi{NAME}
  path: ./Kitchen.Api
  buildpacks:
  - dotnet_core_buildpack
  memory: 256M
```

Now `cd` into the `src` directory (adjacent to the `manifest.yml` file) and run the following command to authenticate with PFC. You will be asked to enter your username and password in two separate prompts.

```powershell
cf login -a https://api.sys.cac.preview.pcf.manulife.com -o JHAS-CAC-DEV -s DE-PLAYAREA-CAC-DEV
```

Once you have authenticated, push the application using the following command:

```powershell
cf push
```

### Configure Real Database

Install Cloud Foundry Actuators

```powershell
dotnet add package Steeltoe.Management.CloudFoundryCore

dotnet add package Steeltoe.Extensions.Configuration.CloudFoundryCore
```

Install Steeltoe Database Service Connectors

```powershell
dotnet add package Steeltoe.CloudFoundry.Connector.EFCore

dotnet add package Pomelo.EntityFrameworkCore.MySql
```
