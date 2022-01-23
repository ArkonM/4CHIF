package com.devglan.service.impl;

import com.devglan.exception.UserAlredyExistsException;
import com.devglan.model.User;
import com.devglan.model.UserDto;
import com.devglan.model.service.UserService;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.Optional;

@RunWith(SpringRunner.class)
@SpringBootTest
public class UserServiceImplTest {

    @Autowired
    private UserService userService;

    private void createUser(String username, String firstname){
        UserDto user = new UserDto();
        user.setUsername(username);
        user.setFirstName(firstname);
        user.setLastName("lastname2");
        user.setPassword("bla2");
        userService.save(user);
    }

    @Test
    public void testSave() {
        String username="username2";
        String firstname="firstname2";
        createUser(username, firstname);

        Optional<User> foundUser = userService.findAll().stream().filter(u -> u.getUsername().equals(username)).findFirst();

        Assert.assertTrue(foundUser.isPresent());
        Assert.assertEquals(username, foundUser.get().getUsername());
        Assert.assertEquals(firstname, foundUser.get().getFirstName());
    }


    @Test
    public void testSaveTwice() {
        String username = "username3";
        String firstname = "firstname3";
        createUser(username, firstname);
        try {
            createUser(username, firstname);
            Assert.fail();
        } catch (UserAlredyExistsException ex){
            Assert.assertEquals("Username already exists!",ex.getMessage());
        }

    }

}
