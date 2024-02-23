dotnet publish HomeWatcher.csproj -r linux-arm64 --configuration Release --output <project_path>\bin\publish\
#first copy with whole dependencies
pscp -pw <password> -r <project_path>\bin\publish\ pi@<ipaddress>:/home/pi/test/home_watcher/