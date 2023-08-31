using Microsoft.EntityFrameworkCore;
using wise_api.BackGround;
using wise_api.Context;

var builder = WebApplication.CreateBuilder(args);
var AllowAll = "AllowAll";
builder.Services.AddHttpClient();
//builder.Services.AddHostedService<BackGroundHttpService>();
// Add services to the container.
string mySqlConnectionStr = builder.Configuration.GetConnectionString("ConnectionDB");
builder.Services.AddDbContext<DataContext>(options => options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));
builder.Services.AddCors(p => p.AddPolicy(AllowAll, builder => { builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); }));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger";
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    // custom CSS
    c.InjectStylesheet("/swagger-ui/custom.css");
});

app.UseCors(AllowAll);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
