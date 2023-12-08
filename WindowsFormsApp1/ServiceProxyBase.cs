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
using WindowsFormsApp1.Domain;

namespace WindowsFormsApp1
{
    public class ServiceProxyBase
    {
        protected int MAX_ATTEMPTS = 3;
        protected ClientWebSocket webSocketClient;
        CookieContainer cookieContainer;
        LoginDetailsDto loginDetailsDto;
        public ServiceProxyBase(LoginDetailsDto loginDetails)
        {
            cookieContainer = new CookieContainer();
            this.loginDetailsDto = loginDetails;
        }

       
        public async Task<HttpResponseMessage> makePostRequest(string c2ServiceUrl, CreateDeviceInventoryDto json) 
        {
            HttpClient sharedClient = new HttpClient()
            {
                BaseAddress = new Uri(loginDetailsDto.ServerURL),
            };
            sharedClient.DefaultRequestHeaders.Add("Accept", "application/json");
            sharedClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(string.Format("{0}:{1}", loginDetailsDto.UserName, loginDetailsDto.Password))));
            sharedClient.Timeout = TimeSpan.FromSeconds(100);


            var data = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");
            var url = $"{loginDetailsDto.ServerURL}/{c2ServiceUrl}";
            var response = await sharedClient.PostAsync(url, data);
            return response;
           

        }

        public async Task<ReponseDto> MakeAPICall(string c2ServiceUrl, CreateDeviceInventoryDto root)
        {
            ReponseDto reponseDto = new ReponseDto();
            var response = await makePostRequest(c2ServiceUrl, root);
            if (response != null)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    // send error of unatuthorixed
                    DeviceLogger.logger.Error("User not authorized to do the operation");
                    var sb = new System.Text.StringBuilder();
                    sb.Append(@"{\rtf1\ansi");
                    sb.Append($@" \b User not authorized  \b0 ");
                    sb.Append(@"}");
                    reponseDto.message = sb.ToString();
                }

                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // send message of api not found
                    DeviceLogger.logger.Error("API not found");
                    var sb = new System.Text.StringBuilder();
                    sb.Append(@"{\rtf1\ansi");
                    sb.Append($@" \b API not found  \b0 ");
                    sb.Append(@"}");
                    reponseDto.message = sb.ToString();
                }

                else if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    // success
                    DeviceLogger.logger.Error("API Request successful");
                    try
                    {
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateDeviceInventoryResponseDto>(await response.Content.ReadAsStringAsync());
                        var sb = new System.Text.StringBuilder();
                        sb.Append(@"{\rtf1\ansi");
                        sb.Append($@"  Success while generating the device \b {root.DeviceInventory.deviceCode} \b0 with  network key as  \b {result.DeviceInventory.networkKey}\b0  and appkey as \b {result.DeviceInventory.appKey}\b0");
                        sb.Append(@"}");
                        reponseDto.message = sb.ToString();
                        // message = $"Success while generating the device with  network key as {result.DeviceInventory.networkKey} and appkey as {result.DeviceInventory.appKey}";
                    }
                    catch (Exception ex)
                    {
                        DeviceLogger.logger.Error("Error while converting success message {error}", ex.Message);
                        reponseDto.message = $"Error while converting success message {ex.Message}";
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    // message of 
                    DeviceLogger.logger.Error("Internal server error");
                    try
                    {
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessageDto>(await response.Content.ReadAsStringAsync());
                        var sb = new System.Text.StringBuilder();
                        sb.Append(@"{\rtf1\ansi");
                        sb.Append($@"  Error while generating the device \b {root.DeviceInventory.deviceCode} \b0  with  \b {result.message}\b0");
                        sb.Append(@"}");
                        reponseDto.message = sb.ToString();
                        //message = $"Error while generating the device with  {result.message}";
                    }
                    catch (Exception ex)
                    {
                        DeviceLogger.logger.Error("Error while converting error message {error}", ex.Message);
                        reponseDto.message = $"Error while converting error message {ex.Message}";
                    }
                }

            }
           
            return reponseDto;
        }
    }
}
