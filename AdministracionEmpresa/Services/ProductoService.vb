Public Class ProductoService
    Private ReadOnly _productoRepo As ProductoRepository
    Private ReadOnly _categoriaRepo As CategoriaRepository

    Public Sub New(productoRepo As ProductoRepository, categoriaRepo As CategoriaRepository)
        _productoRepo = productoRepo
        _categoriaRepo = categoriaRepo
    End Sub

    Public Async Function ObtenerTodosAsync() As Task(Of List(Of ProductoResponseDto))
        Dim productos = Await _productoRepo.ObtenerTodosAsync()
        Return productos.Select(Function(p) New ProductoResponseDto With {
            .Id = p.Id,
            .Nombre = p.Nombre,
            .Descripcion = p.Descripcion,
            .Precio = p.Precio,
            .Stock = p.Stock,
            .FechaRegistro = p.FechaRegistro,
            .CategoriaId = p.CategoriaId,
            .CategoriaNombre = If(p.Categoria IsNot Nothing, p.Categoria.Nombre, String.Empty)
        }).ToList()
    End Function

    Public Async Function ObtenerPorIdAsync(id As Integer) As Task(Of ProductoResponseDto)
        Dim p = Await _productoRepo.ObtenerPorIdAsync(id)
        If p Is Nothing Then Throw New KeyNotFoundException("El producto no fue encontrado.")

        Return New ProductoResponseDto With {
            .Id = p.Id,
            .Nombre = p.Nombre,
            .Descripcion = p.Descripcion,
            .Precio = p.Precio,
            .Stock = p.Stock,
            .FechaRegistro = p.FechaRegistro,
            .CategoriaId = p.CategoriaId,
            .CategoriaNombre = If(p.Categoria IsNot Nothing, p.Categoria.Nombre, String.Empty)
        }
    End Function

    Public Async Function ObtenerPorCategoriaAsync(categoriaId As Integer) As Task(Of List(Of ProductoResponseDto))
        Dim productos = Await _productoRepo.ObtenerPorCategoriaAsync(categoriaId)
        Return productos.Select(Function(p) New ProductoResponseDto With {
            .Id = p.Id,
            .Nombre = p.Nombre,
            .Descripcion = p.Descripcion,
            .Precio = p.Precio,
            .Stock = p.Stock,
            .FechaRegistro = p.FechaRegistro,
            .CategoriaId = p.CategoriaId,
            .CategoriaNombre = If(p.Categoria IsNot Nothing, p.Categoria.Nombre, String.Empty)
        }).ToList()
    End Function

    Public Async Function RegistrarAsync(dto As ProductoCreateDto) As Task(Of ProductoResponseDto)
        Dim prodExistente = Await _productoRepo.ObtenerPorNombreAsync(dto.Nombre)
        If prodExistente IsNot Nothing Then
            Throw New InvalidOperationException("Ya existe un producto registrado con el mismo nombre.")
        End If

        Dim categoria = Await _categoriaRepo.ObtenerPorIdAsync(dto.CategoriaId)
        If categoria Is Nothing Then
            Throw New KeyNotFoundException("La categoría especificada no existe.")
        End If

        If dto.Stock < 0 Then
            Throw New ArgumentException("El stock no puede ser negativo.")
        End If

        Dim producto As New Producto With {
            .Nombre = dto.Nombre,
            .Descripcion = dto.Descripcion,
            .Precio = dto.Precio,
            .Stock = dto.Stock,
            .CategoriaId = dto.CategoriaId
        }

        Dim creado = Await _productoRepo.RegistrarAsync(producto)

        Return New ProductoResponseDto With {
            .Id = creado.Id,
            .Nombre = creado.Nombre,
            .Descripcion = creado.Descripcion,
            .Precio = creado.Precio,
            .Stock = creado.Stock,
            .FechaRegistro = creado.FechaRegistro,
            .CategoriaId = creado.CategoriaId,
            .CategoriaNombre = categoria.Nombre
        }
    End Function

    Public Async Function EliminarAsync(id As Integer) As Task
        Dim producto = Await _productoRepo.ObtenerPorIdAsync(id)
        If producto Is Nothing Then Throw New KeyNotFoundException("El producto no fue encontrado.")
        Await _productoRepo.EliminarAsync(producto)
    End Function
End Class