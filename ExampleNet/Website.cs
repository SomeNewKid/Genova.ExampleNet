// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using Genova.Authentication;
using Genova.Authorization;
using Genova.Common.Attributes;
using Genova.Common.Execution;
using Genova.Common.Headers;
using Genova.Common.Html;
using Genova.Common.Metadata;
using Genova.Common.Models;
using Genova.Common.Models.Csp;
using Genova.Common.Modules;
using Genova.Common.Websites;
using Genova.Content;
using Genova.ExampleNet.Endpoints;
using Genova.ExampleNet.Headers;
using Genova.ExampleNet.Utilities;
using Genova.Generation;
using Genova.Generation.Gateways;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Yarp.ReverseProxy.Configuration;

namespace Genova.ExampleNet;

/// <summary>
/// Represents the ExampleNet website, which integrates with the Engine and provides
/// route and cluster configurations.
/// </summary>
[CodeQuality(Public = true)]
public sealed class Website : IWebsite, IProxyWebsite
{
    /// <summary>
    /// The name which prefixes policy names and view names.
    /// </summary>
    internal const string NamePrefix = "EXAMPLENET";

    /// <summary>
    /// The name of the cache policy used for output caching.
    /// </summary>
    internal const string CachePolicyName = $"{NamePrefix}_CachePolicy";

    /// <summary>
    /// The tag which may be used for items in the output cache, by which items may be purged from the cache.
    /// </summary>
    internal const string CachePolicyTag = $"{NamePrefix}_CacheTag";

    /// <summary>
    /// The GUID identifier for the ExampleNet website.
    /// </summary>
    internal const string Identifier = "c1d2e3f4-a5b6-7c8d-9e0f-1a2b3c4d5e6f";

    private const string AuthenticationSchemeName = $"{NamePrefix}_CookieScheme";
    private readonly IReadOnlyList<IModule> _modules;
    private readonly ProxyConfiguration _proxyConfiguration;
    private readonly Authentication.AuthenticationOptions _authOptions;
    private readonly GenerationOptions _generationOptions;
    private readonly ContentModule _contentModule;
    private IStringLocalizer? _localizer;

    /// <summary>
    /// Initializes a new instance of the <see cref="Website"/> class.
    /// </summary>
    /// <param name="configuration">The configuration for the website.</param>
    public Website(IConfiguration configuration)
    {
        WebsiteConfig websiteConfig = new (configuration, WebsiteId.ToString(), "en");
        Name = websiteConfig.Name;
        TenantId = websiteConfig.TenantId;
        Hosts = websiteConfig.Hosts;
        SupportedCultures = websiteConfig.SupportedCultures;
        DefaultCulture = websiteConfig.DefaultCulture;

        _authOptions = new()
        {
            AuthenticationScheme = AuthenticationSchemeName,
            LoginPath = "/login",
            LogoutPath = "/logout",
            AccessDeniedPath = "/access-denied",
        };

        _generationOptions = new()
        {
            OpenAiApiKey = GetOpenAiApiKey(),
        };

        ContentOptions contentOptions = new()
        {
            AssemblyForEmbeddedRecords = typeof(Website).Assembly,
            EmbeddedArticleRecords = "Genova.ExampleNet.Data.article-records.json",
            EmbeddedSnippetRecords = "Genova.ExampleNet.Data.snippet-records.json",
            EmbeddedMetadataRecords = "Genova.ExampleNet.Data.metadata-records.json",
            EmbeddedWebpageRecords = "Genova.ExampleNet.Data.webpage-records.json",
        };
        _contentModule = new ContentModule(configuration, contentOptions);

        _modules =
        [
            new AuthenticationModule(_authOptions, DefaultCulture),
            new AuthorizationModule(),
            new GenerationModule(),
            _contentModule,
        ];

        _proxyConfiguration = new ProxyConfiguration(Name, Hosts);
    }

    /// <inheritdoc/>
    public string Name { get; }

    /// <inheritdoc/>
    public Guid WebsiteId => Guid.Parse(Identifier);

    /// <inheritdoc/>
    public Guid TenantId { get; }

    /// <inheritdoc/>
    public string[] Hosts { get; }

    /// <inheritdoc/>
    public string[] SupportedCultures { get; }

    /// <inheritdoc/>
    public string DefaultCulture { get; }

    /// <inheritdoc/>
    public IEnumerable<IModule> Modules
    {
        get
        {
            return _modules;
        }
    }

    /// <inheritdoc/>
    public IStringLocalizer? Localizer
    {
        get
        {
            if (_localizer == null)
            {
                StringLocalizerFactory factory = new(DefaultCulture);
                _localizer = factory.CreateLocalizer();
            }

            return _localizer;
        }
    }

    /// <inheritdoc/>
    public string DefaultAuthenticationScheme
    {
        get { return AuthenticationSchemeName; }
    }

    /// <inheritdoc/>
    public string[] ViewLocations
    {
        get
        {
            return [];
        }
    }

    /// <inheritdoc/>
    public IEnumerable<IUrlRedirect> UrlRedirects
    {
        get
        {
            return [];
        }
    }

    /// <inheritdoc/>
    public IEnumerable<IUrlRewrite> UrlRewrites
    {
        get
        {
            return [];
        }
    }

    /// <inheritdoc/>
    public IEnumerable<KeyValuePair<string, Action<OutputCachePolicyBuilder>>> OutputCachePolicies
    {
        get
        {
            return new List<KeyValuePair<string, Action<OutputCachePolicyBuilder>>>
            {
                new(CachePolicyName, policy =>
                {
                    policy.Expire(TimeSpan.FromMinutes(15));
                }),
            };
        }
    }

    /// <inheritdoc/>
    public void ConfigureAuthentication(AuthenticationBuilder authenticationBuilder)
    {
        authenticationBuilder.AddCookie(AuthenticationSchemeName, options =>
        {
            options.LoginPath = _authOptions.LoginPath;
            options.LogoutPath = _authOptions.LogoutPath;
            options.AccessDeniedPath = _authOptions.AccessDeniedPath;
        });

        authenticationBuilder.Services.Configure((Microsoft.AspNetCore.Authentication.AuthenticationOptions options) =>
        {
            options.DefaultScheme = AuthenticationSchemeName;
            options.DefaultChallengeScheme = AuthenticationSchemeName;
        });
    }

    /// <inheritdoc/>
    public bool CanHandleHost(HostString host)
    {
        return Hosts.Contains(host.ToString(), StringComparer.OrdinalIgnoreCase);
    }

    /// <inheritdoc/>
    public void ConfigureMiddleware(WebApplication app)
    {
    }

    /// <inheritdoc/>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddKeyedSingleton<IContentModule>(Identifier, _contentModule);
        services.AddKeyedSingleton<GenerationOptions>(Identifier, _generationOptions);

        services.AddKeyedScoped<IOpenAiApiGateway>(Identifier, (sp, key) =>
        {
            GenerationOptions tenantOptions = sp.GetRequiredKeyedService<GenerationOptions>(key);

            IHttpClientFactory httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
            return new OpenAiApiGateway(tenantOptions, httpClientFactory);
        });
    }

    /// <inheritdoc/>
    public void ConfigureRoutes(IEndpointRouteBuilder endpoints)
    {
        // General pages
        HomePage.MapEndpoints(endpoints);
        AboutPage.MapEndpoints(endpoints);
        NaughtyPage.MapEndpoints(endpoints);

        // Hello pages
        HelloPages.MapEndpoints(endpoints);
        HelloFormPages.MapEndpoints(endpoints);
        HelloCachePages.MapEndpoints(endpoints);
        HelloRewritePage.MapEndpoints(endpoints);
        HelloHeadersPages.MapEndpoints(endpoints);
        HelloRedirectPage.MapEndpoints(endpoints);
        HelloGenerationPages.MapEndpoints(endpoints);
        HelloFriendlyErrorPages.MapEndpoints(endpoints);
        HelloUnfriendlyErrorPages.MapEndpoints(endpoints);

        // Other Files
        WwwFiles.MapEndpoints(endpoints);
        RobotsFiles.MapEndpoints(endpoints);
        SitemapPage.MapEndpoints(endpoints);
        ScannerPage.MapEndpoints(endpoints);
        CultureInfoPages.MapEndpoints(endpoints);
    }

    /// <inheritdoc/>
    public IEnumerable<KeyValuePair<string, Action<AuthorizationPolicyBuilder>>> GetAuthorizationPolicies()
    {
        return new Dictionary<string, Action<AuthorizationPolicyBuilder>>
        {
            { "AuthenticatedOnly", policy => policy.RequireAuthenticatedUser() },
        };
    }

    /// <inheritdoc/>
    public CookiePolicyOptions? GetCookiePolicyOptions()
    {
        return null;
    }

    /// <inheritdoc/>
    public IContentSecurityPolicy GetContentSecurityPolicy(IExecutionContext executionContext, string path)
    {
        return ContentSecurityPolicy.Parse(
            """
            Content-Security-Policy:
                default-src 'none';
                base-uri 'self';
                script-src 'self';
                style-src 'self';
                img-src 'self';
                font-src 'none';
                connect-src 'none';
                media-src 'none';
                object-src 'none';
                frame-src 'none';
                form-action 'none';
                frame-ancestors 'none';
                upgrade-insecure-requests;
                block-all-mixed-content;            
            """);
    }

    /// <inheritdoc/>
    public CorsPolicy? GetCorsPolicy()
    {
        return null;
    }

    /// <inheritdoc/>
    public IEnumerable<RouteConfig> GetRoutes()
    {
        return _proxyConfiguration.GetRoutes();
    }

    /// <inheritdoc/>
    public IEnumerable<ClusterConfig> GetClusters()
    {
        return _proxyConfiguration.GetClusters();
    }

    /// <inheritdoc/>
    public IEnumerable<IHeadersModifier> GetHeadersModifiers(string requestPath, string? responseContentType)
    {
        return
        [
            new XWebsiteNameModifier(),
        ];
    }

    /// <inheritdoc/>
    public IEnumerable<IHtmlModifier> GetHtmlModifiers(string pathAndQuery)
    {
        return [];
    }

    /// <inheritdoc/>
    public HtmlOptions? GetHtmlOptions(IExecutionContext executionContext, string pathAndQuery)
    {
        return new HtmlOptions()
        {
            Minimize = false,
            CloseEmpty = true,
            AlwaysClose = true,
        };
    }

    /// <inheritdoc/>
    public string? GetRazorViewName(IExecutionContext executionContext, object model)
    {
        return null;
    }

    /// <inheritdoc/>
    public string? GetHtmlResponse(HttpContext httpContext, IExecutionContext executionContext, object model)
    {
        return null;
    }

    /// <inheritdoc/>
    public void Populate(DocumentMetadata responseMetadata, IExecutionContext executionContext)
    {
        DocumentMetadata generalMetadata = new()
        {
            Content = new ContentMetadata
            {
                Description = "This is an example page description that provides a concise summary of the page content.",
                Keywords = "example keyword",
                Image = "/-/images/dog.png",
                Website = Name,
            },
            Crawler = new CrawlerMetadata
            {
                Robots = "index, follow",
            },
            WebApp = new WebAppMetadata
            {
                Icons =
                [
                    new IconResource
                    {
                        Rel = "icon",
                        Src = "/favicon.ico",
                        Type = "image/x-icon",
                    },
                    new IconResource
                    {
                        Rel = "icon",
                        Src = "/favicon.svg",
                        Type = "image/svg+xml",
                        Sizes = "any",
                    },
                    new IconResource
                    {
                        Rel = "apple-touch-icon",
                        Src = "/apple-touch-icon.png",
                        Type = "image/png",
                        Sizes = "180x180",
                    },
                ],
                ThemeColor = "#FFFFFF",
            },
        };

        responseMetadata.Merge(generalMetadata);
    }

    [ExcludeFromCodeCoverage(Justification = "Not possible to test the exception.")]
    private static string GetOpenAiApiKey()
    {
        string? apiKey = Environment.GetEnvironmentVariable("OPENAI_A11YGEN_API_KEY");
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("API key is not set in the environment variables.");
        }

        return apiKey;
    }
}
