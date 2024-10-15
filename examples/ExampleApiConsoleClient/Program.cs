﻿using System.Text.Json;
using IdentityModel.Client;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// discover endpoints from metadata
var client = new HttpClient();

var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    Console.WriteLine(disco.Exception);
}


// request token
var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = disco.TokenEndpoint,
    ClientId = "hexid_example_client",
    ClientSecret = "hexid_example_secret",
    Scope = "HexIdExampleApi"
});

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    Console.WriteLine(tokenResponse.ErrorDescription);
    return;
}

Console.WriteLine(tokenResponse.AccessToken);


// call api
var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken!); // AccessToken is always non-null when IsError is false

var response = await apiClient.GetAsync("https://localhost:6001/weatherforecast");
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);
    return;
}

var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
Console.WriteLine(JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true }));
return;