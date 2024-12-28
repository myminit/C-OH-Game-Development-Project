package com.coh.cohBackend.services;

import com.coh.cohBackend.entity.User;
import com.coh.cohBackend.repository.*;
import org.jetbrains.annotations.Debug;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;
import com.coh.cohBackend.config.PasswordConfig;
import com.coh.cohBackend.services.PlayService;

@Service
public class UserService {
    @Autowired
    private UserRepository userRepository;
    private PlayService playService;

    public User registerUser(User user) {

        User existingUser = userRepository.findByUsername(user.getUsername());
        if (existingUser != null) {
            throw new RuntimeException("Username already exists");
        }
        String hashedPassword = PasswordConfig.hashPassword(user.getPassword());
        user.setPassword(hashedPassword);
        userRepository.save(user);
        return user;
    }

    public User authenticateUser(String username, String password) {
        User user = userRepository.findByUsername(username);
        if (user != null && PasswordConfig.checkPassword(password, user.getPassword()))
        {
            return user;
        }
        return null;
    }
}