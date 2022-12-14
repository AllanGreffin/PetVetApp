using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PetVetApp.Data;
using PetVetApp.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("corsConfig",
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:4200",
                                                  "https://localhost:4200")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod()
                                                  .AllowCredentials();
                          });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<IdentityUser>(options => { 
    options.User.RequireUniqueEmail = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<UserManager<IdentityUser>>();
builder.Services.AddScoped<SignInManager<IdentityUser>>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(
    //"JwtBearer",
    o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidIssuer = "PetVetAppApi",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("PetVetAppApiUltraMegaSuperSecretKey")),
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
        JwtBearerDefaults.AuthenticationScheme);

    defaultAuthorizationPolicyBuilder =
        defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("corsConfig");
app.UseAuthentication();
//app.UseRouting();
app.UseAuthorization();
//app.UseEndpoints();
app.MapControllers();

app.Run();
