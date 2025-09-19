using Microsoft.EntityFrameworkCore;
using ReactWithASP.Server.Data;
using ReactWithASP.Server.Services;



var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
string? mySqlDb = configuration["MySQL:Db"];
string? mySqlUser = configuration["MySQL:User"];
string? mySqlPassword = configuration["MySQL:Password"];

var mysqlConn = $"server=localhost;port=3306;user={mySqlUser};password={mySqlPassword};database={mySqlDb};Charset=utf8;TreatTinyAsBoolean=false";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mysqlConn, ServerVersion.AutoDetect(mysqlConn))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGetStudentService, GetStudentService>();
builder.Services.AddScoped<ISaveStudentService, SaveStudentService>();

var app = builder.Build();


app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

 
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
