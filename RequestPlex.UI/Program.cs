﻿#region Copyright
// /************************************************************************
//    Copyright (c) 2016 Jamie Rees
//    File: Program.cs
//    Created By: Jamie Rees
//   
//    Permission is hereby granted, free of charge, to any person obtaining
//    a copy of this software and associated documentation files (the
//    "Software"), to deal in the Software without restriction, including
//    without limitation the rights to use, copy, modify, merge, publish,
//    distribute, sublicense, and/or sell copies of the Software, and to
//    permit persons to whom the Software is furnished to do so, subject to
//    the following conditions:
//   
//    The above copyright notice and this permission notice shall be
//    included in all copies or substantial portions of the Software.
//   
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//    EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//    NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//    LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//    OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ************************************************************************/
#endregion
using System;

using Microsoft.Owin.Hosting;

using Mono.Data.Sqlite;

using RequestPlex.Core;
using RequestPlex.Core.SettingModels;
using RequestPlex.Helpers;
using RequestPlex.Store;
using RequestPlex.Store.Repository;

namespace RequestPlex.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteOutVersion();

            var s = new Setup();
            s.SetupDb();

            var uri = GetStartupUri();

            using (WebApp.Start<Startup>(uri))
            {
                Console.WriteLine($"Request Plex is running on {uri}");
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
            }
        }

        private static void WriteOutVersion()
        {
            var assemblyVer = AssemblyHelper.GetAssemblyVersion();
            Console.WriteLine($"Version: {assemblyVer}");
        }

        private static string GetStartupUri()
        {
            var uri = "http://localhost:3579/";
            var service = new SettingsServiceV2<RequestPlexSettings>(new JsonRepository(new DbConfiguration(new SqliteFactory()), new MemoryCacheProvider()));
            var settings = service.GetSettings();

            if (settings.Port != 0)
            {
                uri = $"http://localhost:{settings.Port}";
            }

            return uri;
        }
    }
}
