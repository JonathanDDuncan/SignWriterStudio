Imports System.Data.SQLite

Namespace DictionaryDataSetTableAdapters
    Partial Public Class DictionaryTableAdapter
        Public Property PublicConnection() As SQLiteConnection
            Get
                Return (Connection)
            End Get
            Set(ByVal value As SQLiteConnection)
                Connection = value
            End Set
        End Property

        Public Sub AssignConnection(ByVal conn As SQLiteConnection,
                                    Optional ByVal trans As SQLiteTransaction = Nothing)
            Connection = conn

            If trans IsNot Nothing Then

                Adapter.InsertCommand.Transaction = trans

                Adapter.DeleteCommand.Transaction = trans

                Adapter.UpdateCommand.Transaction = trans


            End If


            Adapter.AcceptChangesDuringUpdate = False

            Adapter.ContinueUpdateOnError = False
        End Sub
    End Class
    Partial Public Class SignsbyGlossesUnilingualTableAdapter
        Public Property PublicConnection() As SQLiteConnection
            Get
                Return (Connection)
            End Get
            Set(ByVal value As SQLiteConnection)
                Connection = value
            End Set
        End Property

        Public Sub AssignConnection(ByVal conn As SQLiteConnection,
                                    Optional ByVal trans As SQLiteTransaction = Nothing)
            Connection = conn

            'This adapter doesn't have these commands
            If trans IsNot Nothing Then


                '    Adapter.InsertCommand.Transaction = trans

                '    Adapter.DeleteCommand.Transaction = trans

                '    Adapter.UpdateCommand.Transaction = trans


            End If


            Adapter.AcceptChangesDuringUpdate = False

            Adapter.ContinueUpdateOnError = False
        End Sub
    End Class
    Partial Public Class DictionaryGlossTableAdapter
        Public Property PublicConnection() As SQLiteConnection
            Get
                Return (Connection)
            End Get
            Set(ByVal value As SQLiteConnection)
                Connection = value
            End Set
        End Property

        Public Sub AssignConnection(ByVal conn As SQLiteConnection,
                                    Optional ByVal trans As SQLiteTransaction = Nothing)
            Connection = conn

            If trans IsNot Nothing Then

                Adapter.InsertCommand.Transaction = trans

                Adapter.DeleteCommand.Transaction = trans

                Adapter.UpdateCommand.Transaction = trans


            End If


            Adapter.AcceptChangesDuringUpdate = False

            Adapter.ContinueUpdateOnError = False
        End Sub
    End Class

    Partial Public Class SignSymbolsTableAdapter
        Public Property PublicConnection() As SQLiteConnection
            Get
                Return (Connection)
            End Get
            Set(ByVal value As SQLiteConnection)
                Connection = value
            End Set
        End Property

        Public Sub AssignConnection(ByVal conn As SQLiteConnection,
                                    Optional ByVal trans As SQLiteTransaction = Nothing)
            Connection = conn

            If trans IsNot Nothing Then

                Adapter.InsertCommand.Transaction = trans

                Adapter.DeleteCommand.Transaction = trans

                Adapter.UpdateCommand.Transaction = trans


            End If


            Adapter.AcceptChangesDuringUpdate = False

            Adapter.ContinueUpdateOnError = False
        End Sub
    End Class

    Partial Public Class SignSequenceTableAdapter
        Public Property PublicConnection() As SQLiteConnection
            Get
                Return (Connection)
            End Get
            Set(ByVal value As SQLiteConnection)
                Connection = value
            End Set
        End Property

        Public Sub AssignConnection(ByVal conn As SQLiteConnection,
                                    Optional ByVal trans As SQLiteTransaction = Nothing)
            Connection = conn

            If trans IsNot Nothing Then

                Adapter.InsertCommand.Transaction = trans

                Adapter.DeleteCommand.Transaction = trans

                Adapter.UpdateCommand.Transaction = trans


            End If


            Adapter.AcceptChangesDuringUpdate = False

            Adapter.ContinueUpdateOnError = False
        End Sub
    End Class

    Partial Public Class PuddleTextTableAdapter
        Public Property PublicConnection() As SQLiteConnection
            Get
                Return (Connection)
            End Get
            Set(ByVal value As SQLiteConnection)
                Connection = value
            End Set
        End Property

        Public Sub AssignConnection(ByVal conn As SQLiteConnection,
                                    Optional ByVal trans As SQLiteTransaction = Nothing)
            Connection = conn

            If trans IsNot Nothing Then

                Adapter.InsertCommand.Transaction = trans

                Adapter.DeleteCommand.Transaction = trans

                Adapter.UpdateCommand.Transaction = trans


            End If


            Adapter.AcceptChangesDuringUpdate = False

            Adapter.ContinueUpdateOnError = False
        End Sub
    End Class

    Partial Public Class FrameTableAdapter
        Public Property PublicConnection() As SQLiteConnection
            Get
                Return (Connection)
            End Get
            Set(ByVal value As SQLiteConnection)
                Connection = value
            End Set
        End Property

        Public Sub AssignConnection(ByVal conn As SQLiteConnection,
                                    Optional ByVal trans As SQLiteTransaction = Nothing)
            Connection = conn

            If trans IsNot Nothing Then

                Adapter.InsertCommand.Transaction = trans

                Adapter.DeleteCommand.Transaction = trans

                Adapter.UpdateCommand.Transaction = trans


            End If


            Adapter.AcceptChangesDuringUpdate = False

            Adapter.ContinueUpdateOnError = False
        End Sub
    End Class
End Namespace

