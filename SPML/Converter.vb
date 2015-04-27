Option Infer On
Option Strict On
Imports System
Imports System.Drawing
'Imports System.Windows.Forms
Imports System.IO
Imports System.Windows.Forms
Imports SignWriterStudio.SWClasses
Imports System.Text
Imports SignWriterStudio.SWS
Imports SignWriterStudio.General


Public NotInheritable Class SpmlConverter
    Public Function LoadSPML(ByVal xmlFilename As String, ByVal bw As System.ComponentModel.BackgroundWorker) As SPMLDataSet

        Dim spml1 As New SPMLDataSet
        ' Read the XML document back in. 
        ' Create new FileStream to read schema with.
        Dim fsReadXml As New FileStream _
            (xmlFilename, FileMode.Open)

        ' Create an XmlTextReader to read the file.
        Dim xmlReader As New Xml.XmlTextReader(fsReadXml)

        ' Read the XML document into the DataSet.
        xmlReader.XmlResolver = Nothing

        spml1.ReadXml(xmlReader)

        ' Close the XmlTextReader
        xmlReader.Close()
        If bw IsNot Nothing Then
            bw.ReportProgress(5)
        End If

        Return spml1

    End Function
    Public Function LoadSPML(ByVal xmlFilename As String) As SPMLDataSet

        Dim spml1 As New SPMLDataSet
        ' Read the XML document back in. 
        ' Create new FileStream to read schema with.
        Dim fsReadXml As New FileStream _
            (xmlFilename, FileMode.Open)

        ' Create an XmlTextReader to read the file.
        Dim xmlReader As New Xml.XmlTextReader(fsReadXml)

        ' Read the XML document into the DataSet.
        xmlReader.XmlResolver = Nothing

        spml1.ReadXml(xmlReader)

        ' Close the XmlTextReader
        xmlReader.Close()


        Return spml1

    End Function
    Public Function ImportSPML(ByVal xmlFilename As String, ByVal idSignLanguage As Integer, ByVal idCulture As Integer, ByVal bw As System.ComponentModel.BackgroundWorker) As SWCollection(Of SwSign)

        Dim spml1 As SPMLDataSet = LoadSPML(xmlFilename, bw)
        Return SpmlToSWSigns(spml1, idSignLanguage, idCulture, bw)
    End Function
    Public Function ImportSPML(ByVal xmlFilename As String, ByVal idSignLanguage As Integer, ByVal idCulture As Integer) As SWCollection(Of SwSign)

        Dim spml1 As SPMLDataSet = LoadSPML(xmlFilename)
        Return SpmlToSWSigns(spml1, idSignLanguage, idCulture)
    End Function
    Private Shared Function SpmlToSWSigns(ByVal spml As SPMLDataSet, ByVal idSignLanguage As Integer, ByVal idCulture As Integer, ByVal bw As System.ComponentModel.BackgroundWorker) As SWCollection(Of SwSign)
        Dim signs As New SWCollection(Of SwSign)
        Dim I As Integer
        Dim entries = spml.entry
        Dim count As Integer = entries.Rows.Count

        For Each entryRow As SPMLDataSet.entryRow In entries.Rows
            I += 1
            signs.Add(EntryToSWSign(entryRow, idSignLanguage, idCulture))
            If bw IsNot Nothing Then
                bw.ReportProgress(5 + CInt((I / count) * 5))
            End If
        Next

        Return signs
    End Function
    Private Shared Function SpmlToSWSigns(ByVal spml As SPMLDataSet, ByVal idSignLanguage As Integer, ByVal idCulture As Integer) As SWCollection(Of SwSign)
        Dim signs As New SWCollection(Of SwSign)
        Dim entries = spml.entry
        For Each entryRow As SPMLDataSet.entryRow In entries.Rows
            signs.Add(EntryToSWSign(entryRow, idSignLanguage, idCulture))

        Next

        Return signs
    End Function

    Private Shared Function EntryToSWSign(ByVal entryRow As SPMLDataSet.entryRow, ByVal idSignLanguage As Integer, ByVal idCulture As Integer) As SwSign
        Dim newSign As New SwSign
        Dim termRows As SPMLDataSet.termRow()
        Dim textRows As SPMLDataSet.textRow()
        Dim buildStr As String
        Dim sequenceStr As String
        Dim symbolsStr As String

        newSign.SetlanguageIso(idCulture)
        newSign.SetSignLanguageIso(idSignLanguage)
        newSign.SignPuddleId = entryRow.id
        newSign.BkColor = Color.White
        newSign.SWritingSource = UnEncodeXML(entryRow.src)
        If IsNumeric(entryRow.cdt) Then
            newSign.Created = EpochToDateTime(Convert.ToInt32(entryRow.cdt))
        End If
        If IsNumeric(entryRow.mdt) Then
            newSign.LastModified = EpochToDateTime(Convert.ToInt32(entryRow.mdt))
        End If
        newSign.SignPuddleUser = UnEncodeXML(entryRow.usr)
        Dim guid1 As Guid
        If Guid.TryParse(entryRow.uuid, guid1) Then
            newSign.SignWriterGuid = guid1
        End If
        newSign.PuddlePrev = UnEncodeXML(entryRow.prev)
        newSign.PuddleNext = UnEncodeXML(entryRow._next)
        newSign.PuddlePng = UnEncodeXML(entryRow.png)
        newSign.PuddleSvg = UnEncodeXML(entryRow.svg)
        newSign.PuddleVideoLink = UnEncodeXML(entryRow.video)


        termRows = entryRow.GettermRows
        If termRows.Count > 0 Then
            buildStr = GetBuildString(termRows)
            sequenceStr = buildStr.GetSequenceBuildStr()
            symbolsStr = buildStr.GetSymbolsBuildStr()

            Dim signText = SpmlTermsToSwSign(termRows)
            newSign.Gloss = signText.Gloss
            newSign.Glosses = signText.Glosses
            For Each Symbol In SpmlSymbolsToSwSymbols(symbolsStr)
                Symbol.Handcolor = Color.Black.ToArgb
                Symbol.Palmcolor = Color.White.ToArgb
                newSign.Frames(0).SignSymbols.Add(Symbol)
            Next
            Dim I As Integer
            For Each seq In SpmlSequence(sequenceStr)
                I += 1
                newSign.Frames(0).Sequences.Add(New SWSequence(seq, I))
            Next
        End If

        newSign.Frames(0).CenterSymbols()


        textRows = entryRow.GettextRows
        For Each txt In textRows
            newSign.PuddleText.Add(UnEncodeXML(txt.text_Text))
        Next
        Return newSign
    End Function
    Public Shared Function FswtoSwSign(ByVal fsw As String, ByVal idSignLanguage As Integer, ByVal idCulture As Integer) As SwSign
        Dim newSign As New SwSign
        Dim sequenceStr As String
        Dim symbolsStr As String

        newSign.SetlanguageIso(idCulture)
        newSign.SetSignLanguageIso(idSignLanguage)
        newSign.BkColor = Color.White
        newSign.SignWriterGuid = Guid.NewGuid

        sequenceStr = fsw.GetSequenceBuildStr()
        symbolsStr = fsw.GetSymbolsBuildStr()

        For Each Symbol In SpmlSymbolsToSwSymbols(symbolsStr)
            Symbol.Handcolor = Color.Black.ToArgb
            Symbol.Palmcolor = Color.White.ToArgb
            newSign.Frames(0).SignSymbols.Add(Symbol)
        Next
        Dim I As Integer

        For Each seq In SpmlSequence(sequenceStr)
            I += 1
            If SymbolExists(seq) Then
                newSign.Frames(0).Sequences.Add(New SWSequence(seq, I))
            End If
        Next

        newSign.Frames(0).CenterSymbols()

        Return newSign
    End Function

    Private Shared Function SymbolExists(ByVal seq As Integer) As Boolean
        Dim symbol = New SWSymbol()
        symbol.Code = seq
        Return symbol.IsValid
    End Function

    Public Shared Function FswtoSwSigns(ByVal fsw As String, ByVal idSignLanguage As Integer, ByVal idCulture As Integer) As List(Of SwSign)
        Dim signs As New List(Of SwSign)
        Dim sequenceStr As String
        Dim symbolsStr As String


        Dim fswArray = fsw.Split(New [Char]() {" "c, CChar(vbCrLf())}, StringSplitOptions.RemoveEmptyEntries)

        For Each FSWitem In fswArray
            Dim newSign = New SwSign
            newSign.SetlanguageIso(idCulture)
            newSign.SetSignLanguageIso(idSignLanguage)
            newSign.BkColor = Color.White
            newSign.SignWriterGuid = Guid.NewGuid

            sequenceStr = FSWitem.GetSequenceBuildStr()
            symbolsStr = FSWitem.GetSymbolsBuildStr()

            For Each Symbol In SpmlSymbolsToSwSymbols(symbolsStr)
                Symbol.Handcolor = Color.Black.ToArgb
                Symbol.Palmcolor = Color.White.ToArgb
                newSign.Frames(0).SignSymbols.Add(Symbol)
            Next
            Dim I As Integer

            For Each seq In SpmlSequence(sequenceStr)
                I += 1
                newSign.Frames(0).Sequences.Add(New SWSequence(seq, I))
            Next

            newSign.Frames(0).CenterSymbols()

            signs.Add(newSign)
        Next


        Return signs
    End Function
    Public Shared Function FswtoSwDocumentSigns(ByVal fsw As String, ByVal idSignLanguage As Integer, ByVal idCulture As Integer) As List(Of SwDocumentSign)
        Dim signs As New List(Of SwDocumentSign)
        Dim sequenceStr As String
        Dim symbolsStr As String


        Dim fswArray = fsw.Split(New [Char]() {" "c, CChar(vbCrLf())}, StringSplitOptions.RemoveEmptyEntries)

        For Each FSWitem In fswArray
            Dim newSign = New SwDocumentSign
            newSign.SetlanguageIso(idCulture)
            newSign.SetSignLanguageIso(idSignLanguage)
            newSign.BkColor = Color.White
            newSign.SignWriterGuid = Guid.NewGuid

            sequenceStr = FSWitem.GetSequenceBuildStr()
            symbolsStr = FSWitem.GetSymbolsBuildStr()

            If symbolsStr.StartsWith("L") Then newSign.Lane = AnchorStyles.Left
            If symbolsStr.StartsWith("M") Then newSign.Lane = AnchorStyles.None
            If symbolsStr.StartsWith("R") Then newSign.Lane = AnchorStyles.Right


            For Each Symbol In SpmlSymbolsToSwSymbols(symbolsStr)
                Symbol.Handcolor = Color.Black.ToArgb
                Symbol.Palmcolor = Color.White.ToArgb
                newSign.Frames(0).SignSymbols.Add(Symbol)
            Next
            Dim I As Integer

            For Each seq In SpmlSequence(sequenceStr)
                I += 1
                newSign.Frames(0).Sequences.Add(New SWSequence(seq, I))
            Next

            newSign.Frames(0).CenterSpmlSymbols(New Point(250, 250))

            signs.Add(newSign)
        Next


        Return signs
    End Function
    Private Shared Function SpmlTermsToSwSign(ByVal terms() As SPMLDataSet.termRow) As SignText

        Dim glosses As New StringBuilder(String.Empty)
        Dim gloss As String = String.Empty


        Dim listofTerms As List(Of String) = GetTermsNonBuild(terms)
        Dim count As Integer = listofTerms.Count
        Dim I As Integer
        For Each txt In listofTerms
            If I = 0 Then
                gloss = UnEncodeXML(Trim(txt))
            ElseIf I = count - 1 Then
                glosses.Append(UnEncodeXML(Trim(txt)))
            Else
                glosses.Append(UnEncodeXML(Trim(txt)))
                glosses.Append(", ")
            End If
            I += 1
        Next
        Return New SignText With { _
        .Gloss = gloss, _
        .Glosses = glosses.ToString _
        }

    End Function
    Private Class SignText
        Friend Gloss As String
        Friend Glosses As String
    End Class

    Private Shared Function SpmlSymbolsToSwSymbols(ByVal symbolsStr As String) As IEnumerable(Of SWSignSymbol)
        Dim sStr = SplitSymbolBuildStr(symbolsStr)

        Return (From s In sStr Select GetSignSymbol(s)).ToList()
    End Function

    Private Shared Function GetSignSymbol(str As String) As SWSignSymbol
        Dim symbol As New SWSignSymbol
        Dim symbolcodestr = Mid(str, 1, 5)
        Dim symbolcoordinate = Mid(str, 6, 7)
        Dim coordinate = GetCoordinate(symbolcoordinate)
        symbol.Code = GetSymbolCode(symbolcodestr)
        symbol.X = coordinate.X
        symbol.Y = coordinate.Y
        Return symbol
    End Function
    Friend Shared Function GetCoordinate(str As String) As Point
        Dim strArray = str.Split(CChar("x"))
        Return New Point(CInt(strArray(0)), CInt(strArray(1)))
    End Function
    Friend Shared Function GetSymbolCode(str As String) As Integer
        Dim uc1 = Mid(str, 1, 3)
        Dim uc2 = Mid(str, 4, 1)
        Dim uc3 = Mid(str, 5, 1)

        Dim base = CInt("&H" & uc1)
        Dim fill = CInt("&H" & uc2)
        Dim rotation = CInt("&H" & uc3)

        Dim code = 96 * (base - 256) + 16 * (fill) + rotation + 1
        '96 * (Base - 256) + 16 * (Fill) + Rotation

        Return code
    End Function
    Friend Shared Function SpmlSequence(ByVal sequenceStr As String) As List(Of Integer)
        Dim sStr = SplitSequenceBuildStr(sequenceStr)

        Return (From s In sStr Select GetSymbolCode(s)).ToList()
    End Function

    Friend Shared Function CpPointStr(ByVal point As Point) As String
        Dim str As String
        Dim strbuilder As New StringBuilder
        Dim x As String = point.X.ToString
        Dim y As String = point.Y.ToString
        x = x.Replace("-", "n")
        y = y.Replace(CChar("-"), CChar("n"))
        strbuilder.Append(x)
        strbuilder.Append("x")
        strbuilder.Append(y)
        str = Trim(strbuilder.ToString)

        Return str
    End Function

    Private Function SetupSPMLWriter(ByVal spmlFilename As String) As StreamWriter
        ' Create FileStream    
        Dim fs As New FileStream _
            (spmlFilename, FileMode.Create)
        Dim spmlWriter = New StreamWriter(fs, Encoding.UTF8)
        Return spmlWriter
    End Function

    Private _spmlWriter As StreamWriter
    Private Sub SpmlWrite(str As String)
        _spmlWriter.Write(str)
    End Sub

    Private Sub SpmlWriteLine(str As String)
        SpmlWrite(str & vbCrLf())
    End Sub





    'Public Function ExportSPML(ByVal SPMLFilename As String, puddle As String, ByVal Dictionary As SWDict, exportAll As Boolean, ByVal bw As  System.ComponentModel.BackgroundWorker) As Integer
    Function ExportSPML(myExportSettings As ExportSettings, dictionary As SWDict, bw As System.ComponentModel.BackgroundWorker) As Object

        _spmlWriter = SetupSPMLWriter(myExportSettings.Filename)

        Dim dt As DataTable
        If myExportSettings.EntireDictionary Then
            Dim dictionary1 As New SWDict
            dictionary1.BilingualMode = False
            dictionary1.DefaultSignLanguage = Dictionary.DefaultSignLanguage
            dictionary1.FirstGlossLanguage = Dictionary.FirstGlossLanguage
            dictionary1.SearchText("%")
            dt = CType(dictionary1.DictionaryBindingSource1.DataSource, DataTable)
        Else
            'Use current datatables
            dt = CType(Dictionary.DictionaryBindingSource1.DataSource, DataTable)
        End If



        SpmlWriteLine(CreateSPMLDocType())

        Const typ As String = "sgn"

        Dim tauisl As New SignWriterStudio.UI.swsuiDataSetTableAdapters.UISignLanguagesTableAdapter
        Dim guid As Guid = guid.Parse(CStr(tauisl.GetGUIDbyID(dictionary.DefaultSignLanguage)))
        Dim uuid As String = guid.ToString
        Dim cdt As String = DateTimeToEpoch(Date.UtcNow).ToString
        Dim mdt As String = DateTimeToEpoch(Date.UtcNow).ToString



        Dim exportedSigns As Integer = WriteSpmLtag(typ, CStr(myExportSettings.Puddle), myExportSettings.PuddleName, uuid, cdt, mdt, dictionary, dt, bw)
        _spmlWriter.Flush()
        _spmlWriter.Close()
        _spmlWriter = Nothing
        'MessageBox.Show("Exported " & ExportedSigns.ToString)
        Return exportedSigns
    End Function

    Private Sub AssignPuddleIds(dt As DataTable)
        Dim lastInteger As Integer = 0
        Dim idDict As New Dictionary(Of Integer, Integer)
        For Each row As DataRow In DT.Rows
            If IsNumeric(row("IDSignPuddle")) Then
                If Not idDict.ContainsKey(CInt(row("IDSignPuddle"))) Then
                    Dim id As Integer = CInt(row("IDSignPuddle"))
                    idDict.Add(id, id)
                Else
                    row("IDSignPuddle") = "" 'Blank if duplicate
                End If
            End If
        Next
        For Each row As DataRow In DT.Rows
            If IsDBNull(row("IDSignPuddle")) OrElse CStr(row("IDSignPuddle")) = String.Empty Then
                Do
                    lastInteger += 1
                Loop Until (Not idDict.ContainsKey(lastInteger))
                row("IDSignPuddle") = lastInteger
            End If
        Next

    End Sub

    Private Function CreateSPMLDocType() As String
        Dim sb As New StringBuilder

        'Write DOCTYPE
        sb.Append("<?xml version=")
        sb.Append(ControlChars.Quote)
        sb.Append("1.0")
        sb.Append(ControlChars.Quote)
        sb.Append(" ")
        sb.Append("encoding=")
        sb.Append(ControlChars.Quote)
        sb.Append("UTF-8")
        sb.Append(ControlChars.Quote)
        sb.AppendLine("?>")
        sb.Append("<!DOCTYPE spml SYSTEM ")
        sb.Append(ControlChars.Quote)
        sb.Append("http://www.signpuddle.net/spml_1.6.dtd")
        sb.Append(ControlChars.Quote)
        sb.Append(">")

        Return sb.ToString
    End Function

    Private Function WriteSpmLtag(typ As String, puddle As String, puddlename As String, uuid As String, cdt As String, mdt As String, dictionary As SWDict, dt As DataTable, bw As System.ComponentModel.BackgroundWorker) As Integer
        Dim sb As New StringBuilder
        '<spml root="http://www.signbank.org/signpuddle1.6" type="sgn" puddle="4" cdt="1203374232" mdt="1303894575" nextid="11172">

        sb.Append("<spml root=")
        sb.Append(ControlChars.Quote)
        sb.Append("SignWriter Studio™")
        sb.Append(ControlChars.Quote)
        sb.Append(" ")
        sb.Append("uuid=")
        sb.Append(ControlChars.Quote)
        sb.Append(uuid)
        sb.Append(ControlChars.Quote)
        sb.Append(" ")
        sb.Append("type=")
        sb.Append(ControlChars.Quote)
        sb.Append(EncodeXML(typ))
        sb.Append(ControlChars.Quote)
        sb.Append(" ")
        sb.Append("puddle=")
        sb.Append(ControlChars.Quote)
        sb.Append(EncodeXML(puddle))
        sb.Append(ControlChars.Quote)
        sb.Append(" ")
        sb.Append("cdt=")
        sb.Append(ControlChars.Quote)
        sb.Append(cdt)
        sb.Append(ControlChars.Quote)
        sb.Append(" ")
        sb.Append("mdt=")
        sb.Append(ControlChars.Quote)
        sb.Append(mdt)
        sb.Append(ControlChars.Quote)
        sb.Append(">")
        SpmlWriteLine(sb.ToString)
        SpmlWriteLine(CreateTerm(CdataWrap(puddlename)))
        Dim exportedSigns As Integer = WriteSPMLEntries(dictionary, dt, bw)
        SpmlWriteLine("</spml>")
        Return exportedSigns
    End Function
    Private Function WriteSPMLEntries(dictionary As SWDict, dt As DataTable, bw As System.ComponentModel.BackgroundWorker) As Integer
        Dim dictionarySign As SwSign
        Dim exportedSigns As Integer
        Dim totalCount = dt.Rows.Count
        Dim idSignPuddle As Integer
        Dim newPerc As Integer
        Dim oldPerc As Integer
        AssignPuddleIds(DT)
        If bw IsNot Nothing Then
            bw.ReportProgress(6)
        End If
        For Each row As DataRow In DT.Rows

            dictionarySign = dictionary.GetSWSignCached(CInt(row("IDDictionary")))

            If dictionarySign IsNot Nothing Then
                If Not dictionarySign.IsPrivate Then
                    'StackFrames for Export
                    Dim frametoExport As SWFrame
                    If dictionarySign.Frames.Count > 1 Then
                        frametoExport = dictionarySign.StackedFrame
                    Else
                        frametoExport = dictionarySign.Frames(0)
                    End If
                    frametoExport.CenterSpmlSymbols(New Point(500, 500))
                    idSignPuddle = CInt(row("IDSignPuddle"))
                    SpmlWriteLine(CreateBegEntryTag(dictionarySign, idSignPuddle))
                    CreateFirstTermTag(frametoExport)
                    SpmlWrite(CreateAdditionalTermsTag(dictionarySign))
                    SpmlWrite(CreateTextTags(dictionarySign))
                    SpmlWrite(CreateTag("png", Trim(dictionarySign.PuddlePng)))
                    SpmlWrite(CreateTag("svg", Trim(dictionarySign.PuddleSvg)))
                    SpmlWrite(CreateTag("video", Trim(dictionarySign.PuddleVideoLink)))
                    SpmlWrite(CreateTag("src", Trim(dictionarySign.SWritingSource)))
                    SpmlWriteLine(CreateEndEntryTag())
                    SpmlWriteLine(String.Empty)
                    exportedSigns += 1
                End If
                If bw IsNot Nothing Then
                    newPerc = CInt((exportedSigns * 94) / totalCount) + 6
                    If Not newPerc = oldPerc Then

                        bw.ReportProgress(newPerc)
                        oldPerc = newPerc
                    End If
                End If
            End If
        Next
        If bw IsNot Nothing Then
            bw.ReportProgress(100)
        End If
        'Empty Cache
        dictionary.GetSWSignCached(Long.MaxValue)
        Return exportedSigns
    End Function

    Private Sub CreateFirstTermTag(frame As SWFrame)
        If frame.SignSymbols.Count > 0 Or frame.Sequences.Count > 0 Then
            SpmlWrite("  <term>")
            WriteSequenceBuildString(frame)
            WriteSymbolBuildString(frame)
            SpmlWriteLine("</term>")
        End If
    End Sub
    Public Function GetFsw(ByVal sign As SwSign) As String
        Dim sb As New StringBuilder
        Dim frame As SWFrame = sign.Frames.FirstOrDefault
        frame.CenterSpmlSymbols(New Point(500, 500))
        sb.Append(CreateSequenceBuildString(frame))
        sb.Append(CreateSymbolBuildString(frame))

        Return sb.ToString
    End Function
    Public Function GetFsw(ByVal sign As SwDocumentSign) As String
        Dim sb As New StringBuilder
        Dim frame As SWFrame = sign.Frames.FirstOrDefault
        frame.CenterSpmlSymbols(New Point(500, 500))
        sb.Append(CreateSequenceBuildString(frame))
        sb.Append(CreateSymbolBuildString(frame, sign.Lane))

        Return sb.ToString
    End Function
    Private Function CreateAdditionalTermsTag(sign As SwSign) As String
        Const preSpace As String = "  "
        Dim sb As New StringBuilder
        sb.Append(preSpace)
        If Not Trim(sign.Gloss) = String.Empty Then
            sb.AppendLine(CreateTerm(CdataWrap(EncodeXML(Trim(sign.Gloss)))))
        End If
        If Not Trim(sign.Glosses) = String.Empty Then
            Dim glossArray = sign.Glosses.Split(CChar(","))
            For Each Gloss In From gloss1 In glossArray Where Not Trim(gloss1) = String.Empty
                sb.Append(preSpace)
                sb.AppendLine(CreateTerm(CdataWrap(EncodeXML(Trim(Gloss)))))
            Next
        End If
        If Not sign.Gloss = String.Empty OrElse Not sign.Glosses = String.Empty Then
            Return sb.ToString
        Else
            Return String.Empty
        End If
    End Function

    Private Function CreateTextTags(sign As SwSign) As String
        Dim sb As New StringBuilder
        If sign.PuddleText.Count > 0 Then
            For Each txt In sign.PuddleText
                If isSignBox(txt) Then
                    sb.Append(CreateTagNormal("text", txt))
                Else
                    sb.Append(CreateTag("text", txt))
                End If
            Next
            Return sb.ToString
        Else
            Return String.Empty
        End If
    End Function

    Private Function CdataWrap(textinMiddle As String) As String
        Return "<![CDATA[" & textinMiddle & "]]>"
    End Function
    Private Function CreateTerm(termInfo As String) As String
        Return "<term>" & termInfo & "</term>"
    End Function
    Private Function CreateTag(tagName As String, dataStr As String) As String
        If Not dataStr = String.Empty Then
            Dim sb As New StringBuilder
            sb.Append("  ")
            sb.Append("<")
            sb.Append(tagName)
            sb.Append(">")
            sb.Append(CdataWrap(EncodeXML(dataStr)))
            sb.Append("</")
            sb.Append(tagName)
            sb.AppendLine(">")
            Return sb.ToString
        Else
            Return String.Empty
        End If
    End Function
    Private Function CreateTagNormal(tagName As String, dataStr As String) As String
        If Not DataStr = String.Empty Then
            Dim sb As New StringBuilder
            sb.Append("  ")
            sb.Append("<")
            sb.Append(tagName)
            sb.Append(">")
            sb.Append(EncodeXML(DataStr))
            sb.Append("</")
            sb.Append(tagName)
            sb.AppendLine(">")
            Return sb.ToString
        Else
            Return String.Empty
        End If
    End Function
    Private Function CreateBegEntryTag(ByVal dictionarySign As SwSign, idSignPuddle As Integer) As String
        Dim nullDate As Date = Nothing
        Dim sb As New StringBuilder
        '<entry id="2" cdt="1172438870" mdt="1216741367" usr="admin">
        sb.Append("<entry id=")
        sb.Append(ControlChars.Quote)
        sb.Append(EncodeXML(IDSignPuddle.ToString()))
        sb.Append(ControlChars.Quote)
        sb.Append(" uuid=")
        sb.Append(ControlChars.Quote)
        sb.Append(dictionarySign.SignWriterGuid.ToString)
        sb.Append(ControlChars.Quote)
        If Not (dictionarySign.Created = #1/1/1970# OrElse dictionarySign.Created = nullDate) Then
            sb.Append(" cdt=")
            sb.Append(ControlChars.Quote)
            sb.Append(DateTimeToEpoch(dictionarySign.Created))
            sb.Append(ControlChars.Quote)
        End If
        If Not (dictionarySign.LastModified = #1/1/1970# OrElse dictionarySign.Created = nullDate) Then
            sb.Append(" mdt=")
            sb.Append(ControlChars.Quote)
            sb.Append(DateTimeToEpoch(dictionarySign.LastModified))
            sb.Append(ControlChars.Quote)
        End If
        If Not dictionarySign.SignPuddleUser = String.Empty Then
            sb.Append(" usr=")
            sb.Append(ControlChars.Quote)
            sb.Append(EncodeXML(dictionarySign.SignPuddleUser))
            sb.Append(ControlChars.Quote)
        End If
        If Not dictionarySign.PuddlePrev = String.Empty Then
            sb.Append(" prev=")
            sb.Append(ControlChars.Quote)
            sb.Append(EncodeXML(dictionarySign.PuddlePrev))
            sb.Append(ControlChars.Quote)
        End If
        If Not dictionarySign.PuddleNext = String.Empty Then
            sb.Append(" next=")
            sb.Append(ControlChars.Quote)
            sb.Append(EncodeXML(dictionarySign.PuddleNext))
            sb.Append(ControlChars.Quote)
        End If
        sb.Append(">")
        Return sb.ToString
    End Function

    Private Function CreateEndEntryTag() As String
        Return ("</entry>")
    End Function

    Private Sub WriteSymbolBuildString(ByVal frame As SWFrame)

        Dim symbol As SWSignSymbol
        Frame.SignSymbols.Sort()

        Dim ptu As PlainTextUnit
        Dim ptUs As New List(Of PlainTextUnit)
        'Put an M if not a punctuation
        If Not (Frame.SignSymbols.Count = 1 AndAlso IsPunctuationSymbol(Frame.SignSymbols(0).Code)) Then
            ''Todo Get Proper Bounds
            'Frame.CenterSPMLSymbols(New Point(500, 500))

            ptu = New PlainTextUnit With {.Type = PlainTextUnitType.M, .MaxCoordinates = SWFrame.GetMaxCoordinate(Frame)}
            ptUs.Add(ptu)
        End If
        For Each symbol In Frame.SignSymbols
            ptu = New PlainTextUnit
            ptu.FromSwsSignSymbol(symbol)
            ptUs.Add(ptu)
        Next
        WritePtUs(ptUs)
    End Sub
    Private Function CreateSymbolBuildString(ByVal frame As SWFrame, Optional lane As AnchorStyles = AnchorStyles.None) As String


        Dim symbol As SWSignSymbol
        Frame.SignSymbols.Sort()

        Dim ptu As PlainTextUnit
        Dim ptuType As PlainTextUnitType
        Dim ptUs As New List(Of PlainTextUnit)
        'Put an M if not a punctuation
        If Not (Frame.SignSymbols.Count = 1 AndAlso IsPunctuationSymbol(Frame.SignSymbols(0).Code)) Then

            ptuType = PlainTextUnitType.M
            If lane = AnchorStyles.Left Then
                ptuType = PlainTextUnitType.L
            End If
            If lane = AnchorStyles.Right Then
                ptuType = PlainTextUnitType.R
            End If
            ptu = New PlainTextUnit With {.Type = ptuType, .MaxCoordinates = SWFrame.GetMaxCoordinate(Frame)}
            ptUs.Add(ptu)

        End If
        For Each symbol In Frame.SignSymbols
            ptu = New PlainTextUnit
            ptu.FromSwsSignSymbol(symbol)
            ptUs.Add(ptu)
        Next

        Return CreatePtUs(ptUs)
    End Function

    Private Shared Function IsPunctuationSymbol(code As Integer) As Boolean
        Return code >= 62113 AndAlso code <= 62504
    End Function
    Friend Sub WritePtUs(ByVal ptUs As List(Of PlainTextUnit))
        SpmlWrite(CreatePtUs(ptUs))
    End Sub
    Friend Function CreatePtUs(ByVal ptUs As List(Of PlainTextUnit)) As String
        Dim sb As New StringBuilder

        Dim ptu As PlainTextUnit
        For Each ptu In ptUs
            If ptu.Type = PlainTextUnitType.L Or ptu.Type = PlainTextUnitType.M Or ptu.Type = PlainTextUnitType.R Then
                sb.Append(ptu.Type.ToString)
                sb.Append(CpPointStr(ptu.MaxCoordinates))
            ElseIf ptu.Type = PlainTextUnitType.Symbol Then
                sb.Append(ptu.SpmlSymbolString)
                sb.Append(CpPointStr(ptu.Coordinates))
            End If
        Next

        Return sb.ToString
    End Function
    Private Sub WriteSequenceBuildString(ByVal frame As SWFrame)
        SpmlWrite(CreateSequenceBuildString(frame))
    End Sub
    Private Function CreateSequenceBuildString(ByVal frame As SWFrame) As String
        Dim sb As New StringBuilder
        Dim sequence As SWSequence
        Dim symbol As SWSymbol

        Dim ptu As PlainTextUnit
        frame.Sequences.Sort()
        If frame.Sequences.Count > 0 Then
            sb.Append("A")
            For Each sequence In frame.Sequences
                ptu = New PlainTextUnit
                symbol = New SWSymbol With {.Code = sequence.Code}
                ptu.FromSwsSymbol(symbol)
                sb.Append(ptu.SpmlSymbolString)
            Next
        End If
        Return (sb.ToString)
    End Function
    Friend Class PlainTextUnit
        Public Type As PlainTextUnitType
        Public MaxCoordinates As Point
        Public Coordinates As Point
        Public Base As Integer
        Public Fill As Integer
        Public Rotation As Integer

        Public Sub FromSwsSignSymbol(ByVal symbol As SWSignSymbol)
            FromSwsSymbol(symbol.SymbolDetails)
            Coordinates.X = symbol.X
            Coordinates.Y = symbol.Y
        End Sub
        Public Sub FromSwsSymbol(ByVal symbol As SWSymbol)
            Fill = symbol.Fill
            Rotation = symbol.Rotation
            Base = CInt((symbol.Code - 1 - symbol.Rotation - symbol.Fill * 16) / 96) + 256  'Symbol.BaseGroup + 256 
            '   (Code - 1 - Rotation - Fill * 16 ) /96 =     (Base - 256) 
        End Sub

        Function SpmlSymbolString() As String
            Dim sb As New StringBuilder
            sb.Append("S")
            sb.Append(Hex(Base).ToLower)
            sb.Append(Hex(Fill - 1).ToLower)
            sb.Append(Hex(Rotation - 1).ToLower)
            Return sb.ToString
        End Function

    End Class
    Enum PlainTextUnitType
        Symbol
        A
        M
        L
        R
    End Enum

    Public Sub CleanImportedSigns(signs As SWCollection(Of SwSign))
        For Each Sign In Signs
            CleanSign(Sign)
        Next
    End Sub
    Private Sub CleanSign(sign As SwSign)
        Dim symbolstoRemove As New List(Of SWSignSymbol)
        Dim sequencestoRemove As New List(Of SWSequence)
        Using outfile As New StreamWriter(My.Application.Info.DirectoryPath & "\Import Symbol Errors.txt", True)
            For Each Frame In sign.Frames
                For Each Symbol In Frame.SignSymbols
                    If Not Symbol.IsValid Then
                        WriteSignError(sign, Symbol.Code, outfile)
                        symbolstoRemove.Add(Symbol)
                    End If
                Next
                For Each Sequence In Frame.Sequences
                    If Not Sequence.IsValid Then
                        WriteSignError(sign, Sequence.Code, outfile)
                        sequencestoRemove.Add(Sequence)
                    End If
                Next
                For Each Symbol In symbolstoRemove
                    Frame.SignSymbols.Remove(Symbol)
                Next
                For Each Sequence In sequencestoRemove
                    Frame.Sequences.Remove(Sequence)
                Next
            Next
        End Using
    End Sub
    Private Sub WriteSignError(sign As SwSign, code As Integer, outFile As TextWriter)

        Dim sb As New StringBuilder
        sb.Append("Sign ")
        sb.Append(sign.SignPuddleId)
        sb.Append(" ")
        'sb.Append(Sign.SignWriterGuid)
        'sb.Append(" ")
        sb.Append(sign.Gloss)
        sb.Append(",")
        sb.Append(sign.Glosses)
        sb.Append(" has symbol code ")
        sb.Append(code)
        sb.Append(" which is not valid.")

        outFile.WriteLine(sb.ToString())


        Console.WriteLine(sb.ToString)
    End Sub



End Class

Public Class ExportSettings
    Property Puddle As Integer
    Property EntireDictionary As Boolean
    Property PuddleName As String
    Property Filename As String
End Class