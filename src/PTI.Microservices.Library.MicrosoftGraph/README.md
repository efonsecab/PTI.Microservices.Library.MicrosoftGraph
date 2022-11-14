# PTI.Microservices.Library.MicrosoftGraph

This is part of PTI.Microservices.Library set of packages

The purpose of this package is to facilitate the calls to Microsoft Graph APIs, while maintaining a consistent usage pattern among the different services in the group

**Examples:**

## Create User
    CustomHttpClient customHttpClient = new CustomHttpClient(new CustomHttpClientHandler(null));
    MicrosoftGraphService microsoftGraphService = new MicrosoftGraphService(null,
        this.MicrosoftGraphConfiguration, customHttpClient);
    var result = await microsoftGraphService.GetAppTokenAsync();
    Assert.IsNotNull(result);
    CreateUserRequest createUserRequest =
        new CreateUserRequest()
        {
            accountEnabled = true,
            displayName = TEST_DISPLAY_NAME,
            mailNickname = TEST_MAIL_NICKNAME,
            givenName = TEST_GIVEN_NAME,
            surName = TEST_SURNAME,
            //userPrincipalName = TEST_USER_PRINCIPAL_NAME,
            identities = new Identity[] {
                new Identity()
                {
                    signInType="emailAddress",
                    issuer=TEST_ISSUER,
                    issuerAssignedId=TEST_ISSUER_ASSIGNED_ID,
                }
            },
            passwordProfile = new Passwordprofile()
            {
                password = TEST_PASSWORD,
                forceChangePasswordNextSignIn = false,
            },
            passwordPolicies = "DisablePasswordExpiration"
        };

    var createdUser = 
        await microsoftGraphService.CreateB2CUserAsync(result.access_token, createUserRequest);

## Get Users
    CustomHttpClient customHttpClient = new CustomHttpClient(new CustomHttpClientHandler(null));
    MicrosoftGraphService microsoftGraphService = new MicrosoftGraphService(null,
        this.MicrosoftGraphConfiguration, customHttpClient);
    var getAppTokenResult = await microsoftGraphService.GetAppTokenAsync();
    var result = await microsoftGraphService.GetUsersAsync(getAppTokenResult.access_token);

## Get Applications
    CustomHttpClient customHttpClient = new CustomHttpClient(new CustomHttpClientHandler(null));
    MicrosoftGraphService microsoftGraphService = new MicrosoftGraphService(null,
        this.MicrosoftGraphConfiguration, customHttpClient);
    var getAppTokenResult = await microsoftGraphService.GetAppTokenAsync();
    var result = await microsoftGraphService.GetApplicationsAsync(getAppTokenResult.access_token);

## Update Application Roles
    var ptiserverApp = await microsoftGraphService.GetApplicationAsync(getAppTokenResult.access_token,
        Guid.Parse(TEST_APPLICATION_ID));
    ptiserverApp.appRoles = new Microservices.Library.Models.MicrosoftGraphService.GetApplication.Approle[]
    {
        new Microservices.Library.Models.MicrosoftGraphService.GetApplication.Approle()
        {
            description="Test Role Desc",
            displayName="Test Role Display Name",
            id=Guid.NewGuid().ToString(),
            isEnabled=true,
            value="TestRoleClaimValue",
            allowedMemberTypes = new string[]{"User" }//["User","Application"]
        }
    };
    await microsoftGraphService.UpdateApplicationRolesAsync(getAppTokenResult.access_token,
        Guid.Parse(ptiserverApp.id), ptiserverApp.appRoles.ToList());