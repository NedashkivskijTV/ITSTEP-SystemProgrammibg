echo off

echo Build module auto.cs
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /t:module auto.cs

echo Build module sportcar.cs
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /t:library /addmodule:auto.netmodule /out:car.dll sportcar.cs fuel.cs

echo Build module Client.cs
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /r:car.dll Client.cs

pause