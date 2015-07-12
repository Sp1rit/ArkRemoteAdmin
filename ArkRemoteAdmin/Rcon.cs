using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArkRemoteAdmin.Data;
using RconSharp;
using RconSharp.Net45;

namespace ArkRemoteAdmin
{
    static class Rcon
    {
        private static RconSocket socket;
        private static RconMessenger messenger;

        static Regex playerMatch = new Regex(@"[0-9]\. (?<Name>.*), (?<SteamId>[0-9]{17})", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static event EventHandler<CommandExecutedEventArgs> CommandExecuted;
        private static void OnCommandExecuted(string command, string response)
        {
            EventHandler<CommandExecutedEventArgs> handler = CommandExecuted;
            if (handler != null)
                handler(null, new CommandExecutedEventArgs(command, response));
        }

        public static event EventHandler Connected;
        private static void OnConnected()
        {
            EventHandler handler = Connected;
            if (handler != null)
                handler(null, new EventArgs());
        }

        public static event EventHandler Disconnected;
        private static void OnDisconnected()
        {
            EventHandler handler = Disconnected;
            if (handler != null)
                handler(null, new EventArgs());
        }

        public static bool IsConnected
        {
            get { return socket != null && socket.IsConnected; }
        }

        public static Server Server { get; private set; }

        public static async Task<bool> Connect(Server server)
        {
            if (IsConnected)
                throw new Exception("You are already connected to a server.");

            Server = null;

            try
            {
                socket = new RconSocket();
                messenger = new RconMessenger(socket);
                bool connected = await messenger.ConnectAsync(server.Host, server.Port);

                if (!connected)
                {
                    MessageBox.Show("Could not connect to the server.");
                    return false;
                }

                bool authenticated = await messenger.AuthenticateAsync(Encryption.DecryptString(server.Password, "0?NRAnRBm;SWd41BUbKsT7)kN1y=RHLm=DR4ZZUBk&!JF3i\"Ra2Eg,8qwhA0^ydo"));

                if (!authenticated)
                {
                    MessageBox.Show("Could not authenticate to the server.");
                    return false;
                }

                Server = server;
                OnConnected();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not connect to the server.");
            }

            return false;
        }

        public static void Disconnect()
        {
            Server = null;
            messenger.CloseConnection();
            OnDisconnected();
        }

        public static async Task<string> ExecuteCommand(string command)
        {
            try
            {
                if (IsConnected)
                {
                    string response = await messenger.ExecuteCommandAsync(command);

                    if (command != "getchat")
                        OnCommandExecuted(command, response);

                    return response;
                }
            }
            catch (Exception ex)
            {
                Disconnect();
            }

            return "";
        }

        public static async Task<Player[]> ListPlayers()
        {
            if (!IsConnected)
                throw new Exception("You are not connected to a server.");

            string response = await ExecuteCommand("listplayers");

            List<Player> players =
                (from Match match in playerMatch.Matches(response)
                 select new Player()
                 {
                     Name = match.Groups["Name"].Value,
                     SteamId = match.Groups["SteamId"].Value
                 }).ToList();

            return players.ToArray();
        }

        public static async Task<bool> Broadcast(string message)
        {
            if (!IsConnected)
                throw new Exception("You are not connected to a server.");

            string response = await ExecuteCommand(string.Format("broadcast {0}", message.Replace(Environment.NewLine, "\n")));

            return response.Trim() == "Server received, But no response!!";
        }

        public static async Task<string> GetChat()
        {
            if (!IsConnected)
                throw new Exception("You are not connected to a server.");

            string response = await ExecuteCommand("getchat");
            response = response.Trim().Replace("\n", Environment.NewLine);
            if (response != "Server received, But no response!!")
                return response;

            return "";
        }

        public static async Task<bool> SendChatMessage(string message)
        {
            if (!IsConnected)
                throw new Exception("You are not connected to a server.");

            string response = await ExecuteCommand(string.Format("serverchat {0}", message.Replace(Environment.NewLine, "\n")));

            return response.Trim() == "Server received, But no response!!";
        }

        public static async Task<bool> ShowMessageOfTheDay()
        {
            if (!IsConnected)
                throw new Exception("You are not connected to a server.");

            string response = await ExecuteCommand("ShowMessageOfTheDay");

            return response.Trim() == "Message of the day Shown";
        }

        public static async Task<bool> SetTimeOfDay(DateTime time)
        {
            if (!IsConnected)
                throw new Exception("You are not connected to a server.");

            string response = await ExecuteCommand(string.Format("SetTimeOfDay {0:HH:mm:ss}", time));

            return response.Trim() == string.Format("Time of day has been set to {0:HH:mm:ss}", time);
        }

        public static async Task<bool> ExitServer()
        {
            if (!IsConnected)
                throw new Exception("You are not connected to a server.");

            string response = await ExecuteCommand("DoExit");

            return response.Trim() == "Exiting...";
        }
    }
}
