using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO.Ports;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Domain;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public TypeEnum typeEnum;
        public Form1()
        {
            InitializeComponent();
            this.panel1.BringToFront();

            //this.panel2.SendToBack();
            //this.panel2.Visible = false;
        }

        

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            bool isError = false;
            if(string.IsNullOrEmpty(this.UsernameTxt.Text))
            {
                isError = true;
                MessageBox.Show("Username is required");
            }
            if (string.IsNullOrEmpty(this.PasswordTxt.Text))
            {
                isError = true;
                MessageBox.Show("Paasword is required");
            }
            
            // 
            //this.panel1.Visible = false;
            //this.panel2.Visible = true;
            //this.panel1.SendToBack();
            if (! isError)
            this.panel2.BringToFront();
        }

        

        

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {

            System.Windows.Forms.ComboBox senderComboBox = (System.Windows.Forms.ComboBox)sender;

            // Change the length of the text box depending on what the user has 
            // selected and committed using the SelectionLength property.
            if (senderComboBox.SelectedItem.ToString().Equals(TypeEnum.Test.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                typeEnum = TypeEnum.Test;
                this.keyLabel.Text = "Device";
                this.keyTxt.Visible = true;
                this.SerialPortCombo.Visible = false;
            }
            else if(senderComboBox.SelectedItem.ToString().Equals(TypeEnum.Interface.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                typeEnum = TypeEnum.Interface;
                this.keyLabel.Text = "Serial Port";
                this.keyTxt.Visible = false;
                this.SerialPortCombo.Visible = true;
            }
        }

        private async Task proceedButton_click(object sender, EventArgs e)
        {
            
            string c2ServiceUrl = "services/deviceinventoryservice/savedeviceinventory";
            if (typeEnum.Equals(TypeEnum.Test))
            {
                if (string.IsNullOrEmpty(this.keyTxt.Text))
                {
                    MessageBox.Show("Enter the key");
                    return;
                }
                this.richTextBox1.Text = "";
                CreateDeviceInventoryDto root = new CreateDeviceInventoryDto();
                root.DeviceInventory = new DeviceInventory();
                root.DeviceInventory.deviceCode = this.keyTxt.Text;
                ServiceProxyBase proxy = new ServiceProxyBase(new LoginDetailsDto(UsernameTxt.Text, PasswordTxt.Text, ServerURLTxt.Text));
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                this.pictureBox1.Visible = true;
              
                var reponseDto = await proxy.MakeAPICall(c2ServiceUrl, root);
                this.pictureBox1.Visible = false;
                this.Cursor = System.Windows.Forms.Cursors.Default;
                this.richTextBox1.Rtf = reponseDto.message;


            }
            else if (typeEnum.Equals(TypeEnum.Interface))
            {
                if(this.SerialPortCombo.SelectedItem == null )
                {
                    MessageBox.Show("Kindly select the port");
                    return;
                }
                this.richTextBox1.Text = "";

                SerialPortImplementation serialPortImplementation = new SerialPortImplementation(this.SerialPortCombo.SelectedItem.ToString(), new LoginDetailsDto(UsernameTxt.Text, PasswordTxt.Text, ServerURLTxt.Text), c2ServiceUrl);
                var message = await serialPortImplementation.main();
                this.richTextBox1.Text = message;
            }
        }

      

        private object[] GetSerialPort()
        {
            string[] ports = SerialPort.GetPortNames();
            if(ports != null && ports.Length > 0)
            {
                object[] port = new object[ports.Length];
                for (int i = 0; i < ports.Length; i++)
                {
                    
                    DeviceLogger.logger.Debug($"{ports[i]} and i value is :"+i);
                }
                return ports;
            }
            else
            {
                object[] objects = new object[2];
                objects[0] = "COM4";
                objects[1] = "COM11";
                return objects;
            }
           
            //try
            //{


            //    
            //    if (ports != null && ports.Length > 0)
            //    {
            //        object[] objects = new object[ports.Length + 3];
            //        DeviceLogger.logger.Debug("The following serial ports were found:");
            //        objects[0] = "COM1";
            //        objects[1] = "COM2";
            //        objects[2] = "COM3";
            //        // Display each port name to the console.
            //        for (int i = 3; i < ports.Length + 3; i++)
            //        {
            //            string port = ports[i - 3];
            //            if (port == "COM1" || port == "COM2" || port == "COM3")
            //            {
            //                continue;
            //            }

            //            objects[i] = port;
            //            DeviceLogger.logger.Debug($"{port}");
            //        }

            //        return objects;
            //    }
            //    else
            //    {
            //        object[] objects = new object[3];
            //        objects[0] = "COM1";
            //        objects[1] = "COM2";
            //        objects[2] = "COM3";
            //        return objects;
            //    }
            //}
            //catch(Exception ex)
            //{
            //    DeviceLogger.logger.Error("Exeption is getting serial port " + ex.Message);
            //    object[] objects = new object[2];
            //    objects[0] = "COM4";
            //    objects[1] = "COM11";
            //    return objects;
            //}

        }

        private async void proceedButton_Click_1(object sender, EventArgs e)
        {
            await this.proceedButton_click(sender, e);
        }
    }
}
