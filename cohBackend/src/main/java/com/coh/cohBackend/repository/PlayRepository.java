package com.coh.cohBackend.repository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.BeanPropertyRowMapper;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Repository;
import java.util.List;

import com.coh.cohBackend.entity.*;

@Repository
public class PlayRepository {
    @Autowired
    private JdbcTemplate jdbcTemplate;

    public int save(Play play) {
        return jdbcTemplate.update(
                "INSERT INTO play (user_id, level_id, complete) VALUES (?, ?, ?)",
                play.getUserId(), play.getLevelId(), play.isComplete()
        );
    }

}