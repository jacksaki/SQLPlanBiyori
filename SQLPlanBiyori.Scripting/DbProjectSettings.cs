using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SQLPlanBiyori.Scripting
{
    public class DbProjectSettings:INotifyPropertyChanged
    {
        public DbProjectSettings()
        {
            this.GlobalAssemblies.Clear();
            foreach (var asm in GlobalAssembly.GetAll())
            {
                this.GlobalAssemblies.Add(asm);
            }
        }
        public ObservableCollection<GlobalAssembly> GlobalAssemblies
        {
            get;
        } = new ObservableCollection<GlobalAssembly>();

        public ObservableCollection<LoadedAssembly> LoadedAssemblies
        {
            get;
        } = new ObservableCollection<LoadedAssembly>();

        public ObservableCollection<string> Imports
        {
            get;
        } = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
