package com.example.webshop.Model;

import org.hibernate.annotations.Fetch;
import org.springframework.lang.Nullable;

import javax.persistence.*;
import java.util.Date;
import java.util.HashSet;
import java.util.Set;

@Entity
@Table(name = "orders")

public class Order {
        @Id
        @GeneratedValue(strategy = GenerationType.IDENTITY)
        private long id;
        @Column(nullable = false)
        private String andrede;
        private String vorname;
        @Column(nullable = false)
        private String name;
        @Column(nullable = false)
        private String strasse;
        @Column(nullable = false)
        private String plz;
        @Column(nullable = false)
        private String ort;
        @Column(nullable = false)
        private Date datum;


        @ManyToMany(fetch = FetchType.LAZY)
        @JoinTable(
                name = "order_product",
                joinColumns = @JoinColumn(name = "order_id"),
                inverseJoinColumns = @JoinColumn(name = "product_id")
        )

        private Set<Product> products = new HashSet<>();
        @Column(nullable = false)
        private double gesamtpreis;
        @Column(nullable = false)
        private boolean erledigt;
        @Column(nullable = false)
        private boolean storniert;


        public long getId() {
                return id;
        }

        public void setId(long id) {
                this.id = id;
        }

        public String getAndrede() {
                return andrede;
        }

        public void setAndrede(String andrede) {
                this.andrede = andrede;
        }

        public String getVorname() {
                return vorname;
        }

        public void setVorname(String vorname) {
                this.vorname = vorname;
        }

        public String getName() {
                return name;
        }

        public void setName(String name) {
                this.name = name;
        }

        public String getStrasse() {
                return strasse;
        }

        public void setStrasse(String strasse) {
                this.strasse = strasse;
        }

        public String getPlz() {
                return plz;
        }

        public void setPlz(String plz) {
                this.plz = plz;
        }

        public String getOrt() {
                return ort;
        }

        public void setOrt(String ort) {
                this.ort = ort;
        }

        public Date getDatum() {
                return datum;
        }

        public void setDatum(Date datum) {
                this.datum = datum;
        }

        public Set<Product> getProducts() {
                return products;
        }

        public void setProducts(Set<Product> products) {
                this.products = products;
        }

        public double getGesamtpreis() {
                return gesamtpreis;
        }

        public void setGesamtpreis(double gesamtpreis) {
                this.gesamtpreis = gesamtpreis;
        }

        public boolean isErledigt() {
                return erledigt;
        }

        public void setErledigt(boolean erledigt) {
                this.erledigt = erledigt;
        }

        public boolean isStorniert() {
                return storniert;
        }

        public void setStorniert(boolean storniert) {
                this.storniert = storniert;
        }
}
