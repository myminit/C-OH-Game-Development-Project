package com.coh.cohBackend.controller;

import com.coh.cohBackend.entity.*;
import com.coh.cohBackend.repository.LoginResponse;
import com.coh.cohBackend.repository.RankRepository;
import com.coh.cohBackend.repository.StatsRepository;
import com.coh.cohBackend.repository.UserRepository;
import com.coh.cohBackend.services.*;
import jakarta.servlet.http.HttpSession;
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
        private final StatsRepository statsRepository;
        private final RankService rankService;

        @Autowired
        public UserController(UserService userService, PlayService playService, UserRepository userRepository, CorrectService correctService, StatsRepository statsRepository, RankService rankService) {
            this.userService = userService;
            this.playService = playService;
            this.userRepository = userRepository;
            this.correctService = correctService;
            this.statsRepository = statsRepository;
            this.rankService = rankService;
        }

    @PostMapping("/register")
    public ResponseEntity<String> register(@RequestBody User user) {
        try {
            userService.registerUser(user);

            var x = userRepository.findByUsername(user.getUsername());
            if (x == null) {
                throw new RuntimeException("Failed to retrieve userId after saving user.");
            } else {
                playService.createPlay(x.getUserId());

                int rankId = rankService.saveDefault(x);

                correctService.createCorrect(x.getUserId(), rankId);
            }

            return new ResponseEntity<>("Registration successful!", HttpStatus.OK);
        } catch (RuntimeException e) {
            return new ResponseEntity<>("Registration failed: " + e.getMessage(), HttpStatus.BAD_REQUEST);
        }
    }


    @PostMapping("/login")
    public ResponseEntity<LoginResponse> login(@RequestBody User user, HttpSession session) {
        try {
            User existingUser = userService.authenticateUser(user.getUsername(), user.getPassword());
            if (existingUser != null) {
                session.setAttribute("userId", existingUser.getUserId());
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

    @PostMapping("/logout")
    public ResponseEntity<String> logout(HttpSession session) {
        session.invalidate();
        return new ResponseEntity<>("Logout successful", HttpStatus.OK);
    }

    @GetMapping("/profile")
    public ResponseEntity<?> getProfile(HttpSession session) {
        Integer userId = (Integer) session.getAttribute("userId");
        if (userId == null) {
            return new ResponseEntity<>("User is not logged in", HttpStatus.UNAUTHORIZED);
        }

        return new ResponseEntity<>("User is not logg in:" + userId , HttpStatus.UNAUTHORIZED);
    }
}


