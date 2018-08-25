FROM microsoft/dotnet:2.1-sdk as build-env

RUN apt-get update && apt-get dist-upgrade -y && apt-get install -y openjdk-8-jre

#Install Sonar
RUN dotnet tool install -g dotnet-sonarscanner

ENV PATH="${PATH}:/root/.dotnet/tools"

WORKDIR /src

COPY question-metrics-api/question-metrics-api.csproj question-metrics-api/
COPY question-metrics-data/question-metrics-data.csproj question-metrics-data/
COPY question-metrics-domain/question-metrics-domain.csproj question-metrics-domain/
COPY tests/question-metrics-domain-tests/question-metrics-domain-tests.csproj tests/question-metrics-domain-tests/

RUN dotnet restore tests/question-metrics-domain-tests
RUN dotnet restore question-metrics-api/question-metrics-api.csproj

COPY . .

#Run tests
RUN dotnet test tests/question-metrics-domain-tests/question-metrics-domain-tests.csproj

RUN dotnet sonarscanner begin /k:"question-metrics-api" /d:sonar.organization="rafael-miceli-github" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="8dc982d90a4205b1df9aab2d608055ca049fddb2"
RUN dotnet build
RUN dotnet sonarscanner end /d:sonar.login="8dc982d90a4205b1df9aab2d608055ca049fddb2"
#End Sonar

RUN dotnet publish question-metrics-api/question-metrics-api.csproj -c Release -o publish

FROM microsoft/dotnet:2.1-aspnetcore-runtime as runtime-env

COPY --from=build-env src/question-metrics-api/publish .
EXPOSE 80
ENTRYPOINT [ "dotnet", "question-metrics-api.dll" ]
