namespace RedArbor.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Added application and infrastructure layers
        builder.Services.AddWebApiServices();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        // if (app.Environment.IsDevelopment())
        // {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();

        // }

        // Added middlewares for error handling and others
        app.UseMiddlewares();


        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
