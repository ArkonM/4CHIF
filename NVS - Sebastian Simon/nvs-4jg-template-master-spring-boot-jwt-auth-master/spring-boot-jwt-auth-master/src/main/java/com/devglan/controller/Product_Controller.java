package com.example.webshop.Controller;

import com.example.webshop.Model.Product;
import com.example.webshop.Repository.Product_Repository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.List;
import java.util.Map;


@CrossOrigin(value = "x", maxAge = 3600)
@RestController
@RequestMapping("/products")



public class
Product_Controller {

    @Autowired
    Product_Repository product_rep;

    @GetMapping("/get")
    public ResponseEntity<List<Product>> getProducts(){
        List<Product> products = product_rep.findAll();
        return new ResponseEntity<>(products, HttpStatus.OK);
    }

    @PostMapping("/add")
    public ResponseEntity<Product> addProduct(@RequestBody Product product) {
        Product updateProduct = product_rep.save(product);
        return new ResponseEntity<>(updateProduct, HttpStatus.OK);
    }


    @PostMapping("/change")
    public ResponseEntity<Product> updateProduct(@RequestBody Product product) {


        Product newProduct = product_rep.save(product);
        return new ResponseEntity<>(newProduct, HttpStatus.OK);
    }

    @DeleteMapping("/remove/{id}")
    public ResponseEntity<Map<String, Boolean>> deleteProduct(@PathVariable("id") Long id) {
        product_rep.deleteById(id);
        Map<String, Boolean> response = new HashMap<>();
        response.put("deleted", Boolean.TRUE);
        return ResponseEntity.ok(response);
    }
}
