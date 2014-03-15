using PluginBase;
using samplePlugin1.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samplePlugin1
{
    [Export(typeof(IPlugin))]
    [Export(typeof(SamplePlugin1))]
    [ExportMetadata("Version", "0.0.1")]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SamplePlugin1 : IWindowPlugin
    {
        public System.Windows.Window WindowHandle
        {
            get;
            private set;
        }

        public void RequestToCreateHandle()
        {
            this.WindowHandle = new Window1();
        }

        public void RequestToCloseWindow()
        {
            if (this.WindowHandle != null && this.WindowHandle.IsLoaded)
            {
                this.WindowHandle.Close();
            }
        }

        public void Initialize()
        {
            Console.WriteLine("initialize");
        }

        public void Dispose()
        {
            Console.WriteLine("dispose");
        }
    }
}
