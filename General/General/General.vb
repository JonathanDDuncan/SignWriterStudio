Imports NUnit.Framework
Imports System.Xml
Imports System.IO
Imports System.Globalization
Imports System.Drawing.Imaging
Imports System.Collections.Specialized
Imports System.Text
Imports System.Net

#Const NUnitTest = True   ' NUnit Tests on
#Const AssertTest = True    ' Assertion rules on
Imports SignWriterStudio.General
Public Module All

    ''' <summary>
    ''' Converts Unix's epoch time to VB DateTime value
    ''' </summary>
    ''' <param name="epochValue">Epoch time (seconds)</param>
    ''' <returns>VB Date</returns>
    ''' <remarks></remarks>
    Public Function EpochToDateTime(ByVal epochValue As Integer) As Date
        '
        If EpochValue >= 0 Then
            Return CDate("1.1.1970 00:00:00").AddSeconds(EpochValue)
        Else
            Return CDate("1.1.1970 00:00:00")
        End If

    End Function
    ''' <summary>
    ''' Converts VB DateTime value to Unix's epoch time
    ''' </summary>
    ''' <param name="DateTimeValue">DateTime to convert</param>
    ''' <returns>Epoch time (seconds)</returns>
    ''' <remarks></remarks>
    Public Function DateTimeToEpoch(ByVal DateTimeValue As Date) As Integer
        '
        Try
            Return CInt(DateTimeValue.Subtract(CDate("1.1.1970 00:00:00")).TotalSeconds)
        Catch ex As System.OverflowException
            General.LogError(ex, "DateTimeToEpoch error")
            Return -1
        End Try

    End Function
    Public Function VbCrLf() As String
        Return Microsoft.VisualBasic.Strings.ChrW(13) & Microsoft.VisualBasic.Strings.ChrW(10)
    End Function

    Public Function Format(ByVal expression As Object, Optional ByVal style As String = "") As String
        Return Microsoft.VisualBasic.Strings.Format(Expression, Style)
    End Function
    Public Function InputBox(ByVal prompt As String, Optional ByVal title As String = "", Optional ByVal defaultResponse As String = "", Optional ByVal XPos As Integer = -1, Optional ByVal YPos As Integer = -1) As String
        Return Microsoft.VisualBasic.Interaction.InputBox(Prompt, Title, DefaultResponse, XPos, YPos)
    End Function

    Public Function IsDbNull(ByVal value As Object) As Boolean
        Return Convert.IsDBNull(value)
    End Function


    'Function
    ''' <summary>
    ''' Function NZ returns replacement Object if tested object is DbNull or Nothing
    ''' </summary>
    Function NZ(ByVal tested As Object, ByVal replacement As Object) As Object
        Dim returnValue As Object
        'Require Test parameters
#If AssertTest Then
        'If replacement Is Nothing Then
        '    Throw New AssertionException("Replacement object is Nothing.")
        'End If
#End If

        If IsDBNull(tested) OrElse tested Is Nothing Then
            returnValue = replacement
        Else
            returnValue = tested
        End If


        'Ensure Test return value
#If AssertTest Then
        'If returnValue Is Nothing Then
        '    Throw New AssertionException("NZ return value is Nothing")
        'End If
#End If
        Return returnValue
    End Function

    'Function
    ''' <summary>
    ''' Function ByteArraytoImage transforms a ByteArray to an Image
    ''' </summary>
    Public Function ByteArraytoImage(ByVal buffer() As Byte) As Image
        'Require Test parameters
        Dim img As Image = Nothing
#If AssertTest Then
        If buffer Is Nothing Then
            Return Nothing
        End If
#End If

        Dim ms As New MemoryStream(buffer)
        If ms.Length > 0 Then
            Try
                   img = Image.FromStream(ms)
            Catch ex As Exception
                LogError (ex,"Exception")
            End Try
         
            Return img
        Else
            Return Nothing
        End If
    End Function
    'Function
    ''' <summary>
    ''' Function NulltoByteArray description
    ''' </summary>
    Function NulltoByteArray(ByVal nullObject As Byte()) As Byte()
        If IsDBNull(NullObject) Or NullObject Is Nothing Then
            Return Nothing 'type byte()
        Else
            Return NullObject
        End If
    End Function


    'Function
    ''' <summary>
    ''' Function ImageToByteArray description
    ''' </summary>
    Function ImageToByteArray(ByVal img As Image, ByVal format As Imaging.ImageFormat) As Byte()
        'Require Test parameters
#If AssertTest Then
        If img Is Nothing Then
            Throw New AssertionException("Provide an image to ImagetoByteArray")
        End If
        If format Is Nothing Then
            Throw New AssertionException("Provide a valid format to ImagetoByteArray")
        End If
#End If

        Dim ms As New IO.MemoryStream
        If img IsNot Nothing Then
            img.Save(ms, format)
        End If

        'Ensure Test return value
#If AssertTest Then
        If ms.Length <= 1 Then
            Throw New AssertionException("Byte array did not convert to Image.")
        End If
#End If
        Return ms.ToArray()
    End Function
    Function ImageToDataUri(ByVal img As Image, ByVal format As Imaging.ImageFormat) As String
        'Require Test parameters
#If AssertTest Then
        If img Is Nothing Then
            Return String.Empty
        End If
        If format Is Nothing Then
            Throw New AssertionException("Provide a valid format to ImagetoByteArray")
        End If
#End If

        Dim ms As New IO.MemoryStream
        If img IsNot Nothing Then
            img.Save(ms, format)
        End If

        'Ensure Test return value
#If AssertTest Then
        If ms.Length <= 1 Then
            Throw New AssertionException("Byte array did not convert to Image.")
        End If
#End If
        Return "data:image/png;base64," & Convert.ToBase64String(ms.ToArray())
    End Function
    Public Function ImageToBase64(ByVal image As Image) As String
        If image IsNot Nothing Then
            Return Convert.ToBase64String(ImageToByteArray(image))
        Else
            Return String.Empty
        End If
    End Function
    Public Function Base64ToImage(ByVal base64 As String) As Image
        If Not base64 = String.Empty Then
            Dim img As Image
            Dim dataStream As New MemoryStream(Convert.FromBase64String(base64))
            img = Image.FromStream(dataStream)
            dataStream.Close()
            Return img
        Else
            Return Nothing
        End If
    End Function

    Function ImageToByteArray(ByVal img As Image) As Byte()
        Return ImageToByteArray(img, ImageFormat.Png)
    End Function
    ''' <summary>
    ''' Function BinaryCount description
    ''' </summary>
    Function BinaryCount(ByRef Bin As Integer) As Integer
        'Require Test parameters
#If AssertTest Then
        If Bin > 65535 OrElse Bin < 0 Then
            Throw New AssertionException("Binary count is only for positive numbers smaller or equal 65535")
        End If
#End If

        Dim Sum As Integer
        Dim Eat As Integer = Bin
        Dim Bit As Integer
        For I As Integer = 1 To 16
            Bit = Eat And 1
            Sum += Bit
            Eat = Eat >> 1
        Next


        'Ensure Test return value
#If AssertTest Then
        If Sum < 0 Then
            Throw New AssertionException("Sum of BinaryCount is incorrect")
        End If
#End If
        Return Sum
    End Function
    ''' <summary>
    ''' Function CheckSWId checks if SWId is a valid format for a symbol Id
    ''' </summary>
    Public Function CheckId(ByVal SWId As String) As Boolean
        If SWId IsNot Nothing AndAlso SWId IsNot String.Empty AndAlso SWId.Length = 18 AndAlso System.Text.RegularExpressions.Regex.IsMatch(SWId, "\d\d-\d\d-\d\d\d-\d\d-\d\d-\d\d") Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function URLEncode(str As String) As String
        Return System.Web.HttpUtility.UrlEncode(str)
    End Function
    Public Function URLDecode(str As String) As String
        Return System.Web.HttpUtility.UrlDecode(str)
    End Function
    Public Function HtmlEncode(str As String) As String
        Return System.Web.HttpUtility.HtmlEncode(str)
    End Function
    Public Function HtmlDecode(str As String) As String
        Return System.Web.HttpUtility.HtmlDecode(str)
    End Function

    Public Function PrepareParamInfo(ByVal pColl As NameValueCollection) As String
        Dim paramInfo As New StringBuilder
        ' Iterate through the collection and add
        ' each key to the string variable.
        'name1=value1&name2=value2
        Dim i As Integer
        For i = 0 To pColl.Count - 1
            If i = 0 Then
                paramInfo.Append(pColl.GetKey(i))
                paramInfo.Append("=")
            Else
                paramInfo.Append("&")
                paramInfo.Append(pColl.GetKey(i))
                paramInfo.Append("=")
            End If

            ' Create a string array that contains
            ' the values associated with each key.
            Dim pValues() As String = pColl.GetValues(i)

            ' Iterate through the array and add
            ' each value to the string variable.
            For Each value In pValues
                paramInfo.Append(value)
            Next
        Next i
        Return paramInfo.ToString
    End Function
    Public Function HttpPost(uri As String, parameters As String) As String
        ' parameters: name1=value1&name2=value2	
        Dim webRequest1 As WebRequest = WebRequest.Create(uri)
        webRequest1.ContentType = "application/x-www-form-urlencoded"
        'webRequest1.ContentType = "text/xml"
        webRequest1.Method = "POST"

        'Dim bytes As Byte() = Encoding.ASCII.GetBytes(parameters)
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(parameters)
        Dim os As Stream = Nothing
        Try
            ' send the Post
            webRequest1.ContentLength = bytes.Length
            'Count bytes to send
            os = webRequest1.GetRequestStream()
            'Send it
            os.Write(bytes, 0, bytes.Length)
            Try
                ' get the response
                Dim webResponse As WebResponse = webRequest1.GetResponse()
                If webResponse Is Nothing Then
                    Return Nothing
                End If
                Dim sr As New StreamReader(webResponse.GetResponseStream())
                Return sr.ReadToEnd().Trim()
            Catch ex As WebException
                General.LogError(ex, "HTTPPost error")
                Console.WriteLine(ex.Message)
                Throw
            End Try
        Catch ex As WebException
            General.LogError(ex, "HTTPPost error")
            Console.WriteLine(ex.Message)
            Throw
        Finally
            If os IsNot Nothing Then
                os.Close()
            End If
        End Try
        Return Nothing
    End Function ' end HttpPost 

    Public Function SendPost(ByVal remoteUrl As String, Params As NameValueCollection) As String
        Dim ParamInfo As String = PrepareParamInfo(Params)
        Dim postResult As String = HttpPost(remoteUrl, ParamInfo)
        Return postResult
    End Function
    Private Function GetSubstring(str As String, Fromstr As String, Tostr As String) As String
        Dim strStart As Integer = str.IndexOf(Fromstr)
        Dim strStop As Integer = str.IndexOf(Tostr)
        If strStart >= 0 AndAlso strStop >= 0 Then
            Return str.Substring(strStart + Fromstr.Length + 1, strStop - strStart - Tostr.Length + 1)
        Else
            Return String.Empty
        End If
    End Function
    Private Function GetSubstringInclusive(str As String, Fromstr As String, Tostr As String) As String
        Dim strStart As Integer = str.IndexOf(Fromstr)
        Dim strStop As Integer = str.IndexOf(Tostr)
        If strStart >= 0 AndAlso strStop >= 0 Then
            Return str.Substring(strStart, strStop - strStart + Tostr.Length)
        Else
            Return String.Empty
        End If
    End Function
    Public Function GetTagValue(xmltext As String, Tag As String) As String
        Return GetSubstring(xmltext, "<" & Tag, "</" & Tag & ">")
    End Function
    Public Function GetTag(xmltext As String, Tag As String) As String
        Return GetSubstringInclusive(xmltext, "<" & Tag, "</" & Tag & ">")
    End Function
    Public Sub LogError(ex As Exception, Desc As String)
        My.Application.Log.WriteException(ex, TraceEventType.Error, Desc & " Error:" & ex.Message & " StackTrace:" & ex.StackTrace, 0)
    End Sub

End Module
Public Class RadioButtonFull
    Inherits RadioButton

    Protected Overrides Function IsInputKey( _
     ByVal keyData As System.Windows.Forms.Keys) As Boolean

        If keyData = Keys.Up OrElse keyData = Keys.Down OrElse keyData = Keys.Left OrElse keyData = Keys.Right OrElse _
         keyData = (Keys.Shift Or Keys.Up) OrElse keyData = (Keys.Shift Or Keys.Up) OrElse keyData = (Keys.Shift Or Keys.Down) OrElse keyData = (Keys.Shift Or Keys.Left) OrElse keyData = (Keys.Shift Or Keys.Right) Then

            Return True 'False 'true
        Else
            Return MyBase.IsInputKey(keyData)
        End If

    End Function

    Protected Overrides Sub OnKeyDown( _
     ByVal e As System.Windows.Forms.KeyEventArgs)

        'If Not (kevent.KeyData = Keys.Up OrElse kevent.KeyData = Keys.Down OrElse kevent.KeyData = Keys.Left OrElse kevent.KeyData = Keys.Right) Then
        '    Me.SelectedText = "    "
        'Else
        MyBase.OnKeyDown(e)
        'End If

    End Sub

End Class
'Class
''' <summary>
''' Class Paths description
''' </summary>
Public NotInheritable Class Paths
    'Function
    ''' <summary>
    ''' Function AllUserData description
    ''' </summary>
    Public Shared Function AllUsersData() As String

        Static path As String
        If path Is Nothing Then
            path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)

            If Not My.Computer.FileSystem.DirectoryExists(IO.Path.Combine(path, "SignWriter Studio")) Then
                My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(path, "SignWriter Studio"))
            End If
            path = IO.Path.Combine(path, "SignWriter Studio")
        End If



        'Ensure Test return value

#If AssertTest Then
        If path Is Nothing OrElse path.Length = 0 OrElse path = String.Empty Then
            Throw New AssertionException("AllUsersData not returning path.")
        End If
#End If
        Return path
    End Function

    Public Shared Function ApplicationPath() As String
        Return My.Application.Info.DirectoryPath
    End Function
    Public Shared Function Join(ByVal path1 As String, ByVal path2 As String) As String
        Return IO.Path.Combine(path1, path2)
    End Function
    Public Shared Function FileExists(ByVal filename As String) As Boolean
        Return System.IO.File.Exists(filename)
    End Function
    Public Shared Sub Copy(ByVal path1 As String, ByVal path2 As String)
        IO.File.Copy(path1, path2)
    End Sub

End Class
'Class
''' <summary>
''' Class Undo description
''' </summary>
Public NotInheritable Class Undo(Of TItem)
    Private undoStack As New Stack(Of TItem)
    Private redoStack As New Stack(Of TItem)

    ''' <summary>
    ''' Function Undo description
    ''' </summary>
    Public Function Undo(ByVal currentItem As TItem) As TItem
        If currentItem IsNot Nothing Then
            If Me.undoStack.Count > 0 Then
                redoStack.Push(currentItem)
                Return undoStack.Pop
            Else
                Return Nothing
            End If
        End If
    End Function




    Public Function Redo(ByVal currentItem As TItem) As TItem
        If Me.redoStack.Count > 0 Then
            Me.undoStack.Push(currentItem)
            Return redoStack.Pop
        Else
            Return Nothing
        End If
    End Function
    Public Sub Add(ByVal addItem As TItem)
        Me.undoStack.Push(addItem)
        Me.redoStack.Clear()
    End Sub
    Public Sub Clear()
        Me.undoStack.Clear()
        Me.redoStack.Clear()
    End Sub
End Class

'Class
''' <summary>
''' Class SignBounds description
''' </summary>
Public Class SignBounds
    Const Edge As Integer = 1920
    Private _top As Integer = Edge
    Public ReadOnly Property Top() As Integer
        Get
            Return _top
        End Get
    End Property
    Private _bottom As Integer = -Edge
    Public ReadOnly Property Bottom() As Integer
        Get
            Return _bottom
        End Get
    End Property
    Private _right As Integer = -Edge
    Public ReadOnly Property Right() As Integer
        Get
            Return _right
        End Get
    End Property
    Private _left As Integer = Edge
    Public ReadOnly Property Left() As Integer
        Get
            Return _left
        End Get
    End Property
    Private _width As Integer
    Public ReadOnly Property Width() As Integer
        Get
            Return _width
        End Get
    End Property
    Private _height As Integer
    Public ReadOnly Property Height() As Integer
        Get
            Return _height
        End Get
    End Property

    Public Sub Update(ByVal left As Integer, ByVal top As Integer, ByVal width As Integer, ByVal height As Integer)
        If top < _top Then
            _top = top
        End If
        If top + height > _bottom Then
            _bottom = top + height
        End If
        If left < _left Then
            _left = left
        End If
        If left + width > _right Then
            _right = left + width
        End If
        _width = _right - _left
        _height = _bottom - _top
    End Sub


End Class



Public Class SerializeObjects
    'Friend WithEvents monitor As EQATEC.Analytics.Monitor.IAnalyticsMonitor = EQATEC.Analytics.Monitor.AnalyticsMonitorFactory.Create("7A55FE8188FD4072B11C3EA5D30EB7F9")

    Public Shared Function SerializeObject(ByVal obj As Object, ByVal objType As Type, ByVal extraTypes As Type()) As XmlDocument
        Dim myWriter As IO.StringWriter
        Dim xmlDoc As New System.Xml.XmlDocument
        Try
            Dim myObject As Object = obj
            ' Insert code to set properties and fields of the object.
            'Dim mySerializer As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(GetType(CompInfo))
            Dim mySerializer As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(objType, extraTypes)

            Dim str As New System.Text.StringBuilder

            myWriter = New IO.StringWriter(str, CultureInfo.InvariantCulture)
            mySerializer.Serialize(myWriter, myObject)

            xmlDoc.PreserveWhitespace = True
            xmlDoc.LoadXml(str.ToString)
        Catch ex As XmlException
            'monitor.TrackException(ex, _
            '                  TraceEventType.Error.ToString, _
            '                  "Cannot serialize object " & ex.GetType().Name & " Error:" & ex.Message)
            General.LogError(ex, "Cannot serialize object " & ex.GetType().Name)
            'My.Application.Log.WriteException(ex, _
            '                  TraceEventType.Error, _
            '                  "Cannot serialize object " & ex.GetType().Name & " Error:" & ex.Message)
            Dim MBO As MessageBoxOptions = MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign
            MessageBox.Show("Cannot serialize object " & ex.GetType().Name & " Error:" & ex.Message, "Could not save", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBO, False)
        End Try
        Return xmlDoc
    End Function
    Public Shared Function SerializeObject(ByVal obj As Object, ByVal objType As Type) As XmlDocument
        Dim myWriter As IO.StringWriter
        Dim xmlDoc As New System.Xml.XmlDocument
        Try
            Dim myObject As Object = obj
            ' Insert code to set properties and fields of the object.
            'Dim mySerializer As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(GetType(CompInfo))
            Dim mySerializer As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(objType)

            Dim str As New System.Text.StringBuilder

            myWriter = New IO.StringWriter(str, CultureInfo.InvariantCulture)
            mySerializer.Serialize(myWriter, myObject)

            xmlDoc.PreserveWhitespace = True
            xmlDoc.LoadXml(str.ToString)
        Catch ex As XmlException
            'monitor.TrackException(ex, _
            '                  TraceEventType.Error.ToString, _
            '                  "Cannot serialize object " & ex.GetType().Name & " Error:" & ex.Message)
            General.LogError(ex, "Cannot serialize object " & ex.GetType().Name)
            'My.Application.Log.WriteException(ex, _
            '                  TraceEventType.Error, _
            '                  "Cannot serialize object " & ex.GetType().Name & " Error:" & ex.Message)
            Dim MBO As MessageBoxOptions = MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign
            MessageBox.Show("Cannot serialize object " & ex.GetType().Name & " Error:" & ex.Message, "Could not serialize", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBO, False)
        End Try
        Return xmlDoc
    End Function
    Public Shared Function SerializeObject(ByVal obj As Object, ByVal objType As Type, ByVal filename As String)
        Dim myWriter As IO.StringWriter
        Dim xmlDoc As New System.Xml.XmlDocument
        Try
            Dim myObject As Object = obj
            ' Insert code to set properties and fields of the object.
            'Dim mySerializer As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(GetType(CompInfo))
            Dim mySerializer As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(objType)

            Dim str As New System.Text.StringBuilder

            myWriter = New IO.StringWriter(str, CultureInfo.InvariantCulture)
            mySerializer.Serialize(myWriter, myObject)

            xmlDoc.PreserveWhitespace = True
            xmlDoc.Save(filename)
            '.LoadXml(str.ToString)
        Catch ex As XmlException
            'monitor.TrackException(ex, _
            '                  TraceEventType.Error.ToString, _
            '                  "Cannot serialize object " & ex.GetType().Name & " Error:" & ex.Message)
            General.LogError(ex, "Cannot serialize object " & ex.GetType().Name)
            'My.Application.Log.WriteException(ex, _
            '                  TraceEventType.Error, _
            '                  "Cannot serialize object " & ex.GetType().Name & " Error:" & ex.Message)
            Dim MBO As MessageBoxOptions = MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign
            MessageBox.Show("Cannot serialize object " & ex.GetType().Name & " Error:" & ex.Message, "Could not serialize", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBO, False)
        End Try
        Return xmlDoc
    End Function
    Public Shared Function DESerializeObject(ByVal xmlDoc As XmlDocument, ByVal objType As Type) As Object
        If xmlDoc Is Nothing Then
            Throw New ArgumentNullException("xmlDoc")
        End If

        If objType Is Nothing Then
            Throw New ArgumentNullException("objType")
        End If



        Dim obj As Object
        Try
            Dim myReader As TextReader
            ' Construct an instance of the XmlSerializer with the type
            ' of object that is being deserialized.
            Dim mySerializer As Serialization.XmlSerializer = New Serialization.XmlSerializer(objType)
            ' To read the file, create a FileStream.
            myReader = New IO.StringReader(xmlDoc.OuterXml)
            ' Call the Deserialize method and cast to the object type.
            xmlDoc.ToString()
            obj = mySerializer.Deserialize(myReader)
            Return obj
        Catch ex As XmlException
            'monitor.TrackException(ex, _
            '                  TraceEventType.Error.ToString, _
            '                  "Cannot deserialze object.  Error: " & ex.Message, "Could not open ")
            General.LogError(ex, "Cannot deserialize object " & ex.GetType().Name)
            'My.Application.Log.WriteException(ex, _
            '                  TraceEventType.Error, _
            '                  "Cannot deserialze object.  Error: " & ex.Message, 0)
            Dim MBO As MessageBoxOptions = MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign
            MessageBox.Show("Cannot deserialze object " & ex.GetType().Name & " Error:" & ex.Message, "Could not deserialize", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBO, False)
            Return Nothing
        End Try


    End Function

End Class
