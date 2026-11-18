Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.Mvc

<ApiController>
<Route("api/[controller]")>
Public Class ProductosController
    Inherits ControllerBase

    Private ReadOnly _productoService As ProductoService

    Public Sub New(productoService As ProductoService)
        _productoService = productoService
    End Sub

    <HttpGet>
    Public Async Function GetProductos() As Task(Of ActionResult(Of List(Of ProductoResponseDto)))
        Dim lista = Await _productoService.ObtenerTodosAsync()
        Return Ok(lista)
    End Function

    <HttpGet("{id}")>
    Public Async Function GetProducto(id As Integer) As Task(Of ActionResult(Of ProductoResponseDto))
        Dim producto = Await _productoService.ObtenerPorIdAsync(id)
        Return Ok(producto)
    End Function

    <HttpGet("categoria/{categoriaId}")>
    Public Async Function GetProductosPorCategoria(categoriaId As Integer) As Task(Of ActionResult(Of List(Of ProductoResponseDto)))
        Dim lista = Await _productoService.ObtenerPorCategoriaAsync(categoriaId)
        Return Ok(lista)
    End Function

    <HttpPost>
    Public Async Function PostProducto(dto As ProductoCreateDto) As Task(Of ActionResult(Of ProductoResponseDto))
        Dim creado = Await _productoService.RegistrarAsync(dto)
        Return StatusCode(201, creado)
    End Function

    <HttpDelete("{id}")>
    Public Async Function DeleteProducto(id As Integer) As Task(Of ActionResult)
        Await _productoService.EliminarAsync(id)
        Return NoContent()
    End Function
End Class