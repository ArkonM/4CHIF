package com.devglan.service.impl;

import com.devglan.dao.OrderDao;
import com.devglan.service.OrderService;
import com.devglan.model.Order;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service

public class OrderServiceImpl implements OrderService {

    @Autowired()
    OrderDao orderDao;



    @Override
    public Order save(Order order) {
        return orderDao.save(order);
    }

    @Override
    public List<Order> findAll() {
        List<Order> orders = orderDao.findAll();
        return orders;
    }

    @Override
    public void delete(Long id) throws Exception {
        orderDao.deleteById(id);
    }

    @Override
    public Order update(Order order) {
        Long pid = order.getId();
        if (pid != null) {
            orderDao.save(order);
        }
        return order;
    }


    @Override
    public List<Order> findByOrderName() {
        List<Order> orders = orderDao.findAll();
        return orders;
    }

    @Override
    public Order findByName(String name) {
        return orderDao.findByName(name);
    }

    @Override
    public Optional<Order> findById(long id) {
        Optional<Order> order = orderDao.findById(id);
        return order;
    }
}
