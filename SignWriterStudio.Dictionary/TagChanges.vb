Imports NUnit.Framework
Imports SignWriterStudio.SWClasses

Namespace SignWriterStudio.Dictionary
    Public Class TagChanges
        Public Shared Function GetTagChanges(ByVal myDictionary As SWDict) As Tuple(Of List(Of List(Of String)), List(Of List(Of String)))
            Dim ds As New DataSet
            Dim previousDs As DataSet
            Dim dt As DataTable = myDictionary.DictionaryBindingSource1.DataSource
            Dim toAdd As List(Of List(Of String))
            Dim toRemove As List(Of List(Of String))

            If dt IsNot Nothing Then
                If dt.DataSet IsNot Nothing Then
                    previousDs = dt.DataSet
                    previousDs.Tables.Remove(dt)
                End If
                ds.Tables.Add(dt)
                Dim deletedDs = ds.GetChanges(DataRowState.Deleted)
                Dim addedDs = ds.GetChanges(DataRowState.Added)
                Dim updatedDs = ds.GetChanges(DataRowState.Modified)

                Dim result = GetUpdatedChanges(updatedDs)


                toAdd = GetToAdd(addedDs, result.Item1)

                toRemove = GetToRemove(deletedDs, result.Item2)


            End If
            Return Tuple.Create(toAdd, toRemove)
        End Function

        Private Shared Function GetToRemove(ByVal deletedDs As DataSet, ByVal toRemove As List(Of List(Of String))) As List(Of List(Of String))
            If deletedDs IsNot Nothing AndAlso deletedDs.Tables(0) IsNot Nothing Then
                For Each row As DataRow In deletedDs.Tables(0).Rows
                    If Not row.RowState = DataRowState.Deleted Then
                        toRemove.Add(New List(Of String)() From {row.Item("IDDictionary").ToString(), row("Tags", DataRowVersion.Current).ToString()})
                    End If
                Next
            End If
            Return toRemove

        End Function

        Private Shared Function GetToAdd(ByVal addedDs As DataSet, ByVal toAdd As List(Of List(Of String))) As List(Of List(Of String))
            If addedDs IsNot Nothing AndAlso addedDs.Tables(0) IsNot Nothing Then
                For Each row As DataRow In addedDs.Tables(0).Rows
                    toAdd.Add(New List(Of String)() From {row.Item("IDDictionary").ToString(), row("Tags", DataRowVersion.Current).ToString()})
                Next
            End If
            Return toAdd
        End Function

        Private Shared Function GetUpdatedChanges(ByVal updatedDs As DataSet) As Tuple(Of List(Of List(Of String)), List(Of List(Of String)))
            Dim toAdd As New List(Of List(Of String))()
            Dim toRemove As New List(Of List(Of String))()

            If updatedDs IsNot Nothing AndAlso updatedDs.Tables(0) IsNot Nothing Then
                For Each row As DataRow In updatedDs.Tables(0).Rows
                  
                    TagsToAddorRemove(row, toRemove, toAdd)
                Next
            End If
            Return Tuple.Create(toAdd, toRemove)
        End Function

        Private Shared Sub TagsToAddorRemove(ByVal row As DataRow, ByVal toRemove As List(Of List(Of String)), ByVal toAdd As List(Of List(Of String)))
            Dim originalValues = TryCast(row("OriginalTags", DataRowVersion.Current), List(Of String))
            Dim newValues = TryCast(row("Tags", DataRowVersion.Current), List(Of String))

            Dim idDictionary As String = row.Item("IDDictionary").ToString()
            If originalValues IsNot Nothing Then
                For Each origValue In originalValues
                    If Not newValues.Contains(origValue) Then
                        toRemove.Add(New List(Of String)() From {idDictionary, origValue})
                    End If

                Next
                For Each newValue In newValues
                    If Not originalValues.Contains(newValue) Then
                        toAdd.Add(New List(Of String) From {idDictionary, newValue})
                    End If

                Next
            Else
                If newValues IsNot Nothing Then
                    For Each newValue In newValues
                        toAdd.Add(New List(Of String) From {idDictionary, newValue})
                    Next
                End If
            End If
        End Sub
    End Class
End Namespace