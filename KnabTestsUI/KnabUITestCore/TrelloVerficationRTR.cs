using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Playwright;

/*class Program
{
    public static async Task Main(string[] args)
    {
        // Step 1: Fetch the verification code from Outlook
        string verificationCode = await RetrieveVerificationCodeFromOutlook();
        if (verificationCode == null)
        {
            Console.WriteLine("Verification code not found in email.");
            return;
        }

        Console.WriteLine($"Retrieved Verification Code: {verificationCode}");

    // Retrieve verification code from Outlook using Microsoft Graph API
//    private static async Task<string> RetrieveVerificationCodeFromOutlook()
    {
        // Azure AD app credentials
        string tenantId = "BikramTest_Knab";
        string clientId = "c9281c3e94a5b339eeaa449ab9aa7b0c";
        string clientSecret = "75fab8854bd698ef97abb756e6310bbde70c61f52b2bbef9de04618dbc1a0d49";

        // Authenticate with Microsoft Graph
        var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
        var graphClient = new GraphServiceClient(clientSecretCredential);

        // Get the latest email from Trello
        var messages = await graphClient.Me.Messages
            .Request()
            .Filter("from/emailAddress/address eq 'noreply+9f55ff8@id.atlassian.com'") 
            .OrderBy("receivedDateTime desc")
            .Top(1)
            .GetAsync();

        if (!messages.Any())
            return null;

        var email = messages.First();
        Console.WriteLine($"Email Subject: {email.Subject}");

        // Extract the verification code (6-digit) from the email body
        var match = Regex.Match(email.Body.Content, @"\b\d{6}\b");
        return match.Success ? match.Value : null;
    }
}
}*/
