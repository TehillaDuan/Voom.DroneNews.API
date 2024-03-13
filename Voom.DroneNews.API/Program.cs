using Hangfire;
using Voom.DroneNews.API.Repositories;
using Voom.DroneNews.API.Repositories.Interfaces;
using Voom.DroneNews.API.Services;
using Voom.DroneNews.API.Services.Interfaces;
using Hangfire;
using Hangfire.MemoryStorage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<INewsProviderService, NewsOrgProviderService>();
builder.Services.AddHangfire(config => config.UseMemoryStorage());
builder.Services.AddSingleton<INewsRepository, NewsRepository>();
builder.Services.AddScoped<INewsService, NewsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireServer();
app.UseHangfireDashboard();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

RecurringJob.AddOrUpdate<INewsService>(x => x.UpdateNews(DateTime.Today), Cron.Daily);

app.Run();
