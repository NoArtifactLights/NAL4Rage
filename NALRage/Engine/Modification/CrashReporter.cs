using NALRage.Engine.Modification.API;
using Rage;
using System;

namespace NALRage.Engine.Modification
{
    internal class CrashReporter
    {
        public CrashReporter(Exception ex)
        {
            Exc = ex;
        }

        internal static void CreateNewReportAndCrash(Exception ex)
        {
            new CrashReporter(ex).ReportAndCrashPlugin();
        }
        
        internal void ReportAndCrashPlugin()
        {
            Logger.Fatal("CrashReporter", "--------------------------------------------------");
            Logger.Fatal("CrashReporter", "NAL encountered a problem and must exit!");
            Logger.Fatal("CrashReporter", "Exception: " + Exc.GetType());
            Logger.Fatal("CrashReporter", "Hardware Specifications: ");
            Logger.Fatal("CrashReporter", "OS Version: " + Environment.OSVersion.VersionString);
            Logger.Fatal("CrashReporter", "x64 Process: " + Environment.Is64BitProcess);
            Logger.Fatal("CrashReporter", "Game Version: " + Game.BuildNumber);
            Logger.Fatal("CrashReporter", "--------------------------------------------------");
            Logger.Fatal("CrashReporter", "Exception Message: " + Exc.Message);
            Logger.Fatal("CrashReporter", Exc.StackTrace);
            Logger.Fatal("CrashReporter", "--------------------------------------------------");
            throw new Exception("Aborting this instance! See report above!");
        }

        internal Exception Exc { get; private set; }
    }
}