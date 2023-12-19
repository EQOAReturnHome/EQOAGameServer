FROM ubuntu:22.04

COPY . .

# Setup base packages
# https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu -- dotnet setup
RUN apt-get update -y \
    && apt-get install wget -y \
    && apt-get install -y apt-transport-https \
    && apt-get install -y dotnet-sdk-6.0 \
    && apt-get update

# sets paths for both linux and windows
COPY ReturnHome/EQOAProto-C-Sharp/Scripts/ /Scripts\\
COPY ReturnHome/EQOAProto-C-Sharp/Scripts/ /Scripts

# build dll
RUN dotnet build ReturnHome/EQOAProto-C-Sharp/ReturnHome.csproj

# publish dll
RUN dotnet publish -c Release -o out ReturnHome/EQOAProto-C-Sharp/ReturnHome.csproj

EXPOSE 10070/udp

CMD ["dotnet", "out/ReturnHome.dll"]
