Imports Microsoft.EntityFrameworkCore

Public Class ProductoRepository
    Private ReadOnly _context As AppDbContext

    Public Sub New(context As AppDbContext)
        _context = context
    End Sub

    Public Async Function ObtenerTodosAsync() As Task(Of List(Of Producto))
        Return Await _context.Productos.Include(Function(p) p.Categoria).ToListAsync()
    End Function

    Public Async Function ObtenerPorIdAsync(id As Integer) As Task(Of Producto)
        Return Await _context.Productos.Include(Function(p) p.Categoria).FirstOrDefaultAsync(Function(p) p.Id = id)
    End Function

    Public Async Function ObtenerPorCategoriaAsync(categoriaId As Integer) As Task(Of List(Of Producto))
        Return Await _context.Productos.Include(Function(p) p.Categoria).Where(Function(p) p.CategoriaId = categoriaId).ToListAsync()
    End Function

    Public Async Function ObtenerPorNombreAsync(nombre As String) As Task(Of Producto)
        Return Await _context.Productos.FirstOrDefaultAsync(Function(p) p.Nombre.ToLower() = nombre.ToLower())
    End Function

    Public Async Function RegistrarAsync(producto As Producto) As Task(Of Producto)
        _context.Productos.Add(producto)
        Await _context.SaveChangesAsync()
        Return producto
    End Function

    Public Async Function ModificarAsync(producto As Producto) As Task
        _context.Productos.Update(producto)
        Await _context.SaveChangesAsync()
    End Function

    Public Async Function EliminarAsync(producto As Producto) As Task
        _context.Productos.Remove(producto)
        Await _context.SaveChangesAsync()
    End Function
End Class