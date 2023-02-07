echo off

echo Build module auto.cs
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /t:module auto.cs

echo Build module sportcar.cs
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /t:module sportcar.cs

echo Build module fuel.cs
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /t:module fuel.cs

echo Build module sportcar.cs
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /t:library /addmodule:auto.netmodule /addmodule:sportcar.netmodule /addmodule:fuel.netmodule /out:car.dll

echo Build module Client.cs
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /r:car.dll Client.cs

pause