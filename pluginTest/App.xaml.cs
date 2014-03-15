using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

using Livet;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using PluginBase;
using System.ComponentModel.Composition.Primitives;

namespace pluginTest
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DispatcherHelper.UIDispatcher = Dispatcher;
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Models.PluginContainer.Initialize(@"..\plugins");

            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            var p = Models.PluginContainer.Instance.GetPlugins<IWindowPlugin>();
            var m = Models.PluginContainer.Instance.GetPluginMetadata(p.First());
            sw.Stop();
            Console.WriteLine(sw.ElapsedTicks);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Models.PluginContainer.Dispose();
        }

        //集約エラーハンドラ
        //private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        //{
        //    //TODO:ロギング処理など
        //    MessageBox.Show(
        //        "不明なエラーが発生しました。アプリケーションを終了します。",
        //        "エラー",
        //        MessageBoxButton.OK,
        //        MessageBoxImage.Error);
        //
        //    Environment.Exit(1);
        //}
    }
}
