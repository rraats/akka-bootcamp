using System;
using System.Configuration;
using System.Windows.Forms;
using Akka.Actor;
using Akka.Configuration.Hocon;

namespace GithubActors
{
    /// <summary>
    /// Access token: 4b28200d1f13a24c849d492a07abb92d3361f6b4
    /// Repo URL: https://github.com/akkadotnet/akka.net
    /// Repo URL: https://github.com/petabridge/akka-bootcamp
    /// Repo URL: https://github.com/Aaronontheweb/mvc-utilities
    /// Repo URL: https://github.com/rraats/Helpers
    /// </summary>
    static class Program
    {
        /// <summary>
        /// ActorSystem we'llbe using to collect and process data
        /// from Github using their official .NET SDK, Octokit
        /// </summary>
        public static ActorSystem GithubActors;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GithubActors = ActorSystem.Create("GithubActors");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GithubAuth());
        }
    }
}
