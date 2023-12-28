using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;

namespace server
{
    public partial class server : Form
    {
        public server()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Connect();
        }

        IPEndPoint IPclients;
        Socket Server;
        List<Socket> listClients;
        private int currentClientIndex = 0;
        AutoResetEvent tokenEvent = new AutoResetEvent(false);
        void Connect()
        {
            listClients = new List<Socket>();
            IPclients = new IPEndPoint(IPAddress.Any, 5000);
            Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Server.Bind(IPclients);

            Thread Listen = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        Server.Listen(10);
                        Socket client = Server.Accept();
                        listClients.Add(client);
                        Thread listen = new Thread(Receive);
                        listen.IsBackground = true;
                        listen.Start(client);
                        ThreadPool.QueueUserWorkItem(TokenRing);
                    }
                }
                catch
                {
                    IPclients = new IPEndPoint(IPAddress.Any, 5000);
                    Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }


            });
            Listen.IsBackground = true;
            Listen.Start();
        }
        void Receive(object obj)
        {
            Socket client = (Socket)(obj);
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024 * 256];
                    client.Receive(data);
                    String time = DateTime.Now.ToString("HH:mm:ss");
                    String message = (String)DeSerialize(data);
                    String str = time + ":  " + message;
                    foreach (Socket item in listClients)
                    {
                        item.Send(Serialize(str));

                    }
                    AddMess(str);
                }
            }
            catch
            {
                listClients.Remove(client);
                client.Close();
            }

        }
        private void TokenRing(object state)
        {
            while (true)
            {
                AddMess(currentClientIndex.ToString());
                Socket item = listClients[currentClientIndex];
                item.Send(Serialize("passtoken@@@"));
                Thread.Sleep(8000); // Simulate some work

                tokenEvent.Set(); // Signal the current client to perform its task
                item.Send(Serialize("recovertoken@@@"));
                Thread.Sleep(8000);
                // Move to the next client in the ring
                if (listClients.Count == 1){
                    currentClientIndex = 0;
                }else{
                    if (currentClientIndex+1 == listClients.Count){
                        currentClientIndex = 0;
                    }else{
                        currentClientIndex = (currentClientIndex + 1);
                    }
                }                               
                //currentClientIndex = (currentClientIndex + 1) % listClients.Count;

            }
        }
        void Close()
        {
            Server.Close();
        }
        void Send(Socket client)
        {
            String time = DateTime.Now.ToString("HH:mm:ss");
            String str = time + ":  " + txt_send.Text;
            if (txt_send.Text == String.Empty || txt_send.Text.Length > 255) return;
            client.Send(Serialize(str));
        }


        byte[] Serialize(String str)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, str);
            return stream.ToArray();
        }
        object DeSerialize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);

        }
        void AddMess(String str)
        {
            view_mess.Items.Add(new ListViewItem() { Text = str });
        }
        private void server_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            foreach (Socket item in listClients)
            {
                Send(item);

            }
            String time = DateTime.Now.ToString("HH:mm:ss");
            String str = time + ":  " + txt_send.Text;
            AddMess(str);
            txt_send.Text = "";
        }


        private void txt_send_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter) {
                foreach (Socket item in listClients)
                {
                    Send(item);

                }
                String time = DateTime.Now.ToString("HH:mm:ss");
                String str = time + ":  " + txt_send.Text;
                AddMess(str);
                txt_send.Text = "";
            }
        }
    }
}