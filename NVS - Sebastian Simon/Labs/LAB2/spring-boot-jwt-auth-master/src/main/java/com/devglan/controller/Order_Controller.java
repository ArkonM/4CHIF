package com.devglan.controller;


import com.devglan.model.Order;
import com.devglan.service.OrderService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

@CrossOrigin(value = "x", maxAge = 3600)
@RestController
@RequestMapping("/orders")
public class Order_Controller {

    @Autowired
    OrderService orderService;

    @GetMapping("/all")
    public ResponseEntity<List<Order>> getAllOrder() {
        List<Order> orders = orderService.findAll();
        return new ResponseEntity<>(orders, HttpStatus.OK);
    }

    @PostMapping("/add")
    public ResponseEntity<Order> addOrder(@RequestBody Order order) {
        Order order1 = order;
        Order newOrder = orderService.save(order);
        return new ResponseEntity<>(newOrder, HttpStatus.OK);
    }


    @PostMapping("/edit")
    public ResponseEntity<Order> updateOrder(@RequestBody Order order) {
        Order newOrder = orderService.save(order);
        return new ResponseEntity<>(newOrder, HttpStatus.OK);
    }

    @DeleteMapping("/delete/{id}")
    public ResponseEntity<Map<String, Boolean>> deleteOrder(@PathVariable("id") Long id) throws Exception {
        orderService.delete(id);
        Map<String, Boolean> response = new HashMap<>();
        response.put("deleted", Boolean.TRUE);
        return ResponseEntity.ok(response);
    }
}
