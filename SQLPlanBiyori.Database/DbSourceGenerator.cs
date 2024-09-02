using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Diagnostics;
using SQLPlanBiyori.Common;

namespace SQLPlanBiyori.Database;
public class DbSourceGenerator
{
    public class OutputEventArgs : EventArgs
    {
        public OutputEventArgs(string text)
        {
            this.Text = text;
        }
        public string Text
        {
            get;
        }
        public DateTimeOffset DateTime
        {
            get;
        } = TimeService.Now;
    }

    public DbSourceGenerator(DbSourceGeneratorParameter p)
    {
        this.Parameter = p;
    }

    public DbSourceGeneratorParameter Parameter
    {
        get;
    }

    public delegate void OutputEventEventHandler(object sender, OutputEventArgs e);
    public event OutputEventEventHandler Output = delegate { };
    public delegate void ErrorEventEventHandler(object sender, OutputEventArgs e);
    public event ErrorEventEventHandler Error = delegate { };

    protected void OnOutput(string text)
    {
        Output(this, new OutputEventArgs(text));
    }

    protected void OnError(string text)
    {
        Error(this, new OutputEventArgs(text));
    }

    private void DeleteDirectory(string dir)
    {
        try
        {
            // remove directory
            if (System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.Delete(dir, true);
                OnOutput($"{dir}: delete directory.");
            }
        }
        catch (Exception ex)
        {
            OnError(ex.ToString());
        }
    }

    private async Task CreateProjectAsync(string projectName, string dir)
    {
        try
        {
            await foreach (var s in ProcessX.StartAsync(new ProcessStartInfo()
            {
                FileName = "dotnet",
                Arguments = $"new console -n {projectName}",
                WorkingDirectory = dir,
            }))
            {
                OnOutput(s);
            };
        }
        catch (ProcessErrorException ex)
        {
            OnError(ex.ToString());
        }
    }

    private async Task AddManifestAsync(string dir)
    {
        try
        {
            await foreach (var s in ProcessX.StartAsync(new ProcessStartInfo()
            {
                FileName = "dotnet",
                Arguments = $"new tool-manifest",
                WorkingDirectory = dir,
            }))
            {
                OnOutput(s);
            }
        }
        catch (ProcessErrorException ex)
        {
            OnError(ex.ToString());
        }
    }

    private async Task AddPackageAsync(string packageName, string dir)
    {
        try
        {
            await foreach (var s in ProcessX.StartAsync(new ProcessStartInfo()
            {
                FileName = "dotnet",
                Arguments = $"add package {packageName}",
                WorkingDirectory = dir,
            }))
            {
                OnOutput(s);
            }
        }
        catch (ProcessErrorException ex)
        {
            OnError(ex.ToString());
        }
    }

    private async Task InstallDotnetToolAsync(string toolName, string dir)
    {
        try
        {
            await foreach (var s in ProcessX.StartAsync(new ProcessStartInfo()
            {
                FileName = "dotnet",
                Arguments = $"tool install --local {toolName}",
                WorkingDirectory = dir,
            }))
            {
                OnOutput(s);
            }
        }
        catch (ProcessErrorException ex)
        {
            OnError(ex.ToString());
        }
    }


    private async Task ExecuteDbContextScaffoldCommandAsync(string dir)
    {
        try
        {
            await foreach (var s in ProcessX.StartAsync(new ProcessStartInfo()
            {
                FileName = "dotnet",
                Arguments = $"ef dbcontext scaffold \"{this.Parameter.ConnectionString}\" {this.Parameter.NpgsqlEntityFrameworkCoreName} -o {this.Parameter.ModelDir} -c {this.Parameter.DbContextName}",
                WorkingDirectory = dir,
            }))
            {
                OnOutput(s);
            }
        }
        catch (ProcessErrorException ex)
        {
            OnError(ex.ToString());
        }
    }

    public static bool SourceGenerated
    {
        get;
        private set;
    } = false;

    public async Task<DbSourceGeneratorResult> GenerateAsync()
    {
        DeleteDirectory(this.Parameter.TempProjectDirectory);

        // dotnet new console -n tmp_project
        await CreateProjectAsync(System.IO.Path.GetFileName(
            this.Parameter.TempProjectDirectory), 
            System.IO.Path.GetDirectoryName(this.Parameter.TempProjectDirectory));

        // cd tmp_project
        // dotnet new tool-manifest
        await AddManifestAsync(this.Parameter.TempProjectDirectory);

        // dotnet add package Microsoft.EntityFrameworkCore.Design
        await AddPackageAsync("Microsoft.EntityFrameworkCore.Design", this.Parameter.TempProjectDirectory);

        // dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
        await AddPackageAsync("Npgsql.EntityFrameworkCore.PostgreSQL", this.Parameter.TempProjectDirectory);

        // dotnet tool install --local dotnet-ef
        await InstallDotnetToolAsync(this.Parameter.DotnetEFName, this.Parameter.TempProjectDirectory);

        // dotnet ef dbcontext scaffold "接続文字列" Npgsql.EntityFrameworkCore.PostgreSQL -o Models -c AppDbContext
        await ExecuteDbContextScaffoldCommandAsync(this.Parameter.TempProjectDirectory);

        SourceGenerated = true;
        return new DbSourceGeneratorResult()
        {
            EntityPaths = System.IO.Directory.GetFiles(
            System.IO.Path.Combine(this.Parameter.TempProjectDirectory, this.Parameter.ModelDir), "*.cs").
            Where(x => System.IO.Path.GetFileNameWithoutExtension(x) != this.Parameter.DbContextName).ToArray(),
            DbContextPath = System.IO.Path.Combine(this.Parameter.TempProjectDirectory, this.Parameter.ModelDir, $"{this.Parameter.DbContextName}.cs"),
        };
    }
    public async Task<Assembly> BuildAssemblyAsync()
    {
        if (!SourceGenerated)
        {
            await BuildAssemblyAsync();
        }

        try
        {
            var projectPath = System.IO.Path.Combine(
                this.Parameter.TempProjectDirectory, $"{System.IO.Path.GetFileName(this.Parameter.TempProjectDirectory)}.csproj"
                );
            await foreach (var s in ProcessX.StartAsync(new ProcessStartInfo()
            {
                FileName = "dotnet",
                Arguments = $"build {projectPath} -c Debug",
                WorkingDirectory = this.Parameter.TempProjectDirectory,
            }))
            {
                OnOutput(s);
            }
            var asmPath = System.IO.Path.Combine(
                this.Parameter.TempProjectDirectory,
                System.IO.Path.GetFileName(this.Parameter.TempProjectDirectory),
                "bin",
                "Debug");
            return System.Reflection.Assembly.LoadFile(asmPath);
        }
        catch (ProcessErrorException ex)
        {
            OnError(ex.ToString());
            return null;
        }

    }
}
