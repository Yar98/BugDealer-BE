#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /usr/lib/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /usr/lib/ssl/openssl.cnf

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
#COPY Bug.API/Bug.sln ./Bug.API/Bug.sln
COPY Bug.API ./Bug.API/
COPY Bug.Data ./Bug.Data/
COPY Bug.Entities ./Bug.Entities/
COPY Bug.Core ./Bug.Core/
COPY DumbTest ./DumbTest/
RUN dotnet restore Bug.API/Bug.sln
WORKDIR "/src/Bug.API"
RUN dotnet build "Bug.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bug.sln" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bug.API.dll"]