using CSHBlog.Web.Data;
using CSHBlog.Web.Repositories;
using dotenv.net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
//var envVars = DotEnv.Read();


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();




if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<CSHDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CSHDbConnectionString")));
    builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CSHDbConnectionString")));
    //builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDbConnectionString"))); //改使用同一個db
}
else
{
    builder.Services.AddDbContext<CSHDbContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("CSHDbConnectionString")));
    builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("CSHDbConnectionString")));    
}


builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;

});

builder.Services.AddScoped<ITagRepositories, TagRepositories>();


builder.Services.AddScoped<IBlogPostRepositories, BlogPostRepositories>();

builder.Services.AddScoped<IImageRepositories, CloudinaryImageRepositories>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//test
Array array = new int[] { 1, 2, 3 };

foreach (var item in array)
{
    Console.WriteLine(item);
}

var eumerator = array.GetEnumerator();
while (eumerator.MoveNext())
{
    Console.WriteLine(eumerator.Current);
}


array.GetEnumerator();

