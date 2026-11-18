Imports System.Net
Imports System.Net.Http
Imports System.Text.Json
Imports Microsoft.AspNetCore.Http

Public Class ExceptionMiddleware
    Private ReadOnly _next As RequestDelegate

    Public Sub New(nextDelegate As RequestDelegate)
        _next = nextDelegate
    End Sub

    Public Async Function InvokeAsync(context As HttpContext) As Task
        Dim exceptionTask As Task = Nothing

        Try
            Await _next(context)
        Catch ex As Exception
            ' VB no permite usar Await directamente dentro de Catch.
            exceptionTask = HandleExceptionAsync(context, ex)
        End Try

        If exceptionTask IsNot Nothing Then
            Await exceptionTask
        End If
    End Function

    Private Async Function HandleExceptionAsync(context As HttpContext, exception As Exception) As Task
        context.Response.ContentType = "application/json"
        Dim statusCode As Integer = CInt(HttpStatusCode.InternalServerError)
        Dim mensaje As String = "Ocurrió un error inesperado en el servidor."

        If TypeOf exception Is KeyNotFoundException Then
            statusCode = CInt(HttpStatusCode.NotFound)
            mensaje = exception.Message
        ElseIf TypeOf exception Is InvalidOperationException OrElse TypeOf exception Is ArgumentException Then
            statusCode = CInt(HttpStatusCode.Conflict) ' Código 409 para reglas de negocio
            mensaje = exception.Message
        End If

        context.Response.StatusCode = statusCode
        Dim response = New With {Key .status = statusCode, Key .error = mensaje}
        Await context.Response.WriteAsync(JsonSerializer.Serialize(response))
    End Function
End Class