﻿// ****************************************************************************************
// Copyright (C) 2010, Jorn Skaarud Karlsen 
// All rights reserved. 
//
// Redistribution and use in source and binary forms, with or without modification, are 
// permitted provided that the following conditions are met: 
//
// * Redistributions of source code must retain the above copyright notice, this list of 
//   conditions and the following disclaimer. 
// * Redistributions in binary form must reproduce the above copyright notice, this list 
//   of conditions and the following disclaimer in the documentation and/or other 
//   materials provided with the distribution. 
// * Neither the name of OFFIS e.V. nor the names of its contributors may be used to 
//   endorse or promote products derived from this software without specific prior 
//   written permission. 
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY 
// EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF 
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL 
// THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT 
// OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT 
// LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE 
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//****************************************************************************************

using System;
using System.Globalization;
using System.IO;

namespace Dicom2Volume
{
    public class Logger
    {
        public enum LogLevelType
        {
            Error = 3, 
            Warn  = 2, 
            Info  = 1, 
            Debug = 0
        }

        public static void Warn(string message, params object[] arguments)
        {
            Log(LogLevelType.Warn, message, arguments);
        }

        public static void Error(string message, params object[] arguments)
        {
            Log(LogLevelType.Error, message, arguments);
        }

        public static void Info(string message, params object[] arguments)
        {
            Log(LogLevelType.Info, message, arguments);
        }

        public static void Debug(string message, params object[] arguments)
        {
            Log(LogLevelType.Debug, message, arguments);
        }

        public static void Log(LogLevelType levelType, string message, params object[] arguments)
        {
            var logMessage = levelType + "::" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "::" + String.Format(message, arguments);
            var previousForegroundColor = Console.ForegroundColor;
            var messageColor = Console.ForegroundColor;
            switch (levelType)
            {
                case LogLevelType.Warn:
                    messageColor = ConsoleColor.DarkMagenta;
                    break;
                case LogLevelType.Error:
                    messageColor = ConsoleColor.DarkRed;
                    break;
                case LogLevelType.Info:
                    messageColor = ConsoleColor.DarkGreen;
                    break;
                case LogLevelType.Debug:
                    messageColor = ConsoleColor.DarkYellow;
                    break;
            }

            if (levelType < Config.LogLevel) return;

            Console.ForegroundColor = messageColor;
            Console.WriteLine(logMessage);
            Console.ForegroundColor = previousForegroundColor;
        }
    }
}
