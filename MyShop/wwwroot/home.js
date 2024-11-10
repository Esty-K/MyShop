const hideSignInWhenUserIsIn = () => {
    const hide = document.querySelector(".signIn")
    hide.classList.remove("signIn")
}
const hideUpdate = () => {
    const hide = document.querySelector(".update")
    hide.classList.remove("update")
}
const getAllUserDetails = () => {
    const email = document.querySelector("#userName").value
    const password = document.querySelector("#password").value
    const firstName = document.querySelector("#firstName").value
    const lastName = document.querySelector("#lastName").value
    return ({ email, password, firstName, lastName })
}
const getDataForLogIn= () => {
    const email = document.querySelector("#loginUserName").value
    const password = document.querySelector("#loginPassword").value
    return ({ email, password})
}
const validationCheck = (newUser) => {
    if (!newUser.email || !newUser.password || !newUser.firstName || !newUser.lastName) {
        return "All fildes are required"
    }
    if (newUser.firstName.length < 2 || newUser.firstName.length > 20) {
        return "first name must be between 2 tiil 20 letters"
    }
    if (newUser.lastName.length < 2 || newUser.lastName.length > 20) {
        return "last name must be between 2 tiil 20 letters"
    }
    else return "ok";
}
const signIn = async () => {
    const newUser = getAllUserDetails();
    const res = validationCheck(newUser);
    if (res != "ok")
        alert(res);
    else {
        try {
            const responsePost = await fetch("api/Users", {
                method: 'post',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(newUser)
            });
            const dataPost = await responsePost.json();
            console.log('post data', dataPost);
            alert("user added")

        }
        catch {
          // if (!responsePost.ok) 
                throw new Error(`HTTP error! status ${responsePost.status}`)
            
        }
    }
}
const logIn = async () => {
    const newUser = getDataForLogIn()
    try {
        const responsePost = await fetch(`api/Users/login/?email=${newUser.email}&password=${newUser.password}`, {
            method: 'post',
            headers: { 'Content-Type': 'application/json' },
            query: { email: newUser.email, password: newUser.password }
        });
        if (responsePost.status == 204) 
            return alert("user is not found")
        
        const dataPost = await responsePost.json();
        alert(`WELCOME ${dataPost.firstName}`)
        sessionStorage.setItem("id", dataPost.userId)
        window.location.href = "UpdateUserDetails.html"


    }
   
    catch {
        if (!responsePost.ok) {
            throw new Error(`HTTP error! status ${responsePost.status}`)
        }
    }


}
const updateDetails = async () => {
    const newUser = getAllUserDetails();
    const res = validationCheck(newUser);
    if (res != "ok")
        alert(res);
    try {
        const responsePut = await fetch(`api/Users/${sessionStorage.getItem("id")}`, {
            method: 'put',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newUser)
        });
        alert("Uset updated sucssesfully")
    }
     catch {
        if (!responsePut.ok) {
            throw new Error(`HTTP error! status ${responsePut.status}`)
        }
    }

}

