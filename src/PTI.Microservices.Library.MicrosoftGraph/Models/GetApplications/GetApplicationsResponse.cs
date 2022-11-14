using System;
using System.Collections.Generic;
using System.Text;

namespace PTI.Microservices.Library.Models.MicrosoftGraphService.GetApplications
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class GetApplicationsResponse
    {
        public string odatacontext { get; set; }
        public Value[] value { get; set; }
    }

    public class Value
    {
        public string id { get; set; }
        public object deletedDateTime { get; set; }
        public string appId { get; set; }
        public object applicationTemplateId { get; set; }
        public DateTime createdDateTime { get; set; }
        public string displayName { get; set; }
        public object description { get; set; }
        public object groupMembershipClaims { get; set; }
        public string[] identifierUris { get; set; }
        public object isDeviceOnlyAuthSupported { get; set; }
        public bool? isFallbackPublicClient { get; set; }
        public object notes { get; set; }
        public object optionalClaims { get; set; }
        public string publisherDomain { get; set; }
        public string signInAudience { get; set; }
        public object[] tags { get; set; }
        public object tokenEncryptionKeyId { get; set; }
        public Verifiedpublisher verifiedPublisher { get; set; }
        public Spa spa { get; set; }
        public object[] addIns { get; set; }
        public Api api { get; set; }
        public Approle[] appRoles { get; set; }
        public Info info { get; set; }
        public object[] keyCredentials { get; set; }
        public Parentalcontrolsettings parentalControlSettings { get; set; }
        public Passwordcredential[] passwordCredentials { get; set; }
        public Publicclient publicClient { get; set; }
        public Requiredresourceaccess[] requiredResourceAccess { get; set; }
        public Web web { get; set; }
    }

    public class Verifiedpublisher
    {
        public object displayName { get; set; }
        public object verifiedPublisherId { get; set; }
        public object addedDateTime { get; set; }
    }

    public class Spa
    {
        public object[] redirectUris { get; set; }
    }

    public class Api
    {
        public object acceptMappedClaims { get; set; }
        public object[] knownClientApplications { get; set; }
        public int? requestedAccessTokenVersion { get; set; }
        public Oauth2permissionscopes[] oauth2PermissionScopes { get; set; }
        public object[] preAuthorizedApplications { get; set; }
    }

    public class Oauth2permissionscopes
    {
        public string adminConsentDescription { get; set; }
        public string adminConsentDisplayName { get; set; }
        public string id { get; set; }
        public bool isEnabled { get; set; }
        public string type { get; set; }
        public string userConsentDescription { get; set; }
        public string userConsentDisplayName { get; set; }
        public string value { get; set; }
    }

    public class Info
    {
        public object logoUrl { get; set; }
        public object marketingUrl { get; set; }
        public object privacyStatementUrl { get; set; }
        public object supportUrl { get; set; }
        public object termsOfServiceUrl { get; set; }
    }

    public class Parentalcontrolsettings
    {
        public object[] countriesBlockedForMinors { get; set; }
        public string legalAgeGroupRule { get; set; }
    }

    public class Publicclient
    {
        public string[] redirectUris { get; set; }
    }

    public class Web
    {
        public string homePageUrl { get; set; }
        public object logoutUrl { get; set; }
        public string[] redirectUris { get; set; }
        public Implicitgrantsettings implicitGrantSettings { get; set; }
    }

    public class Implicitgrantsettings
    {
        public bool enableAccessTokenIssuance { get; set; }
        public bool enableIdTokenIssuance { get; set; }
    }

    public class Approle
    {
        public string[] allowedMemberTypes { get; set; }
        public string description { get; set; }
        public string displayName { get; set; }
        public string id { get; set; }
        public bool isEnabled { get; set; }
        public string origin { get; set; }
        public string value { get; set; }
    }

    public class Passwordcredential
    {
        public object customKeyIdentifier { get; set; }
        public string displayName { get; set; }
        public DateTime endDateTime { get; set; }
        public string hint { get; set; }
        public string keyId { get; set; }
        public object secretText { get; set; }
        public DateTime startDateTime { get; set; }
    }

    public class Requiredresourceaccess
    {
        public string resourceAppId { get; set; }
        public Resourceaccess[] resourceAccess { get; set; }
    }

    public class Resourceaccess
    {
        public string id { get; set; }
        public string type { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
