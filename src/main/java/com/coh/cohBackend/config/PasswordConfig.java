package com.coh.cohBackend.config;

import com.coh.cohBackend.entity.User;
import org.jetbrains.annotations.NotNull;
import org.springframework.context.annotation.Configuration;
import org.mindrot.jbcrypt.BCrypt;

@Configuration
public class PasswordConfig {

    User user;

    public static @NotNull String hashPassword(String password) {
        return BCrypt.hashpw(password, BCrypt.gensalt());
    }


    public static boolean checkPassword(String plainPassword, String hashedPassword) {
        return BCrypt.checkpw(plainPassword, hashedPassword);
    }
}

