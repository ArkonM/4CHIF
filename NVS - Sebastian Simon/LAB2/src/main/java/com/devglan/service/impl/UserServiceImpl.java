package com.devglan.service.impl;

import com.devglan.dao.UserDao;
import com.devglan.exception.UserAlredyExistsException;
import com.devglan.model.User;
import com.devglan.model.UserDto;
import com.devglan.service.UserService;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.beans.BeanUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Optional;


@Service(value = "userService")
public class UserServiceImpl implements UserDetailsService, UserService {

	private static Logger logger = LogManager.getLogger(UserServiceImpl.class);

	@Autowired
	private UserDao userDao;

	@Autowired
	private BCryptPasswordEncoder bcryptEncoder;

	public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
		logger.debug("loading user {}",username);
		User user = userDao.findByUsername(username);
		if(user == null){
			logger.warn("User {} not found", username);
			throw new UsernameNotFoundException("Invalid username or password.");
		}
		return new org.springframework.security.core.userdetails.User(user.getUsername(), user.getPassword(), getAuthority());
	}

	private List<SimpleGrantedAuthority> getAuthority() {
		return Arrays.asList(new SimpleGrantedAuthority("ROLE_ADMIN"));
	}

	public List<User> findAll() {
		logger.debug("Retrieving user list");
		List<User> list = new ArrayList<>();
		userDao.findAll().iterator().forEachRemaining(list::add);
		return list;
	}

	@Override
	public void delete(Long id) {
		logger.info("Deleting user with id {}",id);
		userDao.deleteById(id);
	}

	@Override
	public User findOne(String username) {
		return userDao.findByUsername(username);
	}

	@Override
	public User findById(Long id) {
		logger.debug("Find user by id {}",id);
		Optional<User> optionalUser = userDao.findById(id);
		return optionalUser.isPresent() ? optionalUser.get() : null;
	}

    @Override
    public UserDto update(UserDto userDto) {
		logger.info("Updating user {}",userDto.getId());
        User user = findById(userDto.getId());
        if(user != null) {
            BeanUtils.copyProperties(userDto, user, "password");
            userDao.save(user);
        }
        return userDto;
    }

    @Override
    public User save(UserDto userDto) {
		logger.info("Saving user new user {}",userDto.getUsername());

		User user = userDao.findByUsername(userDto.getUsername());
		if(user != null){
			logger.warn("User {} already exists", userDto.getUsername());
			throw new UserAlredyExistsException("Username already exists!");
		}

	    User newUser = new User();
	    newUser.setUsername(userDto.getUsername());
	    newUser.setFirstName(userDto.getFirstName());
	    newUser.setLastName(userDto.getLastName());
	    newUser.setPassword(bcryptEncoder.encode(userDto.getPassword()));
		newUser.setAge(userDto.getAge());
		newUser.setSalary(userDto.getSalary());
        return userDao.save(newUser);
    }
}
