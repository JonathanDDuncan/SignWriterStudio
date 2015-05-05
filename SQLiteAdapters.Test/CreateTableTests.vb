Imports System.Collections.Generic
Imports System.IO
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports SignWriterStudio.SQLiteAdapters

<TestClass> _
Public Class CreateTableTests
    <TestMethod> _
    Public Sub CreateTable()
        Dim path1 = Path.GetTempFileName()
        Const tableName As String = "MyNewTable"
        Dim fields = New Dictionary(Of String, String)() From { _
            {"PrimaryKey", "int"}, _
            {"SecondField", "TEXT"}, _
            {"ThirdField", "TEXT"} _
        }

        Dim query = New CreateTableQuery() With { _
            .Path = path1, _
            .TableName = tableName, _
            .PrimaryKey = "PrimaryKey", _
            .Fields = fields _
        }

        query.Execute()

        Dim tableQuery = New GetQuery() With { _
            .TableName = tableName, _
            .Path = path1 _
        }

        Dim tableResult = tableQuery.Execute()

        Assert.AreEqual(1, tableResult.TabularResults.Count())
    End Sub

End Class
