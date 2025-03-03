const hideSignInWhenUserIsIn = () => {
    document.querySelector(".signIn").classList.remove("signIn");
};

const hideUpdate = () => {
    document.querySelector(".update").classList.remove("update");
};

const getAllUserDetails = () => {
    return {
        email: document.querySelector("#userName").value,
        password: document.querySelector("#password").value,
        firstName: document.querySelector("#firstName").value,
        lastName: document.querySelector("#lastName").value
    };
};

const getDataForLogIn = () => {
    return {
        email: document.querySelector("#loginUserName").value,
        password: document.querySelector("#loginPassword").value
    };
};

const validationCheck = async (newUser) => {
    if (!newUser.email || !newUser.password || !newUser.firstName || !newUser.lastName) {
        return "All fields are required";
    }
    if (newUser.firstName.length < 2 || newUser.firstName.length > 20) {
        return "First name must be between 2 and 20 letters";
    }
    if (newUser.lastName.length < 2 || newUser.lastName.length > 20) {
        return "Last name must be between 2 and 20 letters";
    }
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailPattern.test(newUser.email)) {
        return "Email address is incorrect";
    }
    if (newUser.password.length > 20) {
        return "Password must be shorter than 20 characters";
    }
    const strength = await checkPassword();
    if (strength < 3) {
        return "Password is weak";
    }
    return "ok";
};

const checkPassword = async () => {
    const progress = document.querySelector("#progress");
    const password = document.querySelector("#password").value;
    try {
        const response = await fetch(`api/Users/password/?password=${password}`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' }
        });
        const data = await response.json();
        progress.value = data;
        return data;
    } catch (error) {
        alert(error.status);
    }
};

const signIn = async () => {
    const newUser = getAllUserDetails();
    const validationResult = await validationCheck(newUser);

    if (validationResult !== "ok") {
        return alert(validationResult);
    }
    try {
        const response = await fetch("api/Users", {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newUser)
        });
        const data = await response.json();
        if (data.status === 400) {
            throw new Error("The password is weak");
        }
        alert("User added");
    } catch (error) {
        alert(error);
    }
};

const logIn = async () => {
    const newUser = getDataForLogIn();
    try {
        const response = await fetch(`api/Users/login/?email=${newUser.email}&password=${newUser.password}`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' }
        });
        if (response.status === 204) {
            return alert("User is not found");
        }
        const data = await response.json();
        alert(`WELCOME ${data.firstName}`);
        sessionStorage.setItem("userId", data.userId);
        window.location.href = "Products.html";
    } catch (error) {
        alert(`HTTP error! status ${error.status}`);
    }
};

const updateDetails = async () => {
    const newUser = getAllUserDetails();
    const validationResult = await validationCheck(newUser);

    if (validationResult !== "ok") {
        return alert(validationResult);
    }
    try {
        const response = await fetch(`api/Users/${sessionStorage.getItem("id")}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newUser)
        });
        if (response.status === 400) {
            throw new Error("The password is weak");
        }
        alert("User updated successfully");
    } catch (error) {
        alert(`HTTP error! status ${error.status}`);
    }
};
