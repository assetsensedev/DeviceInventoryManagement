using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<int> main()
        {
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
                    var responseDto = await proxyBase.MakeAPICall(c2ServerUrl, createDeviceInventoryDto);
                    var hexAppString = Encryption.EncryptHexString(responseDto.AppKey);

                    var hexNwkString = Encryption.EncryptHexString(responseDto.NwkKey);

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
                                        DeviceLogger.logger.Debug("Success");
                                    }
                                    else
                                    {
                                        DeviceLogger.logger.Error("Error in reading ok after sending Nwk string");
                                    }
                                }
                                else
                                {
                                    DeviceLogger.logger.Error("Error Programming the device. ACK Failed for CMD2");
                                }
                            }
                            else
                            {
                                DeviceLogger.logger.Error("Error in reading ok after sending app string");
                            }
                        }
                        else
                        {
                            DeviceLogger.logger.Error("Error Programming the device. ACK Failed for CMD2");
                        }
                    }

                }
                else
                {
                    DeviceLogger.logger.Error("Acknowledgement not proper from serial port device");
                }
            }
            return 1;
        }

        public bool SerialPort_Init()
        {
            try
            {
                hComm = new SerialPort(pcCommPort, 115200, Parity.None, 8, StopBits.One);
                hComm.ReadTimeout = 5000;
                hComm.WriteTimeout = 1000;
                hComm.Open();
                DeviceLogger.logger.Debug("Opening serial port successful");
            }
            catch (UnauthorizedAccessException)
            {
                DeviceLogger.logger.Error("cannot open port!");
                return false;
            }
            catch (Exception ex)
            {
                DeviceLogger.logger.Error($"invalid handle value! with exception as {ex.Message}");
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

                }
                else
                {
                    DeviceLogger.logger.Error("Serial Port is not open");
                }
            }
            catch (Exception ex)
            {
                DeviceLogger.logger.Error("Error writing text to {0}: {1}", pcCommPort, ex.Message);
            }
        }

        public string SerialPort_Read(StepsEnum step)
        {
            switch (step)
            {
                case StepsEnum.ReadFirstAcknowledgement:
                    try
                    {
                        DeviceLogger.logger.Debug("Receiving first acknowledgement");
                        string readExisting = hComm.ReadExisting();
                        DeviceLogger.logger.Debug($"Read from serial port {readExisting}");
                        if (readExisting.StartsWith("ACK"))
                        {
                            return readExisting.Substring(3);
                        }
                        return null;

                    }
                    catch (Exception ex)
                    {
                        DeviceLogger.logger.Error($"exception in catch {ex.Message}");

                    }
                    break;
                case StepsEnum.ReadSecondAcknowledgement:
                    try
                    {
                        DeviceLogger.logger.Debug("Receiving second acknowledgement");
                        string readExisting = hComm.ReadExisting();
                        DeviceLogger.logger.Debug($"Read from serial port {readExisting}");
                        if (readExisting.EndsWith("ACK"))
                        {
                            return Success;
                        }
                        return null;

                    }
                    catch (Exception ex)
                    {
                        DeviceLogger.logger.Error($"exception in catch {ex.Message}");

                    }
                    break;
                case StepsEnum.ReadFirstOk:
                    try
                    {
                        DeviceLogger.logger.Debug("Receiving first ok");
                        string readExisting = hComm.ReadExisting();
                        DeviceLogger.logger.Debug($"Read from serial port {readExisting}");
                        if (readExisting.EndsWith("OK!!"))
                        {
                            return Success;
                        }
                        return null;

                    }
                    catch (Exception ex)
                    {
                        DeviceLogger.logger.Error($"exception in catch {ex.Message}");

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
            DeviceLogger.logger.Debug($"\n\rKey Length: {byteLength}\n");
            // Calculate the length of the resulting hex string
            int hexStringLength = byteLength * 2 + 1; // Each byte is represented by 2 characters, plus 1 for the null terminator
                                                      // Allocate memory for the hex string
            char[] hexString = new char[hexStringLength];
            // Convert the byte array to a hex string
            Byte2Hex(byteArray, byteLength, hexString);
            // Print the hex string
            DeviceLogger.logger.Debug($"\n\rHex string: {new string(hexString)}\n");
            Array.Copy(Encoding.ASCII.GetBytes(hexString), destBuf, hexStringLength);
            return destBuf;
        }

        public string ConverToHexString(string data)
        {
            string str = data;
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            string hex = BitConverter.ToString(bytes).Replace("-", "");
            DeviceLogger.logger.Debug(hex);
            return hex;
        }
    }
}
