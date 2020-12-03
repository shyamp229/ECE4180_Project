using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;

namespace Final_Project_4180
{
    public partial class Form1 : Form
    {
        private String[] portNames;
        private String[] songs = { "africa-toto", "around_the_world-atc", "beautiful_life-ace_of_base", "dont_speak-no_doubt", "my-love", "Song1_test" };
        private bool play = false;
        private String song = "";
      
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // add port names to comboBox1

            Debug.Print("hello 4180 project!");
            try
            {
                Debug.Print("init load");
                portNames = SerialPort.GetPortNames();
                comboBox1.Items.AddRange(portNames);
                comboBox1.SelectedIndex = 0;
                Debug.Print("end load");
            }
            catch (Exception err)
            {
                Debug.Print("loading error: " + err.Message);
            }
            Debug.Print("Connected Mbed to " + comboBox1.Text);


        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
            serialPort1.PortName = comboBox1.Text;
            serialPort1.Open();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            serialPort1.Close();
        }


        /* Songs:
            Africa
            Around the World
            Beautiful Life
            Don't Speak
            My Love
            Song1_test 
        */
        /*
         * info:
         * button1 = play/pause
         * 1 = Africa
         * 2 = Around the World
         * 3 = Beautiful Life
         * 4 = Don't Speak
         * 5 = My Love
         * 6 = Song1_test
         */
        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedItem.ToString())
            {
                case "Africa":
                    // send song string to mbed
                    Debug.Print("africa-toto");
                    song = "!1";
                    break;
                case "Around the World":
                    Debug.Print("around_the_world-atc");
                    song = "!2";
                    break;
                case "Beautiful Life":
                    Debug.Print("beautiful_life-ace_of_base");
                    song = "!3";
                    break;
                case "Don't Speak":
                    Debug.Print("dont_speak-no_doubt");
                    song = "!4";
                    break;
                case "My Love":
                    Debug.Print("my-love");
                    song = "!5";
                    break;
                case "Song1_test":
                    Debug.Print("Song1_test");
                    song = "!6";
                    break;
                default:
                    break;
            }
            label3.Text = comboBox2.SelectedItem.ToString();
            if (serialPort1.IsOpen)
            {
                serialPort1.Write(song);
            }
            else
            {
                try
                {
                    serialPort1.Open();
                } catch (Exception err)
                {
                    Debug.Print("Error: " + err.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
             * if (play == true)
             *      send signal via serialPort to play song
             *      
             * if (play == false)
             *      send signal via serialPort to stop song
             */
            play = !play;
            if (serialPort1.IsOpen)
            {
                if (play == true)
                {
                    serialPort1.Write("!P");
                }
                else
                {
                    serialPort1.Write("!S");
                }
            }
            else
            {
                try
                {
                    serialPort1.Open();
                } catch (Exception err)
                {
                    Debug.Print("Error: " + err.Message);
                }
            }

        }
    }
}
