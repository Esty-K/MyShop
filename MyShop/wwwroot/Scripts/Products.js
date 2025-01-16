let checkedCategories = []
let minPrice =0
let maxPrice = 0
let searchName =''
const filterCategories = async (checked, id) => {

    if (checked)
        checkedCategories.push(id)
    else {
        const index = checkedCategories.findIndex(g => g == id)
        checkedCategories.splice(index, 1)
    }
    cards()
}
    const getProducts = async () => {
        try {
            let url = `api/Products/?`
            if (checkedCategories.length > 0)
                for (var i = 0; i < checkedCategories.length; i++) {
                    url += `&categoryIds=${checkedCategories[i]}`
                }
            if (minPrice != 0)
                url += `&minPrice=${minPrice}`
            if (maxPrice != 0)
                url += `&maxPrice=${maxPrice}`
            if (searchName != '')
                url += `&searchName=${searchName}`
            console.log(url)
            const response = await fetch(url);
            const data = await response.json();
            return data;
        }
        catch (error) {
            alert(error)
        }
    }
    const cards = async () => {
        const products = await getProducts()
        document.getElementById("PoductList").innerHTML=''
        let tempCard = document.getElementById("temp-card");
        products.forEach(product => {
            let cloneProduct = tempCard.content.cloneNode(true);
            cloneProduct.querySelector("img").src = `../Images/${product.image}`;
            cloneProduct.querySelector(".price").innerText = `$${product.price}`;
            cloneProduct.querySelector(".description").innerText = product.description;
            cloneProduct.querySelector("button").addEventListener('click', () => { addToCart(product) })
            document.getElementById("PoductList").appendChild(cloneProduct)
        })
    }
    const getCategories = async () => {
        try {
            const response = await fetch("api/Categories");
            const data = await response.json();
            return data;
        }
        catch (error) {
            alert(error)
        }
    }
    const categories = async () => {
        const categories = await getCategories()
        let tempCategory = document.getElementById("temp-category");
        categories.forEach(category => {
            let clonecategory = tempCategory.content.cloneNode(true);
            clonecategory.querySelector(".OptionName").innerText = category.name;
            clonecategory.querySelector(".opt").addEventListener('change', (e) => { filterCategories(e.currentTarget.checked, category.id) })
            document.getElementById("categoryList").appendChild(clonecategory)
        })


    }
    const load = () => {
        cards()
        categories()
        document.getElementById('ItemsCountText').innerText = JSON.parse(sessionStorage.getItem("cart"))?.length || 0
}
const filterProducts=()=>{
    minPrice = Number(document.getElementById('minPrice').value)
    maxPrice = Number(document.getElementById('maxPrice').value)
    searchName = document.getElementById('nameSearch').value
    cards()
}
const addToCart = (product) => { 
    const cart = JSON.parse(sessionStorage.getItem("cart")) || []
    cart.push(product)
    document.getElementById('ItemsCountText').innerText = cart.length
    sessionStorage.setItem("cart", JSON.stringify(cart))
}

