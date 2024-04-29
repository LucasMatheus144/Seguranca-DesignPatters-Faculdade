const handlePhone = (event) => {
    let input = event.target;
    input.value = phoneMask(input.value);
};

const phoneMask = (value) => {
    if (!value) return "";
    value = value.replace(/\D/g,'');
    value = value.replace(/(\d{2})(\d)/,"($1) $2");
    value = value.replace(/(\d)(\d{4})$/,"$1-$2");
    return value;
};


const handleCep = (event) => {
    let input = event.target;
    input.value = MaskCep(input.value);
  };
  
  const MaskCep = (value) => {
    if (!value) return "";
    value = value.replace(/\D/g,'');
    value = value.replace(/(\d{5})(\d)/,'$1-$2');
    return value;
  };

const handleAgency = (event) => {
    let input = event.target;
    input.value = formatAgency(input.value);
};

const formatAgency = (value) => {
    if (!value) return "";
    value = value.replace(/\D/g,'');
    value = value.replace(/(\d{3})(\d)/, "$1-$2");
    return value;
};

const handleRg = (event) => {
    let input = event.target;
    input.value = formatRg(input.value);
};

const formatRg = (value) => {
    if (!value) return "";
    value = value.toUpperCase().replace(/[^\dX]/g, '');
    return (value.length == 8 || value.length == 9) ?
        value.replace(/^(\d{1,2})(\d{3})(\d{3})([\dX])$/, '$1.$2.$3-$4') :
        value;
};


const handleEscolaridade = (event) => {
    let input = event.target;
    input.value = formatEscolaridade(input.value);
};

const formatEscolaridade = (value) => {
    if (!value) return "";
    // Remover caracteres indesejados
    value = value.toUpperCase().replace(/[^1-9A-Z]/g, '');
    // Adicionar um ordinal para o número e ajustar o formato
    return value.replace(/^(\d+)([A-Z])$/, '$1°$2');
};





// timeout alertas

setTimeout(function(){
    var Alert = document.getElementById('Alert');
    if(Alert != null) {
        Alert.style.display = 'none';
    }
}, 5000);
