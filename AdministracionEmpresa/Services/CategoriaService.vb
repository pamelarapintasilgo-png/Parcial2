Public Class CategoriaService
    Private ReadOnly _categoriaRepo As CategoriaRepository

    Public Sub New(categoriaRepo As CategoriaRepository)
        _categoriaRepo = categoriaRepo
    End Sub

    Public Async Function ObtenerTodosAsync() As Task(Of List(Of CategoriaResponseDto))
        Dim categorias = Await _categoriaRepo.ObtenerTodosAsync()
        Return categorias.Select(Function(c) New CategoriaResponseDto With {
            .Id = c.Id,
            .Nombre = c.Nombre,
            .Descripcion = c.Descripcion
        }).ToList()
    End Function

    Public Async Function ObtenerPorIdAsync(id As Integer) As Task(Of CategoriaResponseDto)
        Dim c = Await _categoriaRepo.ObtenerPorIdAsync(id)
        If c Is Nothing Then Throw New KeyNotFoundException("La categoría no fue encontrada.")

        Return New CategoriaResponseDto With {
            .Id = c.Id,
            .Nombre = c.Nombre,
            .Descripcion = c.Descripcion
        }
    End Function

    Public Async Function RegistrarAsync(dto As CategoriaCreateDto) As Task(Of CategoriaResponseDto)
        ' Regla 2: Categorías duplicadas
        Dim existente = Await _categoriaRepo.ObtenerPorNombreAsync(dto.Nombre)
        If existente IsNot Nothing Then
            Throw New InvalidOperationException("Ya existe una categoría registrada con ese nombre.")
        End If

        Dim categoria As New Categoria With {
            .Nombre = dto.Nombre,
            .Descripcion = dto.Descripcion
        }

        Dim creada = Await _categoriaRepo.RegistrarAsync(categoria)

        Return New CategoriaResponseDto With {
            .Id = creada.Id,
            .Nombre = creada.Nombre,
            .Descripcion = creada.Descripcion
        }
    End Function

    Public Async Function EliminarAsync(id As Integer) As Task
        Dim categoria = Await _categoriaRepo.ObtenerPorIdAsync(id)
        If categoria Is Nothing Then Throw New KeyNotFoundException("La categoría no fue encontrada.")

        ' Regla 4: No permitir eliminar si tiene productos asociados
        If categoria.Productos IsNot Nothing AndAlso categoria.Productos.Count > 0 Then
            Throw New InvalidOperationException("No se puede eliminar la categoría porque tiene productos asociados.")
        End If

        Await _categoriaRepo.EliminarAsync(categoria)
    End Function
End Class