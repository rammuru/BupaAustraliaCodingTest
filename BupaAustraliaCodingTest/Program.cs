using BupaAustraliaCodingTest.Models;
using BupaAustraliaCodingTest.Options;
using BupaAustraliaCodingTest.Services;
using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.Configure<BookApiOptions>(
    builder.Configuration.GetSection(BookApiOptions.SectionName));

builder.Services.AddHttpClient<IBookCatalogService, BookCatalogService>((serviceProvider, client) =>
{
    var options = serviceProvider.GetRequiredService<IOptions<BookApiOptions>>().Value;

    if (Uri.TryCreate(options.BaseUrl, UriKind.Absolute, out var baseUri))
    {
        client.BaseAddress = baseUri;
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapGet("/", () => Results.Redirect("/Home/Index"));

app.MapRazorPages();

app.Run();
