//************************************************************************************************
// Copyright © 2015 Steven M Cohn.  Yada yada...
//************************************************************************************************

namespace Shadow
{
	using System;
	using System.Configuration;
	using System.Diagnostics;
	using System.IO;
	using System.Text.RegularExpressions;


	static class Program
	{

		[STAThread]
		static void Main ()
		{
			string path = null;

			var args = Environment.GetCommandLineArgs();
			if (args.Length > 1)
			{
				// target specified on command line
				path = args[1];
			}
			else
			{
				// target specified in app.config
				var target = ConfigurationManager.AppSettings["target"];
				if (!string.IsNullOrEmpty(target))
				{
					path = ParseTarget(target);
				}
			}

			// PEBSAK
			if (string.IsNullOrEmpty(path))
			{
				Console.WriteLine("Could not find target");
				Environment.Exit(1);
			}

			// do it!
			var info = new ProcessStartInfo()
			{
				FileName = path,
				WindowStyle = ProcessWindowStyle.Hidden
			};

			Process.Start(info);
		}


		private static string ParseTarget (string target)
		{
			var value = target;

			var matches = Regex.Match(target, @".*?(?<key>[\$\%].*?[\$\%]).*");
			if (matches.Success)
			{
				var match = matches.Groups["key"];
				var key = target.Substring(match.Index + 1, match.Length - 2);

				if (match.Value[0] == '$')
				{
					// $..$ == special folder
					Enum.TryParse<Environment.SpecialFolder>(key, out var special);
					value = target.Replace(match.Value, Path.Combine(Environment.GetFolderPath(special)));
				}
				else
				{
					// %..% == environment variable
					var env = Environment.GetEnvironmentVariable(key);
					value = target.Replace(match.Value, env);
				}
			}

			return value;
		}
	}
}
