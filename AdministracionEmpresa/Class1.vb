Imports Microsoft.AspNetCore.Builder
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.Hosting

Module Program
    Sub Main(args As String())
        Dim builder = WebApplication.CreateBuilder(args)

        ' Configurar Base de Datos SQL Server.
        builder.Services.AddDbContext(Of AppDbContext)(
            Sub(options)
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnexionBD"))
            End Sub)

        ' Inyección de dependencias (repositorios y servicios).
        builder.Services.AddScoped(Of CategoriaRepository)()
        builder.Services.AddScoped(Of ProductoRepository)()
        builder.Services.AddScoped(Of CategoriaService)()
        builder.Services.AddScoped(Of ProductoService)()

        builder.Services.AddControllers()
        builder.Services.AddEndpointsApiExplorer()
        builder.Services.AddSwaggerGen()

        Dim app = builder.Build()

        ' Registrar el middleware global de excepciones.
        app.UseMiddleware(Of ExceptionMiddleware)()

        If app.Environment.IsDevelopment() Then
            app.UseSwagger()
            app.UseSwaggerUI()
        End If

        app.UseHttpsRedirection()
        app.UseAuthorization()
        app.MapControllers()

        app.Run()
    End Sub
End Module
