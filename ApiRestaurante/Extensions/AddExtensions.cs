namespace ApiRestaurante.Extensions
{
    public static class AddExtensions
    {
        public static void UserSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Restaurante");
            });
        }
    }
}
