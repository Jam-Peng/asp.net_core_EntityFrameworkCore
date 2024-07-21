using createWebApi_DominModels.Data;
using createWebApi_DominModels.Mappings;
using createWebApi_DominModels.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(); 

//擴充Swagger功能，新增授權功能的選項
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Walks API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});


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
builder.Services.AddScoped<ITokenRepository, TokenRepository>();              //建立token用


//將AutoMapper套件注入
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


//建立身分驗證的Package
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Walks")
    .AddEntityFrameworkStores<WalksAuthDbContext>()
    .AddDefaultTokenProviders();

//配置身分驗證的選項
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;   //至少一個唯一的字符串
});


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
