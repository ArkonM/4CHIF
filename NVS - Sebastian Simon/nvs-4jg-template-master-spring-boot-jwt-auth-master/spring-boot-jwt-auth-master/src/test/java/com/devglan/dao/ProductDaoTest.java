package com.devglan.dao;


import com.devglan.model.Product;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.Date;
import java.util.Optional;

@RunWith(SpringRunner.class)
@DataJpaTest

public class ProductDaoTest {

    @Autowired
    private ProductDao productDao;

    @Test
    public void testProductSave() {
        Date date = new Date();
        Product p = new Product();
        p.setPreis(15);
        p.setProductName("TestName");
        p.setGueltigBis(date);
        p.setGueltigAb(date);
        p.setBildpfad("path");
        productDao.save(p);

        Product found = productDao.findByProductName("TestName");
        Assert.assertNotNull(found);
        Assert.assertEquals("TestName", found.getProductName());
    }

    @Test
    public void testFindProduct() {
        Optional<Product> p = productDao.findById(1L);
        Assert.assertNotNull(p);
    }
}
