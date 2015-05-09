Imports System.Data.SQLite

Public MustInherit Class BaseQuery
    Implements ISqliteQuery

    Private _path As String

    Public Property Path() As String Implements ISqliteQuery.Path
        Get
            Return _path
        End Get
        Set(value As String)
            _path = StringUtil.GetConnectionFilename(value)
        End Set
    End Property

    Public Property TableName As String

    Public Property Columns As List(Of String)

    Public Property Where As String

    Public Property WhereIn As Tuple(Of String, List(Of String))

    Public Property OrderBy As List(Of String)

    Public Property Fields As Dictionary(Of String, String)

    Public Property PrimaryKey As String


    Public MustOverride Function Execute(ByVal conn As SQLiteConnection, ByVal tr As SQLiteTransaction) As IQueryResult Implements ISqliteQuery.Execute
    Public MustOverride Function Execute() As IQueryResult
       
End Class
