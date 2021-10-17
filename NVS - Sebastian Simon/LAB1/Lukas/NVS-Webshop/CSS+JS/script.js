
let carts = document.querySelectorAll('.add-cart');
productList = [];
sessionStorage.setItem("productList", JSON.stringify(productList));

for (let i=0; i<carts.length; i++) {

    carts[i].addEventListener('click', () => {

        cartNumbers(carts[i].id);
        console.log(carts[i].id)


    })
}


function cartNumbers(id) {
    let productNumber = sessionStorage.getItem('cartNumber');
    productNumber = parseInt(productNumber);

    if (productNumber) {
        sessionStorage.setItem('cartNumber', productNumber + 1);
        document.querySelector('.cart-Anzeige span').textContent = productNumber + 1;
    } else {
        sessionStorage.setItem('cartNumber', 1);
        document.querySelector('.cart-Anzeige span').textContent = 1;
    }

    //Add Product to Warenkorb
    addItem(id);

}

function addItem(id) {
    productList = JSON.parse(sessionStorage.getItem('productList'));
    var gefunden = false;
    if (productList) {
    for (let i = 0; i<productList.length; i++) {
        if (productList[i].productID == id && productList[i].value >= 1) {
            productList[i].value += 1;
            gefunden = true;
        }
    }
    }

    if (!gefunden) {
        product = {
            productID : id,
            value : 1
        }
        productList.push(product);
    }



    sessionStorage.setItem("productList", JSON.stringify(productList));

}


function updateCartAmount() {
    document.querySelector('.cart-Anzeige span').textContent = sessionStorage.getItem('cartNumber');
}

function allowDrop(ev) {
    ev.preventDefault();
  }
  var tempID;
  function drag(ev) {
    tempID = ev;
  }

  function drop(ev) {
    cartNumbers(tempID)
  }



updateCartAmount();
