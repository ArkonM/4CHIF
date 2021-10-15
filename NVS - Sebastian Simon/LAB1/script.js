
let carts = document.querySelectorAll('.add-cart');
productList = [];
sessionStorage.setItem("productList", JSON.stringify(productList));

var shoppingKart = [{productID:1, quantity:0},{productID:2,qunatity:0,{productID:3,quantity:0}];
alert(shoppingKart[1].quantity);
shoppingKart[1].quantity += 1;
alert(shoppingKart[1].quantity);
