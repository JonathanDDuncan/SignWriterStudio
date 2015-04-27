
Imports System.Security.Cryptography
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports System.Security.Cryptography.Xml
Imports System
Imports System.Reflection
Imports System.Management
Imports System.Xml.XPath
Imports System.Globalization

Public NotInheritable Class GetCompInfo
    Public Shared Function SetManufacturerModel(ByVal compInfo As CompInfo) As CompInfo
        ' ManagementObjectSearcher retrieves a collection of WMI objects based on 
        ' the query.  In this case a string is used instead of a SelectQuery object.
        'Try
        If compInfo Is Nothing Then
            Throw New ArgumentNullException("compInfo")
        End If

        Dim search As New ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem")

        ' Display each entry for Win32_ComputerSystem
        Dim info As ManagementObject
        For Each info In search.Get()
            compInfo.Manufacturer = info("manufacturer").ToString()
            compInfo.Model = "Model: " & info("model").ToString()
        Next
        'Catch ex As Exception
        '    monitor.TrackException(ex, _
        '                      TraceEventType.Error.ToString.ToString, _
        '                      "Cannot find model ")
        '    My.Application.Log.WriteException(ex, _
        '                      TraceEventType.Error, _
        '                      "Cannot find model ")
        '    MessageBox.Show("Cannot find model.")
        'End Try
        Return compInfo
    End Function
    Public Shared Function setProcessor(ByVal compInfo As CompInfo) As CompInfo
        'Try
        ' This is to show how to use the SelectQuery object in the place of a SELECT 
        ' statement.
        Dim query As New SelectQuery("Win32_processor")

        'ManagementObjectSearcher retrieves a collection of WMI objects based on 
        ' the query.
        Dim search As New ManagementObjectSearcher(query)

        ' Display each entry for Win32_processor
        Dim info As ManagementObject
        For Each info In search.Get()
            compInfo.Processor = info("caption").ToString()
            compInfo.ProcessorID = info("ProcessorId").ToString()
        Next
        'Catch ex As Exception
        '    monitor.TrackException(ex, _
        '                      TraceEventType.Error.ToString.ToString, _
        '                      "Could not find processor data ")
        '    My.Application.Log.WriteException(ex, _
        '                      TraceEventType.Error, _
        '                      "Could not find processor data ")
        '    MessageBox.Show("Could not find processor data.")
        'End Try
        Return CompInfo
    End Function
    Public Shared Function setDrive(ByVal compInfo As CompInfo) As CompInfo
        'Try
        Dim drive As System.IO.DriveInfo
        drive = My.Computer.FileSystem.GetDriveInfo(My.Application.Info.DirectoryPath)
        compInfo.HardDriveSize = drive.TotalSize

        'compInfo.HardDriveVolume = drive.VolumeLabel
        'Catch ex As Exception
        '    monitor.TrackException(ex, _
        '                      TraceEventType.Error.ToString, _
        '                      "Could not find hard drive info ")
        '    My.Application.Log.WriteException(ex, _
        '                      TraceEventType.Error, _
        '                      "Could not find hard drive info ")
        '    MessageBox.Show("Could not find hard drive info.")
        'End Try
        Return CompInfo
    End Function
    Public Shared Function setBiosInfo(ByVal compInfo As CompInfo) As CompInfo
        'Try
        ' This is to show how to use the SelectQuery object in the place of a SELECT 
        ' statement.
        Dim query As New SelectQuery("Win32_bios")

        'ManagementObjectSearcher retrieves a collection of WMI objects based on 
        ' the query.
        Dim search As New ManagementObjectSearcher(query)

        ' Display each entry for Win32_bios
        Dim info As ManagementObject
        For Each info In search.Get()
            compInfo.BiosSN = info("version").ToString()
            compInfo.BiosSN = info("SerialNumber").ToString()
        Next
        'Catch ex As Exception
        '    monitor.TrackException(ex, _
        '                      TraceEventType.Error.ToString, _
        '                      "Could not get BIOS info.")
        '    My.Application.Log.WriteException(ex, _
        '                      TraceEventType.Error, _
        '                      "Could not get BIOS info.")
        '    MessageBox.Show("Could not get BIOS info.")
        'End Try
        Return CompInfo
    End Function
    Public Shared Function setDriveSN(ByVal compInfo As CompInfo) As CompInfo
        'Try
        ' This is to show how to use the SelectQuery object in the place of a SELECT 
        ' statement.
        Dim query As New SelectQuery("Win32_DiskDrive")

        'ManagementObjectSearcher retrieves a collection of WMI objects based on 
        ' the query.
        Dim search As New ManagementObjectSearcher(query)

        ' Display each entry for Win32_bios
        'Dim info As ManagementObject
        'For Each info In search.Get().
        '    If info.Path.ToString().Contains("PhysicalDrive0") Then
        '    compInfo.DriveSN = info("SerialNumber").ToString()
        '    End If

        'Next
        'Catch ex As Exception
        '    monitor.TrackException(ex, _
        '                      TraceEventType.Error.ToString, _
        '                      "Could not get BIOS info.")
        '    My.Application.Log.WriteException(ex, _
        '                      TraceEventType.Error, _
        '                      "Could not get BIOS info.")
        '    MessageBox.Show("Could not get BIOS info.")
        'End Try
        Return compInfo
    End Function
    'Friend Sub setNics(ByRef CompInfo As CompInfo)
    '    Try
    '        Dim nics As Net.NetworkInformation.NetworkInterface() = Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces
    '        Dim adapter As Net.NetworkInformation.NetworkInterface
    '        Dim Stringbuilder As New StringBuilder
    '        Dim I As Integer
    '        For Each adapter In nics
    '            Dim Address As Net.NetworkInformation.PhysicalAddress = adapter.GetPhysicalAddress()
    '            If Not Stringbuilder.ToString = "" Then
    '                Stringbuilder.Append("-")
    '            End If
    '            Dim bytes As Byte() = Address.GetAddressBytes()
    '            For I = 0 To bytes.Length - 1

    '                '// Display the physical address in hexadecimal.
    '                Stringbuilder.Append(bytes(I).ToString("X2"))
    '                '// Insert a hyphen after each byte, unless we are at the end of the 
    '                '// address.
    '                'if (i != bytes.Length -1)
    '                '{
    '                '     Console.Write("-");
    '                '}
    '            Next

    '        Next adapter
    '        CompInfo.MACAddress = Stringbuilder.ToString
    '    Catch ex As Exception
    '        'monitor.TrackException(ex)
    '        My.Application.Log.WriteException(ex, _
    '                          TraceEventType.Error.ToString, _
    '                          "No se pudo averiguar el MACAddress.")
    '        MessageBox.Show("No se pudo averiguar el MACAddress")
    '    End Try
    'End Sub
    'Public Shared Function setKeys(ByVal compInfo As CompInfo, ByVal LocalFriNode3 As String) As CompInfo
    '    'Try

    '    ' Create a key and save it in a container.
    '    StoreKey.GenKey_SaveInContainer(MemoryEncrypt.UnProtectData(CompInfo.KeyName))
    '    Dim Key As New CspParameters
    '    Key.KeyContainerName = MemoryEncrypt.UnProtectData(CompInfo.KeyName)
    '    'CompInfo.PubKeyName = MemoryEncrypt.UnProtectData(PD)
    '    Dim rsa As New RSACryptoServiceProvider(Key)
    '    'rsa.FromXmlString(StoreKey.GetFriNode3FromContainer(MemoryEncrypt.UnProtectData(PD)))
    '    rsa.FromXmlString(LocalFriNode3)
    '    'MemoryEncrypt.clearBytes(PD.data)
    '    CompInfo.PubKey = rsa.ToXmlString(False)
    '    'Catch ex As Exception
    '    '    monitor.TrackException(ex, _
    '    '                      TraceEventType.Error.ToString, _
    '    '                      "Exception.")
    '    '    My.Application.Log.WriteException(ex, _
    '    '                      TraceEventType.Error, _
    '    '                      "Exception.")
    '    'End Try
    '    Return CompInfo
    'End Function

    Private Sub New()

    End Sub
End Class

Public NotInheritable Class MemoryEncrypt
    Private Sub New()

    End Sub


    Public Shared Function ProtectData(ByVal stringtoEncrypt As String) As ProtData
        'Try
        Dim PD As New ProtData

        Dim toEncrypt As Byte() = UnicodeEncoding.ASCII.GetBytes(stringtoEncrypt)

        ' Create a stream.
        Dim mStream As New MemoryStream()

        ' Create some random entropy.
        PD.entropy = CreateRandomEntropy()

        'Console.WriteLine()
        'Console.WriteLine("Original data: " + UnicodeEncoding.ASCII.GetString(toEncrypt))
        'Console.WriteLine("Encrypting and writing to disk...")

        ' Encrypt a copy of the data to the stream.
        PD.bytesWritten = EncryptDataToStream(toEncrypt, PD.entropy, DataProtectionScope.CurrentUser, mStream)
        PD.data = mStream.ToArray()
        mStream.Dispose()
        mStream.Close()

        Return PD
        'Catch ex As Exception
        '    monitor.TrackException(ex, _
        '                      TraceEventType.Error.ToString, _
        '                      "Exception.")
        '    My.Application.Log.WriteException(ex, _
        '                      TraceEventType.Error, _
        '                      "Exception.")
        '    Dim mStream As New MemoryStream()
        '    Return New ProtData
        'End Try
    End Function

    Friend Shared Function UnProtectData(ByVal PD As ProtData) As String
        'Try

        'Console.WriteLine("Reading data from disk and decrypting...")

        ' Open the file.
        'mStream = New FileStream("Data.dat", FileMode.Open)
        Dim mStream As New MemoryStream(PD.data)
        ' Read from the stream and decrypt the data.
        'System.Diagnostics.Debugger.Break()
        Dim decryptData As Byte() = DecryptDataFromStream(PD.entropy, DataProtectionScope.CurrentUser, mStream, PD.bytesWritten)

        mStream.Dispose()
        mStream.Close()

        'Console.WriteLine("Decrypted data: " + UnicodeEncoding.ASCII.GetString(decryptData))
        Return UnicodeEncoding.ASCII.GetString(decryptData)

        'Catch ex As Exception
        '    monitor.TrackException(ex, _
        '                     TraceEventType.Error.ToString, _
        '                     "Exception.")
        '    My.Application.Log.WriteException(ex, _
        '                      TraceEventType.Error, _
        '                      "Exception.")
        '    Return ""
        'End Try

    End Function



    Private Shared Function CreateRandomEntropy() As Byte()
        ' Create a byte array to hold the random value.
        Dim entropy(15) As Byte

        ' Create a new instance of the RNGCryptoServiceProvider.
        ' Fill the array with a random value.
        Dim RNG As New RNGCryptoServiceProvider()

        RNG.GetBytes(entropy)

        ' Return the array.
        Return entropy

    End Function 'CreateRandomEntropy



    Private Shared Function EncryptDataToStream(ByVal Buffer() As Byte, ByVal Entropy() As Byte, ByVal Scope As DataProtectionScope, ByVal S As Stream) As Integer
        If Buffer.Length <= 0 Then
            Throw New ArgumentException("Buffer length less or equal to 0")
        End If
        If Buffer Is Nothing Then
            Throw New ArgumentNullException("Buffer is nothing")
        End If
        If Entropy.Length <= 0 Then
            Throw New ArgumentException("Entropy length is less or equal to 0")
        End If
        If Entropy Is Nothing Then
            Throw New ArgumentNullException("Entropy is nothing")
        End If
        If S Is Nothing Then
            Throw New ArgumentNullException("S")
        End If
        Dim length As Integer = 0

        ' Encrypt the data in memory. The result is stored in the same same array as the original data.
        Dim encrptedData As Byte() = ProtectedData.Protect(Buffer, Entropy, Scope)

        ' Write the encrypted data to a stream.
        If S.CanWrite AndAlso Not (encrptedData Is Nothing) Then
            S.Write(encrptedData, 0, encrptedData.Length)

            length = encrptedData.Length
        End If

        ' Return the length that was written to the stream. 
        Return length

    End Function 'EncryptDataToStream


    Private Shared Function DecryptDataFromStream(ByVal Entropy() As Byte, ByVal Scope As DataProtectionScope, ByVal S As Stream, ByVal Length As Integer) As Byte()
        If S Is Nothing Then
            Throw New ArgumentNullException("S")
        End If
        If Length <= 0 Then
            Throw New ArgumentException("Length is less or equal to 0")
        End If
        If Entropy Is Nothing Then
            Throw New ArgumentNullException("Entropy is nothing")
        End If
        If Entropy.Length <= 0 Then
            Throw New ArgumentException("Entropy is less or equal to 0")
        End If


        Dim inBuffer(Length) As Byte
        Dim outBuffer() As Byte

        ' Read the encrypted data from a stream.
        If S.CanRead Then
            S.Read(inBuffer, 0, Length)

            outBuffer = ProtectedData.Unprotect(inBuffer, Entropy, Scope)
        Else
            Throw New IOException("Could not read the stream.")
        End If

        ' Return the length that was written to the stream. 
        Return outBuffer

    End Function 'DecryptDataFromStream 

    Public Shared Sub Encrypt(ByVal doc As xmlDocument, ByVal encryptionElement As String, ByVal encryptionElementId As String, ByVal alg As Rsa, ByVal keyName As String)
        ' Check the arguments.
        If doc Is Nothing Then
            Throw New ArgumentNullException("doc")
        End If
        If encryptionElement Is Nothing Then
            Throw New ArgumentNullException("encryptionElement")
        End If
        If encryptionElementId Is Nothing Then
            Throw New ArgumentNullException("encryptionElementId")
        End If
        If alg Is Nothing Then
            Throw New ArgumentNullException("alg")
        End If
        If keyName Is Nothing Then
            Throw New ArgumentNullException("keyName")
        End If
        '//////////////////////////////////////////////
        ' Find the specified element in the XmlDocument
        ' object and create a new XmlElemnt object.
        '//////////////////////////////////////////////
        Dim elementToEncrypt As XmlElement = CType(Doc.GetElementsByTagName(EncryptionElement)(0), System.Xml.XmlElement)

        ' Throw an XmlException if the element was not found.
        If elementToEncrypt Is Nothing Then
            Throw New XmlException("The specified element was not found")
        End If
        Dim sessionKey As RijndaelManaged = Nothing

        Try
            '////////////////////////////////////////////////
            ' Create a new instance of the EncryptedXml class
            ' and use it to encrypt the XmlElement with the
            ' a new random symmetric key.
            '////////////////////////////////////////////////
            ' Create a 256 bit Rijndael key.
            sessionKey = New RijndaelManaged()
            sessionKey.KeySize = 256
            Dim eXml As New EncryptedXml()

            Dim encryptedElement As Byte() = eXml.EncryptData(elementToEncrypt, sessionKey, False)
            '//////////////////////////////////////////////
            ' Construct an EncryptedData object and populate
            ' it with the desired encryption information.
            '//////////////////////////////////////////////
            Dim edElement As New EncryptedData()
            edElement.Type = EncryptedXml.XmlEncElementUrl
            edElement.Id = EncryptionElementID
            ' Create an EncryptionMethod element so that the
            ' receiver knows which algorithm to use for decryption.
            edElement.EncryptionMethod = New EncryptionMethod(EncryptedXml.XmlEncAES256Url)
            ' Encrypt the session key and add it to an EncryptedKey element.
            Dim ek As New EncryptedKey()

            Dim encryptedKey As Byte() = EncryptedXml.EncryptKey(sessionKey.Key, Alg, False)

            ek.CipherData = New CipherData(encryptedKey)

            ek.EncryptionMethod = New EncryptionMethod(EncryptedXml.XmlEncRSA15Url)
            ' Create a new DataReference element
            ' for the KeyInfo element.  This optional
            ' element specifies which EncryptedData
            ' uses this key.  An Xml document can have
            ' multiple EncryptedData elements that use
            ' different keys.
            Dim dRef As New DataReference()

            ' Specify the EncryptedData URI.

            dRef.Uri = "#" + EncryptionElementID

            ' Add the DataReference to the EncryptedKey.
            ek.AddReference(dRef)
            ' Add the encrypted key to the
            ' EncryptedData object.
            edElement.KeyInfo.AddClause(New KeyInfoEncryptedKey(ek))
            ' Set the KeyInfo element to specify the
            ' name of the RSA key.
            ' Create a new KeyInfoName element.
            Dim kin As New KeyInfoName()

            ' Specify a name for the key.
            kin.Value = KeyName

            ' Add the KeyInfoName element to the
            ' EncryptedKey object.
            ek.KeyInfo.AddClause(kin)
            ' Add the encrypted element data to the
            ' EncryptedData object.
            edElement.CipherData.CipherValue = encryptedElement
            '//////////////////////////////////////////////////
            ' Replace the element from the original XmlDocument
            ' object with the EncryptedData element.
            '//////////////////////////////////////////////////
            EncryptedXml.ReplaceElement(elementToEncrypt, edElement, False)
        Catch ex As Exception
            'monitor.TrackException(ex, _
            '                  TraceEventType.Error.ToString, _
            '                  "Exception.")
            General.LogError(ex, "Exception")
            'My.Application.Log.WriteException(ex, _
            '                  TraceEventType.Error, _
            '                  "Exception.")
            ' re-throw the exception.
            Throw
        Finally
            If Not (sessionKey Is Nothing) Then
                sessionKey.Clear()
            End If
        End Try

    End Sub 'Encrypt

    Public Shared Sub Decrypt(ByVal doc As XmlDocument, ByVal alg As RSA, ByVal keyName As String)
        ' Check the arguments.  
        If doc Is Nothing Then
            Throw New ArgumentNullException("doc")
        End If
        If alg Is Nothing Then
            Throw New ArgumentNullException("alg")
        End If
        If keyName Is Nothing Then
            Throw New ArgumentNullException("keyName")
        End If
        ' Create a new EncryptedXml object.
        Dim eXml As New EncryptedXml(Doc)

        ' Add a key-name mapping.
        ' This method can only decrypt documents
        ' that present the specified key name.
        eXml.AddKeyNameMapping(KeyName, Alg)

        ' Decrypt the element.
        eXml.DecryptDocument()

    End Sub 'Decrypt 


    Friend Shared Sub clearBytes(ByVal Buffer() As Byte)
        ' Check arguments.
        If Buffer Is Nothing Then
            Throw New ArgumentException("Buffer")
        End If

        ' Set each byte in the buffer to 0.
        Dim x As Integer
        For x = 0 To Buffer.Length - 1
            Buffer(x) = 0
        Next x

    End Sub
    'Public Shared Function SerializeObject(ByVal obj As Object, ByVal objType As Type, ByVal extraTypes As Type()) As XmlDocument
    '    Dim myWriter As IO.StringWriter
    '    Dim xmlDoc As New System.Xml.XmlDocument
    '    Try
    '        Dim myObject As Object = obj
    '        ' Insert code to set properties and fields of the object.
    '        'Dim mySerializer As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(GetType(CompInfo))
    '        Dim mySerializer As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(objType, extraTypes)

    '        Dim str As New Text.StringBuilder

    '        myWriter = New IO.StringWriter(str, CultureInfo.InvariantCulture)
    '        mySerializer.Serialize(myWriter, myObject)

    '        xmlDoc.PreserveWhitespace = True
    '        xmlDoc.LoadXml(str.ToString)
    '    Catch ex As XmlException
    '        monitor.TrackException(ex, _
    '                          TraceEventType.Error.ToString, _
    '                          "Cannot serialize object " & ex.GetType().Name & " Error:" & ex.Message)
    '        My.Application.Log.WriteException(ex, _
    '                          TraceEventType.Error, _
    '                          "Cannot serialize object " & ex.GetType().Name & " Error:" & ex.Message)
    '        Dim MBO As MessageBoxOptions = MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign
    '        MessageBox.Show("Cannot serialize object " & ex.GetType().Name & " Error:" & ex.Message, "Could not save", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBO, False)
    '    End Try
    '    Return xmlDoc
    'End Function
    'Public Shared Function SerializeObject(ByVal obj As Object, ByVal objType As Type) As XmlDocument
    '    Dim myWriter As IO.StringWriter
    '    Dim xmlDoc As New System.Xml.XmlDocument
    '    Try
    '        Dim myObject As Object = obj
    '        ' Insert code to set properties and fields of the object.
    '        'Dim mySerializer As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(GetType(CompInfo))
    '        Dim mySerializer As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(objType)

    '        Dim str As New Text.StringBuilder

    '        myWriter = New IO.StringWriter(str, CultureInfo.InvariantCulture)
    '        mySerializer.Serialize(myWriter, myObject)

    '        xmlDoc.PreserveWhitespace = True
    '        xmlDoc.LoadXml(str.ToString)
    '    Catch ex As XmlException
    '        monitor.TrackException(ex, _
    '                          TraceEventType.Error.ToString, _
    '                          "Cannot serialize object " & ex.GetType().Name & " Error:" & ex.Message)
    '        My.Application.Log.WriteException(ex, _
    '                          TraceEventType.Error, _
    '                          "Cannot serialize object " & ex.GetType().Name & " Error:" & ex.Message)
    '        Dim MBO As MessageBoxOptions = MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign
    '        MessageBox.Show("Cannot serialize object " & ex.GetType().Name & " Error:" & ex.Message, "Could not serialize", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBO, False)
    '    End Try
    '    Return xmlDoc
    'End Function
    'Public Shared Function DESerializeObject(ByVal xmlDoc As XmlDocument, ByVal objType As Type) As Object
    '    If xmlDoc Is Nothing Then
    '        Throw New ArgumentNullException("xmlDoc")
    '    End If

    '    If objType Is Nothing Then
    '        Throw New ArgumentNullException("objType")
    '    End If



    '    Dim obj As Object
    '    Try
    '        Dim myReader As TextReader
    '        ' Construct an instance of the XmlSerializer with the type
    '        ' of object that is being deserialized.
    '        Dim mySerializer As Serialization.XmlSerializer = New Serialization.XmlSerializer(objType)
    '        ' To read the file, create a FileStream.
    '        myReader = New IO.StringReader(xmlDoc.OuterXml)
    '        ' Call the Deserialize method and cast to the object type.
    '        xmlDoc.ToString()
    '        obj = mySerializer.Deserialize(myReader)
    '        Return obj
    '    Catch ex As XmlException
    '        monitor.TrackException(ex, _
    '                          TraceEventType.Error.ToString, _
    '                          "Cannot deserialze object.  Error: " & ex.Message, "Could not open ")
    '        My.Application.Log.WriteException(ex, _
    '                          TraceEventType.Error, _
    '                          "Cannot deserialze object.  Error: " & ex.Message, 0)
    '        Dim MBO As MessageBoxOptions = MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign
    '        MessageBox.Show("Cannot deserialze object " & ex.GetType().Name & " Error:" & ex.Message, "Could not deserialize", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBO, False)
    '        Return Nothing
    '    End Try


    'End Function
    Public Shared Function Sign(ByVal xmlDoc As XmlDocument, ByVal pdKeyName As ProtData, ByVal pdPrivateKey As ProtData) As XmlDocument
        If xmlDoc Is Nothing Then
            Throw New ArgumentNullException("xmlDoc")
        End If
        If pdKeyName Is Nothing Then
            Throw New ArgumentNullException("pdKeyName")
        End If
        If pdPrivateKey Is Nothing Then
            Throw New ArgumentNullException("pdPrivateKey")
        End If
        Try
            Dim signedXml As New SignedXml(xmlDoc)
            ' Create a new instance of RSACryptoServiceProvider that accesses
            ' the key container MyKeyContainerName.

            Dim cspParams As New CspParameters()
            cspParams.KeyContainerName = MemoryEncrypt.UnProtectData(pdKeyName)
            MemoryEncrypt.clearBytes(pdKeyName.data)
            Dim rsa As New RSACryptoServiceProvider(cspParams)
            If pdPrivateKey.data.Length <> 0 Then
                MemoryEncrypt.UnProtectData(pdPrivateKey)
                rsa.FromXmlString(MemoryEncrypt.UnProtectData(pdPrivateKey))
                MemoryEncrypt.clearBytes(pdPrivateKey.data)
            End If
            signedXml.SigningKey = rsa
            Dim reference As New Reference()
            reference.Uri = ""
            Dim env As New XmlDsigEnvelopedSignatureTransform()
            reference.AddTransform(env)
            signedXml.AddReference(reference)
            signedXml.ComputeSignature()

            Dim XmlDigitalSignature As System.Xml.XmlElement = signedXml.GetXml()
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(XmlDigitalSignature, True))

            rsa.Clear()
        Catch ex As XmlException
            'monitor.TrackException(ex, _
            '                  TraceEventType.Error.ToString, _
            '                  "Error saving file.  " & ex.GetType().Name & " Error:" & ex.Message, "Could not save file.")
            General.LogError(ex, "Error saving file.  " & ex.GetType().Name)
            'My.Application.Log.WriteException(ex, _
            '                  TraceEventType.Error, _
            '                  "Error saving file.  " & ex.GetType().Name & " Error:" & ex.Message, 0)

            Dim MBO As MessageBoxOptions = MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign
            MessageBox.Show("Error saving file.  " & ex.GetType().Name & " Error:" & ex.Message, "Could not save file.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBO, False)

        End Try

        Return xmlDoc

    End Function

    Public Shared Sub SaveXml(ByVal xmlDoc As XmlDocument, ByVal filename As String)
        If xmlDoc Is Nothing Then
            Throw New ArgumentNullException("xmlDoc")
        End If

        If filename Is Nothing Then
            Throw New ArgumentNullException("filename")
        End If

        xmlDoc.Save(Filename)
    End Sub
    Public Shared Sub LoadXml(ByVal xmlDoc As XmlDocument, ByVal filename As String)
        If xmlDoc Is Nothing Then
            Throw New ArgumentNullException("xmlDoc")
        End If

        If filename Is Nothing Then
            Throw New ArgumentNullException("filename")
        End If
        'Try
        xmlDoc.Load(Filename)
        '        Catch badfileEx As XmlException

        '            My.Application.Log.WriteException(badfileEx, _
        '                              TraceEventType.Error, _
        '                              "Cannot open file.  Not a valid Xml file.")
        '            MessageBox.Show("Cannot open file.  Not a valid Xml file.")
        '        Catch ex As Exception
        'monitor.TrackException(ex, _
        '                  TraceEventType.Error.ToString, _
        '                  "Cannot open file ")
        'My.Application.Log.WriteException(ex, _
        '                  TraceEventType.Error, _
        '                  "Cannot open model ")
        'MessageBox.Show("Cannot open file.")
        '        End Try
    End Sub

    Public Shared Function XmlEncrypt(ByVal xmlDoc As XmlDocument, ByVal pdKeyName As ProtData, ByVal pdPrivateKey As ProtData, ByVal encryptionElement As String) As XmlDocument
        If xmlDoc Is Nothing Then
            Throw New ArgumentNullException("xmlDoc")
        End If
        If pdKeyName Is Nothing Then
            Throw New ArgumentNullException("pdKeyName")
        End If
        If pdPrivateKey Is Nothing Then
            Throw New ArgumentNullException("pdPrivateKey")
        End If
        If encryptionElement Is Nothing Then
            Throw New ArgumentNullException("encryptionElement")
        End If
        ' Create a new CspParameters object to specify
        ' a key container.
        Dim cspParams As New CspParameters()
        MemoryEncrypt.UnProtectData(PDKeyName)
        cspParams.KeyContainerName = MemoryEncrypt.UnProtectData(PDKeyName)
        MemoryEncrypt.clearBytes(PDKeyName.data)
        ' Create a new RSA key and save it in the container.  This key will encrypt
        ' a symmetric key, which will then be encryped in the Xml document.
        Dim rsaKey As New RSACryptoServiceProvider(cspParams)
        If PDPrivateKey.data.Length <> 0 Then
            MemoryEncrypt.UnProtectData(PDPrivateKey)
            rsaKey.FromXmlString(MemoryEncrypt.UnProtectData(PDPrivateKey))
            MemoryEncrypt.clearBytes(PDPrivateKey.data)
        End If
        'Console.WriteLine(rsaKey.ToXmlString(False))
        'MessageBox.Show("")
        'Try
        ' Encrypt the "creditcard" element.
        'Encrypt(xmlDoc, "creditcard", "EncryptedElement1", rsaKey, "rsaKey")
        MemoryEncrypt.Encrypt(xmlDoc, EncryptionElement, "EncryptedElement1", rsaKey, "rsaKey")

        'Catch ex As Exception
        '    monitor.TrackException(ex, _
        '                      TraceEventType.Error.ToString, _
        '                      "Exception")
        '    My.Application.Log.WriteException(ex, _
        '                      TraceEventType.Error, _
        '                      "Exception")

        '    Console.WriteLine(ex.Message)
        'Finally
        ' Clear the RSA key.
        rsaKey.Clear()
        'End Try

        Return xmlDoc

    End Function

    Public Shared Function VerifyXmlSignature(ByVal xmlDoc As XmlDocument, ByVal pdKeyName As ProtData, ByVal pdPublicKey As ProtData) As [Boolean]
        If xmlDoc Is Nothing Then
            Throw New ArgumentNullException("xmlDoc")
        End If
        If pdKeyName Is Nothing Then
            Throw New ArgumentNullException("pdKeyName")
        End If
        If pdPublicKey Is Nothing Then
            Throw New ArgumentNullException("pdPublicKey")
        End If

        ' Verify the signature of an Xml file against an asymmetric 
        ' algorithm and return the result.

        Dim cspParams As New CspParameters()

        cspParams.KeyContainerName = MemoryEncrypt.UnProtectData(pdKeyName)
        MemoryEncrypt.clearBytes(pdKeyName.data)

        Dim rsaKey As New RSACryptoServiceProvider(cspParams)
        If pdPublicKey.data.Length <> 0 Then
            MemoryEncrypt.UnProtectData(pdPublicKey)
            rsaKey.FromXmlString(MemoryEncrypt.UnProtectData(pdPublicKey))
            MemoryEncrypt.clearBytes(pdPublicKey.data)
        End If

        ' Check arguments.
        If xmlDoc Is Nothing Then
            Throw New ArgumentException("Doc")
        End If
        If rsaKey Is Nothing Then
            Throw New ArgumentException("Key")
        End If
        ' Create a new SignedXml object and pass it
        ' the Xml document class.
        Dim signedXml As New SignedXml(xmlDoc)
        ' Find the "Signature" node and create a new
        ' XmlNodeList object.
        Dim nodeList As System.Xml.XmlNodeList = xmlDoc.GetElementsByTagName("Signature")
        ' Throw an exception if no signature was found.
        If nodeList.Count <= 0 Then
            Throw New CryptographicException("Verification failed: No Signature was found in the document.")
        End If

        ' This example only supports one signature for
        ' the entire Xml document.  Throw an exception 
        ' if more than one signature was found.
        If nodeList.Count >= 2 Then
            Throw New CryptographicException("Verification failed: More that one signature was found for the document.")
        End If
        'Console.WriteLine("Private Function VerifyXmlSignature")
        'Console.WriteLine(xmlDoc.OuterXml)
        'Console.WriteLine(rsaKey.ToXmlString(True))

        ' Load the first <signature> node.  
        signedXml.LoadXml(CType(nodeList(0), System.Xml.XmlElement))
        ' Check the signature and return the result.
        Try
            Return signedXml.CheckSignature(rsaKey)
        Catch ex As Exception
            Return False
        End Try

    End Function
End Class

<Serializable()> Public Class CompInfo

    'Add WIN32_Bios SerialNumber

    'Physical Drivo0 SerialNumber

    Private _ComputerName As String 'OK
    Public Property ComputerName() As String
        Get
            Return _ComputerName
        End Get
        Set(ByVal value As String)
            _ComputerName = value
        End Set
    End Property
    Private _OSFullName As String 'OK
    Public Property OSFullName() As String
        Get
            Return _OSFullName
        End Get
        Set(ByVal value As String)
            _OSFullName = value
        End Set
    End Property
    Private _OSPlatform As String 'OK
    Public Property OSPlatform() As String
        Get
            Return _OSPlatform
        End Get
        Set(ByVal value As String)
            _OSPlatform = value
        End Set
    End Property
    'Friend OSVersion As String 'OK
    Private _InstalledMemory As UInteger 'OK
    Public Property InstalledMemory() As Long
        Get
            Return _InstalledMemory
        End Get
        Set(ByVal value As Long)
            _InstalledMemory = value
        End Set
    End Property
    Private _HardDriveSize As Long 'OK
    Public Property HardDriveSize() As Long
        Get
            Return _HardDriveSize
        End Get
        Set(ByVal value As Long)
            _HardDriveSize = value
        End Set
    End Property
    ''Friend HardDriveVolume As String 'OK
    'Private _MACAddress As String 'OK
    'Public Property MACAddress() As String
    '    Get
    '        Return _MACAddress
    '    End Get
    '    Set(ByVal value As String)
    '        _MACAddress = value
    '    End Set
    'End Property
    Private _Product As String 'OK
    Public Property Product() As String
        Get
            Return _Product
        End Get
        Set(ByVal value As String)
            _Product = value
        End Set
    End Property
    Private _Version As String 'OK
    Public Property Version() As String
        Get
            Return _Version
        End Get
        Set(ByVal value As String)
            _Version = value
        End Set
    End Property
    Private _Manufacturer As String 'OK
    Public Property Manufacturer() As String
        Get
            Return _Manufacturer
        End Get
        Set(ByVal value As String)
            _Manufacturer = value
        End Set
    End Property
    Private _Model As String 'OK
    Public Property Model() As String
        Get
            Return _Model
        End Get
        Set(ByVal value As String)
            _Model = value
        End Set
    End Property
    Private _Processor As String 'OK
    Public Property Processor() As String
        Get
            Return _Processor
        End Get
        Set(ByVal value As String)
            _Processor = value
        End Set
    End Property
    Private _ProcessorId As String 'OK
    Public Property ProcessorId() As String
        Get
            Return _ProcessorId
        End Get
        Set(ByVal value As String)
            _ProcessorId = value
        End Set
    End Property
    Private _BiosVersion As String 'OK

    Public Property BiosVersion() As String
        Get
            Return _BiosVersion
        End Get
        Set(ByVal value As String)
            _BiosVersion = value
        End Set
    End Property
    Private _BiosSN As String 'OK

    Public Property BiosSN() As String
        Get
            Return _BiosSN
        End Get
        Set(ByVal value As String)
            _BiosSN = value
        End Set
    End Property
    Private _Processors As Integer 'OK
    Public Property Processors() As Integer
        Get
            Return _Processors
        End Get
        Set(ByVal value As Integer)
            _Processors = value
        End Set
    End Property
    Private _DriveSN As String 'OK
    Public Property DriveSN() As String
        Get
            Return _DriveSN
        End Get
        Set(ByVal value As String)
            _DriveSN = value
        End Set
    End Property
    Private _InstalationId As Guid 'OK
    Public Property InstalationId() As Guid
        Get
            Return _InstalationId
        End Get
        Set(ByVal value As Guid)
            _InstalationId = value
        End Set
    End Property
    Private _InstalationDate As Date 'OK
    Public Property InstalationDate() As Date
        Get
            Return _InstalationDate
        End Get
        Set(ByVal value As Date)
            _InstalationDate = value
        End Set
    End Property
    Private _PassDue As Boolean
    Public Property PassDue() As Boolean
        Get
            Return _PassDue
        End Get
        Set(ByVal value As Boolean)
            _PassDue = value
        End Set
    End Property
    'Private _PubKey As String 'OK
    'Public Property PubKey() As String
    '    Get
    '        Return _PubKey
    '    End Get
    '    Set(ByVal value As String)
    '        _PubKey = value
    '    End Set
    'End Property
    'Private _PubKeyName As String 'OK

    'Public Property PubKeyName() As String
    '    Get
    '        Return _PubKeyName
    '    End Get
    '    Set(ByVal value As String)
    '        _PubKeyName = value
    '    End Set
    'End Property
    Public Sub New()

    End Sub
    Public Sub New(ByVal LocalFriNode3 As String)
        'Dim GetCompInfo As New GetCompInfo
        Me.ComputerName = My.Computer.Name
        Me.OSFullName = My.Computer.Info.OSFullName
        Me.OSPlatform = My.Computer.Info.OSPlatform
        'Me.OSVersion = My.Computer.Info.OSVersion
        Me.Processors = System.Environment.ProcessorCount()
        Me.Version = My.Application.Info.Version.ToString
        Me.Product = My.Application.Info.ProductName
        Me.InstalledMemory = CUInt(My.Computer.Info.TotalPhysicalMemory)
        Me.InstalationDate = System.DateTime.Now()
        Me.InstalationId = Guid.NewGuid
        'TODO Review what below code was intended to do.
        GetCompInfo.SetManufacturerModel(Me)
        GetCompInfo.setProcessor(Me)
        GetCompInfo.setDrive(Me)
        GetCompInfo.setBiosInfo(Me)
        GetCompInfo.setDriveSN(Me)
        'GetCompInfo.setNics(Me)
        'GetCompInfo.setKeys(Me, LocalFriNode3)
    End Sub

    Public Function KeyName() As ProtData
        Dim Stringbuilder As New Text.StringBuilder
        Stringbuilder.Append(Me.HardDriveSize.ToString(CultureInfo.InvariantCulture))
        Stringbuilder.Append(Me.InstalationDate.ToString(CultureInfo.InvariantCulture))
        Stringbuilder.Append(Me.InstalationId.ToString())
        Stringbuilder.Append(Me.InstalledMemory.ToString(CultureInfo.InvariantCulture))
        'If Me.MACAddress IsNot Nothing Then
        '    Stringbuilder.Append(Me.MACAddress.ToString)
        'End If
        If Me.OSPlatform IsNot Nothing Then
            Stringbuilder.Append(Me.OSPlatform.ToString(CultureInfo.InvariantCulture))
        End If
        If Me.BiosSN IsNot Nothing Then
            Stringbuilder.Append(Me.BiosSN)
        End If
        Stringbuilder.Append(Me.Processors.ToString(CultureInfo.InvariantCulture))
        If Me.Product IsNot Nothing Then
            Stringbuilder.Append(Me.Product)
        End If
        If Me.Version IsNot Nothing Then
            Stringbuilder.Append(Me.Version)
        End If
        If Me.Product IsNot Nothing Then
            Stringbuilder.Append(Me.Product)
        End If
        If Me.Manufacturer IsNot Nothing Then
            Stringbuilder.Append(Me.Manufacturer)
        End If
        If Me.Model IsNot Nothing Then
            Stringbuilder.Append(Me.Model)
        End If
        If Me.Processor IsNot Nothing Then
            Stringbuilder.Append(Me.Processor)
        End If
        If Me.BiosSN IsNot Nothing Then
            Stringbuilder.Append(Me.BiosSN)
        End If
        Return MemoryEncrypt.ProtectData(Stringbuilder.ToString())
    End Function


End Class

'<Serializable()> Friend Class Licence
'    Protected CompInfo As New CompInfo
'    Protected Keys As CspParameters
'    <Serializable()> Private Class Activated
'        Private SWEditor As Boolean
'        Private SWDocument As Boolean
'    End Class
'End Class

Public NotInheritable Class StoreKey

    Public Shared Sub GenKeySaveInContainer(ByVal containerName As String)
        ' Create the CspParameters object and set the key container 
        ' name used to store the RSA key pair.
        If ContainerName = "" OrElse ContainerName Is Nothing Then
            ContainerName = " "
        End If
        Dim cp As New CspParameters()

        cp.KeyContainerName = ContainerName

        ' Create a new instance of RSACryptoServiceProvider that accesses
        ' the key container MyKeyContainerName.
        Dim rsa As New RSACryptoServiceProvider(cp)
        rsa.PersistKeyInCsp = True

        ' Display the key information to the console.
        'Console.WriteLine("Key added to container:  {0}", rsa.ToXmlString(True))
    End Sub

    Public Shared Function GetFriNode3FromContainer(ByVal containerName As String) As String
        ' Create the CspParameters object and set the key container 
        '  name used to store the RSA key pair.
        Dim cp As New CspParameters()
        cp.KeyContainerName = ContainerName

        ' Create a new instance of RSACryptoServiceProvider that accesses
        ' the key container MyKeyContainerName.
        Dim rsa As New RSACryptoServiceProvider(cp)

        ' Display the key information to the console.
        'Console.WriteLine("Key retrieved from container : {0}", rsa.ToXmlString(True))
        Return rsa.ToXmlString(False)
    End Function

    Public Shared Sub DeleteKeyFromContainer(ByVal containerName As String)
        ' Create the CspParameters object and set the key container 
        '  name used to store the RSA key pair.
        Dim cp As New CspParameters()
        cp.KeyContainerName = ContainerName

        ' Create a new instance of RSACryptoServiceProvider that accesses
        ' the key container.
        Dim rsa As New RSACryptoServiceProvider(cp)

        ' Delete the key entry in the container.
        rsa.PersistKeyInCsp = False

        ' Call Clear to release resources and delete the key from the container.
        rsa.Clear()

        'Console.WriteLine("Key deleted.")
    End Sub

    Private Sub New()

    End Sub
End Class
<Serializable()> _
Public Class License
    Private _Level As String
    Public Property Level() As String
        Get
            Return _Level
        End Get
        Set(ByVal value As String)
            _Level = value
        End Set
    End Property
    Private _LicenseVersion As Integer
    Public Property LicenseVersion() As Integer
        Get
            Return _LicenseVersion
        End Get
        Set(ByVal value As Integer)
            _LicenseVersion = value
        End Set
    End Property

    Private _Owner As String
    Public Property Owner() As String
        Get
            Return _Owner
        End Get
        Set(ByVal value As String)
            _Owner = value
        End Set
    End Property
    Private _CompInfo As CompInfo
    Public Property CompInfo() As CompInfo
        Get
            Return _CompInfo
        End Get
        Set(ByVal value As CompInfo)
            _CompInfo = value
        End Set
    End Property
End Class

Public Class ProtData
    Friend entropy As Byte()
    Friend data As Byte()
    Friend bytesWritten As Integer
End Class
