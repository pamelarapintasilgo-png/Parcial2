Imports Microsoft.EntityFrameworkCore

Public Class CategoriaRepository
    Private ReadOnly _context As AppDbContext

    Public Sub New(context As AppDbContext)
        _context = context
    End Sub

    Public Async Function ObtenerTodosAsync() As Task(Of List(Of Categoria))
        Return Await _context.Categorias.ToListAsync()
    End Function

    Public Async Function ObtenerPorIdAsync(id As Integer) As Task(Of Categoria)
        Return Await _context.Categorias.Include(Function(c) c.Productos).FirstOrDefaultAsync(Function(c) c.Id = id)
    End Function

    Public Async Function ObtenerPorNombreAsync(nombre As String) As Task(Of Categoria)
        Return Await _context.Categorias.FirstOrDefaultAsync(Function(c) c.Nombre.ToLower() = nombre.ToLower())
    End Function

    Public Async Function RegistrarAsync(categoria As Categoria) As Task(Of Categoria)
        _context.Categorias.Add(categoria)
        Await _context.SaveChangesAsync()
        Return categoria
    End Function

    Public Async Function ModificarAsync(categoria As Categoria) As Task
        _context.Categorias.Update(categoria)
        Await _context.SaveChangesAsync()
    End Function

    Public Async Function EliminarAsync(categoria As Categoria) As Task
        _context.Categorias.Remove(categoria)
        Await _context.SaveChangesAsync()
    End Function
End Class