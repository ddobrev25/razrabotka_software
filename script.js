const btn = document.getElementById("btn");
const userNameInput = document.getElementById("username");
const passwordInput = document.getElementById("password");
const form = document.getElementById("form");

btn.addEventListener('click', myFunction);

const url ='http://localhost:5181';

function myFunction() {
    const body = {
        username: userNameInput.value,
        password: passwordInput.value
    }

    fetch(`${url}/User/Login`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(body)
    })
    .then(response => console.log(response))
}