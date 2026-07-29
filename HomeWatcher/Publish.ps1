dotnet publish HomeWatcher.csproj -r linux-arm64 --configuration Release --output D:\External\RasberryPiHome\HomeWatcher\bin\publish\
#first copy with whole dependencies
pscp -pw admin1820136 -r D:\External\RasberryPiHome\HomeWatcher\bin\publish\HomeWatcher* pi@192.168.50.86:/home/pi/test/home_watcher/