Imports System
Imports System.Security
Imports System.Collections
Imports Microsoft.Win32

'The Org.Mentalis.Utilities namespace implements several useful utilities that are missing from the standard .NET framework.
Namespace Org.Mentalis.Utilities
	'/// <summary>List of commands.</summary>
	Friend Structure CommandList
		'/// <summary>
		'/// Holds the names of the commands.
		'/// </summary>
		Public Captions As ArrayList
		'/// <summary>
		'/// Holds the commands.
		'/// </summary>
		Public Commands As ArrayList
	End Structure
	'/// <summary>Properties of the file association.</summary>
	Friend Structure FileType
		'/// <summary>
		'/// Holds the command names and the commands.
		'/// </summary>
		Public Commands As CommandList
		'/// <summary>
		'/// Holds the extension of the file type.
		'/// </summary>
		Public Extension As String
		'/// <summary>
		'/// Holds the proper name of the file type.
		'/// </summary>
		Public ProperName As String
		'/// <summary>
		'/// Holds the full name of the file type.
		'/// </summary>
		Public FullName As String
		'/// <summary>
		'/// Holds the name of the content type of the file type.
		'/// </summary>
		Public ContentType As String
		'/// <summary>
		'/// Holds the path to the resource with the icon of this file type.
		'/// </summary>
		Public IconPath As String
		'/// <summary>
		'/// Holds the icon index in the resource file.
		'/// </summary>
		Public IconIndex As Short
	End Structure
	'/// <summary>Creates file associations for your programs.</summary>
	'/// <example>The following example creates a file association for the type XYZ with a non-existent program.
	'/// <br></br><br>VB.NET code</br>
	'/// <code>
	'/// Dim FA as New FileAssociation
	'/// FA.Extension = "xyz"
	'/// FA.ContentType = "application/myprogram"
	'/// FA.FullName = "My XYZ Files!"
	'/// FA.ProperName = "XYZ File"
	'/// FA.AddCommand("open", "C:\mydir\myprog.exe %1")
	'/// FA.Create
	'/// </code>
	'/// <br>C# code</br>
	'/// <code>
	'/// FileAssociation FA = new FileAssociation();
	'/// FA.Extension = "xyz";
	'/// FA.ContentType = "application/myprogram";
	'/// FA.FullName = "My XYZ Files!";
	'/// FA.ProperName = "XYZ File";
	'/// FA.AddCommand("open", "C:\\mydir\\myprog.exe %1");
	'/// FA.Create();
	'/// </code>
	'/// </example>
	Public Class FileAssociation
		'/// <summary>Initializes an instance of the FileAssociation class.</summary>
		Public Sub New()
			FileInfo = New FileType()
			FileInfo.Commands.Captions = New ArrayList()
			FileInfo.Commands.Commands = New ArrayList()
		End Sub
		'/// <summary>Gets or sets the proper name of the file type.</summary>
		'/// <value>A String representing the proper name of the file type.</value>
		Public Property ProperName() As String
			Get
				Return FileInfo.ProperName
			End Get
			Set(ByVal Value As String)
				FileInfo.ProperName = Value
			End Set
		End Property
		'/// <summary>Gets or sets the full name of the file type.</summary>
		'/// <value>A String representing the full name of the file type.</value>
		Public Property FullName() As String
			Get
				Return FileInfo.FullName
			End Get
			Set(ByVal Value As String)
				FileInfo.FullName = Value
			End Set
		End Property
		'/// <summary>Gets or sets the content type of the file type.</summary>
		'/// <value>A String representing the content type of the file type.</value>
		Public Property ContentType() As String
			Get
				Return FileInfo.ContentType
			End Get
			Set(ByVal Value As String)
				FileInfo.ContentType = Value
			End Set
		End Property
		'/// <summary>Gets or sets the extension of the file type.</summary>
		'/// <value>A String representing the extension of the file type.</value>
		'/// <remarks>If the extension doesn't start with a dot ("."), a dot is automatically added.</remarks>
		Public Property Extension() As String
			Get
				Return FileInfo.Extension
			End Get
			Set(ByVal Value As String)
				If (Value.Substring(0, 1) <> ".") Then Value = "." + Value
				FileInfo.Extension = Value
			End Set
		End Property
		'/// <summary>Gets or sets the index of the icon of the file type.</summary>
		'/// <value>A short representing the index of the icon of the file type.</value>
		Public Property IconIndex() As Short
			Get
				Return FileInfo.IconIndex
			End Get
			Set(ByVal Value As Short)
				FileInfo.IconIndex = Value
			End Set
		End Property
		'/// <summary>Gets or sets the path of the resource that contains the icon for the file type.</summary>
		'/// <value>A String representing the path of the resource that contains the icon for the file type.</value>
		'/// <remarks>This resource can be an executable or a DLL.</remarks>
		Public Property IconPath() As String
			Get
				Return FileInfo.IconPath
			End Get
			Set(ByVal Value As String)
				FileInfo.IconPath = Value
			End Set
		End Property
		'/// <summary>Adds a new command to the command list.</summary>
		'/// <param name="Caption">The name of the command.</param>
		'/// <param name="Command">The command to execute.</param>
		'/// <exceptions cref="ArgumentNullException">Caption -or- Command is null (VB.NET: Nothing).</exceptions>
		Public Sub AddCommand(ByVal Caption As String, ByVal Command As String)
			If (Caption Is Nothing OrElse Command Is Nothing) Then Throw New ArgumentNullException()
			FileInfo.Commands.Captions.Add(Caption)
			FileInfo.Commands.Commands.Add(Command)
		End Sub
		'/// <summary>Creates the file association.</summary>
		'/// <exceptions cref="ArgumentNullException">Extension -or- ProperName is null (VB.NET: Nothing).</exceptions>
		'/// <exceptions cref="ArgumentException">Extension -or- ProperName is empty.</exceptions>
		'/// <exceptions cref="SecurityException">The user does not have registry write access.</exceptions>
		Public Sub Create()
			' remove the extension to avoid incompatibilities [such as DDE links]
			Try
				Remove()
            Catch e As ArgumentException
                General.LogError(e, "the extension doesn't exist")
                ' the extension doesn't exist
			End Try
			' create the exception
			If (Extension = "" OrElse ProperName = "") Then Throw New ArgumentException()
			Dim cnt As Integer
			Try
                Dim RegKey As RegistryKey = Registry.ClassesRoot.CreateSubKey(Extension)
				RegKey.SetValue("", ProperName)
				If (Not ContentType Is Nothing AndAlso ContentType <> "") Then RegKey.SetValue("Content Type", ContentType)
				RegKey.Close()
				RegKey = Registry.ClassesRoot.CreateSubKey(ProperName)
				RegKey.SetValue("", FullName)
				RegKey.Close()
				If (IconPath <> "") Then
                    RegKey = Registry.ClassesRoot.CreateSubKey(ProperName + "\" + "DefaultIcon")
                    'MessageBox.Show(RegKey.ToString)
                    RegKey.SetValue("", IconPath) 
					RegKey.Close()
				End If
				For cnt = 0 To FileInfo.Commands.Captions.Count - 1
					RegKey = Registry.ClassesRoot.CreateSubKey(ProperName + "\" + "Shell" + "\" + CType(FileInfo.Commands.Captions(cnt), String))
					RegKey = RegKey.CreateSubKey("Command")
					RegKey.SetValue("", FileInfo.Commands.Commands(cnt))
					RegKey.Close()
				Next cnt
			Catch
				Throw New SecurityException()
            End Try
            SetExplorerFileExts(Me)
        End Sub
		'/// <summary>Removes the file association.</summary>
		'/// <exceptions cref="ArgumentNullException">Extension -or- ProperName is null (VB.NET: Nothing).</exceptions>
		'/// <exceptions cref="ArgumentException">Extension -or- ProperName is empty -or- the specified extension doesn't exist.</exceptions>
		'/// <exceptions cref="SecurityException">The user does not have registry delete access.</exceptions>
		Public Sub Remove()
			If (Extension Is Nothing OrElse ProperName Is Nothing) Then Throw New ArgumentNullException()
			If (Extension = "" OrElse ProperName = "") Then Throw New ArgumentException()
            Try
                Dim temp1 = Registry.ClassesRoot.OpenSubKey(Extension)
                If temp1 IsNot Nothing Then
                    Registry.ClassesRoot.DeleteSubKeyTree(Extension, False)
                End If
            Catch ex As Exception
                General.LogError(ex, "")
            End Try
            Try
                Dim temp2 = Registry.ClassesRoot.OpenSubKey(ProperName)
                If temp2 IsNot Nothing Then
                    Registry.ClassesRoot.DeleteSubKeyTree(ProperName, False)
                End If
            Catch ex As Exception
                General.LogError(ex, "")
            End Try
           
        End Sub
		'/// <summary>Holds the properties of the file type.</summary>
		Private FileInfo As FileType

        Private Sub SetExplorerFileExts(fileAssociation As FileAssociation)
            Try
                Dim SubKeyFileExts As String = "Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts"

                Dim temp1 = Registry.CurrentUser.OpenSubKey(SubKeyFileExts & "\" & fileAssociation.Extension)
                If temp1 IsNot Nothing Then
                    Registry.CurrentUser.DeleteSubKeyTree(Extension, False)
                End If

                '[HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.SWS]
                Dim RegKey As RegistryKey = Registry.CurrentUser.CreateSubKey(SubKeyFileExts & "\" & fileAssociation.Extension)
                'MessageBox.Show(SubKeyFileExts & "\" & fileAssociation.Extension)
                'MessageBox.Show(RegKey.Name)
                Dim OpenWithList = RegKey.CreateSubKey("OpenWithList")
                RegKey.Flush()
                '[HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.SWS\OpenWithList]
                '"a"="SignWriterStudio.exe"
                OpenWithList.SetValue("a", "SignWriterStudio.exe", RegistryValueKind.String)
                '"MRUList"="a"
                OpenWithList.SetValue("MRUList", "a", RegistryValueKind.String)
                '[HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.SWS\OpenWithProgids]
                OpenWithList.Flush()
                OpenWithList.Close()
                Dim OpenWithProgids = RegKey.CreateSubKey("OpenWithProgids")
                'MessageBox.Show(OpenWithProgids.Name)
                Dim val(0) As Byte
                val(0) = 0
                '"SWSFile"=hex(0):
                OpenWithProgids.SetValue(fileAssociation.ProperName, val, RegistryValueKind.Binary)
                OpenWithProgids.Flush()
                OpenWithProgids.Close()
                RegKey.Close()
            Catch ex As Exception
                General.LogError(ex, "SetExplorerFileExts failed for " & fileAssociation.Extension)
                MessageBox.Show("SetExplorerFileExts failed for " & fileAssociation.Extension)
            End Try


        End Sub

    End Class
End Namespace