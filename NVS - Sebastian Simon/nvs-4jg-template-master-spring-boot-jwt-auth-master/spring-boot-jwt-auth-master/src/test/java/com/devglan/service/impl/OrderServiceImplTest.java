package com.devglan.service.impl;

import com.devglan.model.Order;
import com.devglan.service.OrderService;
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

public class OrderServiceImplTest {

    @Autowired()
    private OrderService orderService;

    @Test
    public void testOrderSave() {
        Date date = new Date();
        Order order = new Order(69, "Mister", "Puppe", "Suppe", "Gemuesestraße", "6969", "Banane", date, null, 155.69, false, false);
        orderService.save(order);

        Order found = orderService.findByName("Suppe");
        Assert.assertNotNull(found);
        Assert.assertEquals("Suppe", found.getName());
    }

    @Test
    public void testFindOrder() {
        Optional<Order> order = orderService.findById(1L);
        Assert.assertNotNull(order);
    }
}
