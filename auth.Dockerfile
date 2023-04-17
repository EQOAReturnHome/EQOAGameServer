FROM ubuntu:22.04

COPY . .

# Setup base packages
# https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu -- dotnet setup
RUN apt-get update -y \
    && apt-get install wget -y \
    && apt-get install -y apt-transport-https \
    && apt-get install -y dotnet-sdk-6.0 \
    && apt-get update

RUN dotnet build AuthenticationServer/Authserver.csproj

EXPOSE 9735

CMD ["dotnet", "AuthenticationServer/bin/Debug/net6.0/Authserver.dll"]
