package com.devglan.dao;

import com.devglan.model.Product;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository

public interface ProductDao extends JpaRepository <Product, Long>{
    Product findByProductName(String name);
}
