using Microsoft.OpenApi.Models;
using SchoolAttendanceSystem.BLL.Extensions;
using SchoolAttendanceSystem.DAL.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDataAccessService(builder.Configuration);
builder.Services.AddRepositoryDependencyInjection();
builder.Services.AddBusinessDependencyInjection();
builder.Services.AddJwtConfiguration(builder.Configuration);


builder.Services.AddCors();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ibnkhaldun", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
});

builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory());
builder.WebHost.UseUrls("https://localhost:5001", "http://localhost:5000", "http://payrolly-001-site1.ftempurl.com");

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ibnkhaldun v1");
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
