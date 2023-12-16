using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public  class SerialPortImplementation
    {
        private readonly string pcCommPort;
        private readonly string c2ServerUrl;
        private const string command1 = "CMD1";
        private const string command2 = "CMD2";
        private const string command3 = "CMD3";
        private  SerialPort hComm;
        private readonly ServiceProxyBase proxyBase;
        private const string Success = "Success";


        public SerialPortImplementation(string port, LoginDetailsDto loginDetailsDto, string c2ServerUrl)
        {
            pcCommPort = port;
            this.c2ServerUrl = c2ServerUrl;
            proxyBase = new ServiceProxyBase(loginDetailsDto);


        }
        public async Task<string > main()
        {
            string message = string.Empty;
            var returnValue = SerialPort_Init();
            if (returnValue)
            {
                SerialPort_Write(command1); // Command to read the DevEUI of LoRa device
                var deviceKey = SerialPort_Read(StepsEnum.ReadFirstAcknowledgement);
                if (!string.IsNullOrEmpty(deviceKey))
                {
                    CreateDeviceInventoryDto createDeviceInventoryDto = new CreateDeviceInventoryDto();
                    createDeviceInventoryDto.DeviceInventory = new DeviceInventory();
                    createDeviceInventoryDto.DeviceInventory.deviceCode = deviceKey;
                    var responseDto = await proxyBase.MakeAPICall(c2ServerUrl, createDeviceInventoryDto, TypeEnum.Interface);
                    if(!string.IsNullOrEmpty(responseDto.NwkKey) && !string.IsNullOrEmpty(responseDto.AppKey))
                    {
                        var hexAppStringBeforeCRC = Encryption.EncryptHexString(responseDto.AppKey);
                        var hexAppString = Encryption.CRCCalculation(hexAppStringBeforeCRC, hexAppStringBeforeCRC.Length);
                        var hexNwkStringBeforeCrc = Encryption.EncryptHexString(responseDto.NwkKey);
                        var hexNwkString = Encryption.CRCCalculation(hexNwkStringBeforeCrc, hexNwkStringBeforeCrc.Length);
                        if (!string.IsNullOrEmpty(hexAppString) && !string.IsNullOrEmpty(hexNwkString))
                        {
                            SerialPort_Write(command2);
                            if (SerialPort_Read(StepsEnum.ReadSecondAcknowledgement) == Success)
                            {
                                SerialPort_Write(hexAppString);
                                if (SerialPort_Read(StepsEnum.ReadFirstOk) == Success)
                                {
                                    SerialPort_Write(command3);
                                    if (SerialPort_Read(StepsEnum.ReadSecondAcknowledgement) == Success)
                                    {
                                        SerialPort_Write(hexNwkString);
                                        if (SerialPort_Read(StepsEnum.ReadFirstOk) == Success)
                                        {
                                            message = "Success";
                                            CloseConnection();
                                            DeviceLogger.MainLogger.Debug("Success");
                                        }
                                        else
                                        {
                                            CloseConnection();
                                            message = "Error in reading ok after sending Nwk string";
                                            DeviceLogger.MainLogger.Error(message);
                                        }
                                    }
                                    else
                                    {
                                        CloseConnection();
                                        message = "Error Programming the device. ACK Failed for CMD2";
                                        DeviceLogger.MainLogger.Error(message);
                                    }
                                }
                                else
                                {
                                    CloseConnection();
                                    message = "Error in reading ok after sending app string";
                                    DeviceLogger.MainLogger.Error(message);
                                }
                            }
                            else
                            {
                                CloseConnection();
                                message = "Error Programming the device. ACK Failed for CMD2";
                                DeviceLogger.MainLogger.Error("Error Programming the device. ACK Failed for CMD2");
                            }
                        }
                        else
                        {
                            CloseConnection();
                            message = "APP key or network key is null or empty";
                        }
                    }
                  

                }
                else
                {
                    CloseConnection();
                    message = "Acknowledgement not proper from serial port device";
                    DeviceLogger.MainLogger.Error(message);
                }
            }
            else
            {
                CloseConnection();
                message = "Could not communicate with serial port ";
            }
            return message;
        }

        public bool SerialPort_Init()
        {
            try
            {
                hComm = new SerialPort(pcCommPort, 115200, Parity.None, 8, StopBits.One);
                hComm.ReadTimeout = 10000;
                hComm.WriteTimeout = 1000;
                hComm.Open();
                DeviceLogger.MainLogger.Debug("Opening serial port successful");
            }
            catch (UnauthorizedAccessException)
            {
                CloseConnection();
                DeviceLogger.MainLogger.Error("cannot open port!");
                return false;
            }
            catch (Exception ex)
            {
                CloseConnection();
                DeviceLogger.MainLogger.Error($"invalid handle value! with exception as {ex.Message}");
                return false;
            }
            return true;
        }


        public void SerialPort_Write(string command)
        {
           try
            {
                if (hComm.IsOpen)
                {
                    hComm.Write(command);
                    DeviceLogger.MainLogger.Error("Writing Command ", command);
                }
                else
                {
                    DeviceLogger.MainLogger.Error("Serial Port is not open");
                }
            }
            catch (Exception ex)
            {
                CloseConnection();
                DeviceLogger.MainLogger.Error("Error writing text to {0}: {1}", pcCommPort, ex.Message);
            }
        }

        public string SerialPort_Read(StepsEnum step)
        {
            switch (step)
            {
                case StepsEnum.ReadFirstAcknowledgement:
                    try
                    {
                        DeviceLogger.MainLogger.Debug("Receiving first acknowledgement");
                        //string readExisting = hComm.ReadExisting();
                        byte[] data = new byte[19];
                        for (int offset = 0; offset < 19;)
                        {
                            int n = hComm.Read(data, offset, data.Length - offset);
                            offset += n;
                        }
                        string readExisting = Encoding.ASCII.GetString(data);
                        DeviceLogger.MainLogger.Debug($"Read from serial port {readExisting}");
                        if (readExisting.StartsWith("ACK"))
                        {
                            return readExisting.Substring(3);
                        }
                        return null;

                    }
                    catch (Exception ex)
                    {
                        DeviceLogger.MainLogger.Error($"exception in catch {ex.Message}");

                    }
                    break;
                case StepsEnum.ReadSecondAcknowledgement:
                    try
                    {
                        DeviceLogger.MainLogger.Debug("Receiving second acknowledgement");
                        byte[] data = new byte[3];
                        for (int offset = 0; offset < 3;)
                        {
                            int n = hComm.Read(data, offset, data.Length - offset);
                            offset += n;
                        }
                        string readExisting = Encoding.ASCII.GetString(data);
                       
                        
                        DeviceLogger.MainLogger.Debug($"Read from serial port {readExisting}");
                        if (readExisting.EndsWith("ACK"))
                        {
                            return Success;
                        }
                        return null;

                    }
                    catch (Exception ex)
                    {
                        DeviceLogger.MainLogger.Error($"exception in catch {ex.Message}");

                    }
                    break;
                case StepsEnum.ReadFirstOk:
                    try
                    {
                        DeviceLogger.MainLogger.Debug("Receiving first ok");
                        byte[] data = new byte[4];
                        for (int offset = 0; offset < 4;)
                        {
                            int n = hComm.Read(data, offset, data.Length - offset);
                            offset += n;
                        }
                        string readExisting = Encoding.ASCII.GetString(data);

                        DeviceLogger.MainLogger.Debug($"Read from serial port {readExisting}");
                        if (readExisting.EndsWith("OK!!"))
                        {
                            return Success;
                        }
                        return null;

                    }
                    catch (Exception ex)
                    {
                        DeviceLogger.MainLogger.Error($"exception in catch {ex.Message}");

                    }
                    break;
            }

            return null;
            
        }

        public void Byte2Hex(byte[] byteArray, int byteLength, char[] hexString)
        {
            for (int i = 0; i < byteLength; i++)
            {
                hexString[i * 2] = byteArray[i].ToString("X2")[0];
                hexString[i * 2 + 1] = byteArray[i].ToString("X2")[1];
            }
        }

        public byte[] ByteArrayToHexString(byte[] byteArray)
        {
            byte[] destBuf = null;
            int byteLength = byteArray.Length - 1;
            DeviceLogger.MainLogger.Debug($"\n\rKey Length: {byteLength}\n");
            // Calculate the length of the resulting hex string
            int hexStringLength = byteLength * 2 + 1; // Each byte is represented by 2 characters, plus 1 for the null terminator
                                                      // Allocate memory for the hex string
            char[] hexString = new char[hexStringLength];
            // Convert the byte array to a hex string
            Byte2Hex(byteArray, byteLength, hexString);
            // Print the hex string
            DeviceLogger.MainLogger.Debug($"\n\rHex string: {new string(hexString)}\n");
            Array.Copy(Encoding.ASCII.GetBytes(hexString), destBuf, hexStringLength);
            return destBuf;
        }

        public string ConverToHexString(string data)
        {
            string str = data;
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            string hex = BitConverter.ToString(bytes).Replace("-", "");
            DeviceLogger.MainLogger.Debug(hex);
            return hex;
        }

        public void CloseConnection()
        {
            try
            {
                hComm.Close();
                DeviceLogger.MainLogger.Info("Closing connection");
            }
            catch(Exception e)
            {
                DeviceLogger.MainLogger.Error("Error while closing ",e.Message );
            }
        }
    }
}
