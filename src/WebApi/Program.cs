namespace RedArbor.WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Added application and infrastructure layers
        builder.Services.AddWebApiServices();
        builder.Services.AddAuthenticationJwt(builder.Configuration);
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();

        // CORS policy name
        const string AllowSpecificOrigins = "_SpecificOrigins";

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(AllowSpecificOrigins, builder =>
            {
                builder.WithOrigins("*")
                       .WithMethods("*")
                       .WithHeaders("*");
            });
        });

        var app = builder.Build();

        // await app.InitializeData();

        // Configure the HTTP request pipeline.
        // if (app.Environment.IsDevelopment())
        // {
        app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI();

        // }

        // Added middlewares for error handling and others
        app.UseMiddlewares();

        app.UseCors(AllowSpecificOrigins);

        // app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
