using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon
{
    class RconClient
    {
        #region Events

        public event EventHandler Connected;
        private void OnConnected()
        {
            EventHandler handler = Connected;
            if (handler != null)
                handler(this, new EventArgs());
        }

        public event EventHandler Disconnected;
        private void OnDisconnected()
        {
            EventHandler handler = Disconnected;
            if (handler != null)
                handler(this, new EventArgs());
        }

        #endregion // Events

        private Rcon rcon;

        public bool IsConnected
        {
            get { return rcon.Connected; }
        }

        public RconClient()
        {
            rcon = new Rcon();
        }

        public bool Connect(string host, int port, string password, out string message)
        {
            try
            {
                if (!rcon.Connect(host, port))
                {
                    message = "Connection to server failed. Check your host and port.";
                    return false;
                }

                if (!rcon.Authenticate(password))
                {
                    message = "Authentication with server failed. The password is incorrect.";
                    rcon.Disconnect();
                    return false;
                }

                OnConnected();
                message = "";
                return true;
            }
            catch (SocketException sEx)
            {
                message = "Connection to server failed. Check your host and port.";
                return false;
            }
            catch (Exception ex)
            {
                ErrorHandler.CaptureException(ex);
                message = ex.Message;
                return false;
            }
        }

        public void Disconnect()
        {
            try
            {
                rcon.Disconnect();
                OnDisconnected();
            }
            catch (Exception ex)
            {
                ErrorHandler.CaptureException(ex);
                // What could happen here?
            }
        }

        public string ExecuteCommand(string command)
        {
            if (!IsConnected) return null;

            try
            {
                RconPacket response = rcon.SendReceive(new RconPacket(PacketType.ServerdataExeccommand, command.Substring(0, Math.Min(command.Length, 479))));
                return response.Body;
            }
            catch (SocketException sEx)
            {
                if (sEx.ErrorCode != 10060 && sEx.ErrorCode != 10004)
                {
                    Disconnect();
                    OnDisconnected();
                }

                if (sEx.ErrorCode == 10004)
                {
                    
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
