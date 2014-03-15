using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using PluginBase;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.IO;

namespace pluginTest.Models
{
    public class PluginContainer //: NotificationObject
    {
        public static PluginContainer Instance { get; private set; }


        private CompositionContainer _container;
        private Dictionary<IPlugin, IPluginMetadata> _pluginDic = new Dictionary<IPlugin,IPluginMetadata>();

        [ImportMany]
        private IEnumerable<Lazy<IPlugin, IPluginMetadata>> _plugins = null;// 警告が邪魔だから明示的に null いれとく


        public static void Initialize(string pluginsDirectory)
        {
            if (!Directory.Exists(pluginsDirectory))
                throw new ArgumentException("Plugins Directory ({0}) is not exists", pluginsDirectory);

            var instance = new PluginContainer();
            try
            {
                var catalog = new DirectoryCatalog(pluginsDirectory);
                instance._container = new CompositionContainer(catalog, CompositionOptions.DisableSilentRejection);
                instance._container.ComposeParts(instance);// ここで ImportMany の Lazy<> 構築

                foreach (var plugin in instance._plugins)
                {
                    plugin.Value.Initialize();// ここで Lazy<> の中身が実体化 + メモ化される
                    instance._pluginDic[plugin.Value] = plugin.Metadata;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            PluginContainer.Instance = instance;
        }


        public static void Dispose()
        {
            var instance = PluginContainer.Instance;

            instance._pluginDic.Clear();
            instance._container.Dispose();// 読み込んだエクスポートはここで Dispose される

            PluginContainer.Instance = null;
        }


        public IEnumerable<T> GetPlugins<T>()
            where T: class, IPlugin
        {
#if false
            return _container.GetExportedValues<IPlugin>()
                .Where(item => item is T)
                .Select(item => item as T);
#elif true
            return _plugins.Where(item => item.Value is T).Select(item => item.Value as T);
#elif false
            return _pluginDic.Where(item => item.Key is T).Select(item => item.Key as T);
#endif
        }


        public IPluginMetadata GetPluginMetadata(IPlugin plugin)
        {
#if true
            IPluginMetadata metadata;
            if (_pluginDic.TryGetValue(plugin, out metadata))
                return metadata;
            return null;
#else
            return _plugins.Where(item => item.Value == plugin).Select(item => item.Metadata).First();
#endif
        }
    }
}
