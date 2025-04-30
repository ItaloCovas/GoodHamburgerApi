# API - Good Hamburger

Este projeto foi desenvolvido como parte de um teste de programação. A proposta era construir uma API funcional de acordo com requisitos pré definidos.

## Como rodar o projeto

### Via terminal (CLI)

- Restaure os pacotes:

   ```bash
   dotnet restore
   ```

- Rode o projeto:

   ```bash
   dotnet run
   ```

- A aplicação estará disponível em algo como:
  (A PORTA VAI VARIAR)
  
```bash
   https://localhost:5001
```

- A documentação Swagger estará acessível em:
  
```bash
   https://localhost:5001/swagger
```

### Via Visual Studio

Se estiver usando o Visual Studio, ele já restaura automaticamente as dependências ao abrir o projeto.
Para rodar, basta pressionar F5 ou clicar em "Start", e o navegador será aberto automaticamente na URL da API (geralmente com o Swagger já carregado).

<img src="https://i.imgur.com/UatamCG.png" alt="Imagem Swagger"  />

## Decisões técnicas
### Sem uso de Repository Pattern
Para o contexto do teste, optei por não aplicar o padrão de repositório. A estrutura direta era suficiente e mais apropriada considerando o escopo. Aplicar esse padrão aqui acabaria sendo um overengineering desnecessário. Usei apenas Controllers e Services.

### Swagger com UI nativa
Usei o Swagger padrão com interface gráfica. É direto ao ponto e já entrega uma ótima experiência para testar os endpoints sem depender de ferramentas externas.

### Código mais verboso
Optei por uma abordagem mais explícita e menos abstraída para facilitar a leitura e entendimento rápido da lógica. Algumas melhorias e boas práticas acabaram ficando de fora por conta do tempo limitado, mas a base está clara e pronta pra evoluir.

## Observações finais
O projeto cobre os requisitos principais propostos no teste. Ele pode ser melhorado em pontos como validações mais robustas, divisão de responsabilidades, testes automatizados, entre outros. Porém foi feito com foco em clareza, funcionalidade e entrega no prazo.
