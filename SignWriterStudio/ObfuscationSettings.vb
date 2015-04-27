Imports System
Imports System.Reflection
Imports System.Text
Imports System.IO
Imports System.Xml
Imports System.Security.Cryptography
Imports System.Security.Cryptography.Xml
Imports System.Management
Imports SignWriterStudio.SWClasses
Imports SignWriterStudio.General
Imports System.Windows.Forms
Imports SignWriterStudio.General.SerializeObjects
<Assembly: ObfuscateAssemblyAttribute(False, StripAfterObfuscation:=False)> 
<Assembly: Obfuscation(Feature:="encrypt symbol names with password SWS01Si02gnWrit03ing", Exclude:=False)> 
''<Assembly: Obfuscation(Feature:="code control flow obfuscation", Exclude:=False)> 

Friend Class ClientApp
    Inherits ClientAppSimple
    Private Node1 As String = "SOFTWARE"
    Private Node2 As String = "SWS"
    Private Node3 As String = "V1"
    Private Node4 As String = "reg"
    Private About As About
    Private Today As Date = Now()
    Private AcercaDE As AcercaDE
    Private Function Check() As Boolean
        Dim CI As New SWClasses.CompInfo(LocalFriNode3)
        Dim CIToCompFile As New SWClasses.CompInfo()
        Dim CIToCompReg As New SWClasses.CompInfo()
        Dim CIXMLFile As New XmlDocument
        Dim CIXMLReg As New XmlDocument
        Dim Ret As Boolean = True

        If About Is Nothing Then
            About = New About() 'AcercaDE)
        End If
        If ExistCompInfoFile() Then
            GetCompInfoFile(CIXMLFile)
            If Not Verify(CIXMLFile, CI) Then
                If ShowAbout Then
                    About.ShowDialog()
                End If
                'NotContinue()
                Application.Exit()
                Ret = False
                Exit Function
            Else
                CIDeserialize(CIXMLFile, CIToCompFile)
            End If
            'Registry Missing replace
            If Not ExistCompInfoReg() Then

                SaveCompInfoReg(SignWriterStudio.General.SerializeObjects.SerializeObject(CIToCompFile, CIToCompFile.GetType), CIToCompFile.KeyName, SWClasses.MemoryEncrypt.ProtectData(LocalFriNode3))
                CIXMLReg = GetCompInfoReg()
                If Not Verify(CIXMLReg, CIToCompFile) Then
                    If ShowAbout Then
                        About.ShowDialog()
                    End If
                    'NotContinue()
                    Application.Exit()
                    Ret = False
                    Exit Function
                Else
                    CIDeserialize(CIXMLReg, CIToCompReg)
                End If
            Else
                CIXMLReg = GetCompInfoReg()

                If Not Verify(CIXMLReg, CIToCompFile) Then
                    If ShowAbout Then
                        About.ShowDialog()
                    End If
                    'NotContinue()
                    Application.Exit()
                    Ret = False
                    Exit Function
                Else
                    CIDeserialize(CIXMLReg, CIToCompReg)
                End If
            End If
            'File missing
        ElseIf ExistCompInfoReg() Then
            CIXMLReg = GetCompInfoReg()
            If Not Verify(CIXMLReg, CI) Then
                If ShowAbout Then
                    About.ShowDialog()
                End If
                'NotContinue()
                Application.Exit()
                Ret = False
                Exit Function
            Else
                CIDeserialize(CIXMLReg, CIToCompReg)
            End If
            'File missing replace
            If Not ExistCompInfoFile() Then
                SaveCompInfoFile(SerializeObject(CIToCompReg, CIToCompReg.GetType), CI.KeyName, MemoryEncrypt.ProtectData(LocalFriNode3))
                GetCompInfoFile(CIXMLFile)
                If Not Verify(CIXMLFile, CIToCompReg) Then
                    If ShowAbout Then
                        About.ShowDialog()
                    End If
                    'NotContinue()
                    Application.Exit()
                    Ret = False
                    Exit Function
                Else

                    CIDeserialize(CIXMLFile, CIToCompFile)
                End If
            Else
                GetCompInfoFile(CIXMLFile)

                If Not Verify(CIXMLFile, CIToCompReg) Then
                    If ShowAbout Then
                        About.ShowDialog()
                    End If
                    'NotContinue()
                    Application.Exit()
                    Ret = False
                    Exit Function
                Else
                    CIDeserialize(CIXMLFile, CIToCompFile)
                End If
            End If
        Else
            'Run for first time
            'SerializeObject(CI, CI.GetType).Save("Text.xml")
            SaveCompInfoFile(SerializeObjects.SerializeObject(CI, CI.GetType), CI.KeyName, MemoryEncrypt.ProtectData(LocalFriNode3))
            SaveCompInfoReg(SerializeObject(CI, CI.GetType), CI.KeyName, MemoryEncrypt.ProtectData(LocalFriNode3))
            GetCompInfoFile(CIXMLFile)
            CIXMLReg = GetCompInfoReg()


            If Not (Verify(CIXMLFile, CI) AndAlso Verify(CIXMLReg, CI)) Then
                If ShowAbout Then
                    About.ShowDialog()
                End If
                'NotContinue()
                Application.Exit()
                Ret = False
                Exit Function
            Else

                CIDeserialize(CIXMLFile, CIToCompFile)
                CIDeserialize(CIXMLReg, CIToCompReg)
            End If
            'Test
            Dim TEXTdoc As XmlDocument = SerializeObject(CI, CI.GetType)
            Dim Signeddoc As XmlDocument
            Dim result As Boolean
            Signeddoc = MemoryEncrypt.Sign(TEXTdoc, MemoryEncrypt.ProtectData("THISKEYNAME1"), MemoryEncrypt.ProtectData(LocalFriNode3))
            result = MemoryEncrypt.VerifyXmlSignature(Signeddoc, MemoryEncrypt.ProtectData("THISKEYNAME"), MemoryEncrypt.ProtectData(LocalFriNode3))
            result = Verify(Signeddoc, CI)
        End If
        If Not Compare(CI, CIToCompFile, CIToCompReg) Then
            If ShowAbout Then
                About.ShowDialog()
            End If
            'NotContinue()
            Application.Exit()
            Return False

        Else
            Return True
        End If
    End Function
    Friend Function Compare(ByVal CI As CompInfo, ByVal CIToCompFile As CompInfo, ByVal CIToCompReg As CompInfo) As Boolean
        Dim Ret As Boolean = True
        Dim DiasPrueba As Integer = (CInt(TrialPeriod) - Today.Subtract(CIToCompFile.InstalationDate).Days)
        If CIToCompFile.PassDue = True OrElse DiasPrueba < 0 Then
            DiasPrueba = 0
        End If

        AcercaDE.Prueba = DiasPrueba.ToString

        'TODO habilitate when no longer Beta
        If ExistLicenseFile() Then
            Dim LicFile As New XmlDocument
            GetLicenseFile(LicFile)
            ''Validate
            'If Not VerifyLicense(LicFile, LicenseFilename) Then
            '    'NotContinue()
            '    If ShowAbout Then
            '        About.ShowDialog()
            '    End If
            '    Application.Exit()
            '    Ret = False
            'Else
            Dim Lic1 As New License
            Lic1 = DESerializeObject(LicFile, Lic1.GetType)
            '    If Not (CompareComp3(Lic1.CompInfo, CI) AndAlso Lic1.Level = "BetaFull") Then
            '        'NotContinue()
            '        If ShowAbout Then
            '            About.ShowDialog()
            '        End If
            '        Application.Exit()
            '        Ret = False
            '    Else
            AcercaDE.Level = Lic1.Level
            AcercaDE.Activado = "Yes"
            AcercaDE.Autorizado = Lic1.Owner
            AcercaDE.Level = Lic1.Level
            AcercaDE.IsBeta = False
            'End If
            'Else

            'Check if File and reg Match
            If Not CompareComp2(CIToCompFile, CIToCompReg) Then
                'NotContinue()
                If ShowAbout Then
                    About.ShowDialog()
                End If
                Application.Exit()
                Ret = False
            ElseIf Not CIToCompFile.PassDue = CIToCompReg.PassDue Then
                'NotContinue()
                If ShowAbout Then
                    About.ShowDialog()
                End If
                Application.Exit()
                Ret = False
            End If

            'Check if same as Computer
            If Not CompareComp4(CI, CIToCompFile) Then
                'NotContinue()
                If ShowAbout Then
                    About.ShowDialog()
                End If
                Application.Exit()
                Ret = False
            End If

            'Check if same as Computer
            If Not CompareComp3(CI, CIToCompReg) Then
                'NotContinue()
                If ShowAbout Then
                    About.ShowDialog()
                End If
                Application.Exit()
                Ret = False
            End If
            'Look for License
            'Check if same Computer
            If Not CompareComp1(CI, CIToCompFile) Then
                'NotContinue()
                If ShowAbout Then
                    About.ShowDialog()
                End If
                Application.Exit()
                Ret = False
            ElseIf Not CompareComp2(CI, CIToCompReg) Then
                'NotContinue()
                If ShowAbout Then
                    About.ShowDialog()
                End If
                Application.Exit()
                Ret = False
            End If



            If (CIToCompFile.PassDue Or CIToCompReg.PassDue) Then 'Check for date temp.
                'MessageBox.Show("Your trial period has ended.")
                MessageBox.Show("This Beta version has expired.")
                If ShowAbout Then
                    About.ShowDialog()
                End If
                'NotContinue()
                Application.Exit()
                Ret = False

            Else
                'TODO normal check.  Below Beta Check 
                'If Today.Subtract(CIToCompFile.InstalationDate).Days > CInt(CompInfoV1) Then
                If Today > BetaEndDate Then
                    CIToCompFile.PassDue = True
                    'Save Change
                    SaveCompInfoFile(SerializeObject(CIToCompFile, CIToCompFile.GetType), CI.KeyName, MemoryEncrypt.ProtectData(LocalFriNode3))
                    SaveCompInfoReg(SerializeObject(CIToCompFile, CIToCompFile.GetType), CI.KeyName, MemoryEncrypt.ProtectData(LocalFriNode3))
                    'NotContinue()
                    'MessageBox.Show("Your trial period has ended.")
                    MessageBox.Show("This Beta version has expired.")
                    If ShowAbout Then
                        About.ShowDialog()
                    End If
                    Application.Exit()
                    Ret = False
                End If


            End If
        End If
        Return Ret
    End Function
    Friend Sub New(ByVal AcercaDE As AcercaDE)
        Me.AcercaDE = AcercaDE
        IntializeObj()

    End Sub
    Friend Sub New(ByVal ShowAbout As Boolean)
        Me.ShowAbout = ShowAbout
        IntializeObj()
    End Sub
    Private Sub IntializeObj()
        Me.BetaEndDate = Date.ParseExact("28/09/12", "dd/MM/yy", My.Application.Culture)
        Check()
    End Sub
    Private Function ExistLicenseFile() As Boolean
        Return Paths.FileExists(Paths.Join(Paths.AllUsersData, LicenseFilename))
    End Function

    Private Sub GetLicenseFile(ByRef XMLDoc As XmlDocument)
        Try
            XMLDoc.PreserveWhitespace = True
            XMLDoc.Load(Paths.Join(Paths.AllUsersData, LicenseFilename))
        Catch badfileEx As XmlException
            General.LogError(badfileEx, "Cannot open file.  Not a valid XML file.")
            'My.Application.Log.WriteException(badfileEx, _
            '                  TraceEventType.Error, _"Cannot open file.  Not a valid XML file."
            '                  )
            MessageBox.Show("Cannot open file.  Not a valid XML file.")
        Catch ex As Exception
            'monitor.TrackException(ex, _
            '                  TraceEventType.Error, _
            '                  "Cannot open file ")
            General.LogError(ex, "Cannot open model ")
            'My.Application.Log.WriteException(ex, _
            '                  TraceEventType.Error, _
            '                "Cannot open model ")
            MessageBox.Show("Cannot open file.")
        End Try
    End Sub
    Private Function ExistCompInfoFile() As Boolean
        'Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
        'If Not My.Computer.FileSystem.DirectoryExists(IO.Path.Combine(path, "SignWriter Studio")) Then
        '    My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(path, "SignWriter Studio"))
        'End If
        Return Paths.FileExists(IO.Path.Combine(Paths.AllUsersData, CompInfoFilename))
    End Function
    Private Function ExistCompInfoReg() As Boolean
        ''Dim basekey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Node1 & "\\" & Node2 & "\\" & Node3, True)
        Dim basekey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Node1 & "\\" & Node2 & "\\" & Node3, True)
        Dim XMLDoc As New XmlDocument
        If basekey IsNot Nothing Then
            If basekey.GetValue(Node4) Is Nothing Then
                Return False
            Else
                Return True
            End If
        End If
    End Function
    Private Sub GetCompInfoFile(ByRef XMLDoc As XmlDocument)
        Try
            XMLDoc.PreserveWhitespace = True
            XMLDoc.Load(Paths.Join(Paths.AllUsersData, CompInfoFilename))
        Catch badfileEx As XmlException
            General.LogError(badfileEx, "Cannot open file.  Not a valid XML file.")
            'My.Application.Log.WriteException(badfileEx, _
            '                  TraceEventType.Error, _
            '                  "Cannot open file.  Not a valid XML file.")
            MessageBox.Show("Cannot open file.  Not a valid XML file.")
        Catch ex As Exception
            'monitor.TrackException(ex, _
            '                  TraceEventType.Error, _
            '                  "Cannot open file ")
            General.LogError(ex, "Cannot open model ")
            'My.Application.Log.WriteException(ex, _
            '                  TraceEventType.Error, _
            '                  "Cannot open model ")
            MessageBox.Show("Cannot open file.")
        End Try
    End Sub

    Private Function CompareComp4(ByVal CI As CompInfo, ByVal CItoComp As CompInfo) As Boolean
        Return CompareComp1(CI, CItoComp)
    End Function
    Private Function Verify(ByVal CompInfoXML As XmlDocument, ByVal CompInfo As CompInfo) As Boolean
        'Console.WriteLine("Private Function Verify")
        'Console.WriteLine(CompInfoXML.OuterXml)
        'Console.WriteLine(MemoryEncrypt.UnProtectData(CompInfo.KeyName))
        'Console.WriteLine(LocalFriNode3)
        Return MemoryEncrypt.VerifyXmlSignature(CompInfoXML, CompInfo.KeyName, MemoryEncrypt.ProtectData(LocalFriNode3))
    End Function
    Private Function VerifyLicense(ByVal LicenseXML As XmlDocument, ByVal KeyName As String) As Boolean
        Return MemoryEncrypt.VerifyXmlSignature(LicenseXML, MemoryEncrypt.ProtectData(KeyName), MemoryEncrypt.ProtectData(ForeignFriNode3))
    End Function
    Private Sub LICDeserialize(ByVal LicXML As XmlDocument, ByRef Lic As License)
        Lic = DESerializeObject(LicXML, Lic.GetType)

    End Sub
    Private Sub CIDeserialize(ByVal CompInfoXML As XmlDocument, ByRef CompInfo As CompInfo)
        CompInfo = DESerializeObject(CompInfoXML, CompInfo.GetType)
    End Sub

    Private Sub SaveCompInfoFile(ByVal XMLDoc As XmlDocument, ByVal PDKeyName As ProtData, ByVal PDPrivateKey As ProtData)
        XMLDoc = MemoryEncrypt.Sign(XMLDoc, PDKeyName, PDPrivateKey)
        XMLDoc.PreserveWhitespace = True
        XMLDoc.Save(Paths.Join(Paths.AllUsersData, CompInfoFilename))
    End Sub

    Private Sub SaveCompInfoReg(ByVal XMLDoc As XmlDocument, ByVal PDKeyName As ProtData, ByVal PDPrivateKey As ProtData)

        XMLDoc = MemoryEncrypt.Sign(XMLDoc, PDKeyName, PDPrivateKey)
        ''Dim regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Node1, True)
        Dim regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Node1, True)
        If regKey Is Nothing Then
            'Microsoft.Win32.Registry.CurrentUser.CreateSubKey(Node1)
            Microsoft.Win32.Registry.CurrentUser.CreateSubKey(Node1)
        End If

        regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Node1 & "\\" & Node2, True)
        If regKey Is Nothing Then
            regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Node1, True)
            regKey.CreateSubKey(Node2)
        End If

        regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Node1 & "\\" & Node2 & "\\" & Node3, True)
        If regKey Is Nothing Then
            regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Node1 & "\\" & Node2, True)
            regKey.CreateSubKey(Node3)
            regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Node1 & "\\" & Node2 & "\\" & Node3, True)
        End If

        Dim XMLString As New StringBuilder

        Dim Writer As New StringWriter(XMLString)
        Writer.Write(XMLDoc)

        regKey.SetValue(Node4, XMLDoc.OuterXml)

    End Sub

    Private Function CompareComp2(ByVal CI As CompInfo, ByVal CItoComp As CompInfo) As Boolean
        If Not CI.BiosSN = CItoComp.BiosSN Then
            Return False
        ElseIf Not CI.HardDriveSize = CItoComp.HardDriveSize Then
            Return False
        ElseIf Not CI.InstalledMemory = CItoComp.InstalledMemory Then
            Return False
            'ElseIf Not CI.MACAddress = CItoComp.MACAddress Then
            '    Return False
        ElseIf Not CI.Manufacturer = CItoComp.Manufacturer Then
            Return False
        ElseIf Not CI.Model = CItoComp.Model Then
            Return False
        ElseIf Not CI.OSFullName = CItoComp.OSFullName Then
            Return False
        ElseIf Not CI.OSPlatform = CItoComp.OSPlatform Then
            Return False
        ElseIf Not CI.Processor = CItoComp.Processor Then
            Return False
        ElseIf Not CI.Processors = CItoComp.Processors Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function CompareComp3(ByVal CI As CompInfo, ByVal CItoComp As CompInfo) As Boolean
        Return CompareComp2(CI, CItoComp)
    End Function

    Private Sub NotContinue()

        If MessageBox.Show("Cannot open program.  Do you want to save the registration file o activate the product?", "Cannot load program", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            If MessageBox.Show("Do you want to save the registration file?", "Save registration", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                Guardarregistration()
            ElseIf MessageBox.Show("Do you wish to activate the product?", "Activate product", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                ActivarProducto()
            End If
        End If
    End Sub



End Class
Friend Class ClientAppSimple
    Protected ForeignFriNode3 As String = "<RSAKeyValue><Modulus>vRApUpxmD1Q2AfW+2A+wqvBdMSffELmqHmtT4u+0orC8Kyji9GcuzQ3Pv7TI3vAwdb/CTAsBYFbsHnBcStauiuC7UmXRjjz1jh9OkEy7QW7ShwLkE3KH9y1MWgzQ2QUqhMwDjaawxJ/AQ8t9F9fl7EW6GnpxRaxlE4zOn2p+h0s=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>"
    Protected LocalFriNode3 As String = "<RSAKeyValue><Modulus>xtwTmxhqv8q/kAMoWNdBtUCS+XYqFf6kj94rjyjTI/iB6fCx1h4HyT/Z3W0/t3dEJT0dXqEwHfyCG3VTA/IUMtJZ0OJogckoJlFNY2/EVbFyMkoPCC/nsESKH+RmMTkv4HbD4HDfeaNEhO2Aw+GlSKB0hmiqYUj7O0+H1BjXI7U=</Modulus><Exponent>AQAB</Exponent><P>43OfCwSSV2lOs0OWY4Kmsk6ix2m0H0V3YpE20497YPEfZpHOOJmtCxI1xumn1WGbrvp0v2K5DoW5e5k3ZuWsOQ==</P><Q>39HAF5DujZeogfBg4ifW+7kP9KXKnESWNoiFllklk8Qo3WpY91uPhtW6ij1JsuE0ifvZOCt2YJS/LwZ4M0orXQ==</Q><DP>qWRqhiIZetAKtKcZUXL0Asddo3Wtl7x8dQQA+P5avI/W+uSL2MtafGxLcKCDRf8zmtUcLYywlYgx40LwZ8mUwQ==</DP><DQ>QeRgZw+2C11gsJByFWKhOg5rkhzVH1hZ2Mgse+zW0T/ov/a1Jm2APbPibRxJ/C2s6AqLEqxI93oTJucCsCbfyQ==</DQ><InverseQ>cC51VrZC3wL9Ib5xLjGQ0ufpg9291m4ZfkFDJVJonggZjx806PrywYdRT4uUocbztVdFiUUkhwijgEl2ecfmZQ==</InverseQ><D>xaYjznl8UFhCAe9BgNuiMXaXHl82AVpxUZUukkGvagcriMt01i5O6tYNwqk9NSrTjnep404uNv+siYIZGsjXDNPCLDTOkgoyb/5rvjgyK6NpjJIkNAh2sKHZOOacOnH1r6R5IkXFCyFgdUzZcdrppmam645wyZOWc5/YI73GpkE=</D></RSAKeyValue>"
    Protected ShowAbout As Boolean = True
    Protected TrialPeriod As String = "30"
    Protected BetaEndDate As Date
    Protected LicenseFilename As String = "LicenceV" & Split(Application.ProductVersion, ".")(0) & ".dat"
    Protected CompInfoFilename As String = "CompInfoV" & Split(Application.ProductVersion, ".")(0) & ".dat"
    Private Node1 As String = "SOFTWARE"
    Private Node2 As String = "SWS"
    Private Node3 As String = "V" & Split(Application.ProductVersion, ".")(0)
    Private Node4 As String = "reg"
    'Friend WithEvents monitor As EQATEC.Analytics.Monitor.IAnalyticsMonitor = EQATEC.Analytics.Monitor.AnalyticsMonitorFactory.Create("7A55FE8188FD4072B11C3EA5D30EB7F9")
    Private acercade As AcercaDE
    Friend Function BetaED() As String
        Return BetaEndDate.ToLongDateString
    End Function
    Friend Sub New()
        IntializeObj()
    End Sub
    Friend Sub New(ByVal ShowAbout As Boolean)
        Me.ShowAbout = ShowAbout
        IntializeObj()
    End Sub
    Private Sub IntializeObj()
        Me.BetaEndDate = Date.ParseExact("28/09/12", "dd/MM/yy", My.Application.Culture)

    End Sub
    Friend Function CompareComp1(ByVal CI As CompInfo, ByVal CItoComp As CompInfo) As Boolean
        If Not CI.BiosSN = CItoComp.BiosSN Then
            Return False
        ElseIf Not CI.HardDriveSize = CItoComp.HardDriveSize Then
            Return False
        ElseIf Not CI.InstalledMemory = CItoComp.InstalledMemory Then
            Return False
            'ElseIf Not CI.MACAddress = CItoComp.MACAddress Then
            '    Return False
        ElseIf Not CI.Manufacturer = CItoComp.Manufacturer Then
            Return False
        ElseIf Not CI.Model = CItoComp.Model Then
            Return False
        ElseIf Not CI.OSFullName = CItoComp.OSFullName Then
            Return False
        ElseIf Not CI.OSPlatform = CItoComp.OSPlatform Then
            Return False
        ElseIf Not CI.Processor = CItoComp.Processor Then
            Return False
        ElseIf Not CI.Processors = CItoComp.Processors Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function ExistLicenseFile() As Boolean
        Return FileExists(Paths.Join(Paths.AllUsersData, LicenseFilename))
    End Function
    Private Function FileExists(ByVal Filename As String) As Boolean
        Return System.IO.File.Exists(Filename)
    End Function
    Private Sub GetLicenseFile(ByRef XMLDoc As XmlDocument)
        Try
            XMLDoc.PreserveWhitespace = True
            XMLDoc.Load(Paths.Join(Paths.AllUsersData, LicenseFilename))
        Catch badfileEx As XmlException

            My.Application.Log.WriteException(badfileEx, _
                              TraceEventType.Error, _
                              "Cannot open file.  Not a valid XML file.")
            MessageBox.Show("Cannot open file.  Not a valid XML file.")
        Catch ex As Exception
            'monitor.TrackException(ex, _
            '                  TraceEventType.Error, _
            '                  "Cannot open file ")
            General.LogError(ex, "Cannot open model ")
            'My.Application.Log.WriteException(ex, _
            '                  TraceEventType.Error, _
            '                  "Cannot open model ")
            MessageBox.Show("Cannot open file.")
        End Try
    End Sub
    Private Function ExistCompInfoFile() As Boolean
        Return FileExists(Paths.Join(Paths.AllUsersData, CompInfoFilename))
    End Function
    Private Function ExistCompInfoReg() As Boolean
        Dim basekey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Node1 & "\\" & Node2, True)
        Dim XMLDoc As New XmlDocument
        If basekey IsNot Nothing Then
            If basekey.GetValue(Node4) Is Nothing Then
                Return False
            Else
                Return True
            End If
        End If
    End Function
    Private Sub GetCompInfoFile(ByRef XMLDoc As XmlDocument)
        Try
            XMLDoc.PreserveWhitespace = True
            XMLDoc.Load(Paths.Join(Paths.AllUsersData, CompInfoFilename))
        Catch badfileEx As XmlException
            General.LogError(badfileEx, "Cannot open file.  Not a valid XML file.")
            'My.Application.Log.WriteException(badfileEx, _
            '                  TraceEventType.Error, _
            '                  "Cannot open file.  Not a valid XML file.")
            MessageBox.Show("Cannot open file.  Not a valid XML file.")
        Catch ex As Exception
            'monitor.TrackException(ex, _
            '                  TraceEventType.Error, _
            '                  "Cannot open file ")
            General.LogError(ex, "Cannot open model ")
            My.Application.Log.WriteException(ex, _
                              TraceEventType.Error, _
                              "Cannot open model ")
            MessageBox.Show("Cannot open file.")
        End Try
    End Sub

    Friend Function GetCompInfoReg() As XmlDocument
        Dim basekey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Node1 & "\\" & Node2 & "\\" & Node3, True)
        Dim XMLDoc As New XmlDocument
        XMLDoc.PreserveWhitespace = True
        If basekey.GetValue(Node4) IsNot Nothing Then
            XMLDoc.LoadXml(basekey.GetValue(Node4).ToString)
        End If
        Return XMLDoc
    End Function
    Private Function CompareComp4(ByVal CI As CompInfo, ByVal CItoComp As CompInfo) As Boolean
        Return CompareComp1(CI, CItoComp)
    End Function
    Private Function Verify(ByVal CompInfoXML As XmlDocument, ByVal CompInfo As CompInfo) As Boolean
        'Console.WriteLine("Private Function Verify")
        'Console.WriteLine(CompInfoXML.OuterXml)
        'Console.WriteLine(MemoryEncrypt.UnProtectData(CompInfo.KeyName))
        'Console.WriteLine(LocalFriNode3)
        Return MemoryEncrypt.VerifyXmlSignature(CompInfoXML, CompInfo.KeyName, MemoryEncrypt.ProtectData(LocalFriNode3))
    End Function
    Private Function VerifyLicense(ByVal LicenseXML As XmlDocument, ByVal KeyName As String) As Boolean
        Return MemoryEncrypt.VerifyXmlSignature(LicenseXML, MemoryEncrypt.ProtectData(KeyName), MemoryEncrypt.ProtectData(ForeignFriNode3))
    End Function
    Private Sub LICDeserialize(ByVal LicXML As XmlDocument, ByRef Lic As License)
        Lic = DESerializeObject(LicXML, Lic.GetType)

    End Sub
    Private Sub CIDeserialize(ByVal CompInfoXML As XmlDocument, ByRef CompInfo As CompInfo)
        CompInfo = DESerializeObject(CompInfoXML, CompInfo.GetType)
    End Sub
    Private Sub SaveCompInfoFile(ByVal XMLDoc As XmlDocument, ByVal PDKeyName As ProtData, ByVal PDPrivateKey As ProtData)
        XMLDoc = MemoryEncrypt.Sign(XMLDoc, PDKeyName, PDPrivateKey)
        XMLDoc.PreserveWhitespace = True
        XMLDoc.Save(Paths.Join(Paths.AllUsersData, CompInfoFilename))
    End Sub

    Private Sub SaveCompInfoReg(ByVal XMLDoc As XmlDocument, ByVal PDKeyName As ProtData, ByVal PDPrivateKey As ProtData)
        XMLDoc = MemoryEncrypt.Sign(XMLDoc, PDKeyName, PDPrivateKey)
        Dim regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Node1, True)
        If regKey Is Nothing Then
            Microsoft.Win32.Registry.CurrentUser.CreateSubKey(Node1)
        End If

        regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Node1 & "\\" & Node2, True)
        If regKey Is Nothing Then
            regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Node1, True)
            regKey.CreateSubKey(Node2)
        End If

        regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Node1 & "\\" & Node2 & "\\" & Node3, True)
        If regKey Is Nothing Then
            regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Node1 & "\\" & Node2, True)
            regKey.CreateSubKey("" & Node3)
            regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Node1 & "\\" & Node2 & "\\" & Node3, True)
        End If

        Dim XMLString As New StringBuilder

        Dim Writer As New StringWriter(XMLString)
        Writer.Write(XMLDoc)

        regKey.SetValue(Node4, XMLDoc.OuterXml)

    End Sub

    Private Function CompareComp2(ByVal CI As CompInfo, ByVal CItoComp As CompInfo) As Boolean
        If Not CI.BiosSN = CItoComp.BiosSN Then
            Return False
        ElseIf Not CI.HardDriveSize = CItoComp.HardDriveSize Then
            Return False
        ElseIf Not CI.InstalledMemory = CItoComp.InstalledMemory Then
            Return False
            'ElseIf Not CI.MACAddress = CItoComp.MACAddress Then
            '    Return False
        ElseIf Not CI.Manufacturer = CItoComp.Manufacturer Then
            Return False
        ElseIf Not CI.Model = CItoComp.Model Then
            Return False
        ElseIf Not CI.OSFullName = CItoComp.OSFullName Then
            Return False
        ElseIf Not CI.OSPlatform = CItoComp.OSPlatform Then
            Return False
        ElseIf Not CI.Processor = CItoComp.Processor Then
            Return False
        ElseIf Not CI.Processors = CItoComp.Processors Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function CompareComp3(ByVal CI As CompInfo, ByVal CItoComp As CompInfo) As Boolean
        Return CompareComp2(CI, CItoComp)
    End Function

    Private Sub NotContinue()

        If MessageBox.Show("Cannot open program.  Do you want to save the registration file o activate the product?", "Cannot load program", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            If MessageBox.Show("Do you want to save the registration file?", "Save registration", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                Guardarregistration()
            ElseIf MessageBox.Show("Do you wish to activate the product?", "Activate product", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                ActivarProducto()
            End If
        End If
    End Sub

    Friend Sub Guardarregistration()
        Dim CI As New CompInfo(LocalFriNode3)
        Dim CIXML As XmlDocument
        Dim CIXMLSigned As XmlDocument
        Dim SD As New SaveFileDialog
        SD.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        SD.FileName = "registration.dat"

        CIXML = SerializeObject(CI, CI.GetType)
        CIXMLSigned = MemoryEncrypt.Sign(CIXML, CI.KeyName, MemoryEncrypt.ProtectData(LocalFriNode3))
        CIXMLSigned.PreserveWhitespace = True
        If SD.ShowDialog = DialogResult.OK Then
            CIXMLSigned.Save(SD.FileName)
        End If

    End Sub
    Friend Sub ActivarProducto()
        Dim CI As New CompInfo(LocalFriNode3)
        Dim OD As New OpenFileDialog
        Dim Lic As New License
        Dim LicXML As New XmlDocument
        OD.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        OD.FileName = "License.dat"
        If OD.ShowDialog() = DialogResult.OK Then
            LicXML.PreserveWhitespace = True
            Try
                LicXML.Load(OD.FileName)

                If Not VerifyLicense(LicXML, LicenseFilename) Then
                    MessageBox.Show("Cannot apply licence.")
                Else
                    Lic = DESerializeObject(LicXML, Lic.GetType)
                    If Not (CompareComp3(Lic.CompInfo, CI) AndAlso Lic.Level = "Full") Then
                        MessageBox.Show("Cannot apply licence.")
                    ElseIf Not Lic.LicenseVersion = CInt(Split(Application.ProductVersion, ".")(0)) Then
                        MessageBox.Show("Cannot apply licence. Not for this version of the program.")
                    Else
                        My.Computer.FileSystem.CopyFile(OD.FileName, Paths.Join(Paths.AllUsersData, LicenseFilename), True)
                        CI.PassDue = False
                        SaveCompInfoFile(SerializeObject(CI, CI.GetType), CI.KeyName, MemoryEncrypt.ProtectData(LocalFriNode3))
                        SaveCompInfoReg(SerializeObject(CI, CI.GetType), CI.KeyName, MemoryEncrypt.ProtectData(LocalFriNode3))
                        acercade.Level = Lic.Level
                        acercade.Activado = "Yes"
                        acercade.Autorizado = Lic.Owner
                        acercade.IsBeta = False

                        MessageBox.Show("Licence applied properly.")
                        MessageBox.Show("This is a Beta version.  License is for testing purposes only and will not really activate the program to continue functioning past the Beta end date.")
                    End If
                End If
            Catch badfileEx As XmlException
                General.LogError(badfileEx, "Cannot open file.  Not a valid XML file.")
                'My.Application.Log.WriteException(badfileEx, _
                '                  TraceEventType.Error, _
                '                  "Cannot open file.  Not a valid XML file.")
                MessageBox.Show("Cannot open file.  Not a valid XML file.")
            Catch ex As Exception
                'monitor.TrackException(ex, _
                '                  TraceEventType.Error, _
                '                  "Cannot open file ")
                General.LogError(ex, "Cannot open model ")
                'My.Application.Log.WriteException(ex, _
                '                  TraceEventType.Error, _
                '                  "Cannot open model ")
                MessageBox.Show("Cannot open file.")
            End Try
        End If
    End Sub
End Class

