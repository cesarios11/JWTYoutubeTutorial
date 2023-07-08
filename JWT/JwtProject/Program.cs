using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//TODO: Se le agrega al contenedor del servicio la autentificación.
//TODO: Cada vez que el usuario se loguee (envía una petición al servidor) primero va a pasar por este middelware (verifica si tiene token, si expiró si está corrupto etc )
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        //TODO: Valida el 'Issuer'. Lo que se creó en el archivo 'appsettings.json'
        ValidateIssuer = true,
        //TODO: Valida el 'Audience'. Lo que se creó en el archivo 'appsettings.json'
        ValidateAudience = true,
        //TODO: Valida si el token expiró
        ValidateLifetime = true,
        //TODO: Valida la clave
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//TODO: Se agrega este middleware de autenticación.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
