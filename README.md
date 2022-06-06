# ShopsRUs
- to run the app, 
- run the following command in the project's root folder 

docker-compose up --build

- for sonarqube code analysis,
- open http://localhost:9000
- enter username and password (username:admğin password:admin)
- create a project with name "ShopsRUs"
- run the following command after replacing the generated token with the "<TOKEN>" keywords 
  
dotnet sonarscanner begin /k:"ShopsRUs" /d:sonar.host.url="http://localhost:9000"  /d:sonar.login="<TOKEN>" sonar.cs.vscoveragexml.reportsPaths="coverage.xml" /d:sonar.cs.vstest.reportsPaths="**\TestResults\*.trx"
dotnet build --no-incremental
dotnet-coverage collect 'dotnet test' -f xml  -o 'coverage.xml'
vstest.console.exe bin\Debug\net6.0\ShopsRUs.UnitTests.dll /ResultsDirectory:"TestResults" /Logger:trx
dotnet sonarscanner end /d:sonar.login="<TOKEN>"
