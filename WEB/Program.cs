using FoodDelivery.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// DbContext domeniu
builder.Services.AddDbContext<FoodDeliveryContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("FoodDeliveryContext"),
        sql => sql.EnableRetryOnFailure()
    ));

// Identity DbContext separat
builder.Services.AddDbContext<FoodDeliveryIdentityContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("FoodDeliveryIdentityContext"),
        sql => sql.EnableRetryOnFailure()
    ));

builder.Services.AddControllers();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<FoodDeliveryIdentityContext>();

// Policies: doar Admin + User
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("AdminOrUser", policy => policy.RequireRole("Admin", "User"));
});



builder.Services.AddRazorPages(options =>
{
    // ADMIN ONLY (CRUD complet)
    options.Conventions.AuthorizeFolder("/Customers", "AdminOnly");
    options.Conventions.AuthorizeFolder("/Payments", "AdminOnly");

    // Restaurants: user vede doar Index/Details, admin are CRUD
    options.Conventions.AuthorizeFolder("/Restaurants", "AdminOnly");
    options.Conventions.AllowAnonymousToPage("/Restaurants/Index");
    options.Conventions.AllowAnonymousToPage("/Restaurants/Details");

    // MenuItems: user vede doar Index/Details, admin are CRUD
    options.Conventions.AuthorizeFolder("/MenuItems", "AdminOnly");
    options.Conventions.AllowAnonymousToPage("/MenuItems/Index");
    options.Conventions.AllowAnonymousToPage("/MenuItems/Details");

    // Orders: user denied total, admin allowed
    options.Conventions.AuthorizeFolder("/Orders", "AdminOnly");

    // OrderItems: user denied total, admin allowed
    options.Conventions.AuthorizeFolder("/OrderItems", "AdminOnly");

    // Reviews: user poate doar Index + Create, admin are tot
    
    options.Conventions.AuthorizePage("/Reviews/Index", "AdminOrUser");
    options.Conventions.AuthorizePage("/Reviews/Create", "AdminOrUser");
    options.Conventions.AuthorizePage("/Reviews/Delete", "AdminOnly");
    options.Conventions.AuthorizePage("/Reviews/Edit", "AdminOnly");
    options.Conventions.AuthorizePage("/Reviews/Details", "AdminOnly");

    // optional: daca vrei si Details vizibil la user
    options.Conventions.AuthorizePage("/Reviews/Details", "AdminOrUser");
});


var app = builder.Build();




if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

// Migrate + Seed roluri + useri (Admin + User)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // 1) migrari Identity
    var identityDb = services.GetRequiredService<FoodDeliveryIdentityContext>();
    await identityDb.Database.MigrateAsync();

    // 2) migrari Domain
    var domainDb = services.GetRequiredService<FoodDeliveryContext>();
    await domainDb.Database.MigrateAsync();

    // 3) seed Domain (restaurants, menuitems, customers, orders, payments, reviews)
    await SeedData.SeedAsync(domainDb);

    // 4) seed Roles + Users
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles = { "Admin", "User" };
    foreach (var role in roles)
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));

    // Admin
    var adminEmail = "admin@food.local";
    var admin = await userManager.FindByEmailAsync(adminEmail);
    if (admin == null)
    {
        admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        var created = await userManager.CreateAsync(admin, "Admin1234!");
        if (created.Succeeded)
            await userManager.AddToRoleAsync(admin, "Admin");
    }

    // User
    var userEmail = "user@food.local";
    var user = await userManager.FindByEmailAsync(userEmail);
    if (user == null)
    {
        user = new IdentityUser { UserName = userEmail, Email = userEmail, EmailConfirmed = true };
        var created = await userManager.CreateAsync(user, "User1234!");
        if (created.Succeeded)
            await userManager.AddToRoleAsync(user, "User");
    }
}

app.Run();
