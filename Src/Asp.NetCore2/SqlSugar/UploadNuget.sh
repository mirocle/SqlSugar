# !/bin/bash
cd ./bin/Debug
dotnet nuget push MT.SqlSugar.*.nupkg -k architecture -s http://172.19.50.115:15840/nuget;
