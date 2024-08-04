function openGoogleMaps(coordinates) {
    const url = `https://www.google.com/maps?q=${coordinates}`;
    window.open(url, '_blank');
}