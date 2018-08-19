FROM microsoft/aspnetcore-build as build

COPY ./question-metrics-api/question-metrics-api.csproj question-metrics-api/
COPY ./question-metrics-data/question-metrics-data.csproj question-metrics-data/
COPY ./question-metrics-domain/question-metrics-domain.csproj question-metrics-domain/
COPY ./tests/question-metrics-domain-tests/question-metrics-domain-tests.csproj tests/question-metrics-domain-tests/




