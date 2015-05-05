Imports System.Collections.Generic
Imports System.Dynamic


Public Class QueryResult
    Implements IQueryResult
    Public Property AffectedRows As Integer Implements IQueryResult.AffectedRows

    Public Property ScalarResults As List(Of Object) Implements IQueryResult.ScalarResults

    Public Property TabularResults As List(Of List(Of ExpandoObject)) Implements IQueryResult.TabularResults

    Public Property SqlCommands As List(Of String) Implements IQueryResult.SqlCommands
End Class