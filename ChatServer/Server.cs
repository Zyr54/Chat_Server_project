using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace Chat
{

    public static class Server
    {

        public static Hashtable clientsList = new Hashtable();

        public static void Launcher()
        {
            TcpListener serverSocket = new TcpListener(8888);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();
            Console.WriteLine("Chat Server Started ....");
            counter = 0;
            while ((true))
            {
                handleClinet client = new handleClinet();
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                System.Console.WriteLine("Loop " + counter + " listOfClient: " + clientsList.ToString());
                NetworkStream networkStream = clientSocket.GetStream();
                System.Console.WriteLine("GetStream n°" + counter);
                var sr = new StreamReader(networkStream);
                System.Console.WriteLine("Streamreader created n°" + counter);
                String dataFromClient = sr.ReadLine();
                System.Console.WriteLine("Readline n°" + counter);
                User user = new User(dataFromClient);
                using (var context = new DatabaseContext()){
                    context.Add(user);
                    context.SaveChanges();
                }
                clientsList.Add(dataFromClient, clientSocket);
                System.Console.WriteLine("AddClient to list n°" + counter);

                broadcast(dataFromClient + " Joined ", dataFromClient, false);

                Console.WriteLine(dataFromClient + " Joined chat room ");
                client.startClient(clientSocket, user, clientsList);
            }
        }

        public static void broadcast(string msg, string uName, bool flag)
        {
            System.Console.WriteLine("broadcast");
            foreach (DictionaryEntry Item in clientsList)
            {
                TcpClient broadcastSocket;
                broadcastSocket = (TcpClient)Item.Value;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                var sw = new StreamWriter(broadcastStream);
                String broadcastMsg = "";

                if (flag == true)
                {
                    broadcastMsg = uName + " says : " + msg;
                }
                else
                {
                    broadcastMsg = msg;
                }

                sw.WriteLine(broadcastMsg);
                broadcastStream.Flush();
                sw.Flush();
            }
        }  //end broadcast function
    }//end Main class

    public class handleClinet
    {
        TcpClient clientSocket;
        User clNo;
        Hashtable clientsList;
        Thread ctThread;

        public void startClient(TcpClient inClientSocket, User clineNo, Hashtable cList)
        {
            System.Console.WriteLine("Start Client");
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            this.clientsList = cList;
            this.ctThread = new Thread(doChat);
            ctThread.Start();
        }

        public static void disconnectedClient(handleClinet hC)
        {
            System.Console.WriteLine("Client " + hC.clNo._username + " disconnected");
            String msg = hC.clNo._username + " has disconnected";
            User clNo = hC.clNo;
            Server.clientsList.Remove(hC.clNo._username);
            Server.broadcast(msg, clNo._username, false);
        }

        private void doChat()
        {
            System.Console.WriteLine("Do Chat");
            int requestCount = 0;
            string serverResponse = null;
            string rCount = null;
            requestCount = 0;

            while (true)
            {
                try
                {
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = clientSocket.GetStream();
                    var sr = new StreamReader(networkStream);
                    string dataFromClient = sr.ReadLine();
                    Console.WriteLine("From client - " + clNo._username + " : " + dataFromClient);
                    Message inputedMsg = new Message(clNo, dataFromClient);
                    using (var context = new DatabaseContext()){

                        User? u = context.Users.FirstOrDefault(x => x._userID == inputedMsg.User._userID);

                        if(u != null)
                        {
                            inputedMsg.User = u;
                        }

                        context.Add(inputedMsg);
                        context.SaveChanges();
                    }
                    rCount = Convert.ToString(requestCount);
                    Server.broadcast(dataFromClient, clNo._username, true);
                }
                catch (Exception ex)
                {
                    disconnectedClient(this);
                    return;
                }
            }//end while
        }//end doChat
    } //end class handleClinet
}//end namespace
