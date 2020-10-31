using System;
using NALRage.Entities.Serialization;
using Rage;

namespace NALRage.Engine.Modification
{
    internal static class ConfigurationHandler
    {
        private static InitializationFile initializationFile = new InitializationFile("NALRage.ini");
        private static Configuration configuration;

        internal static Configuration Config => configuration;
        
        internal static void GenerateConfig()
        {
            configuration = new Configuration(2);
            initializationFile.Create();
            initializationFile.Write("Main", "Version", configuration.Version);
            initializationFile.Write("Main", "DefaultDifficulty", configuration.DefaultDifficulty);
            initializationFile.Write("Main", "LoopInterval", configuration.ProcessInterval);
            initializationFile.Write("Event", "RequirementChance", configuration.EventRequirement);
            initializationFile.Write("Event", "MaxValue", configuration.EventMax);
            initializationFile.Write("Event", "MinimalValue", configuration.EventMinimal);
        }

        internal static void Init()
        {
            if(!initializationFile.Exists()) GenerateConfig();
            configuration = new Configuration();
            try
            {
                configuration.Version = initializationFile.ReadInt32("Main", "Version");
            }
            catch (Exception e)
            {
                initializationFile.Delete();
                Game.DisplayNotification("Please try reload this plug-in once!");
                Game.DisplayNotification("If still can't, grab logs and visit <strong>Service Desk</strong>, URL is in logs!");
                Game.LogTrivial("URL: https://hotworkshop.atlassian.net/servicedesk/customer/portal/2");
                CrashReporter cr = new CrashReporter(e);
                cr.ReportAndCrashPlugin();
            }
            
            if (configuration.Version != 2)
            {
                initializationFile.Delete();
                Game.DisplayHelp("Please reload the plug-in using the console!");
                Game.DisplayNotification("Please reload the plug-in using the console!");
                CrashReporter cr = new CrashReporter(new FormatException("Invalid configuration version - please restart!"));
                cr.ReportAndCrashPlugin();
            }
        }
    }
}