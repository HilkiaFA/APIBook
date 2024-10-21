using LatihanJWT.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBText>(s=>s.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=books;Integrated Security=True;Trust Server Certificate=True"));
builder.Services.AddSwaggerGen(s =>
{
    var security = new OpenApiSecurityScheme
    {
        Name = "Authorizor",
        BearerFormat = "JWT",
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Scheme = "bearer"
    };
    s.AddSecurityDefinition("Bearer", security);
    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id="Bearer",
                    Type=ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

var security = builder.Configuration.GetSection("JWT");
var key = Encoding.UTF8.GetBytes(security.GetValue<string>("Key"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(s =>
    {
        s.SaveToken = true;
        s.RequireHttpsMetadata = false;
        s.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidAudience = security.GetValue<string>("Audience"),
            ValidIssuer = security.GetValue<string>("Issuer"),
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    
}*/
app.UseSwagger();
    app.UseSwaggerUI();
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
