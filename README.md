![Hsc Logo](HSClg.png)

Hsc.ApiFramework is a simple framework to set up an ASP.NET Core Web API with authentication and roles. It is intended for simple and quick projects that need a simple login / register system. It makes use of Microsoft Identity libraries.

Hsc.ApiFramework is written in .NET 6.0 and is designed to be simple to use, and can be added to any existing ASP.NET Core WEB API.

---
# Getting Started


## 1 - Get the required packages from Nuget

Use the following command in the Nuget Package Manager console:

```
    Install-Package Hsc.ApiFramework.Authentication
```
If you have no database set up yet, you may also add one of the following Data Source packages:

```
    Install-Package Hsc.ApiFramework.Database.SqlServer
```

## 2 - Environment variables

Hsc.ApiFramework is dependant on several Environment variables which need to be added. This can be done through Docker compose (see sample project), or by going through 

| Name                     | Description    | Notes |
| -------------------------| :--------------- | --- |
| HSC_DATABASE_CONNECTION  | Connection string to database | *Only required when any of the data source packages are used*
| HSC_JWT_AUDIENCE         | JWT Token Audience - Usually set to the URL of the issuing server (https://localhost:{port} when running locally)
| HSC_JWT_ISSUER           | JWT Token Issuer - Usually set to the URL of the issueing server (https://localhost:{port} when running locally)
| HSC_JWT_SECRET           | secret used for JWT Token generation (can be a randomly generated string)

---

## 3 - Program.cs

After completing the above steps, Hsc.ApiFramework can be activated by adding the following code to Program.cs of a .NET 6.0 ASP.NET Core API project

### 3.1 - Add database (When the `Hsc.ApiFramework.Database.SqlServer` is added)

In this example i've used `HscDatabaseContext` as the database type, however any `IdentityDbContext<TIdentityUser>` can be used. 

There are optional overloads of this function available to specify `TIdentityUser` and `TIdentityRole` if need be.

```csharp

// --- add after builder.Services.AddSwaggerGen() ---
builder.Services.AddHscDatabase<HscDatabaseContext>();

```

### 3.2 - Add Authentication

This line handles the bulk of Hsc.ApiFramework setup. It also adds an Authentication controller and endpoints.

```csharp

// --- add after creating database ---
builder.Services.AddHscAuthentication<HscDatabaseContext>();

```


### 3.3 - Use Authentication

As a final step, we have to tell the app to use the framework. There are parameters available if `app.UseAuthorization()` and / or `app.UseAuthentication` are already used in other places.

```csharp

//--- add before app.MapControllers()
app.UseHscAuthentication();
```

## 4 - After setup

If setup has been done correctly, swagger should list 3 new methods under the Authorization controller. These to register and/or obtain a token to access restricted endpoints.

| Method | Path | Parameters In Body | Returns |
| --- | --- | -- | -- |
| POST | /Authentication/login | *username, password* | JWT Bearer token to use to access protected api endpoints
| POST | /Authentication/register | *username, email, password* | newly created user
| POST | /Authentication/register-admin | *username, email, password* | newly created admin user


### 4.1 - Add authorization support to desired Controllers

Add the `[Authorize]` attribute to any Controller or Controller Method to make it require authentication.

In the sample application it is added to the default Microsoft weather forecast controller template.

```csharp

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
        //-- rest of controller
```