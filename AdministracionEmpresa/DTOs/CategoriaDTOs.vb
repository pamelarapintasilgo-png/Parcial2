Imports System.ComponentModel.DataAnnotations

Public Class CategoriaCreateDto
    <Required(ErrorMessage:="El nombre de la categoría es obligatorio.")>
    <StringLength(50, ErrorMessage:="El nombre no puede superar los 50 caracteres.")>
    Public Property Nombre As String

    <StringLength(200)>
    Public Property Descripcion As String
End Class

Public Class CategoriaResponseDto
    Public Property Id As Integer
    Public Property Nombre As String
    Public Property Descripcion As String
End Class