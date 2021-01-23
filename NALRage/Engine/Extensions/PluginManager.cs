using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NALRage.Engine.Modification.API;

namespace NALRage.Engine.Extensions
{
    internal static class PluginManager
    {
        private static List<Plugin> plugins = new List<Plugin>();

        internal static void LoadPlugins()
        {
            if (!Directory.Exists("plugins\\NAL"))
            {
                Logger.Info("PluginManager", "NAL plug-ins folder does not exist. Aborting.");
                return;
            }
            string[] files = Directory.GetFiles("plugins\\NAL\\", "*.dll");
            foreach (var file in files)
            {
                Assembly assembly = null;
                try
                {
                    assembly = Assembly.LoadFrom(file);
                }
                catch (BadImageFormatException)
                {
                    Logger.Warn("PluginManager", file + " does not seems like to be an assembly. Aborting.");
                    continue;
                }
                catch (FileNotFoundException)
                {
                    Logger.Error("PluginManager", file + "not exists.");
                    continue;
                }
                catch (Exception ex)
                {
                    Logger.Error("PluginManager", $"Unable to load {file} because {ex.Message}.");
                    Logger.Error("PluginManager", ex.ToString());
                }

                if (assembly == null)
                {
                    Logger.Error("PluginManager", $"{file} loaded as a null. Aborting, loading next.");
                    continue;
                }

                Plugin plugin = null;
                Type[] types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsAssignableFrom(typeof(Plugin)))
                    {
                        plugin = (Plugin)Activator.CreateInstance(type);
                        break;
                    }
                }

                plugins.Add(plugin);
                plugin.OnStart();
            }
        }
    }
}
