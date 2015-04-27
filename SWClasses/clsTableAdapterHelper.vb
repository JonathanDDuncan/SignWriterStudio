Imports System

Imports System.Data
Imports System.Reflection
Imports System.Data.SQLite

'Credit to Ryan Whitaker's Blog at weblogs.asp.net/.../441529.aspx

'Converted to VB by Martin Worger 5th July 2007

'Incorporates Roberto F's suggestion

Public Class clsTableAdapterHelper

    Public Shared Function BeginTransaction(ByVal MyTableAdapter As Object) As SQLiteTransaction

        Return BeginTransaction(MyTableAdapter, IsolationLevel.ReadUncommitted)

    End Function

    Public Shared Function BeginTransaction(ByVal MyTableAdapter As Object, ByVal MyIsolationLevel As IsolationLevel) As SQLiteTransaction

        Dim TAType As Type = MyTableAdapter.GetType

        Dim Conn As SQLiteConnection = GetConnection(MyTableAdapter)

        If Conn.State = ConnectionState.Closed Then

            Conn.Open()

        End If

        Dim Tran As SQLite.SQLiteTransaction = Conn.BeginTransaction(MyIsolationLevel)

        SetTransaction(MyTableAdapter, Tran)

        Return Tran

    End Function

    Public Shared Sub SetTransaction(ByVal MyTableAdapter As Object, ByVal MyTransaction As SQLiteTransaction)

        Dim MyType As Type = MyTableAdapter.GetType

        'Original Ryan method

        'Dim CommandsProperty As PropertyInfo = MyType.GetProperty("CommandCollection", BindingFlags.NonPublic Or BindingFlags.Instance)

        'Dim Commands() As SqlCommand = CType(CommandsProperty.GetValue(MyTableAdapter, Nothing), SqlCommand())

        'For Each Command As SqlCommand In Commands

        '    If Command.CommandText <> "" Then

        '        Command.Transaction = MyTransaction

        '    End If

        'Next Command

        'Roberto F's alternative suggestion...

        Dim AdapterProperty As PropertyInfo = MyType.GetProperty("Adapter", BindingFlags.NonPublic Or BindingFlags.Instance)

        Dim MyDataAdapter As SQLiteDataAdapter = CType(AdapterProperty.GetValue(MyTableAdapter, Nothing), SQLiteDataAdapter)

        With MyDataAdapter

            If Not .DeleteCommand Is Nothing Then .DeleteCommand.Transaction = MyTransaction

            If Not .InsertCommand Is Nothing Then .InsertCommand.Transaction = MyTransaction

            If Not .UpdateCommand Is Nothing Then .UpdateCommand.Transaction = MyTransaction

        End With

        SetConnection(MyTableAdapter, MyTransaction.Connection)

    End Sub

    Private Shared Function GetConnection(ByVal MyTableAdapter As Object) As SQLiteConnection

        Dim MyType As Type = MyTableAdapter.GetType

        Dim ConnProperty As PropertyInfo = MyType.GetProperty("Connection", BindingFlags.NonPublic Or BindingFlags.Instance)

        Dim Conn As SQLiteConnection = CType(ConnProperty.GetValue(MyTableAdapter, Nothing), SQLiteConnection)

        Return Conn

    End Function

    Private Shared Sub SetConnection(ByVal MyTableAdapter As Object, ByVal MyConnection As SQLiteConnection)

        Dim MyType As Type = MyTableAdapter.GetType

        Dim ConnProperty As PropertyInfo = MyType.GetProperty("Connection", BindingFlags.NonPublic Or BindingFlags.Instance)

        ConnProperty.SetValue(MyTableAdapter, MyConnection, Nothing)

    End Sub

End Class