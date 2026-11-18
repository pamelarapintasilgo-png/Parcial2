Imports Microsoft.EntityFrameworkCore

Public Class AppDbContext
    Inherits DbContext

    Public Sub New(options As DbContextOptions(Of AppDbContext))
        MyBase.New(options)
    End Sub

    Public Property Categorias As DbSet(Of Categoria)
    Public Property Productos As DbSet(Of Producto)
End Class