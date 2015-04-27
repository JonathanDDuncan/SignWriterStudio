Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Security.Permissions
'The request is placed at the assembly level.
<Assembly: FileIOPermission(SecurityAction.RequestMinimum, Unrestricted:=True)> 
<Assembly: CLSCompliant(True)> 
<Assembly: ObfuscateAssemblyAttribute(False, StripAfterObfuscation:=False)> 
<Assembly: Obfuscation(Feature:="encrypt symbol names with password SWS01Si02gnWrit03ing", Exclude:=False)> 

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes

<Assembly: AssemblyTitle("SWEditor")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("")> 
<Assembly: AssemblyProduct("SWEditor")> 
<Assembly: AssemblyCopyright("SignWriter Studio™ Copyright (C) 2009-2012 Jonathan Duncan All Rights Reserved")> 
<Assembly: AssemblyTrademark("SignWriter Studio™")> 

<Assembly: ComVisible(False)>

'The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("3cea2133-2289-4257-94ec-8c719a528381")> 

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
