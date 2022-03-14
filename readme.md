# Segurança de Aplicações Web - Atividade Aula 2

O código implementado aqui tem como objetivo demonstrar duas vulnerabilidades do OWASP Top 10 e o que pode ser feito para evitá-las.

## [A03:2021 – Injection](https://owasp.org/Top10/A03_2021-Injection/)

A primeira vulnerabilidade escolhida é de injeção (de SQL, neste caso). O arquivo `ProdutosController.cs` contém um endpoint vulnerável à injeção SQL e um endpoint com a vulnerabilidade corrigida - ambos os casos estão comentados no código fonte.

## [A07:2021 – Identification and Authentication Failures](https://owasp.org/Top10/A07_2021-Identification_and_Authentication_Failures/)

A segunda vulnerabilidade é de falhas no controle de autenticação e identificação. Neste caso, a vulnerabilidade é mais especificamente relacionada ao [CWE-259: Use of Hard-coded Password](https://cwe.mitre.org/data/definitions/259.html).

O código em `ProdutosController.cs` está implementado com esta vulnerabilidade. As credenciais do banco de dados estão 'hardcodadas' no código fonte e qualquer vazamento do código fonte ou até mesmo dos binários compilados da aplicação podem entregar essas credenciais para algum atacante. 

Na imagem abaixo foi usado o **ILSpy** para encontrar os dados de conexão com o banco a partir do binário da aplicação.

![IL visualizado com ILSpy - Código vulnerável](/imagens/vulneravel-senha.png)

No arquivo `ProdutosV2Controller.cs` a vulnerabilidade é corrigida. A correção escolhida foi fazer a injeção destas credenciais fora do código-fonte - isso pode ser feito através de qualquer provedor de configurações ou, como foi feito aqui, através da configuração de variáveis de ambiente do servidor.

Na imagem abaixo é possível ver que não é possível encontrar as credencias do banco de dados a partir do binário descompilado.

![IL visualizado com ILSpy - Não vulnerável](/imagens/nao-vulneravel.png)