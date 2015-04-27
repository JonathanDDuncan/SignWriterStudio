Imports System.Data.SQLite

Namespace DictionaryDataSetTableAdapters
    Partial Public Class DictionaryTableAdapter
        Public Property PublicConnection() As SQLiteConnection
            Get
                Return (Me.Connection)

            End Get
            Set(ByVal value As SQLiteConnection)
                Me.Connection = value
            End Set
        End Property
        Public Sub AssignConnection(ByVal conn As SQLiteConnection, _
            Optional ByVal trans As SQLiteTransaction = Nothing)
            Me.Connection = conn

            If trans IsNot Nothing Then

                Me.Adapter.InsertCommand.Transaction = trans

                Me.Adapter.DeleteCommand.Transaction = trans

                Me.Adapter.UpdateCommand.Transaction = trans


            End If



            Me.Adapter.AcceptChangesDuringUpdate = False

            Me.Adapter.ContinueUpdateOnError = False

        End Sub
    End Class
    Partial Public Class DictionaryGlossTableAdapter
        Public Property PublicConnection() As SQLiteConnection
            Get
                Return (Me.Connection)

            End Get
            Set(ByVal value As SQLiteConnection)
                Me.Connection = value
            End Set
        End Property
        Public Sub AssignConnection(ByVal conn As SQLiteConnection, _
           Optional ByVal trans As SQLiteTransaction = Nothing)
            Me.Connection = conn

            If trans IsNot Nothing Then

                Me.Adapter.InsertCommand.Transaction = trans

                Me.Adapter.DeleteCommand.Transaction = trans

                Me.Adapter.UpdateCommand.Transaction = trans


            End If



            Me.Adapter.AcceptChangesDuringUpdate = False

            Me.Adapter.ContinueUpdateOnError = False

        End Sub
    End Class
    Partial Public Class SignSymbolsTableAdapter
        Public Property PublicConnection() As SQLiteConnection
            Get
                Return (Me.Connection)

            End Get
            Set(ByVal value As SQLiteConnection)
                Me.Connection = value
            End Set
        End Property
        Public Sub AssignConnection(ByVal conn As SQLiteConnection, _
         Optional ByVal trans As SQLiteTransaction = Nothing)
            Me.Connection = conn

            If trans IsNot Nothing Then

                Me.Adapter.InsertCommand.Transaction = trans

                Me.Adapter.DeleteCommand.Transaction = trans

                Me.Adapter.UpdateCommand.Transaction = trans


            End If



            Me.Adapter.AcceptChangesDuringUpdate = False

            Me.Adapter.ContinueUpdateOnError = False

        End Sub
    End Class

    Partial Public Class SignSequenceTableAdapter
        Public Property PublicConnection() As SQLiteConnection
            Get
                Return (Me.Connection)

            End Get
            Set(ByVal value As SQLiteConnection)
                Me.Connection = value
            End Set
        End Property
        Public Sub AssignConnection(ByVal conn As SQLiteConnection, _
         Optional ByVal trans As SQLiteTransaction = Nothing)
            Me.Connection = conn

            If trans IsNot Nothing Then

                Me.Adapter.InsertCommand.Transaction = trans

                Me.Adapter.DeleteCommand.Transaction = trans

                Me.Adapter.UpdateCommand.Transaction = trans


            End If



            Me.Adapter.AcceptChangesDuringUpdate = False

            Me.Adapter.ContinueUpdateOnError = False

        End Sub

    End Class
    Partial Public Class PuddleTextTableAdapter
        Public Property PublicConnection() As SQLiteConnection
            Get
                Return (Me.Connection)

            End Get
            Set(ByVal value As SQLiteConnection)
                Me.Connection = value
            End Set
        End Property
        Public Sub AssignConnection(ByVal conn As SQLiteConnection, _
         Optional ByVal trans As SQLiteTransaction = Nothing)
            Me.Connection = conn

            If trans IsNot Nothing Then

                Me.Adapter.InsertCommand.Transaction = trans

                Me.Adapter.DeleteCommand.Transaction = trans

                Me.Adapter.UpdateCommand.Transaction = trans


            End If



            Me.Adapter.AcceptChangesDuringUpdate = False

            Me.Adapter.ContinueUpdateOnError = False

        End Sub

    End Class
    Partial Public Class FrameTableAdapter
        Public Property PublicConnection() As SQLiteConnection
            Get
                Return (Me.Connection)

            End Get
            Set(ByVal value As SQLiteConnection)
                Me.Connection = value
            End Set
        End Property
        Public Sub AssignConnection(ByVal conn As SQLiteConnection, _
         Optional ByVal trans As SQLiteTransaction = Nothing)
            Me.Connection = conn

            If trans IsNot Nothing Then

                Me.Adapter.InsertCommand.Transaction = trans

                Me.Adapter.DeleteCommand.Transaction = trans

                Me.Adapter.UpdateCommand.Transaction = trans


            End If



            Me.Adapter.AcceptChangesDuringUpdate = False

            Me.Adapter.ContinueUpdateOnError = False

        End Sub
    End Class
End Namespace

