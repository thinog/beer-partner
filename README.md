# beer-partner
Encontre a loja mais próxima para comprar sua cerveja gelada!

Pré-requisitos:
- [.NET Core SDK (>= 3.1)](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com)
- [AWS CLI](https://aws.amazon.com/cli/)
- [AWS SAM (Serverless Application Model)](https://aws.amazon.com/pt/serverless/sam/)

Executar testes unitários e verificar coverage:
```bash
dotnet test -p:CollectCoverage=true
```

Subir API e banco de dados local:
```bash
./run-local.sh
```

Criar a stack na AWS e fazer deploy da aplicação:
```bash
./deploy-stack.sh
```

Apenas realizar deploy da aplicação (precisa já ter criado a stack pelo comando acima):
```bash
./publish-app.sh
```

Destruir stack na AWS:
```bash
./destroy-stack.sh
```

## Arquitetura da aplicação na AWS:
![Arquitetura AWS](./assets/beer-partner-aws.png "Arquitetura AWS")

## Arquitetura da aplicação local:
![Arquitetura local](./assets/beer-partner-local.png "Arquitetura local")

## Checklist de aplicação
- [x] Aplicação feita para rodar na AWS
- [x] Microserviços feitos para rodarem como AWS Lambda
- [x] API REST através de AWS API Gateway
- [ ] Banco de dados DynamoDB (rodar em container para não depender da stack criada na AWS)
- [x] Rodar API local com SAM
- [ ] Documentar endpoints com Swagger
- [ ] Montar collection do Postman para testar endpoints
- [ ] Cross-plataform (testar local no Windows e no Linux)
- [x] ~~Criar stack na AWS através de CloudFormation~~ Criar stack na AWS com SAM
- [ ] Montar scripts de criação da stack na AWS, deploy e destruição da stack
- [x] Criar implementação de GeoJSON
- [ ] Testes unitários
- [ ] (Opcional) Container de SonarQube local para analisar código