Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.Mvc

<ApiController>
<Route("api/[controller]")>
Public Class CategoriasController
    Inherits ControllerBase

    Private ReadOnly _categoriaService As CategoriaService

    Public Sub New(categoriaService As CategoriaService)
        _categoriaService = categoriaService
    End Sub

    <HttpGet>
    Public Async Function GetCategorias() As Task(Of ActionResult(Of List(Of CategoriaResponseDto)))
        Dim lista = Await _categoriaService.ObtenerTodosAsync()
        Return Ok(lista)
    End Function

    <HttpGet("{id}")>
    Public Async Function GetCategoria(id As Integer) As Task(Of ActionResult(Of CategoriaResponseDto))
        Dim categoria = Await _categoriaService.ObtenerPorIdAsync(id)
        Return Ok(categoria)
    End Function

    <HttpPost>
    Public Async Function PostCategoria(dto As CategoriaCreateDto) As Task(Of ActionResult(Of CategoriaResponseDto))
        Dim creada = Await _categoriaService.RegistrarAsync(dto)
        Return StatusCode(201, creada)
    End Function

    <HttpDelete("{id}")>
    Public Async Function DeleteCategoria(id As Integer) As Task(Of ActionResult)
        Await _categoriaService.EliminarAsync(id)
        Return NoContent()
    End Function
End Class