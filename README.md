# Seguranca-DesignPatters-Faculdade

#MARCONATO

1 -> Validações de login
  - Caso o usuario erra o login por mais de 3 vezes, o sistema bloqueia o login por 3 minutos
    
2 -> Auditoria
  - Existe 3 auditorias das telas de cadastro, geral, usuario e funcionario, esssa auditoria alem de exibir quais campos foram alterado, tempos  o diferencial do Tipo Alteração(Insert / Update / Delete), horario da alteração e usuario que alterou
   ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/a59f3139-98ee-4e18-8c27-b8c45ef7a20c)

3 -> Permissionamento de acesso
  - Foi desenvolvido um controle de permissao de acesso, onde cada perfil tem acesso exclusivo ao sistema.
    
  3.1 - SuperUser -> Acesso total ao sistema, o diferencial é ter controle de acesso de perfils ao sistema.
    
  3.2 - Admin -> Acesso a cadastro editar e excluir qualquer cadastro ao sistema.
  
  3.3 - User -> Acesso somente a visualizar as informações da tela inicial.
  ![image](https://github.com/LucasMatheus144/Seguranca-DesignPatters-Faculdade/assets/79222732/86082463-77c4-4427-9ec3-db97bca3c1fd)

