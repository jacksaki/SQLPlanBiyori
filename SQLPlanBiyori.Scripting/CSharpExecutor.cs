using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.Logging;
using SQLPlanBiyori.Common;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SQLPlanBiyori.Scripting
{
    public class CSharpExecutor: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CSharpExecutor()
        {
            this.Logs.CollectionChanged += (sender, e) =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Logs)));
            };
        }


        public async Task ExecuteAsync()
        {
            var logService = new ExecLogService();
            var loggerProvider = new ExecLoggerProvider(logService);
            var logger = loggerProvider.CreateLogger("SQLLogger");

            this.Logs.Clear();
            logService.Logs.CollectionChanged += (sender, e) =>
            {
                foreach (ExecLog item in e.NewItems)
                {
                    this.Logs.Add(item);
                }
            };


            var scriptOptions = ScriptOptions.Default;
                //AddReferences(project.Settings.GlobalAssemblies.Where(x => x.IsSelected).Select(x => x.Assembly)).
                //AddReferences(project.Settings.LoadedAssemblies.Select(x => x.Assembly)).
                //AddImports(project.Settings.Imports);
            var sb = new System.Text.StringBuilder();
            //if(!string.IsNullOrWhiteSpace(project.DatabaseContext.SourceText))
            //{
            //    sb.AppendLine(project.DatabaseContext.SourceText);
            //}

            var script = CSharpScript.Create(sb.ToString(), scriptOptions);
//            script = script.ContinueWith(project.QuerySource);
            var compilation = script.GetCompilation();
            var errors = compilation.GetDiagnostics().Where(d => d.Severity == DiagnosticSeverity.Error);
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    logger.LogError($"{error.Location} {error.GetMessage()}");
                }
                return;
            }

            try
            {
                var state = await script.RunAsync();
            }
            catch (CompilationErrorException ce)
            {
                foreach(var msg in ce.Diagnostics)
                {
                    logger.LogError($"{msg.Location} {msg.GetMessage()}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                logger.LogError(ex.StackTrace);
            }
        }

        public ObservableCollection<ExecLog> Logs
        {
            get;
        } = new ObservableCollection<ExecLog>();
    }
}
