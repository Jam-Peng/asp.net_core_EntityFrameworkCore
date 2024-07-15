using createWebApi_DominModels.Data;
using createWebApi_DominModels.Mappings;
using createWebApi_DominModels.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//建立 MSSQL資料庫和 WebApiSampleDbContext 一般資料的資料表連線服務
builder.Services.AddDbContext<WebApiSampleDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("WalksConnectionString")));

//建立 MSSQL資料庫和 WalksAuthDbContext 使用者驗證資料表的連線服務
builder.Services.AddDbContext<WalksAuthDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("WalkAuthConnectionString")));


//將區域儲存庫與SQL儲存庫一起注入
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();         //連線外部 Region 資料表的語句
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();             //連線外部 Walk 資料庫的語句
//builder.Services.AddScoped<IRegionRepository, InMemoryRegionRepository>();  //當使用內存資料庫時改換這一個


//將AutoMapper套件注入
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


//注入Microsoft.AspNetCore.Authentication.JwtBearer套件
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//新增使用Authentication套件
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
