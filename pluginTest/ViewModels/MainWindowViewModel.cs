using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using pluginTest.Models;
using PluginBase;
using System.Windows.Controls;

namespace pluginTest.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {

        #region MenuItems変更通知プロパティ
        private MenuItem[] _MenuItems;

        public MenuItem[] MenuItems
        {
            get
            { return _MenuItems; }
            set
            { 
                if (_MenuItems == value)
                    return;
                _MenuItems = value;
                RaisePropertyChanged("MenuItems");
            }
        }
        #endregion


        private IWindowPlugin[] _windows;
        private IMenuItemPlugin[] _menuItems;

        public void Initialize()
        {
            _windows = Models.PluginContainer.Instance.GetPlugins<IWindowPlugin>().ToArray();
            foreach (var window in _windows)
            {
                window.RequestToCreateHandle();
                window.WindowHandle.Show();
            }

            var itemList = new List<MenuItem>();
            _menuItems = Models.PluginContainer.Instance.GetPlugins<IMenuItemPlugin>().ToArray();
            foreach (var menuItem in _menuItems)
            {
                menuItem.RequestToCreateHandle();
                itemList.Add(menuItem.ItemHandle);
            }
            this.MenuItems = itemList.ToArray();
        }

        protected override void Dispose(bool disposing)
        {
            foreach (var window in _windows)
            {
                window.RequestToCloseWindow();
            }
            base.Dispose(disposing);
        }
    }
}
