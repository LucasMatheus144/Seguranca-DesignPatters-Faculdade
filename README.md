# Seguranca-DesignPatters-Faculdade

 # MARCONATO

1 -> Validações de login
  - Caso o usuario erra o login por mais de 3 vezes, o sistema bloqueia o login por 3 minutos
   ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/92e0c1a6-f5c4-4bf7-be93-3b2f827ca458)  ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/0b074747-fcbb-4961-8bff-fcf157d4ccd0)


    
2 -> Auditoria
  - Existe 3 auditorias das telas de cadastro, geral, usuario e funcionario, esssa auditoria alem de exibir quais campos foram alterado, temos  o diferencial do Tipo Alteração(Insert / Update / Delete), horario da alteração e usuario que alterou
   ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/a59f3139-98ee-4e18-8c27-b8c45ef7a20c)

3 -> Controle de acesso de usuarios
  - Foi desenvolvido uma tabela onde é registrado o horario de acesso do login e o horario do logout
  ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/37ab4380-ce59-448c-a8df-1f8e02049556)

4 -> Permissionamento de acesso
  - Foi desenvolvido um controle de permissao de acesso, onde cada perfil tem acesso exclusivo ao sistema.
    
   4.1 - SuperUser -> Acesso total ao sistema, o diferencial é ter controle de acesso de perfils ao sistema.
    
   4.2 - Admin -> Acesso a cadastro editar e excluir qualquer cadastro ao sistema.
  
   4.3 - User -> Acesso somente a visualizar as informações da tela inicial.
  ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/86082463-77c4-4427-9ec3-db97bca3c1fd)

5 -> Autenticação dos serviços
  - Configuração da autenticação de identidade pelo framework IdentityUser e IdentityRole
  ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/6e94ff6e-de86-4f41-9fd7-51c201c203cb)

    
  - Configuração da autenticação de cookies, na qual é definido o logout caso ficar o tempo ausente(30 minutos), , a expiração do cookie é renovada a cada solicitação , o cookie só pode ser acessado por meio de solicitações HTTP e não pode ser acessado por meio de JavaScript no navegador, cookie só será enviado em conexões seguras (HTTPS).
  ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/dac5cc11-bcbd-4259-bd83-5b38cc69a085)


  - Configuração sobre o Login / Senha , na qual é parametrizado o numero máximo de tentativas falhadas antes do bloqueio(3) , o tempo que o usuario é bloqueado(2), e configurações de senha
   ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/1d5b2f52-6caf-4cd7-a4c2-3c54d7d25053)

  - Definido o horario do servidor para o brasileiro e forçar HTTP Strict Transport Security (HSTS) 
  ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/7d384992-ebb2-42ac-9488-508a8d62e9dd)

  - Politica e privacidade da aplicação
  ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/69cc0601-3125-42fb-a1bd-43ec97302bd7)

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
# EVERTON

No trabalho foi decidido que vamos utilizar 3 designer patters que foi passado, builder patterns, observer patterns e o state patterns

1.0 -> Pattern STATE
 - Foi criado a classe StateItem, onde é validado os tipo do armazem com o tipo do item. Se o armazem for null ou armazem diferente do tipo item -> o sistema trava e nao deixa caso contrario -> valido
 ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/274938d2-792c-4d96-94e2-c6f958736bdb)
 ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/545d1726-0fe4-4f65-bcf4-7c1b6f52d731)
 ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/1209d12a-79c5-462c-81ca-59d2f0b4af60)

2.0 -> Pattern BUILDER
 - Foi desenvolvido a classe chamada Geral e a classe Builderm qua qual o builder instancia a classe geral, logo após fomos no controller HomeController e na função que realiza o cadastro, instanciamos os campos de acordo com o BUILDER
   
   ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/2dfdc744-9cfa-4ebd-bb16-ded082b61caf)
   ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/041a5cad-7ddb-45b9-b148-7c0992ca940c)

3.0 -> Pattern OBSERVER
 -Foi desenvolvido uma interface Itipo para criarmos o void Atualiza, e modificamos o builder Geral, asim foi incluido um Observer no Tipo do Cadastro. A classe que faz toda a validação é Models> Observer > Cadastro

   ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/b70d373a-ffed-43d2-a680-916a783dc8c6)
   ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/9a08ebef-b96f-4b67-a56e-dc3db4f01d04)
   ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/2cb2ba99-ae7c-4454-8bed-afddae7e5ac9)






