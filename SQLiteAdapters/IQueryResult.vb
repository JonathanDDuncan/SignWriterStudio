Imports System.Collections.Generic
Imports System.Dynamic


Public Interface IQueryResult
    Property AffectedRows() As Integer
    Property ScalarResults() As List(Of Object)
    Property TabularResults() As List(Of List(Of ExpandoObject))
    Property SqlCommands() As List(Of String)
End Interface