using Seventh.DGuard.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Seventh.DGuard.Domain.Entities
{
    public class Server : Entity
    {
        private readonly List<Video> _videos = new List<Video>();

        private Server()
        {
        }

        public Server(string name, string ip, UInt16 port)
        {
            Name = name;
            IP = ip;
            Port = port;
        }

        public String Name { get; private set; }

        public String IP { get; private set; }

        public UInt16 Port { get; private set; }

        public IReadOnlyList<Video> Videos => _videos.AsReadOnly();

        public void ChangeInfo(string name, string ip, UInt16 port)
        {
            Name = name;
            IP = ip;
            Port = port;
        }

        public bool IsAvailable()
        {
            using (TcpClient client = new TcpClient(IP, Port))
            {
                IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();

                TcpConnectionInformation[] tcpConnections = ipProperties.GetActiveTcpConnections()
                    .Where(x => x.LocalEndPoint.Equals(client.Client.LocalEndPoint) && x.RemoteEndPoint.Equals(client.Client.RemoteEndPoint))
                    .ToArray();

                if (tcpConnections != null && tcpConnections.Length > 0)
                {
                    TcpState stateOfConnection = tcpConnections.First().State;

                    return stateOfConnection == TcpState.Established;
                }

                return false;
            }
        }

        public Video AddVideo(string path, string description)
        {
            Video newVideo = new Video(description, path);

            _videos.Add(newVideo);

            return newVideo;
        }
    }
}