using System;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyName("")]

#if !(MONOTOUCH || WindowsCE || SILVERLIGHT)
[assembly: AllowPartiallyTrustedCallers()]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, Execution = true)]
#endif
