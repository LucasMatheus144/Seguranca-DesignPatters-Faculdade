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

