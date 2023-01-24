package com.devglan.controller;

import com.devglan.model.Product;
import com.devglan.service.ProductService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.List;
import java.util.Map;


@CrossOrigin(value = "*", maxAge = 3600)
@RestController
@RequestMapping("/products")



public class
Product_Controller {

    @Autowired
    ProductService productService;

    @GetMapping("/get")
    public ResponseEntity<List<Product>> getProducts(){
        List<Product> products = productService.findAll();
        return new ResponseEntity<>(products, HttpStatus.OK);
    }

    @PostMapping("/add")
    public ResponseEntity<Product> addProduct(@RequestBody Product product) {
        Product updateProduct = productService.save(product);
        return new ResponseEntity<>(updateProduct, HttpStatus.OK);
    }


    @PostMapping("/change")
    public ResponseEntity<Product> updateProduct(@RequestBody Product product) {


        Product newProduct = productService.save(product);
        return new ResponseEntity<>(newProduct, HttpStatus.OK);
    }

    @DeleteMapping("/remove/{id}")
    public ResponseEntity<Map<String, Boolean>> deleteProduct(@PathVariable("id") Long id) throws Exception {
        productService.delete(id);
        Map<String, Boolean> response = new HashMap<>();
        response.put("deleted", Boolean.TRUE);
        return ResponseEntity.ok(response);
    }
}
