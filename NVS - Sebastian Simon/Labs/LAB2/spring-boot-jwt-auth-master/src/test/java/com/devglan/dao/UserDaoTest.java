package com.devglan.dao;

import com.devglan.model.User;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.test.context.junit4.SpringRunner;

@RunWith(SpringRunner.class)
@DataJpaTest
public class UserDaoTest {

    @Autowired
    private UserDao userDao;

    private User saveUser(String username, String firstname){
        User user = new User();
        user.setFirstName(firstname);
        user.setLastName("lastname1");
        user.setUsername(username);
        user.setPassword("bla");
        return userDao.save(user);
    }

    @Test
    public void testFindByUserName(){
        String username = "user1";
        String firstname = "firstname1";

        saveUser(username, firstname);

        User user = userDao.findByUsername(username);
        Assert.assertNotNull(user);
        Assert.assertEquals(username, user.getUsername());
        Assert.assertEquals(firstname, user.getFirstName());
    }

    @Test
    public void testFindByUserName_UnknownUsernameNotFound(){
        User user = userDao.findByUsername("unknown");
        Assert.assertNull(user);
    }

    @Test
    public void testDeleteUser(){
        String username = "user2";
        String firstname = "firstname2";

        saveUser(username, firstname);

        User user = userDao.findByUsername(username);
        Assert.assertNotNull(user);

        userDao.delete(user);

        user = userDao.findByUsername(username);
        Assert.assertNull(user);
    }
}
