// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

using Rage;


namespace NALRage.Engine.Modification.API
{
    internal static class Logger
    {
        internal static void Log(string sender, string text, string level)
        {
            Game.LogTrivial($"[{sender}/{level}] {text}");
        }

        internal static void Trace(string sender, string text)
        {
            Log(sender, text, "TRACE");
        }

        internal static void Debug(string sender, string text)
        {
            Log(sender, text, "DEBUG");
        }

        internal static void Info(string sender, string text)
        {
            Log(sender, text, "INFO");
        }

        internal static void Warn(string sender, string text)
        {
            Log(sender, text, "WARN");
        }

        internal static void Error(string sender, string text)
        {
            Log(sender, text, "ERROR");
        }

        internal static void Fatal(string sender, string text)
        {
            Log(sender, text, "FATAL");
        }
    }
}