FROM microsoft/aspnetcore-build as build-env

WORKDIR /src

COPY question-metrics-api/question-metrics-api.csproj question-metrics-api/
COPY question-metrics-data/question-metrics-data.csproj question-metrics-data/
COPY question-metrics-domain/question-metrics-domain.csproj question-metrics-domain/
COPY tests/question-metrics-domain-tests/question-metrics-domain-tests.csproj tests/question-metrics-domain-tests/

RUN dotnet restore tests/question-metrics-domain-tests
RUN dotnet test tests/question-metrics-domain-tests/question-metrics-domain-tests.csproj

RUN dotnet restore question-metrics-api/question-metrics-api.csproj
RUN dotnet publish question-metrics-api/question-metrics-api.csproj -c Release -o publish





