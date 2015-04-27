Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices
'The request is placed at the assembly level.
Imports System.Security.Permissions
<Assembly: FileIOPermission(SecurityAction.RequestMinimum, Unrestricted:=True)> 
<Assembly: CLSCompliant(True)> 

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes

<Assembly: AssemblyTitle("Settings")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("")> 
<Assembly: AssemblyProduct("Settings")> 
<Assembly: AssemblyCopyright("SignWriter Studio™ Copyright (C) 2009-2012 Jonathan Duncan All Rights Reserved")> 
<Assembly: AssemblyTrademark("SignWriter Studio™")> 

<Assembly: ComVisible(False)>

'The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("0a800379-1f0b-44b9-888d-fab26882e4d0")> 

' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:
' <Assembly: AssemblyVersion("1.0.*")> 

<Assembly: AssemblyVersion("1.0.0.0")> 
<Assembly: AssemblyFileVersion("1.0.0.0")> 
