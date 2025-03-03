const checkedCategories = [];
let minPrice = 0;
let maxPrice = 0;
let nameSearch = '';

const cards = async () => {
    const products = await getProducts();
    const productList = document.getElementById("PoductList");
    productList.innerHTML = '';
    const tempCard = document.getElementById("temp-card");

    products.forEach(product => {
        const cloneProduct = tempCard.content.cloneNode(true);
        cloneProduct.querySelector("img").src = `../Images/${product.image}`;
        cloneProduct.querySelector(".price").innerText = `$${product.price}`;
        cloneProduct.querySelector(".description").innerText = product.description;
        cloneProduct.querySelector("button").addEventListener("click", () => addToCart(product));
        productList.appendChild(cloneProduct);
    });
};

const addToCart = (product) => {
    const cart = JSON.parse(sessionStorage.getItem("cart")) || [];
    cart.push(product);
    sessionStorage.setItem("cart", JSON.stringify(cart));
    document.getElementById("ItemsCountText").innerText = cart.length;
};

const categories = async () => {
    const categories = await getCategories();
    const categoryList = document.getElementById("categoryList");
    const tempCategory = document.getElementById("temp-category");

    categories.forEach(category => {
        const cloneCategory = tempCategory.content.cloneNode(true);
        cloneCategory.querySelector(".OptionName").innerText = category.name;
        cloneCategory.querySelector(".opt").addEventListener("change", (e) => filterCategories(e.currentTarget.checked, category.id));
        categoryList.appendChild(cloneCategory);
    });
};

const filterCategories = async (checked, id) => {
    if (checked) {
        checkedCategories.push(id);
    } else {
        const index = checkedCategories.findIndex(c => c === id);
        checkedCategories.splice(index, 1);
    }
    await cards();
};

const filterProducts = () => {
    minPrice = Number(document.getElementById("minPrice").value);
    maxPrice = Number(document.getElementById("maxPrice").value);
    nameSearch = document.getElementById("nameSearch").value;
    cards();
};

const getProducts = async () => {
    try {
        let url = `api/Products/?`;
        if (checkedCategories.length > 0) {
            url += checkedCategories.map(id => `&categoryIds=${id}`).join('');
        }
        if (minPrice > 0) url += `&minPrice=${minPrice}`;
        if (maxPrice > 1) url += `&maxPrice=${maxPrice}`;
        if (nameSearch) url += `&searchName=${nameSearch}`;

        const response = await fetch(url);
        return await response.json();
    } catch (error) {
        alert(error);
    }
};

const getCategories = async () => {
    try {
        const response = await fetch('api/Categories');
        return await response.json();
    } catch (error) {
        alert(error);
    }
};

const load = () => {
    cards();
    categories();
    document.getElementById("ItemsCountText").innerText = JSON.parse(sessionStorage.getItem("cart"))?.length || 0;
};
