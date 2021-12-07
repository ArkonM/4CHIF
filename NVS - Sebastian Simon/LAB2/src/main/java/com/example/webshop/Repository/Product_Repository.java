package com.example.webshop.Repository;

import com.example.webshop.Model.Product;
import org.springframework.data.jpa.repository.JpaRepository;

public interface Product_Repository extends JpaRepository <Product, Long>{
}
