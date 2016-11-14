Option Infer On
Option Strict On
Imports SignWriterStudio.SWClasses

#Const NUnitTest = True   ' NUnit Tests on
#Const AssertTest = True    ' Assertion rules on
Imports NUnit.Framework
'Imports NUnit.Framework.Constraints
'Imports SPML

'Must disable setting LanguageId and SignLanguageID in SWSign for unit test to run.
<TestFixture()> _
Public Class ConverterTests
    Inherits AssertionHelper
    Dim SC As New SPMLConverter
    Dim TestSigns1 As List(Of SwSign)
    Dim BW As New ComponentModel.BackgroundWorker
    Dim TestFilename As String
    '    <TestFixtureSetUp()> _
    '    Public Sub ImportSPMLSetup()
    '        BW.WorkerReportsProgress = True
    '        TestFilename = "M:\My Documents\SignWriterStudio\SPML\data\test.spml"
    '        TestSigns1 = SC.ImportSPML(TestFilename, 54, 4, BW)
    '    End Sub

    <Test()> _
    Public Sub ImportSPMLTest1()
        Assert.AreEqual("cl-1 dropping", TestSigns1(0).Gloss)
    End Sub
    <Test()> _
    Public Sub ImportSPMLTest2()
        Assert.AreEqual("gl1, gl2", TestSigns1(0).Glosses)
    End Sub
    '    <Test()> _ 'Cannot run because unit test can not access Language an SL Ids
    '    Public Sub ImportSPMLTest3()
    '        Assert.AreEqual("us", TestSigns1(0).LanguageIso)
    '    End Sub
    '    <Test()> _
    '    Public Sub ImportSPMLTest4()
    '        Assert.AreEqual("ASL", TestSigns1(0).SignLanguageIso)
    '    End Sub
    <Test()> _
    Public Sub ImportSPMLTest5()
        Assert.AreEqual(Drawing.Color.White, TestSigns1(0).BKColor)
    End Sub
    <Test()> _
    Public Sub ImportSPMLTest6()
        Assert.AreEqual("7900", TestSigns1(0).SignPuddleId)
    End Sub
    <Test()> _
    Public Sub ImportSPMLTest7()
        Assert.AreEqual("Vanessa J.", TestSigns1(0).SWritingSource)
    End Sub
    <Test()> _
    Public Sub ImportSPMLTest8()
        Assert.AreEqual(CDate("2008-05-22 20:04:12.000"), TestSigns1(0).Created)
    End Sub
    <Test()> _
    Public Sub ImportSPMLTest9()
        Assert.AreEqual(CDate("2008-07-29 17:08:24.000"), TestSigns1(0).LastModified)
    End Sub
    <Test()> _
    Public Sub ImportSPMLTest10()
        Assert.AreEqual("129.21.228.108", TestSigns1(0).SignPuddleUser)
    End Sub
    <Test()> _
    Public Sub ImportSPMLTest11()
        Assert.AreEqual("", TestSigns1(0).PuddleNext) 'Error should be 11237 but problem with xsd column name next changed to _next
    End Sub

    <Test()> _
    Public Sub ImportSPMLTest12()
        Assert.AreEqual("11237", TestSigns1(0).PuddlePrev)
    End Sub

    <Test()> _
    Public Sub ImportSPMLTest13()
        Assert.AreEqual("png", TestSigns1(0).PuddlePNG)
    End Sub
    <Test()> _
    Public Sub ImportSPMLTest14()
        Assert.AreEqual("svg", TestSigns1(0).PuddleSVG)
    End Sub
    <Test()> _
    Public Sub ImportSPMLTest15()
        Assert.AreEqual("video link", TestSigns1(0).PuddleVideoLink)
    End Sub
    <Test()> _
    Public Sub ImportSPMLTest16()
        Assert.AreEqual(Guid.Parse("afb227e1-32d7-4a02-ab5d-7bff80d556c9"), TestSigns1(0).SignWriterGuid)

    End Sub

    <Test()> _
    Public Sub GetSymbolCodeTest1()
        Assert.AreEqual(2021, SPMLConverter.GetSymbolCode("11504"))

    End Sub
    <Test()> _
    Public Sub GetSymbolCodeTest2()
        Assert.AreEqual(1, SPMLConverter.GetSymbolCode("10000"))

    End Sub
    <Test()> _
    Public Sub GetCoordinateTest1()
        Assert.AreEqual(1, SPMLConverter.GetCoordinate("1x1").X)

    End Sub
    <Test()> _
    Public Sub GetCoordinateTest2()
        Assert.AreEqual(1, SPMLConverter.GetCoordinate("1x1").Y)

    End Sub
    <Test()> _
    Public Sub GetCoordinateTest3()
        Assert.AreEqual(250, SPMLConverter.GetCoordinate("250x749").X)

    End Sub
    <Test()> _
    Public Sub GetCoordinateTest4()
        Assert.AreEqual(749, SPMLConverter.GetCoordinate("250x749").Y)

    End Sub
    
    <Test()> _
    Public Sub isSignBoxTest2()
        Dim StringtoTest = "AS10040S22b04M509x534S10040492x466S22b04493x504"
        Assert.AreEqual(True, StringParser.isSignBox(StringtoTest))

    End Sub
    <Test()> _
    Public Sub isBuildStringTest1()
        Dim StringtoTest = "AS10040S22b04M509x534S10040492x466S22b04493x504"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))

    End Sub
    <Test()> _
    Public Sub isBuildStringTest2()
        Dim StringtoTest = "AS10020S14c1aS21000S26506M538x518S14c1a462x482S10020489x488S26506523x483S21000500x483"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))

    End Sub
    <Test()> _
    Public Sub isBuildStringTest3()
        Dim StringtoTest = "AS11851S15a57S20500S2e806S22a04M527x532S15a57486x509S11851496x491S2e806472x467S22a04483x484S20500485x501"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))

    End Sub
    <Test()> _
    Public Sub isBuildStringTest4()
        Dim StringtoTest = "M508x515S11a20493x485"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))

    End Sub
    <Test()> _
    Public Sub isBuildStringTest5()
        Dim StringtoTest = "AS31500M518x517S31500482x482"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))

    End Sub
    <Test()> _
    Public Sub isBuildStringTest6()
        Dim StringtoTest = "M521x573S10008490x543S10b11491x542S30c00482x488S30300482x477S2e802447x534S20500487x527"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))

    End Sub
    <Test()> _
    Public Sub isBuildStringTest7()
        Dim StringtoTest = "AS30122M518x524S30122482x476"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))

    End Sub
    <Test()> _
    Public Sub isBuildStringTest8()
        Dim StringtoTest = "M509x524S10044494x476S20500490x513 M525x522S1f701474x478S1f709504x478S37600488x494S20500495x511 M508x537S10040493x463S26600492x507 S38800464x496"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))

    End Sub
    <Test()> _
    Public Sub isBuildStringTest9()
        Dim StringtoTest = "M525x515S14c20476x484S2880a503x497 M539x516S15d0a461x484S15d02475x487S2e700510x491S26500525x501S20500529x485 S38900464x493 M549x517S30007482x482S18010519x483S18018446x484S22104518x470S22104465x470 M513x518S15d02486x499S20500494x482 M525x523S1eb10501x477S15d0a475x477S2450c478x492 M556x528S17610509x512S17614480x512S14c20533x472S14c28445x471S2880a509x492S28812472x491S2fb00493x484 S38800464x496 M513x517S15d02486x498S20500495x482 M528x547S18620509x457S18628472x457S20500495x452S2df1e474x496S2df06507x497S20320513x532S20328474x530 M518x556S28802452x507S32403482x482S10010473x524S10029436x535S28812417x518S2fb01436x494 L516x550S17610494x450S17610493x534S11502484x479S19220495x503 L523x529S10028477x499S1dc20499x489S22e04505x471 S38800464x496 M521x518S10033491x481S20500478x507 M552x517S2ff00482x482S18510527x488S18508450x486S26a00520x468S26a10452x465S2fb00n507x4973 M538x515S1f720462x499S20320493x499S1dc20514x485 M524x513S18530499x498S26506475x492S22104503x486 M547x573S10021517x427S10029453x427S2eb08515x463S2eb14476x463S2fd00491x453S1eb10488x522S15d0a466x526S2450c468x542"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))

    End Sub

    <Test()> _
    Public Sub isBuildStringTest10()
        '        Dim StringtoTest = "AS11512S1151aS27102S2711aM53502x50536S11512502x505S1151a468x521S27102511x464S2711a477x476"
        Dim StringtoTest = "AS11512S1151aS27102S2711aM502x536S11512502x505S1151a468x521S27102511x464S2711a477x476"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))
        'motel
    End Sub
    <Test()> _
    Public Sub isBuildStringTest11()
        '        Dim StringtoTest = "AS15a12S15a1aS20600S20600S37906S37906M53504x50519S37906494x483S37906467x505S15a12467x482S15a1a504x505S20600510x485S20600466x508"
        Dim StringtoTest = "AS15a12S15a1aS20600S20600S37906S37906M504x519S37906494x483S37906467x505S15a12467x482S15a1a504x505S20600510x485S20600466x508"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))
        ' desk
    End Sub
    <Test()> _
    Public Sub isBuildStringTest12()
        Dim StringtoTest = "AS15a51S15a57S26606S26612M53503x50521S15a57488x480S15a51489x480S26606503x505S26612n3503x505"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))
        'no-more 
    End Sub
    <Test()> _
    Public Sub isBuildStringTest13()
        Dim StringtoTest = "AS18510S18518S26500S26510S2ff00S15a40S15a48S22a04S22a14S2fb04M54506x5584S2ff00482x482S18510521x489S18518452x492S26500525x467S26510458x468S15a40507x523S15a48481x524S22a04506x558S22a14480x559S2fb04492x578"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))
        'teacher 
    End Sub
    <Test()> _
    Public Sub isBuildStringTest14()
        Dim StringtoTest = "AS1920cS15d3aS20500M51509x50516S15d3a482x492S1920c496x485S20500509x505"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))
        'it 
    End Sub

    <Test()> _
    Public Sub isBuildStringTest15()
        Dim StringtoTest = "AS1f710S2e300S2f900S33200M52507x50558S33200482x482S2f900495x524S2e300488x531S1f710507x505"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))
        'African 
    End Sub
    <Test()> _
    Public Sub isBuildStringTest16()
        Dim StringtoTest = "AS20301S20309S37600S20321S20329S22b07S22b11M54503x50545S20301474x506S20309503x505S37600487x522S20321522x457S20329456x455S22b0750"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))
        ' 
    End Sub
    <Test()> _
    Public Sub isBuildStringTest17()
        Dim StringtoTest = "AS1920cS15d3aS20500M51509x50516S15d3a482x492S1920c496x485S20500509x505"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))
        'it 
    End Sub
    <Test()> _
    Public Sub isBuildStringTest18()
        Dim StringtoTest = "AS1f710S2e300S2f900S33200M52507x50558S33200482x482S2f900495x524S2e300488x531S1f710507x505"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))
        ' African
    End Sub
    <Test()> _
    Public Sub isBuildStringTest19()
        Dim StringtoTest = "MS10044494x476S20500490x513 MS1f701474x478S1f709504x478S37600488x494S20500495x511 MS10040493x463S26600492x507 S38800464x496"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))
    End Sub
    <Test()> _
    Public Sub isSignBoxTest20()
        Dim StringtoTest = "MS10044494x476S20500490x513 MS1f701474x478S1f709504x478S37600488x494S20500495x511 MS10040493x463S26600492x507 S38800464x496"
        Assert.AreEqual(True, StringParser.isSignBox(StringtoTest))
    End Sub
    <Test()> _
    Public Sub isSignBoxTest19()
        Dim StringtoTest = "AS20301S20309S37600S20321S20329S22b07S22b11M54503x50545S20301474x506S20309503x505S37600487x522S20321522x457S20329456x455S22b07500x475S22b11477x475"
        Assert.AreEqual(False, StringParser.isSignBox(StringtoTest))
        'Salvation 
    End Sub
    <Test()> _
    Public Sub isBuildStringTest20()
        Dim StringtoTest = "AS1f710S2e300S33200M52507x50547S33200482x482S1f710507x505S2e300488x520"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))
        'Africa 
    End Sub

    <Test()> _
    Public Sub isBuildStringTest21()
        Dim StringtoTest = "AS20311S20313S20500S26a20M51507x50527S20313483x506S20311490x489S20500507x505S26a20488x473"
        Assert.AreEqual(True, StringParser.isBuildString(StringtoTest))
        'baseball 
    End Sub

    <Test()> _
    Public Sub isBuildStringTest22()
        Dim StringtoTest = "M56502x50530S11051502x505S11051533x498S2d600504x486S2d610472x494S11851466x470S11851438x482"
        Assert.AreEqual(False, StringParser.isSignBox(StringtoTest))
        'animal jumping, animal leaping 
    End Sub
<Test()> _
Public Sub isBuildStringTest23()
        Dim StringtoTest = "S38800464x496"
        Assert.AreEqual(False, StringParser.isBuildString(StringtoTest))
        'Punkt 
End Sub
<Test()> _
Public Sub isPunctuationTest1()
        Dim StringtoTest = "S38800464x496"
        Assert.AreEqual(True, StringParser.isPunctuation(StringtoTest))
        'Punkt 
End Sub
<Test()> _
Public Sub isSignBoxTest1()
        Dim StringtoTest = "S38800464x496"
        Assert.AreEqual(True, StringParser.isSignBox(StringtoTest))
        'Punkt 
    End Sub
    <Test()> _
    Public Sub EscapeXmlTest1()
        Dim StringtoTest = "<>""'&"
        Assert.AreEqual("&lt;&gt;&quot;&apos;&amp;", StringParser.EncodeXML(StringtoTest))
    End Sub
    <Test()> _
    Public Sub UnEscapeXmlTest1()
        Dim StringtoTest = "&lt;&gt;&quot;&apos;&amp;"
        Assert.AreEqual("<>""'&", StringParser.UnEncodeXML(StringtoTest))
    End Sub
End Class

