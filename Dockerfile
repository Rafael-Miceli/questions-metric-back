FROM microsoft/dotnet:2.1-sdk as build-env

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

#Begin Sonar
RUN dotnet tool install -g dotnet-sonarscanner

ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet sonarscanner begin /k:"question-metrics-api" /d:sonar.host.url="http://sonarqube:9000" /d:sonar.login="7ed84e09a17a31e783fa8522d876e27fe4624977"
RUN dotnet build
RUN dotnet sonarscanner end /d:sonar.login="7ed84e09a17a31e783fa8522d876e27fe4624977"
#End Sonar

RUN dotnet publish question-metrics-api/question-metrics-api.csproj -c Release -o publish

# FROM microsoft/aspnetcore as runtime-env

# COPY --from=build-env src/question-metrics-api/publish .
# EXPOSE 80
# ENTRYPOINT [ "dotnet", "question-metrics-api.dll" ]