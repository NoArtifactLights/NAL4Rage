using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NALRage.Engine.Modification.API;
using Rage;

namespace NALRage.Engine.Extensions
{
    internal static class PluginManager
    {
        private static readonly List<Plugin> Plugins = new List<Plugin>();

        internal static void Finally()
        {
            foreach (var plugin in Plugins)
            {
                plugin?.Finally();
            }
        }
        
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

                var types = assembly.GetTypes();
                Plugin plugin = (from type in types where type.IsAssignableFrom(typeof(Plugin)) select (Plugin) Activator.CreateInstance(type)).FirstOrDefault();

                if (plugin != null)
                {
                    Plugins.Add(plugin);
                    plugin.OnStart();
                }
                else
                {
                    Logger.Warn("PluginManager", "A loaded plugin's instance is null. Will not start it.");
                    Game.DisplayNotification($"During load, <b>{assembly.GetName()}</b> became null. The plugin is never started. Contact author for more information.");
                }
            }
        }
    }
}
