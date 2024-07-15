using createWebApi_DominModels.Data;
using createWebApi_DominModels.Mappings;
using createWebApi_DominModels.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//�إ� MSSQL��Ʈw�M WebApiSampleDbContext �@���ƪ���ƪ�s�u�A��
builder.Services.AddDbContext<WebApiSampleDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("WalksConnectionString")));

//�إ� MSSQL��Ʈw�M WalksAuthDbContext �ϥΪ����Ҹ�ƪ��s�u�A��
builder.Services.AddDbContext<WalksAuthDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("WalkAuthConnectionString")));


//�N�ϰ��x�s�w�PSQL�x�s�w�@�_�`�J
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();         //�s�u�~�� Region ��ƪ��y�y
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();             //�s�u�~�� Walk ��Ʈw���y�y
//builder.Services.AddScoped<IRegionRepository, InMemoryRegionRepository>();  //��ϥΤ��s��Ʈw�ɧﴫ�o�@��


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

app.UseHttpsRedirection();

//�s�W�ϥ�Authentication�M��
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
