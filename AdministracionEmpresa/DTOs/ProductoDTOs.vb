Imports System.ComponentModel.DataAnnotations

Public Class ProductoCreateDto
    <Required(ErrorMessage:="El nombre del producto es obligatorio.")>
    <StringLength(50, ErrorMessage:="El nombre del producto no puede superar los 50 caracteres.")>
    Public Property Nombre As String

    <StringLength(200, ErrorMessage:="La descripción no puede superar los 200 caracteres.")>
    Public Property Descripcion As String

    <Range(0.01, Double.MaxValue, ErrorMessage:="El precio debe ser mayor que cero.")>
    Public Property Precio As Decimal

    <Range(0, Integer.MaxValue, ErrorMessage:="El stock no puede ser negativo.")>
    Public Property Stock As Integer

    <Required(ErrorMessage:="La categoría es obligatoria.")>
    Public Property CategoriaId As Integer
End Class

Public Class ProductoUpdateDto
    <Required>
    Public Property Nombre As String
    <StringLength(200)>
    Public Property Descripcion As String
    <Range(0.01, Double.MaxValue)>
    Public Property Precio As Decimal
    <Range(0, Integer.MaxValue)>
    Public Property Stock As Integer
    <Required>
    Public Property CategoriaId As Integer
End Class

Public Class ProductoResponseDto
    Public Property Id As Integer
    Public Property Nombre As String
    Public Property Descripcion As String
    Public Property Precio As Decimal
    Public Property Stock As Integer
    Public Property FechaRegistro As DateTime
    Public Property CategoriaId As Integer
    Public Property CategoriaNombre As String
End Class