using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Com.Ctrip.Framework.Apollo;
using Com.Ctrip.Framework.Apollo.Model;

namespace Apollo
{
    class Program
    {
        private static void OnChanged(object sender, ConfigChangeEventArgs changeEvent)
        {
            Console.WriteLine("Changes for namespace {0}", changeEvent.Namespace);
            foreach (string key in changeEvent.ChangedKeys)
            {
                ConfigChange change = changeEvent.GetChange(key);
                Console.WriteLine("Change - key: {0}, oldValue: {1}, newValue: {2}, changeType: {3}", change.PropertyName, change.OldValue, change.NewValue, change.ChangeType);
            }
        }

        static void Main(string[] args)
        {
            Config config = ConfigService.GetAppConfig(); //config instance is singleton for each namespace and is never null
            config.ConfigChanged += OnChanged;

            while (true)
            {
                Thread.Sleep(500);
                var timeout = config.GetProperty("timeout", "");
                Console.WriteLine(timeout);
            }
        }
    }
}
