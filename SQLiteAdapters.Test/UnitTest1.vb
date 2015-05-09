Imports System.Collections.Generic
Imports System.Linq
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports SignWriterStudio.SQLiteAdapters

<TestClass> _
Public Class UnitTest1
    Private Const Path As String = "C:\Users\Jonathan\Documents\SignWriter Studio Sample Files\LESHO\LESHO - Copy (9) - Copy.SWS"

    <TestMethod> _
    Public Sub GetData()


        Dim selectColumns = New List(Of String)() From { _
            "IDDictionary" _
        }

        Dim query = New GetQuery() With { _
            .Path = Path, _
            .TableName = "Dictionary", _
            .Columns = selectColumns _
        }
        Dim result = GetSqlite.GetData(query)

        Dim firstOrDefault = result.TabularResults.FirstOrDefault()
        Dim actual = 0
        If firstOrDefault IsNot Nothing Then
            actual = firstOrDefault.Count
        End If

        Assert.AreEqual(1279, actual)
    End Sub

    <TestMethod> _
    Public Sub InsertData()

        Dim insertColumns = New List(Of String)() From { _
            "IDDictionary", _
            "IDSignLanguage", _
            "GUID" _
        }

        Dim values = New List(Of List(Of String))() From { _
            New List(Of String)() From { _
                "112555", _
                "1", _
                Guid.NewGuid().ToString() _
            }, _
            New List(Of String)() From { _
                "112556", _
                "1", _
                Guid.NewGuid().ToString() _
            }, _
            New List(Of String)() From { _
                "112557", _
                "1", _
                Guid.NewGuid().ToString() _
            } _
        }

        Dim query = New InsertQuery() With { _
            .Path = Path, _
            .TableName = "Dictionary", _
            .Columns = insertColumns, _
            .Values = values _
        }
        Dim result = query.Execute()

        Assert.AreEqual(3, result.AffectedRows)
    End Sub

    <TestMethod> _
    Public Sub UpdateData()

        Dim updateColumns = New List(Of String)() From { _
            "IDDictionary", _
            "IDSignLanguage", _
            "GUID" _
        }

        Dim newValues = New List(Of List(Of String))() From { _
            New List(Of String)() From {"112555", "1", Guid.NewGuid().ToString()}, _
            New List(Of String)() From {"112556", "1", Guid.NewGuid().ToString()}, _
            New List(Of String)() From {"112557", "1", Guid.NewGuid().ToString()}}

        Dim query = New UpdateQuery() With { _
            .Path = Path, _
            .TableName = "Dictionary", _
            .Columns = updateColumns, _
            .Values = newValues _
        }
        Dim result = query.Execute()

        Assert.AreEqual(3, result.AffectedRows)
    End Sub

    <TestMethod> _
    Public Sub DeleteData()

        Const where As String = "[IDDictionary] = '112557'"

        Dim query = New DeleteQuery() With { _
            .Path = Path, _
            .TableName = "Dictionary", _
            .Where = where _
        }
        Dim result = query.Execute()

        Assert.AreEqual(1, result.AffectedRows)
    End Sub

    <TestMethod> _
    Public Sub DeleteDataWhereIn()

        Dim wherein = Tuple.Create("IDDictionary", New List(Of String)() From { _
            "112555", "112556", "112557"})


        Dim query = New DeleteQuery() With { _
            .Path = Path, _
            .TableName = "Dictionary", _
            .WhereIn = wherein _
        }

        Dim result = query.Execute()
        Assert.AreEqual(3, result.AffectedRows)
    End Sub

    <TestMethod> _
    Public Sub UnitOfWork()

        Dim insertColumns = New List(Of String)() From { _
            "IDDictionary", _
            "IDSignLanguage", _
            "GUID" _
        }

        Dim values = New List(Of List(Of String))() From { _
            New List(Of String)() From {"112555", "1", Guid.NewGuid().ToString()}, _
            New List(Of String)() From {"112556", "1", Guid.NewGuid().ToString()}, _
            New List(Of String)() From {"112557", "1", Guid.NewGuid().ToString()}}

        Dim insertquery = New InsertQuery() With { _
            .Path = Path, _
            .TableName = "Dictionary", _
            .Columns = insertColumns, _
            .Values = values _
        }

        Dim wherein = Tuple.Create("IDDictionary", New List(Of String)() From { _
            "112555", _
            "112556", _
            "112557" _
        })

        Dim updateColumns = New List(Of String)() From { _
            "IDDictionary", _
            "IDSignLanguage", _
            "GUID" _
        }

        Dim newValues = New List(Of List(Of String))() From { _
            New List(Of String)() From {"112555", "1", Guid.NewGuid().ToString()}, _
            New List(Of String)() From {"112556", "1", Guid.NewGuid().ToString()}, _
            New List(Of String)() From {"112557", "1", Guid.NewGuid().ToString()} _
        }

        Dim updatequery = New UpdateQuery() With { _
            .Path = Path, _
            .TableName = "Dictionary", _
            .Columns = updateColumns, _
            .Values = newValues _
        }
        Dim deletequery = New DeleteQuery() With { _
            .Path = Path, _
            .TableName = "Dictionary", _
            .WhereIn = wherein _
        }
        Dim selectColumns = New List(Of String)() From { _
            "IDDictionary" _
        }

        Dim getquery = New GetQuery() With { _
            .Path = Path, _
            .TableName = "Dictionary", _
            .Columns = selectColumns _
        }
        Dim unitofWork1 = New UnitOfWork()

        unitofWork1.Queries.Add(insertquery)
        unitofWork1.Queries.Add(updatequery)
        unitofWork1.Queries.Add(deletequery)
        unitofWork1.Queries.Add(getquery)

        Dim result = unitofWork1.Execute()

        Assert.AreEqual(4, result.Count())
    End Sub
End Class
