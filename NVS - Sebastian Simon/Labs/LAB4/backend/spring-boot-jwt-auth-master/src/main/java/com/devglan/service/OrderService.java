package com.devglan.service;

import org.springframework.stereotype.Service;
import com.devglan.model.Order;
import java.util.List;
import java.util.Optional;

@Service

public interface OrderService {
    Order save (Order order);

    List<Order> findAll();

    void delete(Long id) throws Exception;

    Order update(Order order);

    List<Order> findByOrderName();

    Order findByName(String suppe);

    Optional<Order> findById(long l);

    void addOrder(Order order);
}

