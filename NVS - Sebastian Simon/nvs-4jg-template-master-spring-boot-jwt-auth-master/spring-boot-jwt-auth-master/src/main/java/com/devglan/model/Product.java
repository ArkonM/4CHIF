package com.devglan.model;

import net.bytebuddy.dynamic.loading.InjectionClassLoader;

import javax.persistence.*;
import java.util.Date;

@Entity
@Table(name = "products")

public class Product {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long id;
    private String productName;
    private String bildpfad;
    private double preis;
    private Date gueltigAb;
    private Date gueltigBis;


    public Product() {
    }

    public Product(long id, String productName, String bildpfad, double preis, Date gueltigAb, Date gueltigBis) {
        this.id = id;
        this.productName = productName;
        this.bildpfad = bildpfad;
        this.preis = preis;
        this.gueltigAb = gueltigAb;
        this.gueltigBis = gueltigBis;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public String getProductName() {
        return productName;
    }

    public void setProductName(String productName) {
        this.productName = productName;
    }

    public String getBildpfad() {
        return bildpfad;
    }

    public void setBildpfad(String bildpfad) {
        this.bildpfad = bildpfad;
    }

    public double getPreis() {
        return preis;
    }

    public void setPreis(double preis) {
        this.preis = preis;
    }

    public Date getGueltigAb() {
        return gueltigAb;
    }

    public void setGueltigAb(Date gueltigAb) {
        this.gueltigAb = gueltigAb;
    }

    public Date getGueltigBis() {
        return gueltigBis;
    }

    public void setGueltigBis(Date gueltigBis) {
        this.gueltigBis = gueltigBis;
    }
}
