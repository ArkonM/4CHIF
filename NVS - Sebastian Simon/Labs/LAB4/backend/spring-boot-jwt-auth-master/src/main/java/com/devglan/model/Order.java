package com.devglan.model;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.util.Date;
import java.util.Set;

@Entity
@Getter
@Setter
@Table(name = "orders")

public class Order {
        @Id
        @GeneratedValue(strategy = GenerationType.IDENTITY)
        private long id;
        @Column(nullable = false)
        private String anrede;
        private String vorname;
        @Column(nullable = false)
        private String name;
        @Column(nullable = false)
        private String strasse;
        @Column(nullable = false)
        private String plz;
        @Column(nullable = false)
        private String ort;
        @Column
        private Date datum;
        @Column(nullable = false, columnDefinition = "boolean default false")
        private boolean erledigt;
        @Column(nullable = false, columnDefinition = "boolean default false")
        private boolean storniert;
        @OneToMany(cascade = CascadeType.ALL)
        private Set<ProductValue> products;
}
