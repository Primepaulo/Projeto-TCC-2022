# Projeto TCC Colégio Pedro II 2022 - Grupo 1

## Este código é referente à um projeto de conclusão de curso feito no Colégio Pedro II.
Este projeto foi feito somente para fins acadêmicos e de avaliação.
Este projeto definitivamente possui diversos bugs e muitas escolhas duvidosas de design e eu aprendi muito enquanto desenvolvia ele.

## Instruções de Uso:

1. Criar o Banco de Dados:

O arquivo ```Gera BD TCC.sql``` deve ser executado no SQL SERVER. (ou localdb)

2. Lembre-se de adicionar seu banco ao arquivo: **Web.config** 

```
<add name="Nome da nova conexão" connectionString="Sua Connection String" providerName="System.Data.SqlClient" />
```

3. Após adicionar no **Web.Config**, modificar o arquivo **Model1** e o arquivo **IdentityModels** na pasta **Models** nessa linha (linha 13 e 54, respectivamente):

```
public Model1()
            : base("name=Nome da conexão criada no passo 3")
```

```
public ApplicationDbContext()
            : base("Nome da conexão criada no passo 3")
        {
        }
```

4. Agora é só compilar e executar.

