# ShopsRUs
- Database UML Diagram; [ShopsRUsDiagram1.png ](https://github.com/ozkan15/ShopsRUs/blob/master/ShopsRUsDiagram1.png)

- to run the app
- run the following command in the project's root folder or start the docker-compose startup project from visual studio

docker-compose up --build

- after docker container is up, open http://localhost:9000 for sonarqube code analysis 
- enter username and password (username:admin password:admin)
- create a project with name "ShopsRUs"
- run the following commands in the ShopsRUs.UnitTest project root folder after replacing the generated token with the "<<GENERATED_TOKEN>>" keywords 
  
dotnet tool install --global dotnet-sonarscanner

dotnet sonarscanner begin /k:"ShopsRUs" /d:sonar.host.url="http://localhost:9000"  /d:sonar.login="<<GENERATED_TOKEN>>" /d:sonar.cs.vscoveragexml.reportsPaths="coverage.xml" /d:sonar.cs.vstest.reportsPaths="**\TestResults\*.trx"
  
dotnet build --no-incremental
  
dotnet-coverage collect 'dotnet test' -f xml  -o 'coverage.xml'
  
vstest.console.exe bin\Debug\net6.0\ShopsRUs.UnitTests.dll /ResultsDirectory:"TestResults" /Logger:trx
  
dotnet sonarscanner end /d:sonar.login="<<GENERATED_TOKEN>>"
