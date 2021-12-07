package com.devglan.exception;

public class UserAlredyExistsException extends RuntimeException{

    public UserAlredyExistsException(String s) {
        super(s);
    }
}
