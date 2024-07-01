using createWebApi_DominModels.Data;
using createWebApi_DominModels.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 建立MSSQL連線服務
builder.Services.AddDbContext<WebApiSampleDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("WalksConnectionString")));

//將區域儲存庫與SQL儲存庫一起注入
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>(); //使用外部資料庫
//builder.Services.AddScoped<IRegionRepository, InMemoryRegionRepository>(); //當使用內存資料庫時改換這一個

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
