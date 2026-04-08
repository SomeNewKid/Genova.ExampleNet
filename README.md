# Genova.ExampleNet

A Genova website implementation for **[www.example.net](http://www.example.net)** that provides example pages, content-driven routes, and platform integration features.

## Installation

```bash
dotnet restore
dotnet build
```

Or reference the website library from a Genova host application.

## Usage

Run it through a host application, for example:

```bash
dotnet run --project ExampleNet.Host
```

The host loads the website into the Genova engine and serves it for the configured hosts.

## Features

* Home and about pages
* Example `/hello` routes for requests, forms, headers, redirects, rewrites, caching, and errors
* Content module integration for embedded articles, snippets, metadata, and webpages
* OpenAI-backed text generation example endpoint
* Authentication and authorization module integration
* `robots.txt`, sitemap, scanner, culture info, and static file endpoints

## Notes

* This project is part of the Genova multi-tenant ASP.NET Core platform.
* It is a class library website executed via a host and engine, not a standalone web app.
* The generation features require `OPENAI_API_KEY` to be set.

## License

GNU General Public License v3.0
