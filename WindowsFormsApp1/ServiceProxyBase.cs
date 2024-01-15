using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DeviceInventory.Domain;

namespace DeviceInventory
{
    public class ServiceProxyBase
    {
        protected int MAX_ATTEMPTS = 3;
        protected ClientWebSocket webSocketClient;
        static CookieContainer cookieContainer;
        LoginDetailsDto loginDetailsDto;
        public ServiceProxyBase(LoginDetailsDto loginDetails)
        {
            if (cookieContainer == null)
            {
                cookieContainer = new CookieContainer();
            }
            this.loginDetailsDto = loginDetails;
        }

       
        public async Task<HttpResponseMessage> makePostRequest<T>(string c2ServiceUrl, T json) 
        {
            HttpClient sharedClient = new HttpClient()
            {
                BaseAddress = new Uri(loginDetailsDto.ServerURL),
            };
            sharedClient.DefaultRequestHeaders.Add("Accept", "application/json");
            sharedClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(string.Format("{0}:{1}", loginDetailsDto.UserName, loginDetailsDto.Password))));
            sharedClient.Timeout = TimeSpan.FromSeconds(100);


            var data = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");
            var url = $"{loginDetailsDto.ServerURL}{c2ServiceUrl}";
            var response = await sharedClient.PostAsync(url, data);
            return response;
           

        }

        public async Task<ReponseDto> MakeAPICall(string c2ServiceUrl, CreateDeviceInventoryDto root, TypeEnum typeEnum)
        {
            ReponseDto reponseDto = new ReponseDto();
            var response = await makePostRequest(c2ServiceUrl, root);
            if (response != null)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    // send error of unatuthorixed
                    DeviceLogger.MainLogger.Error("User not authorized to do the operation");
                    var sb = new System.Text.StringBuilder();
                   
                    sb.Append($@"User not authorized  ");
                    reponseDto.message = sb.ToString();
                }

                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // send message of api not found
                    DeviceLogger.MainLogger.Error("API not found");
                    var sb = new System.Text.StringBuilder();
                   
                    sb.Append($@"API not found");
                    
                    reponseDto.message = sb.ToString();
                }

                else if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    // success
                    DeviceLogger.MainLogger.Error("API Request successful");
                    try
                    {
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateDeviceInventoryResponseDto>(await response.Content.ReadAsStringAsync());
                        var sb = new System.Text.StringBuilder();
                        if(typeEnum == TypeEnum.Test)
                        {
                            sb.Append($@"Successfully created Test device:  {root.DeviceInventory.deviceCode}  ,Network Key : {result.DeviceInventory.networkKey} , App Key : {result.DeviceInventory.appKey}");
                            
                            reponseDto.message = sb.ToString();
                            reponseDto.AppKey = result.DeviceInventory.appKey;
                            reponseDto.NwkKey = result.DeviceInventory.networkKey;
                        }
                        else
                        {
                            sb.Append($@"Successfully created  device:  {root.DeviceInventory.deviceCode}");
                           
                            reponseDto.message = sb.ToString();
                            reponseDto.AppKey = result.DeviceInventory.appKey;
                            reponseDto.NwkKey = result.DeviceInventory.networkKey;
                            DeviceLogger.MainLogger.Debug("App key : " + reponseDto.AppKey + " network key: " + reponseDto.NwkKey);
                        }
                        
                        
                        // message = $"Success while generating the device with  network key as {result.DeviceInventory.networkKey} and appkey as {result.DeviceInventory.appKey}";
                    }
                    catch (Exception ex)
                    {
                        DeviceLogger.MainLogger.Error("Error while converting success message {error}", ex.Message);
                        reponseDto.message = $"Error while converting success message {ex.Message}";
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    // message of 
                    DeviceLogger.MainLogger.Error("Internal server error");
                    try
                    {
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessageDto>(await response.Content.ReadAsStringAsync());
                        var sb = new System.Text.StringBuilder();
                        
                        sb.Append($@"Error creating device: {root.DeviceInventory.deviceCode} ,Error:{result.message}");
                        
                        reponseDto.message = sb.ToString();

                        //message = $"Error while generating the device with  {result.message}";
                    }
                    catch (Exception ex)
                    {
                        DeviceLogger.MainLogger.Error("Error while converting error message {error}", ex.Message);
                        reponseDto.message = $"Error while converting error message {ex.Message}";
                    }
                }

            }
            DeviceLogger.ActivityLogger.Debug(reponseDto.message);
            return reponseDto;
        }
    
        public async Task<bool> CheckUserCreds()
        {
            string c2ServerUrl = "services/loginservice/authenticate";
            LoginModel loginModel = new LoginModel();
            loginModel.username = loginDetailsDto.UserName;
            loginModel.password = loginDetailsDto.Password;
            var response = await makePostRequest(c2ServerUrl, loginModel);
            if (response != null)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    return true;
                }
            }
             return false;
        }
    }
}
