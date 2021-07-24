let wrapper = document.querySelector('.img__wrapper');

function donwload(input) {
    let file = input.files[0];
    let reader = new FileReader();
    reader.readAsDataURL(file);

    reader.onload = function () {
        let img = document.createElement('img');
        wrapper.appendChild(img);
        img.src = reader.result;
    }
}


function myfunction() {
    var input = document.createElement("input");
    input.setAttribute('type', 'file');
    input.name = 'Image';
    wrapper.appendChild(input);
}


////    function myfunction() {
////        alert("how are you");
////         }
