using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.EnterpriseServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("_ComNameToReplace_")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("DomConsult Sp. z o.o.")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("Copyright © 2022 All rights reserved")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("C26012BE-17D1-4497-992B-1B930E286E14")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("22.0.0.1")] //TODO: Wersja zgodna ze schematem Y.M.D.version_number
[assembly: AssemblyFileVersion("22.0.0.1")]
[assembly: ApplicationAccessControl(
        AccessChecksLevel = AccessChecksLevelOption.ApplicationComponent,
        Authentication = AuthenticationOption.Packet,
        ImpersonationLevel = ImpersonationLevelOption.Impersonate,
        Value = false)]
#if RELEASE
[assembly: ApplicationActivation(ActivationOption.Server)]
#endif