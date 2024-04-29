const btnPopup = document.querySelector('.btnLogin');
const iconClose = document.querySelector('.icon-close');
const hublogin = document.querySelector('.hublogin');

hublogin.addEventListener('click', () => {

})

btnPopup.addEventListener('click', () => {
    hublogin.classList.add('active-popup');
});

iconClose.addEventListener('click', () => {
    hublogin.classList.remove('active-popup');
});


setTimeout(function(){
    var Alert = document.getElementById('Alert');
    if(Alert != null) {
        Alert.style.display = 'none';
    }
}, 5000);
