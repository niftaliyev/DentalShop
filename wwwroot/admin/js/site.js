let category = document.getElementById('category')
let categorytwo = document.getElementById('categorytwo')


async function myFunction() {

    document.getElementById("category").style.visibility = "visible";

    console.log(document.getElementById('test').value);
    let index = document.getElementById('test').value;
    let response = await fetch('https://localhost:44304/api/Categories/child/' + index);
    let content = await response.json();
    //console.log(content);
    category.innerHTML = '';

    let key;
    category.innerHTML += `<option disabled selected>Choise category</option>`;
    for (key in content) {

        category.innerHTML += `<option value="${content[key].id}">${content[key].title}</option>`;

    }
  
}

async function myFunctionTwo() {

    document.getElementById("categorytwo").style.visibility = "visible";

    console.log(document.getElementById('category').value);
    let indextwo = document.getElementById('category').value;
    let response = await fetch('https://localhost:44304/api/Categories/child/' + indextwo);
    let contenttwo = await response.json();
    categorytwo.innerHTML = '';

    let key;
    categorytwo.innerHTML += `<option disabled selected>Choise category</option>`;
    for (key in contenttwo) {

        categorytwo.innerHTML += `<option value="${contenttwo[key].id}">${contenttwo[key].title}</option>`;

    }

}