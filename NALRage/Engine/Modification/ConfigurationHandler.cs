// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

using NALRage.Entities.Serialization;
using Rage;
using System;

namespace NALRage.Engine.Modification
{
    internal static class ConfigurationHandler
    {
        private static readonly InitializationFile InitializationFile = new InitializationFile("NAL\\settings.ini");
        private static Configuration configuration;

        internal static Configuration Config => configuration;

        private static void GenerateConfig()
        {
            configuration = new Configuration(2);
            InitializationFile.Create();
            InitializationFile.Write("Main", "Version", configuration.Version);
            InitializationFile.Write("Main", "DefaultDifficulty", configuration.DefaultDifficulty);
            InitializationFile.Write("Main", "LoopInterval", configuration.ProcessInterval);
            InitializationFile.Write("Main", "Riot", configuration.Riot);
            InitializationFile.Write("Event", "RequirementChance", configuration.EventRequirement);
            InitializationFile.Write("Event", "MaxValue", configuration.EventMax);
            InitializationFile.Write("Event", "MinimalValue", configuration.EventMinimal);
        }

        internal static void Init()
        {
            if (!InitializationFile.Exists()) GenerateConfig();
            configuration = new Configuration();
            try
            {
                configuration.Version = InitializationFile.ReadInt32("Main", "Version");
            }
            catch (Exception e)
            {
                InitializationFile.Delete();
                Game.DisplayNotification("Please try reload this plug-in once!");
                Game.DisplayNotification("If still can't, grab logs and visit <strong>Service Desk</strong>, URL is in logs!");
                Game.LogTrivial("URL: https://hotworkshop.atlassian.net/servicedesk/customer/portal/2");
                var cr = new CrashReporter(e);
                cr.ReportAndCrashPlugin();
            }

            if (configuration.Version != 2)
            {
                InitializationFile.Delete();
                Game.DisplayHelp("Please reload the plug-in using the console!");
                Game.DisplayNotification("Please reload the plug-in using the console!");
                new CrashReporter(new FormatException("Invalid configuration version - please restart!")).ReportAndCrashPlugin();
            }
        }
    }
}