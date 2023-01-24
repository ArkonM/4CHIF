package com.devglan.dao;

import com.devglan.model.Order;
import com.devglan.model.Product;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.Date;
import java.util.HashSet;
import java.util.Optional;
import java.util.Set;

@RunWith(SpringRunner.class)
@DataJpaTest

public class OrderDaoTest {

    @Autowired
    private OrderDao orderDao;

    @Test
    public void testOrderSave() {
        Date date = new Date();
        Set<Product> products = new HashSet<>();
        Product p = new Product();
        p.setPreis(15);
        p.setProductName("TestName");
        p.setGueltigBis(date);
        p.setGueltigAb(date);
        p.setBildpfad("path");
        products.add(p);

        Order order = new Order(15, "Herr", "Armin", "Schneider", "Peter Roseggergasse", "4c/1", "Payerbach", date, null, 99.99, false, false);
        orderDao.save(order);

        Order found = orderDao.findByName("Suppe");
        Assert.assertNotNull(found);
        Assert.assertNotNull(found);
        Assert.assertEquals("Suppe", found.getName());
    }

    @Test
    public void testFindOrder() {
        Optional<Order> order = orderDao.findById(1L);
        Assert.assertNotNull(order);
    }
}
