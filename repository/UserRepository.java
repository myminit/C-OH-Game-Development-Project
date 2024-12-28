package com.coh.cohBackend.repository;

import com.coh.cohBackend.entity.User;
import org.springframework.dao.EmptyResultDataAccessException;
import org.springframework.stereotype.Repository;

@Repository
public class UserRepository extends BaseRepository {

    public User findByUsername(String username) {
        try {
            String sql = "SELECT * FROM user WHERE username = ?";
            return queryForObject(sql, new Object[]{username}, User.class);
        } catch (EmptyResultDataAccessException e) {
            return null;
        }
    }

    public User findById(int userId) {
        try {
            String sql = "SELECT * FROM user WHERE user_id = ?";
            return queryForObject(sql, new Object[]{userId}, User.class);
        } catch (EmptyResultDataAccessException e) {
            return null;
        }
    }

    public int save(User user) {
        String sql = "INSERT INTO user (username, password, email) VALUES (?, ?, ?)";
        return update(sql, new Object[]{user.getUsername(), user.getPassword(), user.getEmail()});
    }
}
