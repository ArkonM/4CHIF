
let carts = document.querySelectorAll('.warenkorb');
productList = [];
var summe = 0;
sessionStorage.setItem("productList", JSON.stringify(productList));
sessionStorage.setItem("ItemAmount", summe);

for (let i=0; i<carts.length; i++) {

    carts[i].addEventListener('click', () => {

        itemAdder(carts[i].id);

    })
}


function itemAdder(id){
  productList = JSON.parse(sessionStorage.getItem('productList'));
  var neu = true;
  for(let i = 0; i < productList.length; i++){
    if(productList[i].productID == id){
      productList[i].amount += 1;
      neu = false;
    }
  }

  if(neu){
    item = {
      productID : id,
      amount : 1
    }
    productList.push(item);
  }
  sessionStorage.setItem("productList", JSON.stringify(productList));
  showAmount();
}


function showAmount(){
  productList = JSON.parse(sessionStorage.getItem('productList'));
  summe = 0;
  for(let i = 0; i < productList.length; i++){
    summe += productList[i].amount;
  }
  document.querySelector('.cart-amount').textContent = summe;

  sessionStorage.setItem("ItemAmount", summe);

}


function allowDrop(ev) {
    ev.preventDefault();
  }
  var tempID;
  function drag(ev) {
    tempID = ev;
  }

  function drop(ev) {
    itemAdder(tempID)
  }
