Imports System.IO
Imports System.Runtime.Serialization
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO.Compression
Imports Microsoft.VisualBasic


Public Class Serialize

#Region "Serialization classes for save features"

    <Serializable()> _
    Private Class CompressedObject
        Public CompressedObject As Byte()
        Public Sub New(ByVal CompressedObject As Byte())
            Me.CompressedObject = CompressedObject
        End Sub
    End Class

    <Serializable()> _
    Private Class EncryptedObject
        Public EncryptedObject As Byte()
        Public Sub New(ByVal EncryptedObject As Byte())
            Me.EncryptedObject = EncryptedObject
        End Sub
    End Class

#End Region

    Private Shared Sub StreamSerialize(ByRef s As Stream, ByVal Obj As Object, Optional ByVal Compress As Boolean = True, Optional ByVal EncryptKey As String = Nothing)
        Dim b As New Formatters.Binary.BinaryFormatter
        If Compress = True Then
            'we want to compress this object to a byte array
            Obj = New CompressedObject(Compression.CompressByte(ByteArrSerialize(Obj)))
        End If
        If EncryptKey <> "" Then
            Obj = New EncryptedObject(Encryption.EncryptByte(ByteArrSerialize(Obj), EncryptKey))
        End If
        b.Serialize(s, Obj)
    End Sub

    Public Shared Sub FileSerialize(ByVal File As String, ByVal Obj As Object, Optional ByVal Compress As Boolean = True, Optional ByVal EncryptKey As String = Nothing)
        Using s As New FileStream(File, FileMode.Create, FileAccess.ReadWrite)
            StreamSerialize(s, Obj, Compress, EncryptKey)
            s.Close()
        End Using
    End Sub

    Public Shared Function FileDeserialize(ByVal File As String, Optional ByVal DecryptKey As String = Nothing) As Object
        FileDeserialize = Nothing
        Dim theError As Exception = Nothing
        Using s As New FileStream(File, FileMode.Open, FileAccess.Read)
            FileDeserialize = StreamDeserialize(s, DecryptKey)
            s.Close()
        End Using
    End Function

    Private Shared Function StreamDeserialize(ByVal s As Stream, Optional ByVal DecryptKey As String = Nothing) As Object
        Dim b As New Formatters.Binary.BinaryFormatter
        StreamDeserialize = b.Deserialize(s)

StartFileTypeCheck:
        'lets see if the file was compressed or Encrypted
        If TypeOf StreamDeserialize Is CompressedObject Then
            'we were compressed - so lets decompress
            StreamDeserialize = ByteArrDeserialize(Compression.DecompressByte(CType(StreamDeserialize, CompressedObject).CompressedObject))
            GoTo StartFileTypeCheck
        Else
            'no compression
            'return as is
        End If

        If TypeOf StreamDeserialize Is EncryptedObject Then
            'we were compressed - so lets decompress
            StreamDeserialize = ByteArrDeserialize(Encryption.DecryptByte(CType(StreamDeserialize, EncryptedObject).EncryptedObject, DecryptKey))
            GoTo StartFileTypeCheck
        Else
            'no compression
            'return as is
        End If
    End Function

    Private Shared Function ByteArrSerialize(ByVal Obj As Object) As Byte()
        Using MS As New MemoryStream
            Dim BF As New Formatters.Binary.BinaryFormatter
            BF.Serialize(MS, Obj)
            ByteArrSerialize = MS.ToArray
            MS.Close()
        End Using
    End Function

    Private Shared Function ByteArrDeserialize(ByVal SerializedData() As Byte) As Object
        Using MS As New MemoryStream(SerializedData)
            Dim BF As New Formatters.Binary.BinaryFormatter
            ByteArrDeserialize = BF.Deserialize(MS)
            MS.Close()
        End Using
    End Function

End Class
Public Class Compression

    Public Shared Function CompressByte(ByVal byteSource() As Byte) As Byte()
        ' Create a GZipStream object and memory stream object to store compressed stream
        Dim objMemStream As New MemoryStream()
        Dim objGZipStream As New GZipStream(objMemStream, CompressionMode.Compress, True)
        objGZipStream.Write(byteSource, 0, byteSource.Length)
        objGZipStream.Dispose()
        objMemStream.Position = 0
        ' Write compressed memory stream into byte array
        Dim buffer(objMemStream.Length) As Byte
        objMemStream.Read(buffer, 0, buffer.Length)
        objMemStream.Dispose()
        Return buffer
    End Function

    Public Shared Function DecompressByte(ByVal byteCompressed() As Byte) As Byte()

        Try
            ' Initialize memory stream with byte array.
            Dim objMemStream As New MemoryStream(byteCompressed)

            ' Initialize GZipStream object with memory stream.
            Dim objGZipStream As New GZipStream(objMemStream, CompressionMode.Decompress)

            ' Define a byte array to store header part from compressed stream.
            Dim sizeBytes(3) As Byte

            ' Read the size of compressed stream.
            objMemStream.Position = objMemStream.Length - 5
            objMemStream.Read(sizeBytes, 0, 4)

            Dim iOutputSize As Integer = BitConverter.ToInt32(sizeBytes, 0)

            ' Posistion the to point at beginning of the memory stream to read
            ' compressed stream for decompression.
            objMemStream.Position = 0

            Dim decompressedBytes(iOutputSize - 1) As Byte

            ' Read the decompress bytes and write it into result byte array.
            objGZipStream.Read(decompressedBytes, 0, iOutputSize)

            objGZipStream.Dispose()
            objMemStream.Dispose()

            Return decompressedBytes

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

End Class
Public Class Encryption

    Public Shared Function EncryptByte(ByVal Data As Byte(), ByVal Key As String) As Byte()
        Key = FixKey(Key, 16, 24, 32)
        Dim bytes() As Byte = ASCIIEncoding.ASCII.GetBytes(Key)
        Dim ms As New MemoryStream()
        Dim alg As Rijndael = Rijndael.Create()
        alg.Key = bytes
        alg.IV = New Byte() {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16}
        Dim cs As New CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write)
        cs.Write(Data, 0, Data.Length)
        cs.Close()
        Dim encryptedData As Byte() = ms.ToArray()
        Return encryptedData
    End Function

    Private Shared Function FixKey(ByVal Key As String, ByVal ParamArray FixLength() As Integer) As String
        Dim SortedLength = From xItem In FixLength Order By xItem
        For Each item In SortedLength
            If Len(Key) <= item Then
                If Key Is Nothing Then Key = " "
                Key = Key.PadRight(item)
                Exit For
            End If
        Next
        If Key.Length > FixLength(UBound(FixLength)) Then
            Key = Strings.Left(Key, FixLength(UBound(FixLength)))
        End If
        FixKey = Key
    End Function


    Public Shared Function DecryptByte(ByVal cipherData As Byte(), ByVal Key As String) As Byte()
        Try
            Key = FixKey(Key, 16, 24, 32)
            Dim bytes() As Byte = ASCIIEncoding.ASCII.GetBytes(Key)
            Dim ms As New MemoryStream()
            Dim alg As Rijndael = Rijndael.Create()
            alg.Key = bytes
            alg.IV = New Byte() {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16}
            Dim cs As New CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write)
            cs.Write(cipherData, 0, cipherData.Length)
            cs.Close()
            Dim decryptedData As Byte() = ms.ToArray()
            Return decryptedData
        Catch ex As Exception
            Throw New Exception("Error decrypting - please make sure the key is correct")
        End Try
    End Function


End Class
