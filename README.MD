# Documentação para Start do PIM

essa documentação visa a facilitação do start do projeto em outras máquinas.

Antes de mais nada, mantenha todos os arquivos dentro da mesmas pastas que foram baixadas pois o Desktop usa DLLs do Projeto Web.

```
PIM
├───Assets
├───desktop
└───web
```

## 1 - Banco de Dados

Na pasta do projeto você encontrará um arquivo chamado **criandoBancoDados.sql** ele é um script para banco de dados e precisa ser executado no SGBD, para criar as tabelas com os dados, conforme foi pedido.

após a execução do script de banco de dados, você precisa alterar a conexão da aplicação.

Você vai entrar na pasta: 
>web\Bitnvest\DAO\Bitnvest.DataAcess

onde vai encontrar o arquivo **database.json** abrindo ele verá

```
  "ConnectionStrings": {
    "Sql": "Data Source=localhost,1433;Initial Catalog=Bitnvest;User ID=sa;Password=Senha123;"
  }
```

Onde precisará ser alterado para a sua conexão de banco de dados.

E o mesmo processo precisará ser feito no projeto Desktop,
o arquivo **database.json** estará no caminho:

>pim\desktop\Bitnvest\Bitnvest

Caso tenha duvidas acesse esse link:

[https://www.connectionstrings.com/](https://www.connectionstrings.com/)

## 2 - Execução

Para executar o projeto, precisa ter instalado, a versão do .NET Core 2.1 e 3.1.

Instalando o Visual Studio 2019 eles já vem inclusos, caso não tenha e não queira instalar basta baixar no site oficial:

[https://dotnet.microsoft.com/download/dotnet-core](https://dotnet.microsoft.com/download/dotnet-core)

Para executar abra a solução Web, no Visual Studio e aperta Play que fica na parte superior.

para executar sem o visual studio, abra o terminal, na pasta do projeto Web e dite:

```
dotnet run --project Bitnvest
```

de Enter e pronto, e entre na URL [https://localhost:5001](https://localhost:5001) 

e basta navegar normalmente.


No projeto Desktop, o sistema com Visual Studio 2019 é o mesmo, na parte superior aperte o Play verde e navegue.

Caso não queira usar, entre no projeto Desktop e abra um terminal e digite:

``` 
dotnet run --project Bitnvest
```

Dessa forma deve abrir a tela do programa e você será capaz de usá-lo.

## 3 - Pontos de Atenção

_Desktop_

Todo esse processo foi feito na minha máquina para que esse arquivo fosse escrito, e no Aplicativo Desktop as cobranças de Tarifas não tinham sido efetuadas.

Para efetuar uma cobrança basta colocar a data do seu computador, com a data de vencimento de Alguma cobrança, deslogar e logar novamente.

_Web_

No projeto Web ao tentar logar aparecer o erro **"Esta página não está funcionando"** basta clicar em voltar no navegador e logar novamente.

Tanto no projeto Desktop quanto no projeto Web já temos um usuário admin, onde no Web ele é um Correntista, e no Desktop ele é um funcionário.

para acessar em Ambos os casos o login é: 
>admin@admin.com

e a senha:
>123
