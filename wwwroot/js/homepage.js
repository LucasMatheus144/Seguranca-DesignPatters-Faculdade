document.addEventListener('DOMContentLoaded', () => {
    const btnPopup = document.querySelector('.pop');
    const iconClose = document.querySelector('.icon-close');
    const hublogin = document.querySelector('.wrapper');

    btnPopup.addEventListener('click', () => {
        hublogin.classList.add('active-popup');
    });

    iconClose.addEventListener('click', () => {
        hublogin.classList.remove('active-popup');
    });
});

document.addEventListener("DOMContentLoaded", function () {
    const hublogin = document.querySelector('.wrapper');
    const principal = document.getElementById("principal");
    const endereco = document.getElementById("endereco");
    const stepRow = document.querySelector(".step-row");

    const envio = document.querySelector("#next-ende");
    const voltar = document.querySelector("#back-princ");


    const NextTela = document.getElementById("next-tela");
    const VoltarEnde = document.getElementById("back-ende");
    const funcionario = document.getElementById("funcionario");
    const usuario = document.getElementById("usuario");

    const Ende_usu = document.getElementById("ende-back");
    const Usu_info = document.getElementById("next-info");
    const Info_Usu = document.getElementById("back-usu");
    


    const elementosAdicionados = []; // Array para armazenar os elementos adicionados

    // Step de cadastro Principal e Endereço
    function CadPrincipal(destinoPrincipal, destinoEndereco, alturaHubLogin) {
        principal.style.left = destinoPrincipal;
        endereco.style.left = destinoEndereco;
        hublogin.style.height = alturaHubLogin;
    }

    // Endereco para Funcionario
    function avancarFuncionario(destinoPrincipal, destinoEndereco, alturaHubLogin) {
        endereco.style.left = destinoPrincipal;
        funcionario.style.left = destinoEndereco;
        hublogin.style.height = alturaHubLogin;
    }

    //Voltar Endereço tanto Funcionario quanto Usuario
    function voltarEndereco(destinoPrincipal, destinoEndereco, alturaHubLogin, op) {
        endereco.style.left = destinoPrincipal;
        op.style.left = destinoEndereco;
        hublogin.style.height = alturaHubLogin;
    }

    //Ende Ir para tela Usuario
    function moverUsuario (destinoPrincipal, destinoEndereco, alturaHubLogin) {
        endereco.style.left = destinoPrincipal;
        usuario.style.left = destinoEndereco;
        hublogin.style.height = alturaHubLogin;
    }

    //Usuairo para Info
    function moverInfo(destinoPrincipal, destinoEndereco, alturaHubLogin) {
        usuario.style.left = destinoPrincipal;
        info.style.left = destinoEndereco;
        hublogin.style.height = alturaHubLogin;
    }

     //Info para Usuario
     function VoltarUsuario(destinoPrincipal, destinoEndereco, alturaHubLogin) {
        usuario.style.left = destinoPrincipal;
        info.style.left = destinoEndereco;
        hublogin.style.height = alturaHubLogin;
    }

    // Função para adicionar elementos com base no tipo selecionado
    function adicionarElementos(tipoSelecionado) {
        if (tipoSelecionado === "2") { // Usuário
            const usuarioDiv1 = criarDiv("Usuario");
            const usuarioDiv2 = criarDiv("Info");
            adicionarElemento(usuarioDiv1);
            adicionarElemento(usuarioDiv2);
        } else { // Funcionário ou outro tipo
            const funcionarioDiv = criarDiv("Funcionario");
            adicionarElemento(funcionarioDiv);
        }
    }

    // Função para criar uma div com um texto específico
    function criarDiv(texto) {
        const div = document.createElement("div");
        div.className = "step-col";
        div.innerHTML = "<small>" + texto + "</small>";
        return div;
    }

    // Função para adicionar um elemento ao DOM e ao array de elementos adicionados
    function adicionarElemento(elemento) {
        stepRow.appendChild(elemento);
        elementosAdicionados.push(elemento);
    }

    // Função para remover todos os elementos adicionados do DOM e do array de elementos adicionados
    function limparElementosAdicionados() {
        elementosAdicionados.forEach(elemento => {
            stepRow.removeChild(elemento);
        });
        elementosAdicionados.length = 0; // Limpa o array
    }

    // Evento de clique para avançar para a próxima etapa
    envio.addEventListener("click", function (e) {
        e.preventDefault();
        const tipo = document.querySelector("#geral_tipo_select");
        if (tipo) { // Verifica se o elemento foi encontrado
            const valor = tipo.value;
            CadPrincipal("800px", "190px", "700px");
            adicionarElementos(valor);
        } else {
            console.error("Elemento não encontrado.");
        }
    });

    // Evento de clique para voltar para a etapa anterior
    voltar.addEventListener("click", function (e) {
        e.preventDefault();
        limparElementosAdicionados();
        CadPrincipal("190px", "-450px", "800px");
    });



    // Ação do botão NextTela quando o tipo é diferente de 2
    NextTela.addEventListener("click", function () {
        const element = document.querySelector("#geral_tipo_select").value;
        if (element !== "2" ) {
            // Verifica se o elemento funcionario existe
            if (funcionario) {
                avancarFuncionario("800px", "190px", "900px");
            } else {
                console.error("Formulário de funcionário não encontrado.");
            }
        } else {
            // Se o tipo for igual a 2, execute o comportamento padrão de avançar para a próxima etapa
            moverUsuario("800px", "190px", "900px");
        }
    });

    // Evento de clique para o botão VoltarEnde
    VoltarEnde.addEventListener("click", function () {
        const elemento = document.querySelector("#geral_tipo_select");
        const valor = elemento.value;
        if (valor !== "2") {
            voltarEndereco("190px", "-450px", "700px", funcionario);
        }
        else{
            voltarEndereco("190px", "-450px", "700px", usuario);
        }
    });

    // Usuario para Endereco    
    Ende_usu.addEventListener("click", function () {
        const elemen = document.querySelector("#geral_tipo_select");
        const x = elemen.value;
        if (x !== "1"){
            voltarEndereco("190px", "-450px", "700px", usuario);
        }
    });

    Usu_info.addEventListener("click", function () {
        const elemen = document.querySelector("#geral_tipo_select");
        const x = elemen.value;
        if (x !== "1"){
            moverInfo("800px", "190px", "820px");
        }
    });


    Info_Usu.addEventListener("click", function () {
        const elemen = document.querySelector("#geral_tipo_select");
        const x = elemen.value;
        if (x !== "1"){
            VoltarUsuario("190px", "-450px", "820px");
        }
    });

});
