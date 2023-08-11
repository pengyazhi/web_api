using Microsoft.EntityFrameworkCore;
using prjWebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAll",
        builder=>builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddControllersWithViews();
//這裡的DemoConnection要跟「appsettings.json」中ConnectionStrings的要連接的資料庫的名稱相同(自己取名)
builder.Services.AddDbContext<NorthwindContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("NorthwindConnection"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//UseCors()要放在UseHttpsRedirection()之前避免執行順序而不能使用
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
