package com.devglan.controller;

import com.devglan.model.LoginUser;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.web.server.LocalServerPort;
import org.springframework.test.context.junit4.SpringRunner;
import static io.restassured.RestAssured.*;
import static io.restassured.matcher.RestAssuredMatchers.*;
import static org.hamcrest.Matchers.*;

@RunWith(SpringRunner.class)
@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
public class AuthenticationControllerIT {

    @LocalServerPort
    private int port;

    @Test
    public void whenPostOnGenerateTokenWithValidCredentialsReturnOK() {
        LoginUser loginUser = new LoginUser();
        loginUser.setUsername("alex123");
        loginUser.setPassword("alex123");

        given().port(port).when()
                .contentType("application/json")
                .body(loginUser)
                .post("/token/generate-token")
                .then().statusCode(200)
                .assertThat()
                .body("message", equalTo("success"));
    }
}
