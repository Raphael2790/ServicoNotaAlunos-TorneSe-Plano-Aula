# Comando para adicionar projetos a solução vazia
``` bash
A partir de uma solução criada digitamos o comando para adicionar a referencia ao csproj dos projetos
    dotnet sln add src\TorneSe.ServicoNotaAluno.Worker\TorneSe.ServicoNotaAluno.Worker.csproj
    dotnet sln remove src\TorneSe.ServicoNotaAluno.Worker\TorneSe.ServicoNotaAluno.Worker.csproj  
 ```

 # Comando para subir container Docker configurado
- docker run -p 5432:5432 -v /c/Users/raphael.silvestre/Documents/database:/var/lib/postgresql/data -e POSTGRES_PASSWORD=1234 -e POSTGRES_USER=torneSe -e POSTGRES_DB=TorneSeDb -d postgres