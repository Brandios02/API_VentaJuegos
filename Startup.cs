public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // ... otras configuraciones

        services.AddAuthentication("TuEsquemaDeAutenticacion") // Reemplaza "TuEsquemaDeAutenticacion" con tu esquema real.
            .AddTuProveedorDeAutenticacion(options =>
            {
                // Configuración del proveedor de autenticación
            });

        services.AddAuthorization(options =>
        {
            // Configuración de políticas de autorización si es necesario
        });

        // ... otras configuraciones
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication(); // Asegúrate de que UseAuthentication esté configurado antes de otros middleware.

        app.UseAuthorization();

        // ... otras configuraciones

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
