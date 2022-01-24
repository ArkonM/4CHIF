package com.devglan.service;

import org.springframework.stereotype.Service;
import com.devglan.model.Product;

import java.util.List;
import java.util.Optional;

@Service

public interface ProductService {
    Product save (Product product) ;

    List<Product> findAll();

    void delete(Long id) throws Exception;

    Product update(Product product);

    Product findByProductName(String name);

    Optional<Product> findById(int i);
}
