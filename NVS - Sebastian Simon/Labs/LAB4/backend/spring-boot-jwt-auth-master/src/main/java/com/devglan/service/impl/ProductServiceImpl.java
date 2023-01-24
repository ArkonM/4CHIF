package com.devglan.service.impl;

import com.devglan.dao.ProductDao;
import com.devglan.model.Order;
import com.devglan.service.ProductService;
import com.devglan.model.Product;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Optional;

@Service

public class ProductServiceImpl implements ProductService {

    @Autowired
    ProductDao productDao;

    @Override
    public Product save(Product product) {
        return productDao.save(product);
    }

    @Override
    public List<Product> findAll() {
        List<Product> products = productDao.findAll();
        /*List<Product> result = new ArrayList<>();
        SimpleDateFormat data = new SimpleDateFormat("dd-MM-yyyy");
        Date today = data.parse((new Date()).toString());
        for(Product product : products){
            if(product.getGueltigAb().compareTo(date) >= 0 && product.getGueltigBis().compareTo(date) >= 0){

            }
        }*/
        return products;
    }

    @Override
    public void delete(Long id) throws Exception {
        if(productDao.findById(id) != null){
            productDao.deleteById(id);
        }
        else {
            throw new Exception("ID not found!");
        }
    }

    @Override
    public Product update(Product product) {
        Long pid = product.getId();
        if (pid != null) {
            productDao.save(product);
        }
        return product;
    }

    @Override
    public Product findByProductName(String name) {
        return productDao.findByProductName(name);
    }

    @Override
    public Optional<Product> findById(int i) {
        Optional<Product> product = productDao.findById((long)i);
        return product;
    }
}
