using System;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyName("")]

#if !(MONOTOUCH || WindowsCE || SILVERLIGHT)
[assembly: AllowPartiallyTrustedCallers()]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, Execution = true)]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("FlickrNetTest, PublicKey=002400000480000094000000060200000024000052534131000400000100010039a991f658101cf82d418ece9ab591a8acd377989a62476f1d58198bed5af088625ca7b04abb869226a06c6dbaecc58366fd588b5319a42cc2734ed940cd23de0a0756465b3bab83ab7f43faa8719195981470cf0d5382815b5b2d372b4bfeedf08ba41678cd86a2ea62592a849c47e5fd58916cece1c3397694a6650668bfe1")]
#endif

