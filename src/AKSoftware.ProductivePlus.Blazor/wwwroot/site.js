window.onscroll = function () {
    // Get page scroll top value 
    const scrolltop = document.documentElement.scrollTop || document.body.scrollTop;
    const appHeader = document.getElementsByClassName('app-header')[0];
    const acrylicBackdrop = document.getElementsByClassName('acrylic-backdrop')[0];
    const logo = document.getElementsByClassName('app-logo')[0]; 
    const appName = document.getElementsByClassName('app-logo-name')[0];
    if (scrolltop > 100) {
        appHeader.classList.remove('thick-header');
        appHeader.classList.add('slim-header');

        acrylicBackdrop.classList.remove('thick-header');
        acrylicBackdrop.classList.add('slim-header');

        logo.classList.remove('big-logo');
        logo.classList.add('small-logo');

        appName.classList.add('font-s');
    }
    else {
        appHeader.classList.add('thick-header');
        appHeader.classList.remove('slim-header');

        acrylicBackdrop.classList.add('thick-header');
        acrylicBackdrop.classList.remove('slim-header');

        logo.classList.add('big-logo');
        logo.classList.remove('small-logo');

        appName.classList.remove('font-s');
    }
}