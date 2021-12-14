package com.example.webshop.Controller;

import com.example.webshop.Model.Product;
import com.example.webshop.Repository.ProductRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;


@CrossOrigin(value = "x", maxAge = 3600)
@RestController
@RequestMapping("/products")



public class ProductController {

    @Autowired
    ProductRepository product_rep;

    @GetMapping("/get")
    public ResponseEntity<List<Product>> getProducts(){
        List<Product> products = product_rep.findAll();
        return new ResponseEntity<>(products, HttpStatus.OK);
    }
}
