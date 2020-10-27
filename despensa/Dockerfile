FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY bin/Release/netcoreapp3.1/publish/ despensa/
WORKDIR /despensa
EXPOSE 9091
# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /despensa
ENTRYPOINT ["dotnet", "despensa.dll"]