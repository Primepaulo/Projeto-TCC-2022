# Projeto TCC - Grupo 1
## Instruções de Uso:

1. Criar o Banco de Dados:

O arquivo ```Gera BD TCC.sql``` deve ser executado no SQL SERVER. (ou localdb, porém não é recomendável)

1.1 Gerar o Banco de Dados:

Caso o arquivo necessite de ser refeito, clicar com o botão direito no banco desejado, clicar em Tasks -> Generate Scripts... -> Arquivo gerado.
Após isso é só deletar o banco antigo e executar novamente.

2. Popular o Banco:

~~Para popular o banco num ambiente de teste, utilize o arquivo ```Teste.sql```~~ Removido

3. Lembre-se de adicionar seu banco ao arquivo: **Web.config** 

```
<add name="Nome da nova conexão" connectionString="Sua Connection String" providerName="System.Data.SqlClient" />
```

4. Após adicionar no **Web.Config**, modificar o arquivo **Model1** na pasta **Models** nessa linha (linha 13):

```
public Model1()
            : base("name=Nome da conexão criada no passo 3")
```

## Avisos e Dicas:

### Criar Rotas:

1. Acessar o arquivo ```RouteConfig.cs```, presente em ```Projeto TCC 2022/App_Start```

2. Adicionar o seguinte código:

```
routes.MapRoute(
                name: "Nome da Rota",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Nome do Controller", action = "Nome da View", id = UrlParameter.Optional }
            );
```

Obs.: Caso não funcione, tente forçar o controller, como a seguir:

```
routes.MapRoute(
                name: "Nome da Rota",
                url: "Nome do Controller/{action}/{id}",
                defaults: new { controller = "Nome do Controller", action = "Nome da View", id = UrlParameter.Optional }
            );
```

### As diferentes sobrecargas de Html.ActionLink:

Após observar o código, percebi diversas sobrecargas desse método. A mais correta de se utilizar para nosso projeto é a seguinte:

```
@Html.ActionLink("Texto", "Ação", "Controller", Parâmetros da Rota (pode ser NULL), new { @class = "CSS" })
```

### Regras de Padronização:

1. Utilizar camel casing com primeira letra maiúscula:
``` var Id``` ou 
``` var NomeDoControlador```.

2. Nomes de função começam com letra maiúscula e tem que ser lógicos: Get, Search, Insert, Add, Update.

3. Seria interessante que houvessem comentários para os queries e funções mais complexas, para aumentar a legibilidade.

### Ajax:

1. Chamar Ajax com a sintaxe:

```
$.ajax
({
    type: 'GET' ou 'POST',
    url: url,
    dataType: 'html' ou 'Objeto' (se POST),
    cache: false,
    success: function (data) {
        $('#Id').html(data);
        OU QUALQUER OUTRA FUNÇÃO.
    },
    error: function () {
        console.log("Erro");
    }
})
```

## Sobre Docker:
**TBA**
