Imports System.ComponentModel.DataAnnotations

Public Class Categoria
    <Key>
    Public Property Id As Integer

    <Required>
    <StringLength(50)>
    Public Property Nombre As String

    <StringLength(200)>
    Public Property Descripcion As String

    Public Property Productos As List(Of Producto)
End Class