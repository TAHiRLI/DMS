const selectedServiceId = getCookie('selectedService');
if (selectedServiceId !== null) {
    const selectedElement = document.querySelector(`.card_offers [data-id="${selectedServiceId}"]`);

    const selectedElement2 = selectedElement.children[0];
    const childElementInsideSecond = selectedElement2.querySelector('.info_title h3');

    const selectedElement3 = selectedElement.children[1];
    const childElementInsideThird = selectedElement3.querySelector('p');

    if (selectedElement !== null) {
        selectedElement.style.background = 'rgb(82, 0, 255)'
        childElementInsideSecond.style.color = 'rgb(255, 255, 255)';
        childElementInsideThird.style.color = 'rgb(255, 255, 255)';
    }
}

function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

