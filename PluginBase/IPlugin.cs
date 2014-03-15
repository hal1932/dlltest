using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PluginBase
{
    public interface IPlugin : IDisposable
    {
        void Initialize();
    }

    public interface IPluginMetadata
    {
        string Version { get; }
    }


    public interface IGuiPlugin : IPlugin
    {
        void RequestToCreateHandle();
    }

    public interface IMenuItemPlugin : IGuiPlugin
    {
        MenuItem ItemHandle { get; }
    }

    public interface IWindowPlugin : IGuiPlugin
    {
        Window WindowHandle { get; }
        void RequestToCloseWindow();
    }

    public interface IControlPlugin : IGuiPlugin
    {
        UserControl ControlHandle { get; }
    }

    public interface IContextMenuPlugin : IGuiPlugin
    {
        ContextMenu MenuHandle { get; }
    }
}
