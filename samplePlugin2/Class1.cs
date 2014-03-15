using PluginBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samplePlugin2
{
    [Export(typeof(IPlugin))]
    [Export(typeof(IMenuItemPlugin))]
    [ExportMetadata("Version", "0.0.2")]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class Class1 : IMenuItemPlugin
    {
        public System.Windows.Controls.MenuItem ItemHandle
        {
            get;
            private set;
        }

        public void RequestToCreateHandle()
        {
            this.ItemHandle = new Views.UserControl1().hoge;
        }

        public void Initialize()
        {
            Console.WriteLine("item initialize");
        }

        public void Dispose()
        {
            Console.WriteLine("item dispose");
        }
    }
}
