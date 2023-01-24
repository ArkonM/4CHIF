package com.devglan.service.impl;

import com.devglan.model.Product;
import com.devglan.service.ProductService;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.Date;
import java.util.Optional;

@RunWith(SpringRunner.class)
@SpringBootTest

public class ProductServiceImplTest {

    @Autowired()
    private ProductService productService;

    @Test
    public void testProductSave() {
        Date date = new Date();
        Product p = new Product();
        p.setPreis(15);
        p.setProductName("TestName");
        p.setGueltigBis(date);
        p.setGueltigAb(date);
        p.setBildpfad("path");
        productService.save(p);

        Product found = productService.findByProductName("TestName");
        Assert.assertNotNull(found);
        Assert.assertEquals("TestName", found.getProductName());
    }

    @Test
    public void testFindProduct() {
        Optional<Product> p = productService.findById(1);
        Assert.assertNotNull(p);
    }
}
