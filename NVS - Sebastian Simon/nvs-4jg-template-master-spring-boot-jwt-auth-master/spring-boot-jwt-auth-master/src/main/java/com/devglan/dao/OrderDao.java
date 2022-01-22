package com.example.webshop.Repository;

import com.example.webshop.Model.Order;
import org.springframework.data.jpa.repository.JpaRepository;

public interface Order_Repository extends JpaRepository<Order, Long> {
}
