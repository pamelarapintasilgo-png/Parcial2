Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Public Class Producto
    <Key>
    Public Property Id As Integer

    <Required>
    <StringLength(50)>
    Public Property Nombre As String

    <StringLength(200)>
    Public Property Descripcion As String

    <Column(TypeName:="decimal(18,2)")>
    Public Property Precio As Decimal

    Public Property Stock As Integer

    Public Property FechaRegistro As DateTime = DateTime.Now

    Public Property CategoriaId As Integer

    Public Property Categoria As Categoria
End Class