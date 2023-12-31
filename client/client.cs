using Accessibility;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace client
{
    public partial class client : Form
    {
        public client()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Connect();
        }
        IPEndPoint IPServer;
        Socket clients;
        Socket node;

        bool token = false;
        private void btn_send_Click(object sender, EventArgs e)
        {
            if (this.token)
            {
                Send();
                //AddMess(txt_send.Text);
                txt_send.Text = "";
            }
            else
            {
                MessageBox.Show("chưa có quyền");
            }

        }
        void Connect()
        {

            clients = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clients.Connect(IPServer);
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối1");
            }

            Thread listen = new Thread(Receive);
            listen.IsBackground = true;
            listen.Start();
        }
        void Close()
        {
            clients.Close();
        }
        void Send()
        {
            if (txt_send.Text == String.Empty || txt_send.Text.Length > 255) return;
            try
            {
                clients.Send(Serialize(txt_send.Text));
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối");
            }

        }
        void ReceiveToken()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024];
                    node.Receive(data);
                    String token = (String)DeSerialize(data);
                    MessageBox.Show("da nhan node " + token);
                    if (token.Equals("passtoken"))
                    {
                        this.token = true;
                        Thread.Sleep(3000);
                        passToken();
                    }

                }
            }
            catch
            {
                MessageBox.Show("dell nhan được node " + token);
                node.Close();
            }
        }
        void passToken()
        {
            try
            {
                node.Send(Serialize("1"));
                this.token = false;
            }
            catch
            {
                MessageBox.Show("không thể pass node");
            }
        }
        void Receive()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024 * 256];
                    clients.Receive(data);
                    String message = (String)DeSerialize(data);
                    if (message.Equals("passtoken@@@"))
                    {
                        token = true;
                        MessageBox.Show("da nhan node ");
                    }
                    else
                    {
                        if (message.Equals("recovertoken@@@"))
                        {
                            token = false;
                            MessageBox.Show("da huy node ");
                        }
                        else
                        {
                            AddMess(message);
                        }
                    }
                }
            }
            catch
            {
                Close();
            }

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
        private void client_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            passToken();
        }

        private void txt_send_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.token)
                {
                    Send();
                    //AddMess(txt_send.Text);
                    txt_send.Text = "";
                }
                else
                {
                    MessageBox.Show("chưa có quyền");
                }
            }
        }
    }
}