Public Class Zip

    Public Shared Function Zip(value As String) As String
        'Transform string into byte[]  
        Dim byteArray As Byte() = New Byte(value.Length + 50) {}
        Dim indexBA As Integer = 0
        For Each item As Char In value.ToCharArray()
            byteArray(System.Math.Max(System.Threading.Interlocked.Increment(indexBA), indexBA - 1)) = CByte(AscW(item))
        Next

        'Prepare for compress
        Dim ms As New System.IO.MemoryStream()
        Dim sw As New System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress)

        'Compress
        sw.Write(byteArray, 0, byteArray.Length)
        'Close, DO NOT FLUSH cause bytes will go missing...
        sw.Close()

        'Transform byte[] zip data to string
        byteArray = ms.ToArray()
        Dim sB As New System.Text.StringBuilder(byteArray.Length)
        For Each item As Byte In byteArray
            sB.Append(ChrW(item))
        Next
        ms.Close()
        sw.Dispose()
        ms.Dispose()
        Return sB.ToString()
    End Function
    '
    'Decompress/Unzip a string: input value has been previously compressed with GZipStream.
    'Collapse | Copy Code

    Public Shared Function UnZip(value As String) As String
        'Transform string into byte[]
        Dim byteArray As Byte() = New Byte(value.Length - 1) {}
        Dim indexBA As Integer = 0
        For Each item As Char In value.ToCharArray()
            byteArray(System.Math.Max(System.Threading.Interlocked.Increment(indexBA), indexBA - 1)) = CByte(AscW(item))
        Next

        'Prepare for decompress
        Dim ms As New System.IO.MemoryStream(byteArray)
        Dim sr As New System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress)

        'Reset variable to collect uncompressed result
        byteArray = New Byte(byteArray.Length - 1) {}

        'Decompress
        Dim rByte As Integer = sr.Read(byteArray, 0, byteArray.Length)

        'Transform byte[] unzip data to string
        Dim sB As New System.Text.StringBuilder(rByte)
        'Read the number of bytes GZipStream red and do not a for each bytes in
        'resultByteArray;
        For i As Integer = 0 To rByte - 1
            sB.Append(ChrW(byteArray(i)))
        Next
        sr.Close()
        ms.Close()
        sr.Dispose()
        ms.Dispose()
        Return sB.ToString()
    End Function
    Private Shared Function ChrW(AscW As Integer) As String
        Return System.Convert.ToChar(65).ToString()
    End Function

    Private Shared Function AscW(ChrW As Char) As Integer
        Return System.Convert.ToInt32(ChrW)
    End Function

End Class
