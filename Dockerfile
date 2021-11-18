FROM mcr.microsoft.com/dotnet/sdk:5.0 as build-sdk

WORKDIR /src
COPY ["src/OzonEdu.Merchandise/OzonEdu.Merchandise.csproj", "src/OzonEdu.Merchandise/"]

RUN dotnet restore "src/OzonEdu.Merchandise/OzonEdu.Merchandise.csproj"

COPY . .

WORKDIR "/src/src/OzonEdu.Merchandise"

RUN dotnet build "OzonEdu.Merchandise.csproj" -c  Release -o /app/build

FROM build-sdk as publish
RUN dotnet publish "OzonEdu.Merchandise.csproj" -c  Release -o /app/publish
COPY "entrypoint.sh" "/app/publish/."
FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime

WORKDIR /app

EXPOSE 80
EXPOSE 433

FROM runtime as final
WORKDIR /src
COPY --from=publish /app/publish .

RUN chmod +x entrypoint.sh
CMD /bin/bash entrypoint.sh