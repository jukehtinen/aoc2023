$day = (Get-Date).Day
$day = $day.ToString().PadLeft(2, '0')

dotnet new console -n Day$day
dotnet sln add .\Day$day\Day$day.csproj
New-Item -Path .\Day$day\input.txt -ItemType File