using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
namespace Client;

using System.Diagnostics;
using System.Windows.Forms;

public partial class Form1 : Form
{
    private TextBox textBox1;
    private TextBox textBox2;
    private TextBox textBox3;
    System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
    NetworkStream serverStream = default(NetworkStream);
    string readData = null;
    public Form1()
    {
        InitializeComponent();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        System.Console.WriteLine("click 2");
        //byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox2.Text + "$");
        var sw = new StreamWriter(serverStream);
        System.Console.WriteLine("stream writer created");
        sw.WriteLine(textBox2.Text);
        System.Console.WriteLine("wrote");
        //serverStream.Write(outStream, 0, outStream.Length);
        serverStream.Flush();
        sw.Flush();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        readData = "Connected to Chat Server ...";
        System.Console.WriteLine("before msg");
        msg();
        Debug.WriteLine("after message");
        clientSocket.Connect("127.0.0.1", 8888);
        Debug.WriteLine("after connect");
        serverStream = clientSocket.GetStream();
        Debug.WriteLine("stream getted");
        var sw = new StreamWriter(serverStream);
        Debug.WriteLine("new streamreader");
        sw.WriteLine(textBox1.Text);
        Debug.WriteLine("wrote in stream");

        //byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox1.Text + "$");
        //serverStream.Write(outStream, 0, outStream.Length);
        serverStream.Flush();
        sw.Flush();

        Thread ctThread = new Thread(getMessage);
        ctThread.Start();
    }

    private void button3_Click(object sender, EventArgs e){
        Environment.Exit(1);
        Application.Exit();
    }

    private void getMessage()
    {
        while (true)
        {
            serverStream = clientSocket.GetStream();
            var sr = new StreamReader(serverStream);
            string returndata = sr.ReadLine();
            //int buffSize = 0;
            //byte[] inStream = new byte[10025];
            //buffSize = clientSocket.ReceiveBufferSize;
            //serverStream.Read(inStream, 0, buffSize);
            //string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            readData = "" + returndata;
            msg();
        }
    }

    private void msg()
    {
        if (this.InvokeRequired)
            this.Invoke(new MethodInvoker(msg));
        else
            textBox3.Text = textBox3.Text + Environment.NewLine + " >> " + readData;
    }
}
