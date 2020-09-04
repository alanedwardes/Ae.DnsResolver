# Ae.Dns
A pure C# implementation of a DNS client, server and configurable caching/filtering layer. This project offers the following packages:
* [Ae.Dns.Client](#aednsclient) - HTTP and UDP DNS clients
* [Ae.Dns.Server](#aednsserver) - UDP DNS server with filtering capabilities
* [Ae.Dns.Protocol](#aednsprotocol) - Low level DNS wire protocol handling

## Ae.Dns.Client
[![](https://img.shields.io/nuget/v/Ae.Dns.Client)](https://www.nuget.org/packages/Ae.Dns.Client/)

Offers both HTTP(S) and UDP DNS clients, with a simple round robin client implementation.
### Basic HTTPS Client Usage
This example is a very simple setup of the HTTP client using CloudFlare.
```csharp
using var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://cloudflare-dns.com/")
};
IDnsClient dnsClient = new DnsHttpClient(httpClient);

// Run an A query for google.com
DnsAnswer answer = await dnsClient.Query(DnsHeader.CreateQuery("google.com"));
```
### Advanced HTTPS Client Usage
This example uses [`Microsoft.Extensions.Http.Polly`](https://www.nuget.org/packages/Microsoft.Extensions.Http.Polly/) to allow retrying of failed requests.
```csharp
IServiceCollection services = new ServiceCollection();

var dnsUri = new Uri("https://cloudflare-dns.com/");
services.AddHttpClient<IDnsClient, DnsHttpClient>(x => x.BaseAddress = dnsUri)
        .AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt))));

IServiceProvider provider = servics.BuildServiceProvider();

IDnsClient dnsClient = provider.GetRequiredService<IDnsClient>();

// Run an A query for google.com
DnsAnswer answer = await dnsClient.Query(DnsHeader.CreateQuery("google.com"));
```
### Basic UDP Client Usage
This example is a very setup of the UDP client using CloudFlare.
```csharp
using IDnsClient dnsClient = new DnsUdpClient(IPAddress.Parse("1.1.1.1"));

// Run an A query for google.com
DnsAnswer answer = await dnsClient.Query(DnsHeader.CreateQuery("google.com"));
```

### Round Robin Client Usage
This example uses multiple DNS clients in a round-robin fashion.

```csharp
using IDnsClient cloudFlare1 = new DnsUdpClient(IPAddress.Parse("1.1.1.1"));
using IDnsClient cloudFlare2 = new DnsUdpClient(IPAddress.Parse("1.0.0.1"));
using IDnsClient google1 = new DnsUdpClient(IPAddress.Parse("8.8.8.8"));
using IDnsClient google2 = new DnsUdpClient(IPAddress.Parse("8.8.4.4"));

IDnsClient dnsClient = new DnsRoundRobinClient(cloudFlare1, cloudFlare2, google1, google2);

// Run an A query for google.com
DnsAnswer answer = await dnsClient.Query(DnsHeader.CreateQuery("google.com"));
```

## Ae.Dns.Server
[![](https://img.shields.io/nuget/v/Ae.Dns.Client)](https://www.nuget.org/packages/Ae.Dns.Server/)

Offers a UDP server with filtering capabilities.

### Basic UDP Server

```csharp
using IDnsClient dnsClient = new DnsUdpClient(IPAddress.Parse("1.1.1.1"));

// Block www.google.com
using IDnsFilter dnsFilter = new DnsDelegateFilter(x => x.Host == "www.google.com" ? false : true);

using var server = new DnsUdpServer(new IPEndPoint(IPAddress.Any, 53), dnsClient, dnsFilter);

await server.Recieve(CancellationToken.None);
```

## Ae.Dns.Protocol
[![](https://img.shields.io/nuget/v/Ae.Dns.Protocol)](https://www.nuget.org/packages/Ae.Dns.Protocol/)

Provides the low-level protocol handling for the over-the-wire DNS protocol format as per [RFC 1035](https://tools.ietf.org/html/rfc1035).

See the following types:
* DnsHeader
* DnsAnswer
* DnsResourceRecord
* DnsIpAddressResource
* DnsMxResource
* DnsSoaResource
* DnsTextResource
* DnsUnknownResource
