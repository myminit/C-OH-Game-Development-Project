package com.coh.cohBackend.controller;

import com.coh.cohBackend.entity.*;
import com.coh.cohBackend.repository.UserRepository;
import com.coh.cohBackend.services.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/users")
public class UserController {

        private final UserService userService;
        private final PlayService playService;
        private final UserRepository userRepository;
        private final CorrectService correctService;

        @Autowired
        public UserController(UserService userService, PlayService playService, UserRepository userRepository, CorrectService correctService) {
            this.userService = userService;
            this.playService = playService;
            this.userRepository = userRepository;
            this.correctService = correctService;
        }

        @PostMapping("/register")
        public ResponseEntity<String> register(@RequestBody User user) {
            try {
                userService.registerUser(user);
                var x = userRepository.findByUsername(user.getUsername());
                if (x == null) {
                    throw new RuntimeException("Failed to retrieve userId after saving user.");
                }else {
                    playService.createPlay(x.getUserId());
                    correctService.createCorrect(x.getUserId());
                }
                return new ResponseEntity<>("Registration successful!", HttpStatus.OK);
            } catch (RuntimeException e) {
                return new ResponseEntity<>("Registration failed: " + e.getMessage(), HttpStatus.BAD_REQUEST);
            }
        }

    @PostMapping("/login")
    public ResponseEntity<LoginResponse> login(@RequestBody User user) {
        try {
            User existingUser = userService.authenticateUser(user.getUsername(), user.getPassword());
            if (existingUser != null) {
                LoginResponse response = new LoginResponse("success", String.valueOf(existingUser.getUserId()), "Login successful!");
                return new ResponseEntity<>(response, HttpStatus.OK);
            } else {
                LoginResponse response = new LoginResponse("error", String.valueOf(user.getUserId()), "Invalid username or password");
                return new ResponseEntity<>(response, HttpStatus.UNAUTHORIZED);
            }
        } catch (RuntimeException e) {
            LoginResponse response = new LoginResponse("error", String.valueOf(user.getUserId()), "Login failed: " + e.getMessage());
            return new ResponseEntity<>(response, HttpStatus.BAD_REQUEST);
        }
    }

}

