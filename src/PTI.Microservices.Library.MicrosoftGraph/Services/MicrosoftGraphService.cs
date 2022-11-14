using Microsoft.Extensions.Logging;
using PTI.Microservices.Library.Configuration;
using PTI.Microservices.Library.Interceptors;
using PTI.Microservices.Library.MicrosoftGraph.Models.CreateUser;
using PTI.Microservices.Library.MicrosoftGraph.Models.GetUser;
using PTI.Microservices.Library.Models.MicrosoftGraphService.GetApplication;
using PTI.Microservices.Library.Models.MicrosoftGraphService.GetApplications;
using PTI.Microservices.Library.Models.MicrosoftGraphService.GetAppToken;
using PTI.Microservices.Library.Models.MicrosoftGraphService.GetUsers;
using PTI.Microservices.Library.Models.MicrosoftGraphService.UpdateApplicationRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PTI.Microservices.Library.Services
{
    /// <summary>
    /// Service in cahrge of exposing access to Microsoft Graph functionality
    /// </summary>
    public sealed class MicrosoftGraphService
    {
        private ILogger<MicrosoftGraphService> Logger { get; }
        private MicrosoftGraphConfiguration MicrosoftGraphConfiguration { get; }
        private CustomHttpClient CustomHttpClient { get; }

        /// <summary>
        /// Creates a new instance of <see cref="MicrosoftGraphService"/>
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="microsoftGraphConfiguration"></param>
        /// <param name="customHttpClient"></param>
        public MicrosoftGraphService(ILogger<MicrosoftGraphService> logger, MicrosoftGraphConfiguration microsoftGraphConfiguration,
            CustomHttpClient customHttpClient)
        {
            this.Logger = logger;
            this.MicrosoftGraphConfiguration = microsoftGraphConfiguration;
            this.CustomHttpClient = customHttpClient;
        }

        /// <summary>
        /// Gets the app token
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GetAppTokenResponse> GetAppTokenAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                string requestUrl = $"https://login.microsoftonline.com/{this.MicrosoftGraphConfiguration.TenantId}/oauth2/v2.0/token";
                List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("grant_type","client_credentials"),
                new KeyValuePair<string, string>("client_id",this.MicrosoftGraphConfiguration.ClientId),
                new KeyValuePair<string, string>("scope","https://graph.microsoft.com/.default"),
                new KeyValuePair<string, string>("client_secret",this.MicrosoftGraphConfiguration.ClientSecret),
            };
                System.Net.Http.FormUrlEncodedContent formUrlEncodedContent =
                    new FormUrlEncodedContent(keyValuePairs);
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl)
                {
                    Content = formUrlEncodedContent
                };
                var response = await this.CustomHttpClient.SendAsync(httpRequestMessage, completionOption: HttpCompletionOption.ResponseContentRead, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<GetAppTokenResponse>();
                    return result;
                }
                else
                {
                    string reason = response.ReasonPhrase;
                    string detailedError = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Reason: {reason}. Details: {detailedError}");
                }
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Gets a list of users
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GetUsersResponse> GetUsersAsync(string accessToken, CancellationToken cancellationToken = default)
        {
            try
            {
                string requestUrl = $"https://graph.microsoft.com/v1.0/users";
                this.CustomHttpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var result = await this.CustomHttpClient.GetFromJsonAsync<GetUsersResponse>(requestUrl);
                return result;
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Gets a list of applications
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GetApplicationsResponse> GetApplicationsAsync(string accessToken, CancellationToken cancellationToken = default)
        {
            try
            {
                string requestUrl = $"https://graph.microsoft.com/v1.0/applications";
                this.CustomHttpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var result = await this.CustomHttpClient.GetFromJsonAsync<GetApplicationsResponse>(requestUrl);
                return result;
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Get the application details
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="applicationId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GetApplicationResponse> GetApplicationAsync(string accessToken, Guid applicationId, CancellationToken cancellationToken = default)
        {
            try
            {
                string requestUrl = $"https://graph.microsoft.com/v1.0/applications/{applicationId.ToString()}";
                this.CustomHttpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var result = await this.CustomHttpClient.GetFromJsonAsync<GetApplicationResponse>(requestUrl);
                return result;
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Uploades the applications roles
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="applicationId"></param>
        /// <param name="approles"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task UpdateApplicationRolesAsync(string accessToken, Guid applicationId,
            List<Models.MicrosoftGraphService.GetApplication.Approle> approles,
            CancellationToken cancellationToken = default)
        {
            try
            {
                string requestUrl = $"https://graph.microsoft.com/v1.0/applications/{applicationId.ToString()}";
                this.CustomHttpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                UpdateApplicationRolesRequest updateBody = new UpdateApplicationRolesRequest()
                {
                    appRoles = approles
                };
                await this.CustomHttpClient.PatchAsJsonAsync<UpdateApplicationRolesRequest>(requestUrl, updateBody);
                //return result;
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<CreateUserResponse> CreateB2CUserAsync(string accessToken, CreateUserRequest model, CancellationToken cancellationToken = default)
        {
            try
            {
                string requestUrl = "https://graph.microsoft.com/v1.0/users";
                this.CustomHttpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await this.CustomHttpClient.PostAsJsonAsync<CreateUserRequest>(requestUrl, model, cancellationToken: cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    CreateUserResponse result = await response.Content.ReadFromJsonAsync<CreateUserResponse>(cancellationToken: cancellationToken);
                    return result;
                }
                else
                {
                    string reason = response.ReasonPhrase;
                    string detailedError = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
                    throw new Exception($"Reason: {reason}. Details: {detailedError}");
                }
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<GetUserResponse> GetUserAsync(string accessToken, string userId, CancellationToken cancellationToken = default)
        {
            try
            {
                string requestUrl = $"https://graph.microsoft.com/v1.0/users/{userId}";
                this.CustomHttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await this.CustomHttpClient.GetAsync(requestUrl, cancellationToken: cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    GetUserResponse result = await response.Content.ReadFromJsonAsync<GetUserResponse>();
                    return result;
                }
                else
                {
                    string reason = response.ReasonPhrase;
                    string detailedError = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
                    throw new Exception($"Reason: {reason}. Details: {detailedError}");
                }
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task DeleteUserAsync(string accessToken, string userId, CancellationToken cancellationToken = default)
        {
            try
            {
                var requestUrl = $"https://graph.microsoft.com/v1.0/users/{userId}";
                this.CustomHttpClient.DefaultRequestHeaders.Authorization =
                 new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await this.CustomHttpClient.DeleteAsync(requestUrl);
                if (!response.IsSuccessStatusCode)
                {
                    string reason = response.ReasonPhrase;
                    string detailedError = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
                    throw new Exception($"Reason: {reason}. Details: {detailedError}");
                }
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
