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

No trabalho foi decidido que vamos utilizar 3 designer patters que foi passado, Builder patterns, Strategy patterns e o state patterns

1.0 -> Patterns Builder
![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/8281a1f2-3627-42fc-a88e-838a8f9b60ad)
![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/2877c57e-f563-4fc9-bae8-3e0f6ef03de5)

![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/324b269d-bbac-44d6-a441-4108d6a4af09)
![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/90237a5b-ac8f-424e-b107-c9309e674437)

![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/db50c8dd-e6de-4893-b28c-3089e7c3fa16)
![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/e5131858-a4d2-4cf2-858d-84bf81acea9a)


2.0 -> Patterns Strategy 

![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/4fb86872-d502-4c58-9c9e-8965c94222a2)
![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/89c5257e-844a-4e53-b148-f73a189ddc96)
![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/97021975-adc3-4cfb-a698-42b95b93fbae)
![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/c646a81f-346b-4d87-9dfa-e2ffa38811f4)






"Host=localhost;Port=5432;Database=EspacoPotencial;Username=postgres;Password=Lucas#!23;"



