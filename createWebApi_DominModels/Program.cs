using createWebApi_DominModels.Data;
using createWebApi_DominModels.Mappings;
using createWebApi_DominModels.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using Serilog;
using createWebApi_DominModels.Middlewares;

var builder = WebApplication.CreateBuilder(args);


//�ϥ�Serilog�M�� �إ� log ��x
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/Walks_Log.txt", rollingInterval: RollingInterval.Minute)  //����x�����@�Ӹ��|�M�W�١B�إ߷s��x�ɶ�
    .MinimumLevel.Information()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


// Add services to the container.
builder.Services.AddControllers();

//�b�A�ȩΨ�L�D��������X�� HTTP �ШD�H��
builder.Services.AddHttpContextAccessor();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(); 

//�X�RSwagger�\��A�s�W���v�\�઺�ﶵ
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Walks API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
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


//�إ� MSSQL��Ʈw�M WebApiSampleDbContext �@���ƪ���ƪ�s�u�A��
builder.Services.AddDbContext<WebApiSampleDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("WalksConnectionString")));

//�إ� MSSQL��Ʈw�M WalksAuthDbContext �ϥΪ����Ҹ�ƪ��s�u�A��
builder.Services.AddDbContext<WalksAuthDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("WalkAuthConnectionString")));


//�N�ϰ��x�s�w�PSQL�x�s�w�@�_�`�J
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();         //�s�u�~�� Region ��ƪ��y�y
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();             //�s�u�~�� Walk ��ƪ��y�y
//builder.Services.AddScoped<IRegionRepository, InMemoryRegionRepository>();  //��ϥΤ��s��Ʈw�ɧﴫ�o�@��
builder.Services.AddScoped<ITokenRepository, TokenRepository>();              //�إ�token��
builder.Services.AddScoped<IImageRepository, LoaclImageRepository>();         //�s���~�� Image ��ƪ��y�y


//�NAutoMapper�M��`�J
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


//�إߨ������Ҫ�Package
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Walks")
    .AddEntityFrameworkStores<WalksAuthDbContext>()
    .AddDefaultTokenProviders();

//�t�m�������Ҫ��ﶵ
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;   //�ܤ֤@�Ӱߤ@���r�Ŧ�
});


//�`�JMicrosoft.AspNetCore.Authentication.JwtBearer�M��
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

//�פJ�B�z log �� Middleware
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

//�s�W�ϥ�Authentication�M��
app.UseAuthentication();

app.UseAuthorization();

//�վ�i�H�ϥ��R�A��� - �z�Lurl�i�H�N�Ϥ����
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.MapControllers();

app.Run();
