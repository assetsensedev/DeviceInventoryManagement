﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeviceInventory.Domain;
using WindowsFormsApp1.Domain;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DeviceInventory
{
    public partial class DeviceInventoryForm : Form
    {
        public TypeEnum typeEnum;
        public GetDeviceProfileResponseDto deviceProfiles;
        public GetDeviceTypeResponseDto deviceTypes;
        public DeviceInventoryForm()
        {
            InitializeComponent();
            ReadServerAndUserName();
            this.loginPanel.BringToFront();
            
         
            //this.panel2.SendToBack();
            //this.panel2.Visible = false;
        }

        

        private async void LoginBtn_Click(object sender, EventArgs e)
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
                MessageBox.Show("Password is required");
            }
            if (string.IsNullOrEmpty(this.ServerURLTxt.Text))
            {
                isError = true;
                MessageBox.Show("Server Url is required");
            }
           
            if (isError)
            {
                return;
            }
            if (!ServerURLTxt.Text.EndsWith("/"))
            {
                ServerURLTxt.Text = ServerURLTxt.Text + "/";

            }
            ServiceProxyBase  serviceProxyBase = new ServiceProxyBase(new LoginDetailsDto
            (this.UsernameTxt.Text,  this.PasswordTxt.Text,this.ServerURLTxt.Text)
            );
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.loader.Visible = true;

            isError = ! await serviceProxyBase.CheckUserCreds();

           

            if (isError)
            {
                MessageBox.Show("Invalid Username and password");
                this.Cursor = System.Windows.Forms.Cursors.Default;
                this.loader.Visible = false;
            }
            // 
            //this.panel1.Visible = false;
            //this.panel2.Visible = true;
            //this.panel1.SendToBack();
            if (!isError)
            {
                deviceTypes = await GetDeviceTypes(serviceProxyBase);
                deviceProfiles = await GetDeviceProfiles(serviceProxyBase);
                this.DeviceTypeCombo.Items.Clear();
                this.DeviceTypeCombo.Items.AddRange(deviceTypes.DeviceTypes.Keys.Cast<Object>().ToArray());
                ReadActivityLog();
                var enviroment = System.Environment.CurrentDirectory + @"\logs\";
                string FileName = enviroment + "loginDetails.txt";
                if (!File.Exists(FileName))
                {
                    try
                    {
                        if (!Directory.Exists(enviroment))
                        {
                            Directory.CreateDirectory(enviroment);
                        }
                        File.WriteAllText(FileName, this.ServerURLTxt.Text + ", username: " + this.UsernameTxt.Text);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            
                this.devicePanel.BringToFront();
                
            
            }
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.loader.Visible = false;
        }

        private async Task<GetDeviceTypeResponseDto> GetDeviceTypes(ServiceProxyBase serviceProxyBase)
        {
            return await serviceProxyBase.GetDeviceTypes("services/lookupservice/getcategory/7240");
        }

        private async Task<GetDeviceProfileResponseDto> GetDeviceProfiles(ServiceProxyBase serviceProxyBase)
        {
            return await serviceProxyBase.GetDeviceProfile("services/deviceprofileservice/getdeviceprofiles");
        }
 
        private void ReadServerAndUserName()
        {
            try
            {
                var enviroment = System.Environment.CurrentDirectory + @"\logs\";
                string FileName = enviroment + "loginDetails.txt";
                
                if (File.Exists(FileName))
                {
                    using (FileStream fileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (StreamReader streamReader = new StreamReader(fileStream))
                        {
                           string loginDetails = streamReader.ReadToEnd();
                            if (!string.IsNullOrEmpty(loginDetails))
                            {


                                var splitArray = loginDetails.Split(new string[] { ", username: " }, StringSplitOptions.None);
                                var splitServerName = splitArray[0];
                                if (splitArray.Length >= 2)
                                {
                                    var splitUserName = splitArray[1];
                                    if (!string.IsNullOrEmpty(splitUserName))
                                    {
                                        this.UsernameTxt.Text = splitUserName;
                                    }

                                }

                                if (!string.IsNullOrEmpty(splitServerName))
                                {
                                    this.ServerURLTxt.Text = splitServerName;
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void SerialPortRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.TestRadioButton.Checked)
            {
                typeEnum = TypeEnum.Test;
                this.keyLabel.Text = "Device :";
                this.keyTxt.Visible = true;
                this.SerialPortCombo.Visible = false;
            }
            else if (this.SerialPortRadioButton.Checked)
            {
                typeEnum = TypeEnum.Interface;
                this.keyLabel.Text = "   Port :";
                this.keyTxt.Visible = false;
                this.SerialPortCombo.Visible = true;
            }
        }
        private void deviceComboValueChanged(object sender, EventArgs e)
        {

            System.Windows.Forms.ComboBox senderComboBox = (System.Windows.Forms.ComboBox)sender;

            var deviceTypeSelectedValue = (string)senderComboBox.SelectedItem;
            var deviceTypeSelectedkey = deviceTypes.DeviceTypes[deviceTypeSelectedValue];

           var deviceProfilesFiltered = deviceProfiles.DeviceProfiles.Where(x => x.Value.DeviceTypePresent.Equals(deviceTypeSelectedkey));
             
            this.DeviceProfileCombo.Items.Clear();
            this.DeviceProfileCombo.Items.AddRange(deviceProfilesFiltered.ToList().Select(x => x.Value.ProfileName).Cast<Object>().ToArray());
            if(this.DeviceProfileCombo.Items.Count > 0)
            {
                this.DeviceProfileCombo.SelectedIndex = 0;
                this.DeviceProfileCombo.Enabled = true;
            }
                
            else
            {
                this.DeviceProfileCombo.Enabled = false;
            }
        }

        private async Task proceedButton_click(object sender, EventArgs e)
        {
            
            string c2ServiceUrl = "services/deviceinventoryservice/savedeviceinventory";
            if (this.DeviceTypeCombo.SelectedItem == null || string.IsNullOrEmpty(this.DeviceTypeCombo.SelectedItem?.ToString()))
            {
                MessageBox.Show("Select the device type");
                return;
            }

            if (this.DeviceProfileCombo.SelectedItem == null || string.IsNullOrEmpty(this.DeviceProfileCombo.SelectedItem?.ToString()) )
            {
                MessageBox.Show("Select the device profile ");
                return;
            }
            if(this.DeviceProfileCombo.Enabled == false)
            {
                MessageBox.Show("Select different device Type as for current device type no device profile is present ");
                return;
            }

            var deviceTypeId = deviceTypes.DeviceTypes[this.DeviceTypeCombo.SelectedItem.ToString()];
            var deviceProfileDictionay = deviceProfiles.DeviceProfiles.FirstOrDefault(x => x.Value.ProfileName.Equals(this.DeviceProfileCombo.SelectedItem.ToString()));
            var profileId= deviceProfileDictionay.Key;
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
                root.DeviceInventory.deviceType = new DeviceType();
                root.DeviceInventory.deviceType.id = deviceTypeId;

                root.DeviceInventory.deviceProfile = new DeviceProfile();
                root.DeviceInventory.deviceProfile.id = profileId;

                ServiceProxyBase proxy = new ServiceProxyBase(new LoginDetailsDto(UsernameTxt.Text, PasswordTxt.Text, ServerURLTxt.Text));
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                this.loader.Visible = true;
              
                var reponseDto = await proxy.CreateDevice(c2ServiceUrl, root, typeEnum);
                this.loader.Visible = false;
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
            else if (typeEnum.Equals(TypeEnum.Interface))
            {
                if(this.SerialPortCombo.SelectedItem == null )
                {
                    MessageBox.Show("Kindly select the port");
                    return;
                }
                //this.richTextBox1.Text = "";
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                this.loader.Visible = true;
                SerialPortImplementation serialPortImplementation = new SerialPortImplementation(this.SerialPortCombo.SelectedItem.ToString(), new LoginDetailsDto(UsernameTxt.Text, PasswordTxt.Text, ServerURLTxt.Text), c2ServiceUrl, deviceTypeId, profileId);
                var message = await serialPortImplementation.main();
                this.loader.Visible = false;
                this.Cursor = System.Windows.Forms.Cursors.Default;
              
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
                    
                    DeviceLogger.MainLogger.Debug($"{ports[i]} and i value is :"+i);
                }
                return ports;
            }
            else
            {
               
                return new object[0];
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
            ReadActivityLog();
        }

        private void RefreshLogs(object sender, EventArgs e)
        {
            ReadActivityLog();
        }
        private void ReadActivityLog()
        {
            var enviroment = System.Environment.CurrentDirectory+@"\logs\";
            string FileName = enviroment + "activityFile.txt";
            //if (File.Exists(FileName))
            //{
            //   //string[] lines = ;
            //    foreach (string line in File.ReadAllLines(FileName).Reverse())
            //    {
            //        this.richTextBox1.AppendText(line+"\n");
            //    }
                
            //}
            if (File.Exists(FileName))
            {
                try
                {
                    using (FileStream fileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (StreamReader streamReader = new StreamReader(fileStream))
                        {
                            richTextBox1.Text = streamReader.ReadToEnd();
                        }
                    }
                    this.richTextBox1.ScrollToBottom();
                    // wfe.scrollToBottom(txtActivityStatusLog);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            
        }

       
    }
}
