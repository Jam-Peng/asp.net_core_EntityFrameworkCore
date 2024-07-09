using createWebApi_DominModels.Data;
using createWebApi_DominModels.Mappings;
using createWebApi_DominModels.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// �إ�MSSQL�s�u�A��
builder.Services.AddDbContext<WebApiSampleDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("WalksConnectionString")));

//�N�ϰ��x�s�w�PSQL�x�s�w�@�_�`�J
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();         //�s�u�~�� Region ��ƪ��y�y
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();             //�s�u�~�� Walk ��Ʈw���y�y
//builder.Services.AddScoped<IRegionRepository, InMemoryRegionRepository>();  //��ϥΤ��s��Ʈw�ɧﴫ�o�@��

//�NAutoMapper�M��`�J
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

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
